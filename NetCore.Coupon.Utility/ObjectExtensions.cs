using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Utility
{
    public static class ObjectExtensions
    {
        public static string NoZeroString(this decimal input)
        {
            return input.ToString("#0.######");
        }

        public static string NoZeroString(this double input)
        {
            return input.ToString("#0.######");
        }

        public static long ToLong(this object source, long def = 0)
        {
            if (null == source)
            {
                return 0;
            }
            long result = 0;
            if (long.TryParse(source.ToString(), out result))
                return result;
            else
                return def;
        }

        public static int ToInt32(this object source, int def = 0)
        {
            if (null == source)
            {
                return 0;
            }
            int result = 0;
            if (int.TryParse(source.ToString(), out result))
                return result;
            else
                return def;
        }

        public static decimal ToDecimal(this object source, decimal def = 0)
        {
            if (null == source)
            {
                return 0;
            }
            decimal result = 0;
            if (decimal.TryParse(source.ToString(), out result))
                return result;
            else
                return def;
        }
        public static double ToDouble(this string input, double def = 0)
        {
            double result = 0;
            if (double.TryParse(input, out result))
            {
                return result;
            }
            return def;
        }

        /// <summary>
        /// 乱序集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            List<T> newList = new List<T>();
            if (source == null)
            {
                return newList;
            }

            Random random = new Random();
            foreach (T item in source)
            {
                newList.Insert(random.Next(newList.Count / 2, newList.Count), item);
            }
            return newList;
        }
    }
}
