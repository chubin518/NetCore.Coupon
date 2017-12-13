using NetCore.Coupon.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Response
{
    public class ConfigIndexResponse
    {
        public ConfigIndexResponse()
        {
            this.Banners = new List<BannerItem>();
            this.Top100 = new List<TbkProductInfo>();
            this.Products = new List<TbkProductInfo>();
        }

        public List<BannerItem> Banners { get; set; }

        public List<TbkProductInfo> Top100 { get; set; }

        public List<TbkProductInfo> Products { get; set; }
    }
}
