using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using MargieBot;
using MargieBot.Responders;
using SlackBot.DI;

namespace SlackBot
{
    public class Program
    {
        private static IWindsorContainer container;

        static void Main(string[] args)
        {
            container = BotRunnerBootstrapper.Init();

            Bot bot = new Bot();
            IResponder[] responders = container.ResolveAll<IResponder>();

            foreach (var responder in responders)
            {
                bot.Responders.Add(responder);
            }

            var connect = bot.Connect(ConfigurationManager.AppSettings["SlackBotApiToken"]);
            
            while (Console.ReadLine() != "close")
            {
                
            }
        }
    }
}
