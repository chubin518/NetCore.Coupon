using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Entity
{

    public class DataokeData
    {

        [JsonProperty("api_type")]
        public string ApiType { get; set; }

        [JsonProperty("update_time")]
        public string UpdateTime { get; set; }

        [JsonProperty("total_num")]
        public int TotalNum { get; set; }

        [JsonProperty("update_content")]
        public string UpdateContent { get; set; }
    }

    public class DaotaokeItem
    {

        [JsonProperty("ID")]
        public string ID { get; set; }
        /*商品淘宝id*/
        [JsonProperty("GoodsID")]
        public string GoodsID { get; set; }
        /*商品标题*/
        [JsonProperty("Title")]
        public string Title { get; set; }
        /*商品短标题*/
        [JsonProperty("D_title")]
        public string DTitle { get; set; }
        /*商品主图*/
        [JsonProperty("Pic")]
        public string Pic { get; set; }
        /*分类ID*/
        [JsonProperty("Cid")]
        public string Cid { get; set; }
        /*正常售价*/
        [JsonProperty("Org_Price")]
        public string OrgPrice { get; set; }
        /*券后价*/
        [JsonProperty("Price")]
        public double Price { get; set; }
        /*是否天猫 1-是*/
        [JsonProperty("IsTmall")]
        public string IsTmall { get; set; }
        /*商品销量*/
        [JsonProperty("Sales_num")]
        public string SalesNum { get; set; }
        /*商品描述分*/
        [JsonProperty("Dsr")]
        public string Dsr { get; set; }
        /*卖家ID*/
        [JsonProperty("SellerID")]
        public string SellerID { get; set; }
        
        [JsonProperty("Commission")]
        public string Commission { get; set; }
        /*计划(通用)佣金比例*/
        [JsonProperty("Commission_jihua")]
        public string CommissionJihua { get; set; }
        /*高佣鹊桥佣金比例*/
        [JsonProperty("Commission_queqiao")]
        public string CommissionQueqiao { get; set; }
        /*计划链接*/
        [JsonProperty("Jihua_link")]
        public string JihuaLink { get; set; }

        [JsonProperty("Que_siteid")]
        public string QueSiteid { get; set; }

        [JsonProperty("Jihua_shenhe")]
        public string JihuaShenhe { get; set; }
        /*商品文案*/
        [JsonProperty("Introduce")]
        public string Introduce { get; set; }
        /*优惠券ID*/
        [JsonProperty("Quan_id")]
        public string QuanId { get; set; }
        /*优惠券金额*/
        [JsonProperty("Quan_price")]
        public string QuanPrice { get; set; }
        /*优惠券结束时间*/
        [JsonProperty("Quan_time")]
        public string QuanTime { get; set; }
        /*优惠券剩余数量*/
        [JsonProperty("Quan_surplus")]
        public string QuanSurplus { get; set; }
        /*已领券数量*/
        [JsonProperty("Quan_receive")]
        public string QuanReceive { get; set; }
        /*优惠券使用条件*/
        [JsonProperty("Quan_condition")]
        public string QuanCondition { get; set; }
        /*手机链接*/
        [JsonProperty("Quan_m_link")]
        public string QuanMLink { get; set; }
        /*手机券短链*/
        [JsonProperty("Quan_link")]
        public string QuanLink { get; set; }
        /*淘宝客链接（需用大淘客助手转链）*/
        [JsonProperty("ali_click")]
        public string AliClick { get; set; }
    }

    public class DataokeSearchResponse
    {

        [JsonProperty("data")]
        public DataokeData Data { get; set; }

        [JsonProperty("result")]
        public DaotaokeItem[] Result { get; set; }
    }
}
