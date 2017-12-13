using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{
    public class TaokoulingRequest : BaseRequest
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Logo { get; set; }
    }
}
