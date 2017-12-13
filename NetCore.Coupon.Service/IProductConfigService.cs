using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Service
{
    public interface IProductConfigService
    {
        ConfigIndexResponse HomeConfig(string k = "");
    }
}