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
        IProductListService productListService;
        IMemoryCache memoryCache;
        public ConfigController(IProductConfigService productConfigService, IProductListService productListService, IMemoryCache memoryCache)
        {
            this.productConfigService = productConfigService;
            this.productListService = productListService;
            this.memoryCache = memoryCache;
        }
        [Route("index")]
        public ConfigIndexResponse Index(string k = "")
        {
            return memoryCache.GetOrCreate(k, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                var result = productConfigService.HomeConfig(k);
                result.Products = productListService.CatProducts(new ProductListRequest() { Cat = 0, PageNo = 1 })?.Datas;
                result.Top100 = productListService.TopicProducts(new ProductSpecialRequest() { Type = 1, PageNo = 1 })?.Datas;
                return result;
            });
        }
    }
}
