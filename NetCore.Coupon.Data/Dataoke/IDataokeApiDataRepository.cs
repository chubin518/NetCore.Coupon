using System.Collections.Generic;
using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.Dataoke
{
    public interface IDataokeApiDataRepository
    {
        Task<List<TbkProductInfo>> All(ProductTopicRequest request);
        Task<List<TbkProductInfo>> Top100(ProductTopicRequest request);
        Task<List<TbkProductInfo>> XiaoLiang(ProductTopicRequest request);
        Task<List<TbkProductInfo>> QQ(ProductTopicRequest request);
    }
}