using System.Collections.Generic;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using System.Threading.Tasks;
using Top.Api.Response;
using Top.Api.Domain;

namespace NetCore.Coupon.Data.Taobao.SDK
{
    public interface ITaobaoSdkDataRepository
    {
        string GetTaokouling(TaokoulingRequest request);
        Task<List<TbkProductInfo>> Search(ProductSearchRequest request);
        NTbkItem GetDetail(long id);
    }
}