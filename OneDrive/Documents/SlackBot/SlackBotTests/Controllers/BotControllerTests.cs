using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlackBot.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SlackBotTests.Responders
{
    [TestClass()]
    public class BotControllerTests
    {
        private const string BaseUrl = "http://localhost:8081";
        private const string RelativeUrl = "api/v1/tenants/2/banks/accounts/groups";
        private BotController m_BotController;
        private HttpResponseMessage m_HttpResponseMessage;

        [TestMethod()]
        public void GetAverageTest()
        {
            //Given

            m_BotController = new BotController();
            Uri requestUri = new Uri(new Uri(BaseUrl), RelativeUrl);

            //When
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            m_HttpResponseMessage = m_BotController.GetSlackAverage(httpRequestMessage);

            //Then
            Assert.Fail();
        }
    }
}
