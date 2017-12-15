using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Service
{
    public interface IProductSearchService
    {
        Task<ProductListResponse> List(ProductSearchRequest request);
    }
}