using System.Collections.Generic;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.Qingtaoke
{
    public interface IQingtaokeApiDataRepository
    {
        /// <summary>
        /// 销量爆款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TbkProductInfo> BaoKuan(ProductSearchRequest request);
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TbkProductInfo> ItemList(ProductListRequest request);
        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<TbkProductInfo> Search(ProductSearchRequest request);
    }
}