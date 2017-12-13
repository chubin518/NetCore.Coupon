using NetCore.Coupon.Contracts.Entity;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Contracts.Domain;

namespace NetCore.Coupon.Data.Taobao.Api
{
    public class TaobaoApiDataRepository : ITaobaoApiDataRepository
    {
        /// <summary>
        /// 获取详情描述
        /// </summary>
        public ProductDetailData GetProductDetail(ProductDetailRequest request)
        {
            string reqData = Uri.EscapeDataString(JsonConvert.SerializeObject(new TaobaoProductDetailRequest()
            {
                ItemNumId = request.ProductId.ToString(),
                DetailV = "3.0.7",
                ExParams = JsonConvert.SerializeObject(new ProductDetailExParams()
                {
                    CountryCode = "CN",
                    Id = request.ProductId.ToString(),
                    ItemId = request.ProductId.ToString(),
                    PhoneType = "iPhone7,1",
                    Time = ToolUtils.GetUnixTime()
                })
            }));

            TaobaoProductDetailResponse response = new HttpUtils().DoGet<TaobaoProductDetailResponse>(ConstantsUtils.TAOBAO_API_PRODUCTDETAIL + "?data=" + reqData,
              null);
            if (null != response && null != response.Data)
            {
                return response.Data;
            }
            return new ProductDetailData();

        }

        /// <summary>
        /// 获取图文描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<string> GetProductDescx(ProductDetailRequest request)
        {
            ProductDescxResponse response = new HttpUtils().DoGet<ProductDescxResponse>(ConstantsUtils.TAOBAO_API_PRODUCTDESCX,
                new Dictionary<string, string>() {
                { "data","{'item_num_id':"+request.ProductId+"}"},
                { "type","json"},
                { "_",DateTime.Now.Ticks.ToString()}
            });
            if (null != response && response.Data != null && null != response.Data.Images)
            {
                return response.Data.Images.Select(item => ToolUtils.GetThumbnail(item)).ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<TbkProductInfo> CouponList(CouponListRequest request)
        {
            ProductCouponListResponse response = new HttpUtils().DoGet<ProductCouponListResponse>(ConstantsUtils.TAOBAO_API_COUPONLIST, new Dictionary<string, string>(){
                {"queryCount","500" },
                { "pid",ConstantsUtils.TAOBAO_API_PID},
                { "category",request.CategoryId.ToString()},
                { "_",DateTime.Now.Ticks.ToString() } });

            if (null != response && response.Result != null && response.Result.CouponList != null)
            {
                return response.Result.CouponList
                    .Select(coupon => new TbkProductInfo()
                    {
                        SPID = coupon.Item.ItemId,
                        CP = coupon.Amount.ToString(),
                        FP = (coupon.Item.DiscountPrice - coupon.Amount).NoZeroString(),
                        PTLX = coupon.Item.Tmall,
                        SPJG = coupon.Item.DiscountPrice.ToString(),
                        SPMC = coupon.Item.Title,
                        SPYHQTGLJ = ToolUtils.GetHttpsUrl(coupon.Item.ShareUrl),
                        SPYXL = coupon.Item.Biz30Day,
                        SPZT = ToolUtils.GetThumbnail(coupon.Item.PicUrl),
                    }).ToList();
            }
            return new List<TbkProductInfo>();
        }
    }
}
