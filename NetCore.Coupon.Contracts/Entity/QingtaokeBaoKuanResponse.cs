using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Entity
{

    public class QingtaokeBaoKuanResponse
    {

        [JsonProperty("er_code")]
        public int ErCode { get; set; }

        [JsonProperty("er_msg")]
        public string ErMsg { get; set; }

        [JsonProperty("data")]
        public QingtaokeItem[] Data { get; set; }
    }

}
