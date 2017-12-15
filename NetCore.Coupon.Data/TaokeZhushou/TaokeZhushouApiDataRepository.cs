using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Entity;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Coupon.Data.TaokeZhushou
{
    public class TaokeZhushouApiDataRepository : ITaokeZhushouApiDataRepository
    {
        public Task<List<TbkProductInfo>> All(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var dtkResponse = new HttpUtils().DoGet<TaokeZhushouDataResponse>(ConstantsUtils.TAOKEZHUSHOU_ALL, new Dictionary<string, string>() {
                        { "app_key", ConstantsUtils.TAOKEZHUSHOU_KEY },
                        { "page", request.PageNo.ToString() }
                    });
                if (null != dtkResponse && dtkResponse.Data != null)
                {
                    return GetProductList(dtkResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }
        public Task<List<TbkProductInfo>> TopHour(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var dtkResponse = new HttpUtils().DoGet<TaokeZhushouDataResponse>(ConstantsUtils.TAOKEZHUSHOU_TOPHOUR, new Dictionary<string, string>() {
                        { "app_key", ConstantsUtils.TAOKEZHUSHOU_KEY },
                        { "page", request.PageNo.ToString() }
                    });
                if (null != dtkResponse && dtkResponse.Data != null)
                {
                    return GetProductList(dtkResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        public Task<List<TbkProductInfo>> TopDay(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var dtkResponse = new HttpUtils().DoGet<TaokeZhushouDataResponse>(ConstantsUtils.TAOKEZHUSHOU_TOPDAY, new Dictionary<string, string>() {
                        { "app_key", ConstantsUtils.TAOKEZHUSHOU_KEY },
                        { "page", request.PageNo.ToString() }
                    });
                if (null != dtkResponse && dtkResponse.Data != null)
                {
                    return GetProductList(dtkResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        public Task<List<TbkProductInfo>> Search(ProductSearchRequest request)
        {
            return Task.Factory.StartNew(() =>
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
                if (request.Juhuasuan == 1)
                {
                    dicParams.Add("juhuasuan", "1");
                }
                if (request.Taoqianggou == 1)
                {
                    dicParams.Add("taoqianggou", "1");
                }
                if (request.Haitao == 1)
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
                if (request.Tmall == 1)
                {
                    dicParams.Add("is_tmall", request.Tmall.ToString());
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
                var dtkResponse = new HttpUtils().DoGet<TaokeZhushouDataResponse>(ConstantsUtils.TAOKEZHUSHOU_SEARCH, dicParams);
                if (null != dtkResponse && dtkResponse.Data != null)
                {
                    return GetProductList(dtkResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        private static List<TbkProductInfo> GetProductList(IEnumerable<TaokeZhushouItem> lstDatas)
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
