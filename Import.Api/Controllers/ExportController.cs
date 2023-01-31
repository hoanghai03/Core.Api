using Core.Domain.Shared.Cache;
using Core.Domain.Shared.Export;
using Core.Domain.Shared.ModelKafka;
using Core.MemoryCache;
using ExchangeBus.Kafka.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;

namespace Import.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        public ICached _cache;
        private readonly IKafkaProducer _producer;
        public ExportController(ICached cache, IKafkaProducer producer)
        {
            _cache = cache;
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> ExportExcel([FromBody] ExportParam data) 
        {
            //CacheRequest cacheRequest = new CacheRequest();
            ExchangeModel exchangeModel = new ExchangeModel();
            exchangeModel.dataType = data.ImportTypeInfo;
            exchangeModel.exportId = data.ExportId;
            exchangeModel.exportType= data.ExportType;

            // set cache
            //_cache.SetCache<ExportParam>(cacheRequest, data);

            _producer.PushAsync(exchangeModel);

            return Ok();

        }

    }
}
