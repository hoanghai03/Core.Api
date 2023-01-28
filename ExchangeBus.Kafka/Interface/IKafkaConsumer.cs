using Core.Domain.Shared.ModelKafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBus.Kafka.Interface
{
    public interface IKafkaConsumer
    {
        Task<int> StartAsync(CancellationToken cancellationToken, Func<ExchangeModel, Task<string>> func);

        Task<int> Commit();
    }
}
