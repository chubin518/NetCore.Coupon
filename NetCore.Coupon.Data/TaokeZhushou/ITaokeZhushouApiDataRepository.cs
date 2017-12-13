using System.Collections.Generic;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.TaokeZhushou
{
    public interface ITaokeZhushouApiDataRepository
    {
        List<TbkProductInfo> Search(ProductSearchRequest request);
        List<TbkProductInfo> TopDay(ProductSpecialRequest request);
        List<TbkProductInfo> TopHour(ProductSpecialRequest request);
    }
}