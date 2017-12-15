using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Service
{
    public interface IProductClassifyService
    {
        Task<ProductListResponse> List(ProductClassifyRequest request);
    }
}