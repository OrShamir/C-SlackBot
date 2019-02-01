namespace API.ChatApi
{
    public interface IChatApi
    {
        PostMessageResponseModel PostMessage(string slackApiToken, string channel, string text);
    }
}
