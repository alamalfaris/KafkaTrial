using Confluent.Kafka;
using KafkaProducer.Api.Interfaces;
using KafkaProducer.Api.Requests;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace KafkaProducer.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public OrderService(IConfiguration configuration)
        {
            _bootstrapServers = configuration.GetSection("KafkaConfigurations:BootstrapServer").Value;
            _topic = configuration.GetSection("KafkaConfigurations:Topic").Value;
        }

        public async Task<bool> SendOrderRequest(OrderRequest orderRequest)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            bool isProduceSuccess = true;

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer
                        .ProduceAsync(_topic, 
                            new Message<Null, string> { Value = JsonSerializer.Serialize(orderRequest) });

                    Console.WriteLine($"Delivery Timestamp: {result.Timestamp.UtcDateTime}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                isProduceSuccess = false;
            }
            return await Task.FromResult(isProduceSuccess);
        }
    }
}
