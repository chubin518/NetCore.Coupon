using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{

    public class TaobaoProductDetailRequest
    {
        [JsonProperty("exParams")]
        [JsonIgnore]
        public string ExParams { get; set; }

        [JsonProperty("detail_v")]
        [JsonIgnore]
        public string DetailV { get; set; }

        [JsonProperty("itemNumId")]
        public string ItemNumId { get; set; }
    }

    public class ProductDetailExParams
    {

        [JsonProperty("countryCode")]
        [JsonIgnore]
        public string CountryCode { get; set; }

        [JsonProperty("id")]
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("itemId")]
        [JsonIgnore]
        public string ItemId { get; set; }

        [JsonProperty("phoneType")]
        [JsonIgnore]
        public string PhoneType { get; set; }

        [JsonProperty("time")]
        [JsonIgnore]
        public string Time { get; set; }
    }
}
