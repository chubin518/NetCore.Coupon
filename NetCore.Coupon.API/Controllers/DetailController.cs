using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.Coupon.Service;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using Microsoft.Extensions.Caching.Memory;
using NetCore.Coupon.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCore.Coupon.API.Controllers
{
    [Route("detail/")]
    public class DetailController : Controller
    {
        IProductDetailService detailService;
        IMemoryCache memoryCache;
        public DetailController(IProductDetailService detailService,
            IMemoryCache memoryCache)
        {
            this.detailService = detailService;
            this.memoryCache = memoryCache;
        }
        [Route("getdetail/")]

        public async Task<ProductDetailResponse> GetProductDetail(long id)
        {
            return await memoryCache.GetOrCreate(id, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3);
                var result = await detailService.ProductDetail(new ProductDetailRequest()
                {
                    ProductId = id
                });
                result.Recommends = (await detailService.GetRecommendProducts(new RecommendProductRequest() { CategoryId = result.SPLM.ToLong() }))?.Datas;
                return result;
            });
        }

        [Route("get/")]

        public async Task<ProductDetailResponse> ProductDetail(long id)
        {
            return await memoryCache.GetOrCreate(id, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3);
                return detailService.ProductDetail(new ProductDetailRequest()
                {
                    ProductId = id
                });
            });
        }

        [Route("similar")]

        public async Task<ProductListResponse> GetRecommendProducts(long cat)
        {
            return await memoryCache.GetOrCreate(cat, (entry) =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                return detailService.GetRecommendProducts(new RecommendProductRequest() { CategoryId = cat });
            });

        }
    }
}
