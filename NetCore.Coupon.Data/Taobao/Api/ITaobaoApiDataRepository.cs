using System.Collections.Generic;
using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Data.Taobao.Api
{
    public interface ITaobaoApiDataRepository
    {
        Task<List<TbkProductInfo>> CouponList(CouponListRequest request);
        Task<List<string>> GetProductDescx(ProductDetailRequest request);
        Task<ProductDetailData> GetProductDetail(ProductDetailRequest request);
    }
}