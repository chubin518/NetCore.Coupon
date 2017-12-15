using NetCore.Coupon.Contracts.Request;
using NetCore.Coupon.Data.Taobao.SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Service
{
    public class TaokooulingService : ITaokooulingService
    {
        private ITaobaoSdkDataRepository sdkDataRepository;
        public TaokooulingService(ITaobaoSdkDataRepository sdkDataRepository)
        {
            this.sdkDataRepository = sdkDataRepository;
        }

        public string GetTaokouling(TaokoulingRequest request)
        {
            return sdkDataRepository.GetTaokouling(request);
        }
    }
}
