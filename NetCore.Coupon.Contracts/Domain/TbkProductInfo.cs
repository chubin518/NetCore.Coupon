using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Domain
{
    public class TbkProductInfo
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long SPID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string SPMC { get; set; }
        /// <summary>
        /// 商品主图
        /// </summary>
        public string SPZT { get; set; }
        /// <summary>
        /// 月销量
        /// </summary>
        public long SPYXL { get; set; }
        /// <summary>
        /// 平台类型：0-淘宝；1-天猫
        /// </summary>
        public string PTLX { get; set; }
        /// <summary>
        /// 推广链接
        /// </summary>
        public string SPYHQTGLJ { get; set; }
        /// <summary>
        /// 券后价
        /// </summary>
        public string FP { get; set; }
        /// <summary>
        /// 券价格
        /// </summary>
        public string CP { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public string SPJG { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        [JsonIgnore]
        public double Rate { get; set; }
    }
}
