using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Entity;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Coupon.Data.TaokeJidi
{
    public class TaokeJidiApiDataRepository : ITaokeJidiApiDataRepository
    {
        private static readonly IDictionary<int, int> CatMapping = new Dictionary<int, int>() {
            {0,0},{ 1,1},{2,2},{ 3,3},{ 4,10},{ 5,8},{ 6,5},{ 7,4},{ 8,7},{ 9,6},{ 10,9},{ 11,12}
        };

        public Task<List<TbkProductInfo>> Top100(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var tkjdResponse = new HttpUtils().DoGet<TaokeJidiDataResponse>(ConstantsUtils.TAOKEJIDI_TOP100, new Dictionary<string, string>() {
                        { "appkey", ConstantsUtils.TAOKEJIDI_KEY },
                        { "page", request.PageNo.ToString() }
                    });
                if (null != tkjdResponse && tkjdResponse.Data != null)
                {
                    return GetProductList(tkjdResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        public Task<List<TbkProductInfo>> PinPai(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var tkjdResponse = new HttpUtils().DoGet<TaokeJidiDataResponse>(ConstantsUtils.TAOKEJIDI_DAPAI, new Dictionary<string, string>() {
                        { "appkey", ConstantsUtils.TAOKEJIDI_KEY },
                        { "page", request.PageNo.ToString() }
                    });
                if (null != tkjdResponse && tkjdResponse.Data != null)
                {
                    return GetProductList(tkjdResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        public Task<List<TbkProductInfo>> BiPai(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var tkjdResponse = new HttpUtils().DoGet<TaokeJidiDataResponse>(ConstantsUtils.TAOKEJIDI_BIPAI, new Dictionary<string, string>() {
                        { "appkey", ConstantsUtils.TAOKEJIDI_KEY },
                        { "page", request.PageNo.ToString() }
                    });
                if (null != tkjdResponse && tkjdResponse.Data != null)
                {
                    return GetProductList(tkjdResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        public Task<List<TbkProductInfo>> Search(ProductSearchRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var tkjdResponse = new HttpUtils().DoGet<TaokeJidiDataResponse>(ConstantsUtils.TAOKEJIDI_SEARCH,
                    new Dictionary<string, string>()
                    {
                    { "appkey", ConstantsUtils.TAOKEJIDI_KEY },
                    { "page", request.PageNo.ToString() },
                    { "keyword",request.KeyWord}
                    });

                if (null != tkjdResponse && tkjdResponse.Data != null)
                {
                    return GetProductList(tkjdResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        public Task<List<TbkProductInfo>> Classify(ProductClassifyRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                int cat = 0;
                CatMapping.TryGetValue(request.Cat, out cat);
                if (cat <= 0)
                    return new List<TbkProductInfo>();
                var tkjdResponse = new HttpUtils().DoGet<TaokeJidiDataResponse>(ConstantsUtils.TAOKEJIDI_CATEGORY,
                    new Dictionary<string, string>()
                    {
                    { "appkey", ConstantsUtils.TAOKEJIDI_KEY },
                    { "cid",cat.ToString()},
                    { "page", request.PageNo.ToString() }
                    });

                if (null != tkjdResponse && tkjdResponse.Data != null)
                {
                    return GetProductList(tkjdResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        public Task<List<TbkProductInfo>> All(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var tkjdResponse = new HttpUtils().DoGet<TaokeJidiDataResponse>(ConstantsUtils.TAOKEJIDI_ALL,
                    new Dictionary<string, string>()
                    {
                    { "appkey", ConstantsUtils.TAOKEJIDI_KEY },
                    { "page", request.PageNo.ToString() }
                    });

                if (null != tkjdResponse && tkjdResponse.Data != null)
                {
                    return GetProductList(tkjdResponse.Data);
                }
                return new List<TbkProductInfo>();
            });
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="qtkResponse"></param>
        /// <returns></returns>
        private static List<TbkProductInfo> GetProductList(IEnumerable<TaokeJidiItem> lstDatas)
        {
            if (lstDatas == null || lstDatas.Count() <= 0)
            {
                return new List<TbkProductInfo>();
            }
            return lstDatas.Select(item => new TbkProductInfo
            {
                SPID = item.GoodsId.ToLong(),
                CP = item.PriceCoupons,
                FP = item.PriceAfterCoupons.ToDouble().NoZeroString(),
                PTLX = item.Src,
                SPJG = item.Price,
                SPMC = item.GoodsName,
                SPYHQTGLJ = ToolUtils.GetTGLink(item.QuanId, item.GoodsId, ConstantsUtils.TAOKEJIDI_PID),
                SPYXL = item.Sales.ToLong(),
                SPZT = ToolUtils.GetThumbnail(item.Pic),
                Desc = item.QuanGuidContent,
                Rate = item.Rate.ToDouble()
            }).ToList();
        }
    }
}
