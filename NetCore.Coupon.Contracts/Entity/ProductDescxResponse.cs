using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Entity
{
    public class ProductDescxData
    {

        [JsonProperty("pages")]
        public string[] Pages { get; set; }

        [JsonProperty("images")]
        public string[] Images { get; set; }
    }

    public class ProductDescxResponse
    {
        [JsonProperty("api")]
        public string Api { get; set; }

        [JsonProperty("v")]
        public string V { get; set; }

        [JsonProperty("ret")]
        public string[] Ret { get; set; }

        [JsonProperty("data")]
        public ProductDescxData Data { get; set; }
    }
}
