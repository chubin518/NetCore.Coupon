using System.Collections.Generic;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Data.Taobao.Api
{
    public interface ITaobaoApiDataRepository
    {
        List<TbkProductInfo> CouponList(CouponListRequest request);
        List<string> GetProductDescx(ProductDetailRequest request);
        ProductDetailData GetProductDetail(ProductDetailRequest request);
    }
}