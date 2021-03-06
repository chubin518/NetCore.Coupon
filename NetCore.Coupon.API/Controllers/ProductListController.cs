﻿using System;
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
        IProductTopicService productTopicService;
        IProductSearchService productSearchService;
        IProductClassifyService productClassifyService;
        IMemoryCache memoryCache;
        public ProductListController(IMemoryCache memoryCache, IProductTopicService productTopicService,
        IProductSearchService productSearchService,
        IProductClassifyService productClassifyService)
        {
            this.memoryCache = memoryCache;
            this.productClassifyService = productClassifyService;
            this.productSearchService = productSearchService;
            this.productTopicService = productTopicService;
        }

        [Route("Product")]

        public async Task<ProductListResponse> Search(string k = "", int pageno = 1, int pagesize = 100, int sort = 0)
        {
            return await memoryCache.GetOrCreate($"Searchproduct{k}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(9);
                return productSearchService.List(new ProductSearchRequest()
                {
                    KeyWord = k,
                    PageNo = pageno,
                    PageSize = pagesize,
                    Sort = sort
                });
            });
        }

        [Route("Cats")]

        public async Task<ProductListResponse> CatProducts(int cat = 0, int pageno = 1, int pagesize = 100, int sort = 0)
        {
            return await memoryCache.GetOrCreate($"CatProduct{cat}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(13);
                return productClassifyService.List(new ProductClassifyRequest()
                {
                    Cat = cat,
                    PageNo = pageno,
                    PageSize = pagesize,
                    Sort = sort
                });
            });
        }

        [Route("Favorite")]

        public async Task<ProductListResponse> Topic(int t = 0, int pageno = 1, int pagesize = 50, int sort = 0, string ak = "")
        {
            return await memoryCache.GetOrCreate($"Topicproduct{t}_{pageno}{pagesize}_{sort}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(11);
                return productTopicService.WeiXin(new ProductTopicRequest()
                {
                    Type = t,
                    PageNo = pageno,
                    Sort = sort
                });
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
                        Icon ="https://www.51hui.xin/images/9k91.png"
                    },
                    new ConfigItem(){
                        ID=8,
                        Name="20元封顶",
                        Icon ="https://www.51hui.xin/images/20yuan.png"
                    },
                    new ConfigItem(){
                        ID=10,
                        Name="品牌特卖",
                        Icon ="https://www.51hui.xin/images/temai.png"
                    },
                    new ConfigItem(){
                        ID=9,
                        Name="精品海淘",
                        Icon ="https://www.51hui.xin/images/haitao.png"
                    },
                    new ConfigItem(){
                        ID=1,
                        Name="小编推荐",
                        Icon ="https://www.51hui.xin/images/pinpai.png"
                    },
                    new ConfigItem(){
                        ID=2,
                        Name="销量爆款",
                        Icon ="https://www.51hui.xin/images/pinpai.png"
                    }
                }
            };
        }
    }
}
