using System.Collections.Generic;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.Taobao.SDK
{
    public interface ITaobaoSdkDataRepository
    {
        string GetTaokouling(TaokoulingRequest request);
        List<TbkProductInfo> Search(ProductSearchRequest request);
    }
}