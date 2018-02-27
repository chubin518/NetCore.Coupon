using NetCore.Coupon.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Domain
{
    public class TbkDetailInfo
    {
        public string NumIid { get; set; }

        public string Title { get; set; }

        public string CategoryId { get; set; }

        public List<string> Images { get; set; }

        public string ShopName { get; set; }

        public string ShopIcon { get; set; }

        public List<Evaluate> Evaluates { get; set; }
    }
}
