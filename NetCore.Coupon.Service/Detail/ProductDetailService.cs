using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Data.Taobao.Api;
using NetCore.Coupon.Data.Taobao.SDK;
using NetCore.Coupon.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api.Domain;

namespace NetCore.Coupon.Service
{
    /// <summary>
    /// https://acs.m.taobao.com/h5/mtop.taobao.detail.getdetail/6.0/?data=%7B"itemNumId"%3A"10031645140"%7D&qq-pf-to=pcqq.group
    /// 
    /// </summary>
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
            var detailTask = taobaoApiDataRepository.GetProductDetail(request);
            var imageTask = taobaoApiDataRepository.GetProductDescx(request);

            return await Task.WhenAll(detailTask, imageTask).ContinueWith(tasks =>
              {
                  TbkDetailInfo apiDetail = detailTask.Result;
                  if (null != detailTask && !string.IsNullOrWhiteSpace(apiDetail.NumIid))
                  {
                      return new ProductDetailResponse()
                      {
                          Details = imageTask.Result,
                          SPLM = apiDetail.CategoryId,
                          SPMC = apiDetail.Title,
                          Images = apiDetail.Images,
                          Evaluates = apiDetail.Evaluates,
                          DPMC = apiDetail.ShopName,
                          DPZT = apiDetail.ShopIcon
                      };
                  }
                  else
                  {
                      NTbkItem sdkDetail = taobaoSdkDataRepository.GetDetail(request.ProductId);
                      if (sdkDetail == null)
                      {
                          return new ProductDetailResponse();
                      }
                      sdkDetail.SmallImages.Insert(0, sdkDetail.PictUrl);
                      return new ProductDetailResponse()
                      {
                          SPMC = sdkDetail.Title,
                          Images = sdkDetail.SmallImages?.Select(item => ToolUtils.GetThumbnail(item, size: ""))?.ToList(),
                          Details = imageTask.Result
                      };
                  }
              });
        }

        public async Task<ProductListResponse> GetRecommendProducts(RecommendProductRequest request)
        {
            var sdkTask = taobaoSdkDataRepository.Search(new ProductSearchRequest() { CategoryIds = new List<long>() { request.CategoryId }, PageSize = 200, PageNo = 1 });
            var apiSdk = taobaoApiDataRepository.CouponList(new CouponListRequest() { CategoryId = request.CategoryId });
            return await Task.WhenAll(sdkTask, apiSdk).ContinueWith(tasks =>
             {
                 List<TbkProductInfo> lstResult = new List<TbkProductInfo>();
                 tasks.Result.ToList().ForEach(item =>
                 {
                     lstResult.AddRange(item);
                 });
                 lstResult = lstResult.Distinct(new ProductIdComparer()).OrderByDescending(item => item.SPYXL).Skip(new Random().Next(1, 20)).Take(20).ToList();
                 return new ProductListResponse()
                 {
                     Total = lstResult.Count,
                     Datas = lstResult
                 };
             });

        }

        public string GetTaokouling(TaokoulingRequest request)
        {
            return taobaoSdkDataRepository.GetTaokouling(request);
        }
    }
}
