using API.ChannelApi;
using API.Models;
using MargieBot.Models;
using ServiceStack.Text;
using SlackBot.Models;
using System.Text.RegularExpressions;

namespace SlackBot.Extensions
{
    public static class SlackMessageExtensions
    {
        public static string GetTimeStamp(this SlackMessage message)
        {
            var serializer = new JsonSerializer<SlackRawMessageModel>();
            var rawData = serializer.DeserializeFromString(message.RawData);
            return rawData.ts;
        }

        public static string GetChannelId(this SlackMessage message)
        {
            var serializer = new JsonSerializer<SlackRawMessageModel>();
            var rawData = serializer.DeserializeFromString(message.RawData);
            return rawData.channel;
        }

        public static bool isNumber(this SlackMessage message)
        {
            bool isNumber = true;
            try
            {
                int.Parse(message.Text);
            }
            catch
            {
                isNumber = false;
            }

            return isNumber;
        }

        public static bool isNumber(this Message message)
        {
            bool isNumber = true;
            try
            {
                int.Parse(message.text);
            }
            catch
            {
                isNumber = false;
            }

            return isNumber;
        }

        //public static bool isHasNumbers(this SlackMessage message)
        //{
        //      rgxx
        //}

        //public static IEnumerable GetNumbers(this SlackMessage message)
        //{
        //    MatchCollection mc = Regex.Matches(text, rgx);

        //    return mc;
        //}


    }
}
