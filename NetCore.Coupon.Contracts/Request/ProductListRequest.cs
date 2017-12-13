using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{
    public class ProductListRequest : BaseRequest
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }
        /// <summary>
        /// 0 全部 1 女装 2 男装 3 内衣 4 数码家电 5 美食 6 美妆个护 7 母婴 8 鞋包配饰 9 家居家装 10 文体车品 11 其他
        /// </summary>
        public int Cat { get; set; } = -1;

        public int TodayNew { get; set; }

        public int Juhuasuan { get; set; }

        public int Taoqianggou { get; set; }

        public int Haitao { get; set; }

        public float MinPrice { get; set; } = -1;

        public float MaxPrice { get; set; }

        public int Sort { get; set; }
    }
}
