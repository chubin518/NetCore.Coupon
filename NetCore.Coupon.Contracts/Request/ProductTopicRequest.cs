﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Request
{
    public class ProductTopicRequest : BaseRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }

        public int PageNo { get; set; }

        public int Sort { get; set; }

        public int Cat { get; set; }
    }
}
