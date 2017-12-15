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
    [Route("Topic")]
    public class ProductTopicController : Controller
    {
        private IProductTopicService productTopicService;
        private IMemoryCache memoryCache;

        public ProductTopicController(IProductTopicService productTopicService, IMemoryCache memoryCache)
        {
            this.productTopicService = productTopicService;
            this.memoryCache = memoryCache;
        }
        [Route("TeMai")]
        public async Task<ProductListResponse> TeMai(int t = 0, int pageno = 1, int pagesize = 50, int sort = 0, string ak = "")
        {
            return await memoryCache.GetOrCreate($"temai_product{t}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15);
                return productTopicService.TeMai(new ProductTopicRequest() { Type = t, PageNo = pageno, Sort = sort });
            });
        }
        [Route("SaleTop")]
        public async Task<ProductListResponse> SaleTop(int t = 0, int pageno = 1, int pagesize = 50, int sort = 0, string ak = "")
        {
            return await memoryCache.GetOrCreate($"saletop_product{t}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);
                return productTopicService.SaleTop(new ProductTopicRequest() { Type = t, PageNo = pageno, Sort = sort });
            });
        }
        [Route("Channel")]
        public async Task<ProductListResponse> Channel(int t = 0, int pageno = 1, int pagesize = 50, int sort = 0, string ak = "")
        {
            return await memoryCache.GetOrCreate($"channel_product{t}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return productTopicService.Channel(new ProductTopicRequest() { Type = t, PageNo = pageno, Sort = sort });
            });
        }
        [Route("All")]
        public async Task<ProductListResponse> All(int t = 0, int pageno = 1, int pagesize = 50, int sort = 0, string ak = "")
        {
            return await memoryCache.GetOrCreate($"all_product{t}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return productTopicService.All(new ProductTopicRequest() { Type = t, PageNo = pageno, Sort = sort });
            });
        }
    }
}
