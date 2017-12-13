using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{
    public class CouponListRequest : BaseRequest
    {
        public long CategoryId { get; set; }
    }
}
