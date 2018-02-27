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
using System.Threading.Tasks;

namespace NetCore.Coupon.Data.Taobao.Api
{
    public class TaobaoApiDataRepository : ITaobaoApiDataRepository
    {
        /// <summary>
        /// 获取详情描述6.0
        /// </summary>
        public Task<TbkDetailInfo> GetProductDetail(ProductDetailRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                return new TbkDetailInfo();
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
                int random = ToolUtils.GetRandomNum() % 3;
                string url = "";
                if (random == 0)
                {
                    url = $"{ConstantsUtils.TAOBAO_API_PRODUCTDETAIL}?id={request.ProductId}";
                    TaobaoProductDetailV5Response responseV5 = new HttpUtils().DoGet<TaobaoProductDetailV5Response>(url, null);
                    if (null != responseV5
                    && responseV5.Data != null
                    && responseV5.Data.ItemInfoModel != null
                    && responseV5.Data.Seller != null)
                    {
                        var lstImage = responseV5.Data.ItemInfoModel.PicsPath?.Select(item => ToolUtils.GetThumbnail(item, "")).ToList();
                        return new TbkDetailInfo()
                        {
                            Title = responseV5.Data.ItemInfoModel.Title,
                            CategoryId = responseV5.Data.ItemInfoModel.CategoryId,
                            Evaluates = responseV5.Data.Seller.EvaluateInfo.Select(item => new Evaluate()
                            {
                                Title = item.Title,
                                Score = item.Score
                            }).ToList(),
                            ShopIcon = ToolUtils.GetThumbnail(responseV5.Data.Seller.PicUrl, ""),
                            NumIid = responseV5.Data.ItemInfoModel.ItemId,
                            ShopName = responseV5.Data.Seller.ShopTitle,
                            Images = lstImage ?? new List<string>()
                        };
                    }
                }
                else
                {
                    if (random == 1)
                    {
                        url = $"{ConstantsUtils.TAOBAO_API_PRODUCTDETAIL1}?data={reqData}";
                    }
                    else
                    {
                        url = $"{ConstantsUtils.TAOBAO_API_PRODUCTDETAIL2}?data={reqData}";
                    }
                    TaobaoProductDetailResponse responseV6 = new HttpUtils().DoGet<TaobaoProductDetailResponse>(url, null);
                    if (null != responseV6
                        && null != responseV6.Data
                        && null != responseV6.Data.Item
                        && null != responseV6.Data.Seller)
                    {
                        var lstImage = responseV6.Data.Item.Images?.Select(item => ToolUtils.GetThumbnail(item, "")).ToList();
                        return new TbkDetailInfo()
                        {
                            NumIid = responseV6.Data.Item.ItemId,
                            Title = responseV6.Data.Item.Title,
                            CategoryId = responseV6.Data.Item.CategoryId,
                            Images = lstImage ?? new List<string>(),
                            ShopIcon = ToolUtils.GetThumbnail(responseV6.Data.Seller.ShopIcon, ""),
                            ShopName = responseV6.Data.Seller.ShopName,
                            Evaluates = responseV6.Data.Seller.Evaluates.Select(item => new Evaluate
                            {
                                Score = item.Score,
                                Title = item.Title
                            }).ToList()
                        };
                    }
                }
                return new TbkDetailInfo();
            });
        }

        /// <summary>
        /// 获取图文描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<string>> GetProductDescx(ProductDetailRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                ProductDescxResponse response = new HttpUtils().DoGet<ProductDescxResponse>(ConstantsUtils.TAOBAO_API_PRODUCTDESCX,
                    new Dictionary<string, string>() {
                { "data","{'item_num_id':"+request.ProductId+"}"},
                { "type","json"},
                { "_",DateTime.Now.Ticks.ToString()}
                });
                if (null != response && response.Data != null && null != response.Data.Images)
                {
                    return response.Data.Images.Select(item => ToolUtils.GetThumbnail(item, "")).ToList();
                }
                return new List<string>();
            });
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> CouponList(CouponListRequest request)
        {
            return Task.Factory.StartNew(() =>
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
            });
        }
    }
}
