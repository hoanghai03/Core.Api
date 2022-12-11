using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Product.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public string First()
        {
            return "Product - First";
        }

        [HttpGet]
        public string Second()
        {
            return "Product - Second";
        }
    }
}
