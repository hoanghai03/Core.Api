using Core.Domain.Shared.Cache;
using Core.Domain.Shared.Export;
using Core.MemoryCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Import.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        public ICached _cache;
        public ExportController(ICached cache)
        {
            _cache= cache;
        }

        [HttpGet]
        public async Task<IActionResult> Export([FromBody] ExportParam data) 
        {
            CacheRequest cacheRequest = new CacheRequest();
            // set cache
            _cache.SetCache<ExportParam>(cacheRequest, data);
            return Ok();

        }

    }
}
