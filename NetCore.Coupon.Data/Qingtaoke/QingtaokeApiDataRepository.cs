using NetCore.Coupon.Contracts.Entity;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Top.Api.Response;
using System.Linq;
using NetCore.Coupon.Contracts.Domain;
using System.Threading.Tasks;

namespace NetCore.Coupon.Data.Qingtaoke
{
    public class QingtaokeApiDataRepository : IQingtaokeApiDataRepository
    {
        private static readonly IDictionary<int, int> CatMapping = new Dictionary<int, int>
        {
            {0,0},{ 1,10},{2,12},{ 3,11},{ 4,8},{ 5,6},{ 6,3},{ 7,2},{ 8,5},{ 9,4},{ 10,7},{ 11,9}
        };

        //page_size
        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> Search(ProductSearchRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var reqParams = new Dictionary<string, string>() {
                        { "app_key", ConstantsUtils.QINGTAOKE_APP_KEY },
                        { "page", request.PageNo.ToString() },
                        { "key_word", request.KeyWord}};
                switch (request.Sort)
                {
                    case 0:
                        reqParams.Add("sort", "1");
                        break;
                    case 1:
                        reqParams.Add("sort", "3");
                        break;
                    case 2:
                        reqParams.Add("sort", "4");
                        break;
                    case 3:
                        reqParams.Add("sort", "5");
                        break;
                }
                var qtkResponse = new HttpUtils().DoGet<QingtaokeSearchResponse>(ConstantsUtils.QINGTAOKE_SEARCH, reqParams);

                if (qtkResponse != null && qtkResponse.Data != null && qtkResponse.Data.List?.Length >= 1)
                {
                    return GetProductList(qtkResponse.Data.List);
                }
                return new List<TbkProductInfo>();
            });
        }

        /// <summary>
        /// 列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> ItemList(ProductClassifyRequest request)
        {
            return Task.Factory.StartNew(() =>
             {
                 var reqParams = new Dictionary<string, string>() {
                { "app_key", ConstantsUtils.QINGTAOKE_APP_KEY },
                { "page", request.PageNo.ToString() },};
                 if (request.PageSize == 100 || request.PageSize == 200)
                 {
                     reqParams.Add("page_size", request.PageSize.ToString());
                 }
                 if (request.Juhuasuan == 1)
                 {
                     reqParams.Add("is_ju", "1");
                 }
                 if (request.Taoqianggou == 1)
                 {
                     reqParams.Add("Is_tqg", "1");
                 }
                 if (request.MinPrice >= 0)
                 {
                     reqParams.Add("min_price", Math.Ceiling(request.MinPrice).ToString());
                 }
                 if (request.MaxPrice > 1)
                 {
                     reqParams.Add("max_price", Math.Ceiling(request.MaxPrice).ToString());
                 }
                 int cat = -1;
                 if (request.Cat >= 0 && CatMapping.TryGetValue(request.Cat, out cat))
                 {
                     if (cat > -1)
                     {
                         reqParams.Add("cat", cat.ToString());
                     }
                 }
                 if (request.TodayNew == 1)
                 {
                     reqParams.Add("new", "1");
                 }
                 switch (request.Sort)
                 {
                     case 0:
                         reqParams.Add("sort", "1");
                         break;
                     case 1:
                         reqParams.Add("sort", "3");
                         break;
                     case 2:
                         reqParams.Add("sort", "4");
                         break;
                     case 3:
                         reqParams.Add("sort", "5");
                         break;
                 }
                 QingtaokeSearchResponse qtkResponse = new HttpUtils().DoGet<QingtaokeSearchResponse>(ConstantsUtils.QINGTAOKE_LIST, reqParams);
                 if (qtkResponse != null && qtkResponse.Data != null && qtkResponse.Data.List?.Length >= 1)
                 {
                     return GetProductList(qtkResponse.Data.List);
                 }
                 return new List<TbkProductInfo>();
             });
        }

        /// <summary>
        /// 爆款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> BaoKuan(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                QingtaokeBaoKuanResponse qtkResponse = new HttpUtils().DoGet<QingtaokeBaoKuanResponse>(ConstantsUtils.QINGTAOKE_BAOKUAN, new Dictionary<string, string>() {
                { "app_key", ConstantsUtils.QINGTAOKE_APP_KEY } });

                if (qtkResponse != null && qtkResponse.Data != null && qtkResponse.Data?.Length >= 1)
                {
                    return GetProductList(qtkResponse.Data);
                }

                return new List<TbkProductInfo>();
            });
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="qtkResponse"></param>
        /// <returns></returns>
        private static List<TbkProductInfo> GetProductList(IEnumerable<QingtaokeItem> lstDatas)
        {
            if (lstDatas == null || lstDatas.Count() <= 0)
            {
                return new List<TbkProductInfo>();
            }
            return lstDatas.Select(item => new TbkProductInfo
            {
                SPID = item.GoodsId.ToLong(),
                CP = item.CouponPrice,
                FP = (item.GoodsPrice.ToDouble() - item.CouponPrice.ToDouble()).NoZeroString(),
                PTLX = item.IsTmall,
                SPJG = item.GoodsPrice,
                SPMC = item.GoodsTitle,
                SPYHQTGLJ = ToolUtils.GetTGLink(item.CouponId, item.GoodsId, ConstantsUtils.QINGTAOKE_PID),
                SPYXL = item.GoodsSales.ToLong(),
                SPZT = ToolUtils.GetThumbnail(item.GoodsPic),
                Desc = item.GoodsIntroduce,
                Rate = item.Commission.ToDouble()
            }).ToList();
        }
    }
}
