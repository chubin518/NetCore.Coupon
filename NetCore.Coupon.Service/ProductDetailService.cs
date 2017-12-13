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

        public ProductDetailResponse ProductDetail(ProductDetailRequest request)
        {
            ProductDetailResponse response = new ProductDetailResponse();
            ProductDetailData detailData = taobaoApiDataRepository.GetProductDetail(request);
            if (detailData == null || detailData.Item == null)
            {
                return response;
            }
            List<string> lstImages = taobaoApiDataRepository.GetProductDescx(request) ?? new List<string>();
            return new ProductDetailResponse()
            {
                SPMC = detailData.Item.Title,
                SPLM = detailData.Item.CategoryId,
                DPMC = detailData.Seller.ShopName,
                DPZT = ToolUtils.GetThumbnail(detailData.Seller.ShopIcon),
                Images = detailData.Item.Images.Select(item => ToolUtils.GetThumbnail(item)).ToList(),
                Evaluates = detailData.Seller.Evaluates?.Select(item => new Evaluate() { Score = item.Score, Title = item.Title }).ToList(),
                Details = lstImages
            };
        }

        public ProductListResponse GetRecommendProducts(RecommendProductRequest request)
        {
            List<TbkProductInfo> lstResult = new List<TbkProductInfo>();
            List<TbkProductInfo> lstTaoSdk = taobaoSdkDataRepository.Search(new ProductSearchRequest() { CategoryIds = new List<long>() { request.CategoryId }, PageSize = 200, PageNo = 1 });
            if (null != lstTaoSdk)
            {
                lstResult.AddRange(lstTaoSdk);
            }
            List<TbkProductInfo> lstTaoApi = taobaoApiDataRepository.CouponList(new CouponListRequest() { CategoryId = request.CategoryId });
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
