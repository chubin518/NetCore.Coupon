using NetCore.Coupon.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Service
{
    public class ProductIdComparer : IEqualityComparer<TbkProductInfo>
    {
        public bool Equals(TbkProductInfo x, TbkProductInfo y)
        {
            if (x == null)
                return y == null;
            return x.SPID == y.SPID;
        }

        public int GetHashCode(TbkProductInfo obj)
        {
            if (obj == null)
                return 0;
            return obj.SPID.GetHashCode();
        }
    }
}
