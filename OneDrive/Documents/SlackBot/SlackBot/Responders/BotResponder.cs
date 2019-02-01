using System.Configuration;
using System.Text;
using API.ChatApi;
using MargieBot.Models;
using MargieBot.Responders;
using SlackBot.Extensions;

namespace SlackBot.Responders
{
    public class BotResponder : Responder
    {
        private readonly IChatApi m_ChatApi;

        public BotResponder(IChatApi chatApi)
        {
            this.m_ChatApi = chatApi;
        }

        public override bool CanRespond(ResponseContext context)
        {
            return context.Message.ChatHub.ID.StartsWith("D") && context.Message.isNumber()
                   || context.Message.ChatHub.ID.StartsWith("C") && context.Message.isNumber();
                
        }

        public override BotMessage GetResponse(ResponseContext context)
        {
            this.CanRespondCounter++;
            this.NumberMessageSum += int.Parse(context.Message.Text);

            var builder = new StringBuilder();
            builder.Append("Hello ");
            builder.AppendLine(context.Message.User.FormattedUserID);
            builder.AppendLine(string.Format("The Average of all the chat numbers is {0}", this.NumberMessageSum / this.CanRespondCounter));

            return new BotMessage { Text = builder.ToString() };
        }
    }
}
