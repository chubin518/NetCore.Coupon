using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Response
{
    public class BaseResponse
    {

    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
