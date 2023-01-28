using Confluent.Kafka;
using Core.Domain.Shared.ModelKafka;
using ExchangeBus.Kafka.Interface;
using ExchangeBus.Kafka.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace ExchangeBus.Kafka.Services
{
    public class KafkaProducer : IDisposable, IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly KafkaConfig _config;
        public KafkaProducer(KafkaConfig config)
        {
            _config = config;
            _config.Producer = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _config.Topic = "is-topic";

            _producer = new ProducerBuilder<string,string>(_config.Producer).Build();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Đẩu dữ liệu lên kafka
        /// </summary>
        /// <param name="exchangeModel"></param>
        /// <returns></returns>
        public async Task PushAsync(ExchangeModel exchangeModel)
        {
            var topic = _config.Topic;
            await _producer.ProduceAsync(topic, new Message<string, string> { Key = exchangeModel.exportId.ToString(), Value = JsonConvert.SerializeObject(exchangeModel) });
            //_producer.Dispose();
        }
    }
}
