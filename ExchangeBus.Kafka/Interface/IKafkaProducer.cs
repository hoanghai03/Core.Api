using Core.Domain.Shared.ModelKafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBus.Kafka.Interface
{
    public interface IKafkaProducer
    {
        Task PushAsync(ExchangeModel exchangeModel);
    }
}
