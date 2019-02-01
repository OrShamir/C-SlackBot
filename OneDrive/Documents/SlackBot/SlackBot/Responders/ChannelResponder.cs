using SlackBot.Extensions;
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using API.ChannelApi;
using API.Extensions;
using MargieBot.Models;

namespace SlackBot.Responders
{
    public class ChannelResponder : Responder
    {
        private readonly IChannelApi m_ChannelApi;
        string m_Average;

        public ChannelResponder(IChannelApi channelApi)
        {
            this.m_ChannelApi = channelApi;
        }

        public override bool CanRespond(ResponseContext context)
        {
            var lastMinGroupsHistory = new GroupsHistoryResponseModel();

            lastMinGroupsHistory = m_ChannelApi.GetChannelHistory(
                        ConfigurationManager.AppSettings["SlackBotApiToken"],
                        "CFE61KQN4",
                        DateTime.Now.AddMinutes(-1),
                        lastMinGroupsHistory.messages.Any() ? lastMinGroupsHistory.messages.Last().ts.ToLocalDateTime() : DateTime.Now,
                        1);

            m_Average = GetChannelAverage().ToString();

            return this.CanRespondCounter > 0 && 
                   context.Message.ChatHub.ID.Equals("CFE61KQN4") &&
                   lastMinGroupsHistory.messages.Count() > 1 &&
                   lastMinGroupsHistory.messages.Last().user != context.BotUserName; 
            
        }

        public override BotMessage GetResponse(ResponseContext context)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Hello ");
            builder.AppendLine(string.Format("The Average of all the chat numbers is {0}", m_Average));

            return new BotMessage { Text = builder.ToString() };
        }

        public decimal GetChannelAverage()
        {
            var history = new GroupsHistoryResponseModel();

            history = m_ChannelApi.GetChannelHistory(
                    ConfigurationManager.AppSettings["SlackBotApiToken"],
                    "CFE61KQN4",
                    DateTime.Now.AddYears(-1),
                    history.messages.Any() ? history.messages.Last().ts.ToLocalDateTime() : DateTime.Now,
                    1000
                );

                this.NumberMessageSum = history.messages.Where(m => m.isNumber()).Sum(m=> int.Parse(m.text));
                this.CanRespondCounter = history.messages.Count();

            if (this.CanRespondCounter != 0)
            {
                return ((decimal)this.NumberMessageSum / (decimal)this.CanRespondCounter);
            }
            else
            {
                return 0;
            }
        }

        public decimal GetUserAverage(string userName)
        {
            var history = new GroupsHistoryResponseModel();

            history = m_ChannelApi.GetChannelHistory(
                    ConfigurationManager.AppSettings["SlackBotApiToken"],
                    "CFE61KQN4",
                    DateTime.Now.AddYears(-1),
                    history.messages.Any() ? history.messages.Last().ts.ToLocalDateTime() : DateTime.Now,
                    1000
                );

            this.NumberMessageSum = history.messages.Where(m => m.isNumber() && m.user == userName).Sum(m => int.Parse(m.text));
            this.CanRespondCounter = history.messages.Where(m => m.user == userName).Count();

            if (this.CanRespondCounter != 0)
            {
                return ((decimal)this.NumberMessageSum / (decimal)this.CanRespondCounter);
            }
            else
            {
                return 0;
            }
        }
    }
}
