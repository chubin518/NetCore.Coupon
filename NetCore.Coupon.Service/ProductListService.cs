using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Data.Qingtaoke;
using NetCore.Coupon.Data.Taobao.Api;
using NetCore.Coupon.Data.Taobao.SDK;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NetCore.Coupon.Data.Dataoke;
using NetCore.Coupon.Data.TaokeZhushou;
using NetCore.Coupon.Utility;

namespace NetCore.Coupon.Service
{
    public class ProductListService : IProductListService
    {
        private ITaobaoSdkDataRepository sdkDataRepository;
        private IQingtaokeApiDataRepository qingtaokeApiDataRepository;
        private ITaobaoApiDataRepository taobaoApiDataRepository;
        private IDataokeApiDataRepository dataokeApiDataRepository;
        private ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository;
        public ProductListService(ITaobaoSdkDataRepository sdkDataRepository,
            IQingtaokeApiDataRepository qingtaokeApiDataRepository,
            ITaobaoApiDataRepository taobaoApiDataRepository,
            IDataokeApiDataRepository dataokeApiDataRepository,
            ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository)
        {
            this.sdkDataRepository = sdkDataRepository;
            this.qingtaokeApiDataRepository = qingtaokeApiDataRepository;
            this.taobaoApiDataRepository = taobaoApiDataRepository;
            this.dataokeApiDataRepository = dataokeApiDataRepository;
            this.taokeZhushouApiDataRepository = taokeZhushouApiDataRepository;
        }

        public ProductListResponse Search(ProductSearchRequest request)
        {
            List<TbkProductInfo> lstQing = qingtaokeApiDataRepository.Search(request);
            List<TbkProductInfo> lstZhushou = taokeZhushouApiDataRepository.Search(request);

            List<TbkProductInfo> lstResult = new List<TbkProductInfo>();
            if (null != lstQing)
            {
                lstResult.AddRange(lstQing);
            }
            if (null != lstZhushou)
            {
                lstResult.AddRange(lstZhushou);
            }
            lstResult = lstResult.Distinct(new ProductIdComparer()).Sort(request.Sort).ToList();
            if (null == lstResult || lstResult.Count <= 0)
            {
                List<TbkProductInfo> lstTao = sdkDataRepository.Search(request);
                if (null != lstTao)
                {
                    lstResult.AddRange(lstTao);
                }
            }
            return new ProductListResponse()
            {
                Total = lstResult.Count,
                Datas = lstResult
            };
        }

        public ProductListResponse CatProducts(ProductListRequest request)
        {
            List<TbkProductInfo> lstQing = qingtaokeApiDataRepository.ItemList(request);
            List<TbkProductInfo> lstZhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
            {
                Cat = request.Cat,
                Sort = request.Sort,
                PageSize = request.PageSize,
                PageNo = request.PageNo
            });
            List<TbkProductInfo> lstResult = new List<TbkProductInfo>();
            if (null != lstQing)
            {
                lstResult.AddRange(lstQing);
            }
            if (null != lstZhushou)
            {
                lstResult.AddRange(lstZhushou);
            }
            lstResult = lstResult.Distinct(new ProductIdComparer()).Sort(request.Sort).ToList();
            return new ProductListResponse()
            {
                Total = lstResult.Count,
                Datas = lstResult
            };
        }

        public ProductListResponse TopicProducts(ProductSpecialRequest request)
        {
            List<TbkProductInfo> qingtaoke = null, dataoke = null, taokezhushou = null;

            switch (request.Type)
            {
                case 1:
                    dataoke = dataokeApiDataRepository.Top100(new ProductSearchRequest());
                    break;
                case 2:
                    qingtaoke = qingtaokeApiDataRepository.BaoKuan(new ProductSearchRequest());
                    taokezhushou = taokeZhushouApiDataRepository.TopHour(new ProductSpecialRequest());
                    break;
                case 3:
                    dataoke = dataokeApiDataRepository.XiaoLiang(new ProductSearchRequest());
                    taokezhushou = taokeZhushouApiDataRepository.TopDay(new ProductSpecialRequest());
                    break;
                case 4:
                    qingtaoke = qingtaokeApiDataRepository.ItemList(new ProductListRequest()
                    {
                        TodayNew = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    });
                    taokezhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        TodayNew = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    });
                    break;
                case 5:
                    taokezhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Juhuasuan = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    });
                    qingtaoke = qingtaokeApiDataRepository.ItemList(new ProductListRequest()
                    {
                        Juhuasuan = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    });
                    break;
                case 6:
                    qingtaoke = qingtaokeApiDataRepository.ItemList(new ProductListRequest()
                    {
                        Taoqianggou = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    });
                    taokezhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Taoqianggou = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    });
                    break;
                case 7:
                    qingtaoke = qingtaokeApiDataRepository.ItemList(new ProductListRequest()
                    {
                        MinPrice = 0,
                        MaxPrice = 9.9F,
                        PageNo = request.PageNo,
                        Sort = request.Sort,
                    });
                    taokezhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        MinPrice = 0,
                        MaxPrice = 9.9F,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    });
                    break;
                case 8:
                    qingtaoke = qingtaokeApiDataRepository.ItemList(new ProductListRequest()
                    {
                        MinPrice = 10,
                        MaxPrice = 20,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    });
                    taokezhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        MinPrice = 10,
                        MaxPrice = 20,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    });
                    break;
                case 9:
                    taokezhushou = taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Haitao = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    });
                    break;
            }

            List<TbkProductInfo> lstResult = new List<TbkProductInfo>();

            if (null != qingtaoke)
            {
                lstResult.AddRange(qingtaoke);
            }
            if (null != taokezhushou)
            {
                lstResult.AddRange(taokezhushou);
            }
            if (null != dataoke)
            {
                lstResult.AddRange(dataoke);
            }

            lstResult = lstResult.Distinct(new ProductIdComparer()).Sort(request.Sort).ToList();
            return new ProductListResponse()
            {
                Datas = lstResult,
                Total = lstResult.Count
            };
        }

    }

    public static class ProductSortExtensions
    {
        public static IEnumerable<TbkProductInfo> Sort(this IEnumerable<TbkProductInfo> source, int sort)
        {
            IEnumerable<TbkProductInfo> result = new List<TbkProductInfo>();
            if (source == null)
                return result;
            switch (sort)
            {
                case 1:
                    result = source.OrderByDescending(item => item.SPYXL);
                    break;
                case 2:
                    result = source.OrderBy(item => item.FP.ToDouble());
                    break;
                case 3:
                    result = source.OrderByDescending(item => item.Rate).ThenByDescending(item => (item.CP.ToDouble() / 1000) * item.SPYXL);
                    break;
                default:
                    result = source.OrderByDescending(item => (item.CP.ToDouble() / 1000) * item.SPYXL * (item.Rate / 1000)).ThenBy(item => item.FP.ToDouble()).Shuffle();
                    break;
            }
            return result;
        }
    }
}
