using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Response
{
    public class ProductCouponListResponse
    {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public CouponListResult Result { get; set; }
    }

    public class CouponListItem
    {

        [JsonProperty("clickUrl")]
        public string ClickUrl { get; set; }

        [JsonProperty("picUrl")]
        public string PicUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("reservePrice")]
        public double ReservePrice { get; set; }

        [JsonProperty("discountPrice")]
        public double DiscountPrice { get; set; }

        [JsonProperty("biz30Day")]
        public int Biz30Day { get; set; }

        [JsonProperty("tmall")]
        public string Tmall { get; set; }

        [JsonProperty("postFree")]
        public string PostFree { get; set; }

        [JsonProperty("itemId")]
        public long ItemId { get; set; }

        [JsonProperty("commission")]
        public object Commission { get; set; }

        [JsonProperty("shareUrl")]
        public string ShareUrl { get; set; }
    }

    public class CouponListDetail
    {

        [JsonProperty("retStatus")]
        public int RetStatus { get; set; }

        [JsonProperty("startFee")]
        public double StartFee { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("shopLogo")]
        public object ShopLogo { get; set; }

        [JsonProperty("shopName")]
        public object ShopName { get; set; }

        [JsonProperty("couponFlowLimit")]
        public bool CouponFlowLimit { get; set; }

        [JsonProperty("effectiveStartTime")]
        public string EffectiveStartTime { get; set; }

        [JsonProperty("effectiveEndTime")]
        public string EffectiveEndTime { get; set; }

        [JsonProperty("couponKey")]
        public string CouponKey { get; set; }

        [JsonProperty("pid")]
        public object Pid { get; set; }

        [JsonProperty("item")]
        public CouponListItem Item { get; set; }
    }

    public class CouponListResult
    {

        [JsonProperty("couponList")]
        public List<CouponListDetail> CouponList { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
