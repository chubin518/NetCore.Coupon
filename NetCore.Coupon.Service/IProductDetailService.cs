using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Service
{
    public interface IProductDetailService
    {
        ProductDetailResponse ProductDetail(ProductDetailRequest request);
        ProductListResponse GetRecommendProducts(RecommendProductRequest request);
        string GetTaokouling(TaokoulingRequest request);
    }
}