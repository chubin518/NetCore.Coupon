using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{

    public class TaobaoProductDetailRequest
    {
        [JsonProperty("exParams")]
        public string ExParams { get; set; }

        [JsonProperty("detail_v")]
        public string DetailV { get; set; }

        [JsonProperty("itemNumId")]
        public string ItemNumId { get; set; }
    }

    public class ProductDetailExParams
    {

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("phoneType")]
        public string PhoneType { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
