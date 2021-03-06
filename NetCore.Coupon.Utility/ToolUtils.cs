﻿using System;
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
                result = "https:" + url;
            }
            else if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("http:"))
            {
                result = url.Replace("http:", "https:", StringComparison.OrdinalIgnoreCase);
            }

            if (!string.IsNullOrWhiteSpace(size)
                && !string.IsNullOrWhiteSpace(result)
                && (result.EndsWith(".jpg") || result.EndsWith(".png")))
            {
                result = result + size;
            }
            if (string.IsNullOrWhiteSpace(size))
            {
                return result;
            }
            return result + url.Substring(url.LastIndexOf('.'));
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

        public static int GetRandomNum(int start = 0, int next = 200)
        {
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            return ran.Next(start, next);
        }
    }
}
