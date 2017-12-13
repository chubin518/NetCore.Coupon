using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetCore.Coupon.Utility
{
    public static class CouponInfoFomarter
    {
        static Regex regex = new Regex(@"([1-9]\d*\.?\d*)|(0\.\d*[1-9])");

        /// <summary>
        /// 获取优惠券价格信息 item1-limit item2-price
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Tuple<decimal, string> GetCouponPrice(string info)
        {
            Tuple<decimal, string> coupon = new Tuple<decimal, string>(0, info);
            var matchs = regex.Matches(info);
            if (matchs.Count == 1)
            {
                coupon = new Tuple<decimal, string>(0, matchs[0].Value);
            }
            if (matchs.Count == 2)
            {
                coupon = new Tuple<decimal, string>(matchs[0].Value.ToDecimal(), matchs[1].Value);
            }
            return coupon;
        }
    }
}
