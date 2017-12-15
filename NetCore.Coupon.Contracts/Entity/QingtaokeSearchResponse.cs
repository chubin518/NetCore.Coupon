using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Entity
{

    public class QingtaokeItem
    {

        [JsonProperty("goods_id")]
        public string GoodsId { get; set; }

        [JsonProperty("goods_pic")]
        public string GoodsPic { get; set; }

        [JsonProperty("goods_title")]
        public string GoodsTitle { get; set; }

        [JsonProperty("goods_short_title")]
        public string GoodsShortTitle { get; set; }

        [JsonProperty("goods_cat")]
        public string GoodsCat { get; set; }

        [JsonProperty("goods_price")]
        public string GoodsPrice { get; set; }

        [JsonProperty("goods_sales")]
        public string GoodsSales { get; set; }

        [JsonProperty("goods_introduce")]
        public string GoodsIntroduce { get; set; }

        [JsonProperty("is_tmall")]
        public string IsTmall { get; set; }

        [JsonProperty("commission")]
        public string Commission { get; set; }

        [JsonProperty("commission_type")]
        public string CommissionType { get; set; }

        [JsonProperty("commission_link")]
        public string CommissionLink { get; set; }

        [JsonProperty("coupon_is_check")]
        public string CouponIsCheck { get; set; }

        [JsonProperty("coupon_type")]
        public string CouponType { get; set; }

        [JsonProperty("seller_id")]
        public string SellerId { get; set; }

        [JsonProperty("coupon_id")]
        public string CouponId { get; set; }

        [JsonProperty("coupon_price")]
        public string CouponPrice { get; set; }

        [JsonProperty("coupon_number")]
        public string CouponNumber { get; set; }

        [JsonProperty("coupon_limit")]
        public string CouponLimit { get; set; }

        [JsonProperty("coupon_over")]
        public string CouponOver { get; set; }

        [JsonProperty("coupon_condition")]
        public string CouponCondition { get; set; }

        [JsonProperty("coupon_start_time")]
        public string CouponStartTime { get; set; }

        [JsonProperty("coupon_end_time")]
        public string CouponEndTime { get; set; }

        [JsonProperty("is_ju")]
        public string IsJu { get; set; }

        [JsonProperty("is_tqg")]
        public string IsTqg { get; set; }

        [JsonProperty("is_ali")]
        public string IsAli { get; set; }
    }

    public class QingtaokeData
    {

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("list")]
        public QingtaokeItem[] List { get; set; }
    }


    public class QingtaokeSearchResponse
    {

        [JsonProperty("er_code")]
        public int ErCode { get; set; }

        [JsonProperty("er_msg")]
        public string ErMsg { get; set; }

        [JsonProperty("data")]
        public QingtaokeData Data { get; set; }
    }
}
