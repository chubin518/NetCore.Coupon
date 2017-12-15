using NetCore.Coupon.Contracts.Domain;
using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Contracts.Response;
using NetCore.Coupon.Data.Dataoke;
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
    public class ProductTopicService : IProductTopicService
    {
        private IQingtaokeApiDataRepository qingtaokeApiDataRepository;
        private IDataokeApiDataRepository dataokeApiDataRepository;
        private ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository;
        private ITaokeJidiApiDataRepository taokeJidiApiDataRepository;
        public ProductTopicService(
            IQingtaokeApiDataRepository qingtaokeApiDataRepository,
            IDataokeApiDataRepository dataokeApiDataRepository,
            ITaokeZhushouApiDataRepository taokeZhushouApiDataRepository,
            ITaokeJidiApiDataRepository taokeJidiApiDataRepository)
        {
            this.qingtaokeApiDataRepository = qingtaokeApiDataRepository;
            this.dataokeApiDataRepository = dataokeApiDataRepository;
            this.taokeZhushouApiDataRepository = taokeZhushouApiDataRepository;
            this.taokeJidiApiDataRepository = taokeJidiApiDataRepository;
        }

        public async Task<ProductListResponse> WeiXin(ProductTopicRequest request)
        {
            List<Task<List<TbkProductInfo>>> lstTask = new List<Task<List<TbkProductInfo>>>();

            switch (request.Type)
            {
                case 1:
                    lstTask.Add(dataokeApiDataRepository.Top100(request));
                    lstTask.Add(taokeJidiApiDataRepository.Top100(request));
                    break;
                case 2:
                    lstTask.Add(qingtaokeApiDataRepository.BaoKuan(request));
                    lstTask.Add(taokeZhushouApiDataRepository.TopHour(request));
                    break;
                case 3:
                    lstTask.Add(dataokeApiDataRepository.XiaoLiang(request));
                    lstTask.Add(taokeZhushouApiDataRepository.TopDay(request));
                    break;
                case 4:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        TodayNew = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        TodayNew = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 5:
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Juhuasuan = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        Juhuasuan = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    break;
                case 6:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        Taoqianggou = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Taoqianggou = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 7:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        MinPrice = 0,
                        MaxPrice = 9.9F,
                        PageNo = request.PageNo,
                        Sort = request.Sort,
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        MinPrice = 0,
                        MaxPrice = 9.9F,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 8:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        MinPrice = 10,
                        MaxPrice = 20,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        MinPrice = 10,
                        MaxPrice = 20,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 9:
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Haitao = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
            }
            return await GetProductList(request, lstTask, request.Type != 9);
        }

        public async Task<ProductListResponse> TeMai(ProductTopicRequest request)
        {
            List<Task<List<TbkProductInfo>>> lstTask = new List<Task<List<TbkProductInfo>>>();
            switch (request.Type)
            {
                case 1:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        MinPrice = 0,
                        MaxPrice = 9.9F,
                        PageNo = request.PageNo,
                        Sort = request.Sort,
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        MinPrice = 0,
                        MaxPrice = 9.9F,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 2:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        MinPrice = 10,
                        MaxPrice = 20,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        MinPrice = 10,
                        MaxPrice = 20,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
            }

            return await GetProductList(request, lstTask);
        }

        public async Task<ProductListResponse> SaleTop(ProductTopicRequest request)
        {
            List<Task<List<TbkProductInfo>>> lstTask = new List<Task<List<TbkProductInfo>>>();
            switch (request.Type)
            {
                case 1:
                    lstTask.Add(dataokeApiDataRepository.Top100(request));
                    lstTask.Add(taokeJidiApiDataRepository.Top100(request));
                    break;
                case 2:
                    lstTask.Add(dataokeApiDataRepository.XiaoLiang(request));
                    lstTask.Add(taokeZhushouApiDataRepository.TopHour(request));
                    break;
                case 3:
                    lstTask.Add(taokeJidiApiDataRepository.BiPai(request));
                    lstTask.Add(taokeZhushouApiDataRepository.TopDay(request));
                    break;
                case 4:
                    lstTask.Add(taokeJidiApiDataRepository.BiPai(request));
                    lstTask.Add(qingtaokeApiDataRepository.BaoKuan(request));
                    break;
                case 5:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        TodayNew = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        TodayNew = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
            }

            return await GetProductList(request, lstTask);
        }

        public async Task<ProductListResponse> Channel(ProductTopicRequest request)
        {
            List<Task<List<TbkProductInfo>>> lstTask = new List<Task<List<TbkProductInfo>>>();
            switch (request.Type)
            {
                case 1:
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Juhuasuan = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        Juhuasuan = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    break;
                case 2:
                    lstTask.Add(qingtaokeApiDataRepository.ItemList(new ProductClassifyRequest()
                    {
                        Taoqianggou = 1,
                        PageNo = request.PageNo,
                        Sort = request.Sort
                    }));
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Taoqianggou = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 3:
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Haitao = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 4:
                    lstTask.Add(taokeZhushouApiDataRepository.Search(new ProductSearchRequest()
                    {
                        Tmall = 1,
                        Sort = request.Sort,
                        PageNo = request.PageNo
                    }));
                    break;
                case 5:
                    lstTask.Add(taokeJidiApiDataRepository.PinPai(request));
                    break;
            }
            bool isfilter = !(request.Type == 3 || request.Type == 4 || request.Type == 5);
            return await GetProductList(request, lstTask, isfilter);
        }

        public async Task<ProductListResponse> All(ProductTopicRequest request)
        {
            List<Task<List<TbkProductInfo>>> lstTask = new List<Task<List<TbkProductInfo>>>();
            lstTask.Add(dataokeApiDataRepository.All(request));
            lstTask.Add(taokeZhushouApiDataRepository.All(request));
            lstTask.Add(taokeJidiApiDataRepository.All(request));
            return await GetProductList(request, lstTask);
        }

        private Task<ProductListResponse> GetProductList(ProductTopicRequest request, List<Task<List<TbkProductInfo>>> lstTask, bool isFilter = true)
        {
            if (lstTask.Count == 0)
            {
                return Task.FromResult(new ProductListResponse());
            }
            else
            {
                return Task.WhenAll(lstTask).ContinueWith(task =>
                {
                    List<TbkProductInfo> lstResult = new List<TbkProductInfo>();

                    task.Result.ToList().ForEach(item =>
                    {
                        if (item == null)
                            return;
                        lstResult.AddRange(item);
                    });

                    lstResult = lstResult.Distinct(new ProductIdComparer()).Sort(request.Sort, isFilter).ToList();

                    return new ProductListResponse()
                    {
                        Datas = lstResult,
                        Total = lstResult.Count
                    };
                });
            }
        }
    }
}
