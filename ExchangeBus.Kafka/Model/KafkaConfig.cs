using Confluent.Kafka;
using ExchangeBus.Kafka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBus.Kafka.Model
{
    public class KafkaConfig
    {
        /// <summary>
        /// Tên của topic
        /// </summary>
        public string Topic { get; set; }
        public ProducerConfig Producer { get; set; }
        public ConsumerConfig Consumer { get; set; }
    }
}
