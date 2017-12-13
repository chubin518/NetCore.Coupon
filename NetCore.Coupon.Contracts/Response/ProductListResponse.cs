using NetCore.Coupon.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Response
{
    public class ProductListResponse
    {
        public ProductListResponse()
        {
            this.Datas = new List<TbkProductInfo>();
        }
        public long Total { get; set; }

        public List<TbkProductInfo> Datas { get; set; }
    }
}
