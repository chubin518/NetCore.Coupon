using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Entity;
using NetCore.Coupon.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NetCore.Coupon.Utility;
using System.Threading.Tasks;

namespace NetCore.Coupon.Data.Dataoke
{
    public class DataokeApiDataRepository : IDataokeApiDataRepository
    {
        /// <summary>
        /// TOP100人气榜
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> Top100(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
             {
                 if (request.PageNo >= 2)
                 {
                     return new List<TbkProductInfo>();
                 }
                 var dtkResponse = new HttpUtils().DoGet<DataokeSearchResponse>(ConstantsUtils.DATAOKE_TOP100, new Dictionary<string, string>() {
                        { "appkey", ConstantsUtils.DATAOKE_APP_KEY }
                     });
                 if (null != dtkResponse && dtkResponse.Result != null)
                 {
                     return GetProductList(dtkResponse.Result);
                 }
                 return new List<TbkProductInfo>();
             });
        }

        /// <summary>
        /// 实时销量榜
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> XiaoLiang(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                if (request.PageNo >= 2)
                {
                    return new List<TbkProductInfo>();
                }
                var dtkResponse = new HttpUtils().DoGet<DataokeSearchResponse>(ConstantsUtils.DATAOKE_PAOLIANG, new Dictionary<string, string>() {
                        { "appkey", ConstantsUtils.DATAOKE_APP_KEY }
                    });
                if (null != dtkResponse && dtkResponse.Result != null)
                {
                    return GetProductList(dtkResponse.Result);
                }
                return new List<TbkProductInfo>();
            });
        }

        /// <summary>
        /// 全站领券API接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> All(ProductTopicRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                var dtkResponse = new HttpUtils().DoGet<DataokeSearchResponse>(ConstantsUtils.DATAOKE_ALL, new Dictionary<string, string>() {
                        { "appkey", ConstantsUtils.DATAOKE_APP_KEY },
                        { "page", request.PageNo.ToString() }
                    });
                if (null != dtkResponse && dtkResponse.Result != null)
                {
                    return GetProductList(dtkResponse.Result);
                }
                return new List<TbkProductInfo>();
            });
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="qtkResponse"></param>
        /// <returns></returns>
        private static List<TbkProductInfo> GetProductList(IEnumerable<DaotaokeItem> lstDatas)
        {
            if (null != lstDatas && lstDatas.Any())
            {
                return lstDatas.Select(item => new TbkProductInfo
                {
                    CP = item.QuanPrice,
                    FP = item.Price.ToString(),
                    PTLX = item.IsTmall,
                    SPID = item.GoodsID.ToLong(),
                    SPJG = item.OrgPrice,
                    SPMC = item.Title,
                    SPYHQTGLJ = ToolUtils.GetTGLink(item.QuanId, item.GoodsID, ConstantsUtils.DATAOKE_PID),
                    SPYXL = item.SalesNum.ToLong(),
                    SPZT = ToolUtils.GetThumbnail(item.Pic),
                    Desc = item.Introduce,
                    Rate = item.Commission.ToDouble()
                }).ToList();
            }
            return new List<TbkProductInfo>();
        }
    }
}
