using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Entity;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace NetCore.Coupon.Data.TaokeZhushou
{
    public class TaokeZhushouApiDataRepository : ITaokeZhushouApiDataRepository
    {
        public List<TbkProductInfo> TopHour(ProductSpecialRequest request)
        {
            var dtkResponse = new HttpUtils().DoGet<TaokeZhushouDataResponse>(ConstantsUtils.TAOKEZHUSHOU_URL_TOPHOUR, new Dictionary<string, string>() {
                        { "app_key", ConstantsUtils.TAOKEZHUSHOU_KEY },
                        { "page", request.PageNo.ToString() }
                    });
            if (null != dtkResponse && dtkResponse.Data != null)
            {
                return GetProductList(dtkResponse.Data);
            }
            return new List<TbkProductInfo>();
        }

        public List<TbkProductInfo> TopDay(ProductSpecialRequest request)
        {
            var dtkResponse = new HttpUtils().DoGet<TaokeZhushouDataResponse>(ConstantsUtils.TAOKEZHUSHOU_URL_TOPDAY, new Dictionary<string, string>() {
                        { "app_key", ConstantsUtils.TAOKEZHUSHOU_KEY },
                        { "page", request.PageNo.ToString() }
                    });
            if (null != dtkResponse && dtkResponse.Data != null)
            {
                return GetProductList(dtkResponse.Data);
            }
            return new List<TbkProductInfo>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<TbkProductInfo> Search(ProductSearchRequest request)
        {
            var dicParams = new Dictionary<string, string>() {
                        { "app_key", ConstantsUtils.TAOKEZHUSHOU_KEY },
                        { "page", request.PageNo.ToString() }
                    };
            if (!string.IsNullOrWhiteSpace(request.KeyWord))
            {
                dicParams.Add("q", request.KeyWord);
            }
            if (request.TodayNew >= 1)
            {
                dicParams.Add("today", "1");
            }
            if (request.Juhuasuan >= 1)
            {
                dicParams.Add("juhuasuan", "1");
            }
            if (request.Taoqianggou >= 1)
            {
                dicParams.Add("taoqianggou", "1");
            }
            if (request.Haitao >= 1)
            {
                dicParams.Add("haitao", "1");
            }
            if (request.MinPrice > 0)
            {
                dicParams.Add("price_start", request.MinPrice.ToString());
            }
            if (request.MaxPrice > 0)
            {
                dicParams.Add("price_end", request.MaxPrice.ToString());
            }
            if (request.Cat >= 0)
            {
                dicParams.Add("cate_id", request.Cat.ToString());
            }
            switch (request.Sort)
            {
                case 0:
                    dicParams.Add("sort", "one_day_sale_num");
                    break;
                case 1:
                    dicParams.Add("sort", "sale_num");
                    break;
                case 2:
                    dicParams.Add("sort", "price_asc");
                    break;
                case 3:
                    dicParams.Add("sort", "commission_rate_desc");
                    break;
            }
            var dtkResponse = new HttpUtils().DoGet<TaokeZhushouDataResponse>(ConstantsUtils.TAOKEZHUSHOU_URL_SEARCH, dicParams);
            if (null != dtkResponse && dtkResponse.Data != null)
            {
                return GetProductList(dtkResponse.Data);
            }
            return new List<TbkProductInfo>();
        }

        private static List<TbkProductInfo> GetProductList(IEnumerable<ZhushouProductItem> lstDatas)
        {
            if (lstDatas == null || lstDatas.Count() <= 0)
            {
                return new List<TbkProductInfo>();
            }
            return lstDatas.Select(item => new TbkProductInfo()
            {
                CP = item.CouponAmount,
                Desc = item.GoodsIntro,
                FP = (item.GoodsPrice.ToDouble() - item.CouponAmount.ToDouble()).NoZeroString(),
                PTLX = item.IsTmall.ToString(),
                SPID = item.GoodsId.ToLong(),
                SPJG = item.GoodsPrice,
                SPMC = item.GoodsTitle,
                SPYHQTGLJ = ToolUtils.GetTGLink(item.CouponId, item.GoodsId, ConstantsUtils.TAOKEZHUSHOU_PID),
                SPYXL = item.GoodsSaleNum,
                SPZT = ToolUtils.GetThumbnail(item.GoodsPic),
                Rate = item.CommissionRate
            }).ToList();
        }
    }
}
