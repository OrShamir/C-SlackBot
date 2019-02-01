using MargieBot.Models;
using MargieBot.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Responders
{
    public abstract class Responder : IResponder
    {
        private long m_CanRespondCounter;
        private long m_NumberMessageSum;

        public long CanRespondCounter
        {
            get
            {
                return this.m_CanRespondCounter;
            }
            protected set
            {
                this.m_CanRespondCounter = value;
            }
        }

        public long NumberMessageSum
        {
            get
            {
                return this.m_NumberMessageSum;
            }
            protected set
            {
                this.m_NumberMessageSum = value;
            }
        }

        public abstract bool CanRespond(ResponseContext context);

        public abstract BotMessage GetResponse(ResponseContext context);
        
    }
}
