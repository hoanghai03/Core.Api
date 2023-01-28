using Core.Export;
using Core.Export.Interface;
using Core.Export.Services;
using ExchangeBus.Kafka.Interface;
using ExchangeBus.Kafka.Model;
using ExchangeBus.Kafka.Services;
using ExportExcelWorkerr;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ExportExcelWorker>();
        services.AddSingleton<IExportService, ExportService>();
        KafkaConfig kafkaConfig = new KafkaConfig();
        services.AddSingleton<IKafkaConsumer, KafkaConsumer>((c) => new KafkaConsumer(kafkaConfig));
    })
    .Build();

await host.RunAsync();
