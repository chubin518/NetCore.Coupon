using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Service
{
    public interface IProductListService
    {
        ProductListResponse Search(ProductSearchRequest request);

        ProductListResponse CatProducts(ProductListRequest request);

        ProductListResponse TopicProducts(ProductSpecialRequest request);
    }
}