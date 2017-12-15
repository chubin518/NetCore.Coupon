using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{
    public class ProductSearchRequest : BaseRequest
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string KeyWord { get; set; }

        public int Cat { get; set; }

        public int TodayNew { get; set; }

        public int Tmall { get; set; }

        public int Juhuasuan { get; set; }

        public int Taoqianggou { get; set; }

        public int Haitao { get; set; }

        public float MinPrice { get; set; } = -1;

        public float MaxPrice { get; set; }

        public int Sort { get; set; }

        public List<long> CategoryIds { get; set; }
    }
}
