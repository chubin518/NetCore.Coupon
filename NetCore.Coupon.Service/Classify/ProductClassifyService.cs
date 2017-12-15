using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Data.Qingtaoke;
using NetCore.Coupon.Data.TaokeJidi;
using NetCore.Coupon.Data.TaokeZhushou;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Coupon.Service
{
    public class ProductClassifyService : IProductClassifyService
    {
        private IQingtaokeApiDataRepository qingtaokeApiDataRepository;
        private ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository;
        private ITaokeJidiApiDataRepository taokeJidiApiDataRepository;

        public ProductClassifyService(
            IQingtaokeApiDataRepository qingtaokeApiDataRepository,
            ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository,
            ITaokeJidiApiDataRepository taokeJidiApiDataRepository)
        {
            this.qingtaokeApiDataRepository = qingtaokeApiDataRepository;
            this.taokeZhushouApiDataRepository = taokeZhushouApiDataRepository;
            this.taokeJidiApiDataRepository = taokeJidiApiDataRepository;
        }

        public async Task<ProductListResponse> List(ProductClassifyRequest request)
        {
            var qing = qingtaokeApiDataRepository.ItemList(request);
            var zhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
            {
                Cat = request.Cat,
                Sort = request.Sort,
                PageSize = request.PageSize,
                PageNo = request.PageNo
            });
            var jidi = taokeJidiApiDataRepository.Classify(request);
            return await Task.WhenAll(qing, zhushou, jidi).ContinueWith(task =>
            {
                List<TbkProductInfo> lstResult = new List<TbkProductInfo>();

                task.Result.ToList().ForEach(item =>
                {
                    if (item == null)
                        return;
                    lstResult.AddRange(item);
                });

                lstResult = lstResult.Distinct(new ProductIdComparer()).Sort(request.Sort).ToList();

                return new ProductListResponse()
                {
                    Datas = lstResult,
                    Total = lstResult.Count
                };
            });
        }
    }
}
