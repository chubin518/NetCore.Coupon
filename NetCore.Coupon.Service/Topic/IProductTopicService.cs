using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Service
{
    public interface IProductTopicService
    {
        Task<ProductListResponse> Channel(ProductTopicRequest request);
        Task<ProductListResponse> SaleTop(ProductTopicRequest request);
        Task<ProductListResponse> TeMai(ProductTopicRequest request);
        Task<ProductListResponse> WeiXin(ProductTopicRequest request);
        Task<ProductListResponse> All(ProductTopicRequest request);
    }
}