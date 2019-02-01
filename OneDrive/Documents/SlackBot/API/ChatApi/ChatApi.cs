﻿using Flurl.Http;
using ServiceStack.Text;

namespace API.ChatApi
{
    public class ChatApi : IChatApi
    {
        public PostMessageResponseModel PostMessage(string slackApiToken, string channel, string text)
        {
            var responseString = "https://slack.com/api/chat.postMessage"
                .PostUrlEncodedAsync(
                    new
                    {
                        token = slackApiToken,
                        channel,
                        text,
                        as_user = true
                    })
                .ReceiveString()
                .Result;

            var serializer = new JsonSerializer<PostMessageResponseModel>();
            var response = serializer.DeserializeFromString(responseString);
            return response;
        }
    }
}
