using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public string First()
        {
            return "Cart - First";
        }

        [HttpGet]
        public string Second()
        {
            return "Cart - Second";
        }
    }
}
