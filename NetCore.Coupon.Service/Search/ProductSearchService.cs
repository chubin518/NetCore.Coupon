using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Data.Qingtaoke;
using NetCore.Coupon.Data.Taobao.SDK;
using NetCore.Coupon.Data.TaokeJidi;
using NetCore.Coupon.Data.TaokeZhushou;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NetCore.Coupon.Contracts.Response;

namespace NetCore.Coupon.Service
{
    public class ProductSearchService : IProductSearchService
    {
        private ITaobaoSdkDataRepository sdkDataRepository;
        private IQingtaokeApiDataRepository qingtaokeApiDataRepository;
        private ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository;
        private ITaokeJidiApiDataRepository taokeJidiApiDataRepository;

        public ProductSearchService(ITaobaoSdkDataRepository sdkDataRepository,
            IQingtaokeApiDataRepository qingtaokeApiDataRepository,
            ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository,
            ITaokeJidiApiDataRepository taokeJidiApiDataRepository)
        {
            this.sdkDataRepository = sdkDataRepository;
            this.qingtaokeApiDataRepository = qingtaokeApiDataRepository;
            this.taokeZhushouApiDataRepository = taokeZhushouApiDataRepository;
            this.taokeJidiApiDataRepository = taokeJidiApiDataRepository;
        }

        public async Task<ProductListResponse> List(ProductSearchRequest request)
        {
            var qing = qingtaokeApiDataRepository.Search(request);
            var zhushou = taokeZhushouApiDataRepository.Search(request);
            var jidi = taokeJidiApiDataRepository.Search(request);

            return await Task.WhenAll(qing, zhushou, jidi).ContinueWith(task =>
             {
                 List<TbkProductInfo> lstResult = new List<TbkProductInfo>();

                 task.Result.ToList().ForEach(item =>
                 {
                     if (item == null)
                         return;
                     lstResult.AddRange(item);
                 });

                 lstResult = lstResult.Distinct(new ProductIdComparer()).Sort(request.Sort, false).ToList();

                 if (lstResult == null || lstResult.Count <= 0)
                 {
                     List<TbkProductInfo> lstTao = sdkDataRepository.Search(request);
                     if (null != lstTao)
                     {
                         lstResult.AddRange(lstTao);
                     }
                 }

                 return new ProductListResponse()
                 {
                     Datas = lstResult,
                     Total = lstResult.Count
                 };
             });
        }
    }
}
