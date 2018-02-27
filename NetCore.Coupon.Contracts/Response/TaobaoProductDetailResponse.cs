using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Response
{
    public class ProductDetailItem
    {

        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("images")]
        public string[] Images { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("rootCategoryId")]
        public string RootCategoryId { get; set; }

        [JsonProperty("brandValueId")]
        public string BrandValueId { get; set; }

        [JsonProperty("countMultiple")]
        public object[] CountMultiple { get; set; }

        [JsonProperty("commentCount")]
        public string CommentCount { get; set; }

        [JsonProperty("favcount")]
        public string Favcount { get; set; }

        [JsonProperty("taobaoDescUrl")]
        public string TaobaoDescUrl { get; set; }

        [JsonProperty("tmallDescUrl")]
        public string TmallDescUrl { get; set; }

        [JsonProperty("taobaoPcDescUrl")]
        public string TaobaoPcDescUrl { get; set; }
    }

    public class ProductEvaluate
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }
    }

    public class ProductSeller
    {

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("shopId")]
        public string ShopId { get; set; }

        [JsonProperty("shopName")]
        public string ShopName { get; set; }

        [JsonProperty("shopUrl")]
        public string ShopUrl { get; set; }

        [JsonProperty("taoShopUrl")]
        public string TaoShopUrl { get; set; }

        [JsonProperty("shopIcon")]
        public string ShopIcon { get; set; }

        [JsonProperty("fans")]
        public string Fans { get; set; }

        [JsonProperty("allItemCount")]
        public string AllItemCount { get; set; }

        [JsonProperty("showShopLinkIcon")]
        public bool ShowShopLinkIcon { get; set; }

        [JsonProperty("shopCard")]
        public string ShopCard { get; set; }

        [JsonProperty("sellerType")]
        public string SellerType { get; set; }

        [JsonProperty("shopType")]
        public string ShopType { get; set; }

        [JsonProperty("evaluates")]
        public ProductEvaluate[] Evaluates { get; set; }

        [JsonProperty("sellerNick")]
        public string SellerNick { get; set; }

        [JsonProperty("creditLevel")]
        public string CreditLevel { get; set; }

        [JsonProperty("starts")]
        public string Starts { get; set; }

        [JsonProperty("goodRatePercentage")]
        public string GoodRatePercentage { get; set; }
    }

    public class ProductDetailData
    {
        [JsonProperty("item")]
        public ProductDetailItem Item { get; set; }

        [JsonProperty("seller")]
        public ProductSeller Seller { get; set; }
    }

    public class TaobaoProductDetailResponse
    {

        [JsonProperty("api")]
        [JsonIgnore]
        public string Api { get; set; }

        [JsonProperty("v")]
        [JsonIgnore]
        public string V { get; set; }

        [JsonProperty("ret")]
        [JsonIgnore]
        public string[] Ret { get; set; }

        [JsonProperty("data")]
        public ProductDetailData Data { get; set; }
    }
}
