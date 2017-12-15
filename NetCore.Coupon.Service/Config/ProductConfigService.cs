using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Data.Dataoke;
using NetCore.Coupon.Data.Qingtaoke;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Service
{
    public class ProductConfigService : IProductConfigService
    {
        public ProductConfigService()
        {
        }
        public ConfigIndexResponse HomeConfig(string k = "")
        {
            return new ConfigIndexResponse()
            {
                Banners = new List<BannerItem>() {
                    new BannerItem(){
                        ID=1,
                        Icon="http://51hui.xin/staticcdn/yhqtg.jpg"
                    }
                }
            };
        }
    }
}
