using Core.Domain.Shared.ModelKafka;
using Core.Export.Interface;
using Core.Export.Services;
using ExchangeBus.Kafka.Interface;
using System.Runtime.InteropServices;

namespace ExportExcelWorkerr
{
    public class ExportExcelWorker : BackgroundService
    {
        private readonly ILogger<ExportExcelWorker> _logger;
        private readonly IKafkaConsumer _kafkaConsumer;      
        private readonly IExportService _exportService;
        public ExportExcelWorker(ILogger<ExportExcelWorker> logger, IKafkaConsumer kafkaConsumer, IExportService exportService)
        {
            _logger = logger;
            _kafkaConsumer = kafkaConsumer;
            _exportService = exportService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int a = 1;
            while (!stoppingToken.IsCancellationRequested && a == 1)
            {
                DoWorker(stoppingToken);
                a++;
            }
        }

        public void DoWorker(CancellationToken stoppingToken)
        {
            Task.Run(async () => {
                await _kafkaConsumer.StartAsync(stoppingToken, async (ExchangeModel model) =>
                {
                    try
                    {
                        await _kafkaConsumer.Commit();
                        switch (model.dataType)
                        {
                            case "export-grid":
                                if(model.type != null)
                                {
                                _exportService.ExportExcel(model.exportId,model.type);

                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    return null;
                });
            });
        }
    }
}