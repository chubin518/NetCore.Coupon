using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCore.Coupon.API.Extensions
{
    public class ApiErrorHandleAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            LogUtils.Error(context.Exception);

            context.Result = new ObjectResult(new BaseResponse()
            {

            });
        }
    }
}
