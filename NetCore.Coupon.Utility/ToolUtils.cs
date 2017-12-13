using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetCore.Coupon.Utility
{
    public class ToolUtils
    {
        public static string GetUnixTime()
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
            DateTime dtNow = DateTime.Now;
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 4);
        }

        public static void Waiting()
        {
            Thread.Sleep(1000);
        }

        public static string GetThumbnail(string url, string size = "_400x400")
        {
            string result = url;
            if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("//"))
            {
                url = "https:" + url;
            }

            if (!string.IsNullOrWhiteSpace(url) && url.EndsWith(".jpg"))
            {
                result = url + size;
            }
            return result;
        }

        public static string GetHttpsUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("//"))
            {
                return "https:" + url;
            }
            return url;
        }

        /// <summary>
        /// 获取推广链接
        /// </summary>
        /// <param name="couponId"></param>
        /// <param name="itemId"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string GetTGLink(string couponId, string itemId, string pid)
        {
            return string.Format("https://uland.taobao.com/coupon/edetail?activityId={0}&pid={1}&itemId={2}",
                couponId, pid, itemId);
        }

        public static int GetRandomNum()
        {
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            return ran.Next(0, 200);
        }
    }
}
