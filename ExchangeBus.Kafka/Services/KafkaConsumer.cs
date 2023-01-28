using Confluent.Kafka;
using Core.Domain.Shared.ModelKafka;
using ExchangeBus.Kafka.Interface;
using ExchangeBus.Kafka.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBus.Kafka.Services
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<string,string> _consumer;
        private readonly KafkaConfig _config;

        public KafkaConsumer(KafkaConfig config)
        {
            _config = config;
            _config.Consumer = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = 0
            };
            _config.Topic = "is-topic";
            _consumer = new ConsumerBuilder<string,string>(_config.Consumer).Build();
        }

        public async Task<int> Commit()
        {
            _consumer.Commit();
            return 1;
        }

        public async Task<int> StartAsync(CancellationToken cancellationToken, Func<ExchangeModel, Task<string>> func)
        {
            try
            {
                _consumer.Subscribe(_config.Topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var c = _consumer.Consume(cancellationToken);
                        if (c != null)
                        {
                            var data = JsonConvert.DeserializeObject<ExchangeModel>(c.Message.Value);
                            await func.Invoke(data);
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        throw ex;
                    }
                }
            } catch (OperationCanceledException)
            {
                _consumer.Close();
            }
            return 1;
            
        }
    } 
}
