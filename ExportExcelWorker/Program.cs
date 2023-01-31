using Core.Domain.Postgre.Base;
using Core.Domain.Repos;
using Core.Export;
using Core.Export.Interface;
using Core.Export.Services;
using Core.HostBase.Configurations;
using ExchangeBus.Kafka.Interface;
using ExchangeBus.Kafka.Model;
using ExchangeBus.Kafka.Services;
using ExportExcelWorkerr;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ExportExcelWorker>();
        services.AddSingleton<IExportService, ExportService>();
        services.AddSingleton<IBaseRepo, BaseRepo>();
        
        KafkaConfig kafkaConfig = new KafkaConfig();
        services.AddSingleton<IKafkaConsumer, KafkaConsumer>((c) => new KafkaConsumer(kafkaConfig));
    }).ConfigureAppsettings(typeof(Program), new string[] { })
    .Build();

await host.RunAsync();
