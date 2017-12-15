using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Coupon.API.Controllers
{
    [Route("Classify")]
    public class ProductClassifyController : Controller
    {
        private IProductClassifyService productClassifyService;
        private IMemoryCache memoryCache;

        public ProductClassifyController(IProductClassifyService productClassifyService, IMemoryCache memoryCache)
        {
            this.productClassifyService = productClassifyService;
            this.memoryCache = memoryCache;
        }

        [Route("List")]
        public async Task<ProductListResponse> List(int cat = 0, int pageno = 1, int pagesize = 100, int sort = 0)
        {
            return await memoryCache.GetOrCreate($"classify_product{cat}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(13);
                return productClassifyService.List(new ProductClassifyRequest() { Cat = cat, PageNo = pageno, Sort = sort, PageSize = pagesize });
            });
        }
    }
}
