using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Coupon.Service
{
    public static class ProductSortExtensions
    {
        public static IEnumerable<TbkProductInfo> Sort(this IEnumerable<TbkProductInfo> source, int sort, bool isFilter = true)
        {
            IEnumerable<TbkProductInfo> result = new List<TbkProductInfo>();
            if (source == null)
                return result;
            if (isFilter)
            {
                source = source.Where(item => item.SPYXL >= 1000);
            }
            switch (sort)
            {
                case 1:
                    result = source.OrderByDescending(item => item.SPYXL);
                    break;
                case 2:
                    result = source.OrderBy(item => item.FP.ToDouble());
                    break;
                case 3:
                    result = source.OrderByDescending(item => item.Rate).ThenByDescending(item => (item.CP.ToDouble() / 1000) * item.SPYXL);
                    break;
                default:
                    result = source.OrderByDescending(item => (item.CP.ToDouble() / 1000) * item.SPYXL * (item.Rate / 1000)).ThenBy(item => item.FP.ToDouble()).Shuffle();
                    break;
            }
            return result;
        }
    }
}
