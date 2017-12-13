using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCore.Coupon.API.Controllers
{
    [Route("tpwd/")]
    public class TaokoulingController : Controller
    {
        IProductDetailService productDetailService;
        public TaokoulingController(IProductDetailService productDetailService)
        {
            this.productDetailService = productDetailService;
        }
        [Route("create/")]
        public string GetTaokouling(TaokoulingRequest request)
        {
            return productDetailService.GetTaokouling(request);
        }
    }
}
