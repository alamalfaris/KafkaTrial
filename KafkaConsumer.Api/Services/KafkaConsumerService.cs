using Confluent.Kafka;
using KafkaConsumer.Api.Requests;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace KafkaConsumer.Api.Services
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly string _topic = "order-test";
        private readonly string _groupId = "order-test-id";
        private readonly string _bootstrapServers = "alamalfaris:9092";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(_topic);
                    var cancelToken = new CancellationTokenSource();

                    while (true)
                    {
                        var consumer = consumerBuilder.Consume(cancelToken.Token);
                        var orderRequest = JsonSerializer.Deserialize<OrderRequest>(consumer.Message.Value);
                        Console.WriteLine($"Processing Order Id:{orderRequest?.OrderId}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
