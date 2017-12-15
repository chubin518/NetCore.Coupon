using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.Coupon.Service;
using Microsoft.Extensions.Caching.Memory;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Contracts.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCore.Coupon.API.Controllers
{
    [Route("Search")]
    public class ProductSearchController : Controller
    {
        private IProductSearchService productSearchService;
        private IMemoryCache memoryCache;

        public ProductSearchController(IProductSearchService productSearchService, IMemoryCache memoryCache)
        {
            this.productSearchService = productSearchService;
            this.memoryCache = memoryCache;
        }

        [Route("List")]
        public async Task<ProductListResponse> List(string k = "", int pageno = 1, int pagesize = 100, int sort = 0)
        {
            return await memoryCache.GetOrCreate($"search_product{k}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(13);
                return productSearchService.List(new ProductSearchRequest() { KeyWord = k, PageNo = pageno, PageSize = pagesize, Sort = sort });
            });
        }
    }
}
