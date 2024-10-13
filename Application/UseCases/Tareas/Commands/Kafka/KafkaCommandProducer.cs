using Confluent.Kafka;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.Kafka
{
    public class KafkaCommandProducer : ICommandSender<OrderCommand>
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaCommandProducer()
        {
            var config = new ProducerConfig { 
                 BootstrapServers = "lmsKafkareto3.servicebus.windows.net:9093",
                 SecurityProtocol = SecurityProtocol.SaslSsl,
                 SaslMechanism = SaslMechanism.Plain,
                 SaslUsername = "$ConnectionString",
                 SaslPassword = "Endpoint=sb://lmskafkareto3.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=r5h+sikd7rmVe7UpX4maLxCPK4hJ8Ly5W+AEhAWu+ho=",
                 MessageTimeoutMs = 60000
                //SslCaLocation = "/etc/ssl/certs"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendCommandAsync(OrderCommand command)
        {
            var message = new Message<Null, string>
            {
                Value = $"OrderId: {command.OrderId}, Product: {command.ProductName}"
            };
            try
            {
                await _producer.ProduceAsync("eventoreto3", message);
            }
            catch(Exception exp)
            {

            }
           
        }
    }
}
