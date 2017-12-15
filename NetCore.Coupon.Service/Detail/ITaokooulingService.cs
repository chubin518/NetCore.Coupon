using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Service
{
    public interface ITaokooulingService
    {
        string GetTaokouling(TaokoulingRequest request);
    }
}