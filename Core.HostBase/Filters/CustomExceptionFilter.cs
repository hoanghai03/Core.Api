﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.HostBase.Filters
{
    public class CustomExceptionFilter : IActionFilter,IOrderedFilter
    {
        public int Order => int.MaxValue -10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException httpResponseException)
            {
                //  TH: Xảy ra ngoại lệ
                var result = new
                {
                    msgDev = "",
                    userMsg = "Đã xảy ra ngoại lệ , vui lòng liên hệ Hoàng Hải",
                    data = httpResponseException.Message,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                var result = new
                {
                    msgDev = "",
                    userMsg = "Đã xảy ra ngoại lệ , vui lòng liên hệ Hoàng Hải",
                    data = DBNull.Value,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
