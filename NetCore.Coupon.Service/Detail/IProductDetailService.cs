using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using System.Threading.Tasks;

namespace NetCore.Coupon.Service
{
    public interface IProductDetailService
    {
        Task<ProductDetailResponse> ProductDetail(ProductDetailRequest request);
        Task<ProductListResponse> GetRecommendProducts(RecommendProductRequest request);
        string GetTaokouling(TaokoulingRequest request);
    }
}