using System.Collections.Generic;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.Dataoke
{
    public interface IDataokeApiDataRepository
    {
        /// <summary>
        /// 全站领券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TbkProductInfo> QuanZhan(ProductSearchRequest request);
        /// <summary>
        /// 实时销量榜
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TbkProductInfo> XiaoLiang(ProductSearchRequest request);
        /// <summary>
        /// TOP人气榜
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TbkProductInfo> Top100(ProductSearchRequest request);
    }
}