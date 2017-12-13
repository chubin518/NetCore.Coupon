using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{
    public class RecommendProductRequest : BaseRequest
    {
        public long CategoryId { get; set; }

        public long ProductId { get; set; }
    }
}
