using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Apis
{
    [ApiController]
    [Route("[controller]s")]
    public abstract class BaseApi : ControllerBase
    {
    }
}
