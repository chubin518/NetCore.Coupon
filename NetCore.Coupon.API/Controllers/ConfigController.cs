using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Service;
using Microsoft.Extensions.Caching.Memory;
using NetCore.Coupon.Contracts.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCore.Coupon.API.Controllers
{

    [Route("config")]
    public class ConfigController : Controller
    {
        IProductConfigService productConfigService;
        IMemoryCache memoryCache;
        public ConfigController(IProductConfigService productConfigService, IMemoryCache memoryCache)
        {
            this.productConfigService = productConfigService;
            this.memoryCache = memoryCache;
        }
        [Route("index")]
        public ConfigIndexResponse Index(string k = "")
        {
            return memoryCache.GetOrCreate(k, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                var result = productConfigService.HomeConfig(k);
                return result;
            });
        }
    }
}
