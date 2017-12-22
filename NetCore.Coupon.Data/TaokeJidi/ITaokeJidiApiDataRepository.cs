using System.Collections.Generic;
using System.Threading.Tasks;
using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;

namespace NetCore.Coupon.Data.TaokeJidi
{
    public interface ITaokeJidiApiDataRepository
    {
        Task<List<TbkProductInfo>> BiPai(ProductTopicRequest request);
        Task<List<TbkProductInfo>> Classify(ProductClassifyRequest request);
        Task<List<TbkProductInfo>> PinPai(ProductTopicRequest request);
        Task<List<TbkProductInfo>> Search(ProductSearchRequest request);
        Task<List<TbkProductInfo>> Top100(ProductTopicRequest request);
        Task<List<TbkProductInfo>> All(ProductTopicRequest request);
        Task<List<TbkProductInfo>> QQ(ProductTopicRequest request);
    }
}