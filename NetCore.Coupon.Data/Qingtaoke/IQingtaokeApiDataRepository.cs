using System.Collections.Generic;
using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.Qingtaoke
{
    public interface IQingtaokeApiDataRepository
    {
        Task<List<TbkProductInfo>> BaoKuan(ProductTopicRequest request);
        Task<List<TbkProductInfo>> ItemList(ProductClassifyRequest request);
        Task<List<TbkProductInfo>> Search(ProductSearchRequest request);
    }
}