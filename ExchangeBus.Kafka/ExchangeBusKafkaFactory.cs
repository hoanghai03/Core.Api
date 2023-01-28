using Confluent.Kafka;
using ExchangeBus.Kafka.Interface;
using ExchangeBus.Kafka.Model;
using ExchangeBus.Kafka.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace ExchangeBus.Kafka
{
    public static class ExchangeBusKafkaFactory
    {
        public static void InjectServices(IServiceCollection services, IConfiguration configuration)
        {
            KafkaConfig kafkaConfig = new KafkaConfig();
            services.AddSingleton<IKafkaProducer, KafkaProducer>((p)=> new KafkaProducer(kafkaConfig));
            services.AddSingleton<IKafkaConsumer, KafkaConsumer>((c) => new KafkaConsumer(kafkaConfig));
        }
    }
}
