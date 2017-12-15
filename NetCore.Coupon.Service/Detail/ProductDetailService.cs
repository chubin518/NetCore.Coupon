using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Data.Taobao.Api;
using NetCore.Coupon.Data.Taobao.SDK;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Coupon.Service
{
    public class ProductDetailService : IProductDetailService
    {
        private ITaobaoApiDataRepository taobaoApiDataRepository;
        private ITaobaoSdkDataRepository taobaoSdkDataRepository;
        public ProductDetailService(ITaobaoApiDataRepository taobaoApiDataRepository,
            ITaobaoSdkDataRepository taobaoSdkDataRepository)
        {
            this.taobaoApiDataRepository = taobaoApiDataRepository;
            this.taobaoSdkDataRepository = taobaoSdkDataRepository;
        }

        public async Task<ProductDetailResponse> ProductDetail(ProductDetailRequest request)
        {
            ProductDetailResponse response = new ProductDetailResponse();
            var detailTask = taobaoApiDataRepository.GetProductDetail(request);
            var imageTask = taobaoApiDataRepository.GetProductDescx(request);

            await Task.WhenAll(detailTask, imageTask);

            ProductDetailData productItem = detailTask.Result;
            if (productItem == null || productItem.Item == null || productItem.Seller == null)
                return new ProductDetailResponse();

            return new ProductDetailResponse()
            {
                SPMC = productItem.Item.Title,
                SPLM = productItem.Item.CategoryId,
                DPMC = productItem.Seller.ShopName,
                DPZT = ToolUtils.GetThumbnail(productItem.Seller.ShopIcon),
                Images = productItem.Item.Images.Select(item => ToolUtils.GetThumbnail(item, size: "")).ToList(),
                Evaluates = productItem.Seller.Evaluates?.Select(item => new Evaluate() { Score = item.Score, Title = item.Title }).ToList(),
                Details = imageTask.Result
            };
        }

        public async Task<ProductListResponse> GetRecommendProducts(RecommendProductRequest request)
        {
            List<TbkProductInfo> lstResult = new List<TbkProductInfo>();
            List<TbkProductInfo> lstTaoSdk = taobaoSdkDataRepository.Search(new ProductSearchRequest() { CategoryIds = new List<long>() { request.CategoryId }, PageSize = 200, PageNo = 1 });
            if (null != lstTaoSdk)
            {
                lstResult.AddRange(lstTaoSdk);
            }
            List<TbkProductInfo> lstTaoApi = await taobaoApiDataRepository.CouponList(new CouponListRequest() { CategoryId = request.CategoryId });
            if (null != lstTaoApi)
            {
                lstResult.AddRange(lstTaoApi);
            }
            lstResult = lstResult.Distinct(new ProductIdComparer()).OrderByDescending(item => item.SPYXL).Skip(new Random().Next(1, 20)).Take(20).ToList();
            return new ProductListResponse()
            {
                Total = lstResult.Count,
                Datas = lstResult
            };
        }

        public string GetTaokouling(TaokoulingRequest request)
        {
            return taobaoSdkDataRepository.GetTaokouling(request);
        }
    }
}
