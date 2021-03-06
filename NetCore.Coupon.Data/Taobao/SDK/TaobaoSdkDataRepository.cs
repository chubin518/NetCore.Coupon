﻿using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Top.Api;
using Top.Api.Request;
using System.Linq;
using Top.Api.Response;
using System.Threading.Tasks;
using Top.Api.Domain;

namespace NetCore.Coupon.Data.Taobao.SDK
{
    public class TaobaoSdkDataRepository : ITaobaoSdkDataRepository
    {
        ITopClient client;
        public TaobaoSdkDataRepository(ITopClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<TbkProductInfo>> Search(ProductSearchRequest request)
        {
            return Task.Factory.StartNew(() =>
            {
                List<TbkProductInfo> result = new List<TbkProductInfo>();
                TbkDgItemCouponGetRequest req = new TbkDgItemCouponGetRequest();
                req.AdzoneId = ConstantsUtils.ADZONE_ID;
                req.Platform = ConstantsUtils.PLATFORM;
                req.PageSize = request.PageSize;
                req.PageNo = request.PageNo;
                if (!string.IsNullOrWhiteSpace(request.KeyWord))
                {
                    req.Q = request.KeyWord;
                }
                if (request.CategoryIds != null && request.CategoryIds.Count >= 1)
                {
                    req.Cat = string.Join(",", request.CategoryIds);
                }
                var response = client.Execute(req);
                if (null != response && response.Results != null)
                {
                    response.Results.ForEach(item =>
                    {
                        Tuple<decimal, string> coupon = CouponInfoFomarter.GetCouponPrice(item.CouponInfo);
                        if (coupon.Item1 <= 0 || item.ZkFinalPrice.ToDecimal() - coupon.Item1 < 0)
                        {
                            return;
                        }
                        result.Add(new TbkProductInfo()
                        {
                            CP = coupon.Item2,
                            FP = (item.ZkFinalPrice.ToDouble() - coupon.Item2.ToDouble()).ToString(),
                            PTLX = item.UserType.ToString(),
                            SPID = item.NumIid,
                            SPMC = item.Title,
                            SPYHQTGLJ = item.CouponClickUrl,
                            SPYXL = item.Volume,
                            SPZT = ToolUtils.GetThumbnail(item.PictUrl),
                            SPJG = item.ZkFinalPrice,
                            Rate = item.CommissionRate.ToDouble(),
                            Desc = item.ItemDescription,
                        });
                    });
                }
                return result;
            });
        }

        public NTbkItem GetDetail(long id)
        {
            TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
            req.Fields = "cat_leaf_name,cat_name,click_url,commission_rate,coupon_price,coupon_start_fee,item_url,nick,num_iid,pict_url,provcity,reserve_price,seller_id,shop_title,small_images,title,tk_rate,user_type,volume,zk_final_price,zk_final_price_wap";
            req.Platform = 2L;
            req.NumIids = id.ToString();
            var response = client.Execute(req);
            if (null != response && response.Results != null && response.Results.Count >= 1)
            {
                return response.Results.FirstOrDefault();
            }
            return new NTbkItem();
        }

        /// <summary>
        /// 获取淘口令
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetTaokouling(TaokoulingRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Title)
                && !string.IsNullOrWhiteSpace(request.Url))
            {
                TbkTpwdCreateResponse response = client.Execute(new TbkTpwdCreateRequest()
                {
                    Logo = request.Logo,
                    Text = request.Title,
                    Url = ToolUtils.GetHttpsUrl(request.Url)
                });
                if (null != response && null != response.Data && !string.IsNullOrWhiteSpace(response.Data.Model))
                {
                    return response.Data.Model;
                }
            }
            return ConstantsUtils.DEFUALTTAOKOULING;
        }
    }
}
