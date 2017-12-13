using NetCore.Coupon.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Response
{
    public class ProductDetailResponse
    {
        /// <summary>
        /// 商品类目
        /// </summary>
        public string SPLM { get; set; }
        /// <summary>
        /// 店铺主图
        /// </summary>
        public string DPZT { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string SPMC { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string DPMC { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        public List<string> Details { get; set; }
        /// <summary>
        /// 商品主图
        /// </summary>
        public List<string> Images { get; set; }
        /// <summary>
        /// 商店描述
        /// </summary>
        public List<Evaluate> Evaluates { get; set; }

        /// <summary>
        /// 商品推荐
        /// </summary>
        public List<TbkProductInfo> Recommends { get; set; }
    }

    public class Evaluate
    {
        public string Title { get; set; }

        public string Score { get; set; }
    }
}
