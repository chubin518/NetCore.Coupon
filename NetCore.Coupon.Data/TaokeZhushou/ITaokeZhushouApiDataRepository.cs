using System.Collections.Generic;
using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.TaokeZhushou
{
    public interface ITaokeZhushouApiDataRepository
    {
        Task<List<TbkProductInfo>> Search(ProductSearchRequest request);
        Task<List<TbkProductInfo>> TopDay(ProductTopicRequest request);
        Task<List<TbkProductInfo>> TopHour(ProductTopicRequest request);
        Task<List<TbkProductInfo>> All(ProductTopicRequest request);
    }
}