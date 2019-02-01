using MargieBot.Responders;
using SlackBot.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SlackBot.Controllers
{
    public class BotController: ApiController
    {
        private ChannelResponder m_ChannelResponder;

        public BotController(ChannelResponder channelResponder)
        {
            if(channelResponder == null)
            {
                throw new ArgumentNullException("channelResponder");
            }

            m_ChannelResponder = channelResponder;

        }

        [HttpGet]
        [Route("average")]
        public HttpResponseMessage GetSlackAverage(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            decimal channelAverage = m_ChannelResponder.GetChannelAverage();

            try
            {
                response = request.CreateResponse(HttpStatusCode.OK, channelAverage);
            }
            catch (Exception ex)
            {
                string error = string.Format("Error Getting Average");

                response = request.CreateErrorResponse(HttpStatusCode.BadGateway, error);
            }

            return response;
        }

        [HttpGet]
        [Route("average/{slack_username}")]
        public HttpResponseMessage GetSlackAverageByUserName(HttpRequestMessage request, string userName)
        {
            HttpResponseMessage response = null;

            if (userName == null)
            {
                return response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter user name");
            }

            decimal userAverage = m_ChannelResponder.GetUserAverage(userName);

            try
            {
                response = request.CreateResponse(HttpStatusCode.OK, userAverage);
            }
            catch (Exception ex)
            {
                string error = string.Format("Error Getting Average");

                response = request.CreateErrorResponse(HttpStatusCode.BadGateway, error);
            }

            return response;
        }
    }
}

