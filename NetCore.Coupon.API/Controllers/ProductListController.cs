using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Service;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCore.Coupon.API.Controllers
{
    [Route("search")]
    public class ProductListController : Controller
    {
        IProductListService productListService;
        IMemoryCache memoryCache;
        public ProductListController(IProductListService productListService, IMemoryCache memoryCache)
        {
            this.productListService = productListService;
            this.memoryCache = memoryCache;
        }

        [Route("Product")]

        public ProductListResponse Search(string k = "", int pageno = 1, int pagesize = 100, int sort = 0)
        {
            return memoryCache.GetOrCreate($"product{k}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(9);
                return productListService.Search(new ProductSearchRequest() { KeyWord = k, PageNo = pageno, PageSize = pagesize, Sort = sort });
            });
        }

        [Route("Cats")]

        public ProductListResponse CatProducts(int cat = 0, int pageno = 1, int pagesize = 100, int sort = 0)
        {
            return memoryCache.GetOrCreate($"product{cat}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(13);
                return productListService.CatProducts(new ProductListRequest() { Cat = cat, PageNo = pageno, Sort = sort, PageSize = pagesize });
            });
        }

        [Route("Favorite")]

        public ProductListResponse TopicProducts(int t = 0, int pageno = 1, int pagesize = 50, int sort = 0, string ak = "")
        {
            return memoryCache.GetOrCreate($"product{t}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(11);
                return productListService.TopicProducts(new ProductSpecialRequest() { Type = t, Sort = sort, PageNo = pageno });
            });
        }

        [Route("Config")]
        public HomeConfigResponse Config(string k = "")
        {
            return new HomeConfigResponse()
            {
                v = 1,
                Title = "购便宜惠生活",
                Banners = new List<BannerItem>() {
                    new BannerItem(){
                        ID=1,
                        Icon="http://51hui.xin/staticcdn/yhqtg.jpg"
                    }
                },
                //1女装 2男装 3内衣 4数码家电 5美食 6美妆个护 7母婴 8鞋包配饰 9家居家装 10文体车品 11其他
                Cats = new List<ConfigItem>() {
                    new ConfigItem(){
                        ID=0,
                        Name="精选"
                    },
                    new ConfigItem(){
                        ID=1,
                        Name="女装"
                    },
                    new ConfigItem(){
                        ID=2,
                        Name="男装"
                    },
                    new ConfigItem(){
                        ID=3,
                        Name="内衣"
                    },
                    new ConfigItem(){
                        ID=4,
                        Name="数码家电"
                    },
                    new ConfigItem(){
                        ID=5,
                        Name="美食"
                    },
                    new ConfigItem(){
                        ID=6,
                        Name="美妆个护"
                    },
                    new ConfigItem(){
                        ID=7,
                        Name="母婴"
                    },
                    new ConfigItem(){
                        ID=8,
                        Name="鞋包配饰"
                    },
                    new ConfigItem(){
                        ID=9,
                        Name="家居家装"
                    },
                    new ConfigItem(){
                        ID=10,
                        Name="文体车品"
                    },
                    new ConfigItem(){
                        ID=11,
                        Name="其他"
                    }
                },
                Favorites = new List<ConfigItem>()
                {
                    new ConfigItem(){
                        ID=7,
                        Name="超值9块9",
                        Icon ="https://www.51hui.xin/images/9k9.jpg"
                    },
                    new ConfigItem(){
                        ID=8,
                        Name="20元封顶",
                        Icon ="https://www.51hui.xin/images/20yuan.jpg"
                    },
                    new ConfigItem(){
                        ID=9,
                        Name="精品海淘",
                        Icon ="https://www.51hui.xin/images/muying.jpg"
                    },
                    new ConfigItem(){
                        ID=2,
                        Name="销量爆款",
                        Icon ="https://www.51hui.xin/images/tejia.jpg"
                    }
                }
            };
        }
    }
}
