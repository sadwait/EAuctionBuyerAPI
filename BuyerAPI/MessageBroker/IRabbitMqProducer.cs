using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerAPI.MessageBroker
{
    public interface IRabbitMqProducer
    {
        void Publish(string message);
    }
}
