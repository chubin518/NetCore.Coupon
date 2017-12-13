using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{
    public class ProductDetailRequest : BaseRequest
    {
        public long ProductId { get; set; }
    }
}
