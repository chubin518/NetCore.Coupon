using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Utility
{
    public static class ConstantsUtils
    {
        public const string CHARSET_UTF8 = "utf-8";
        public const string CONTENT_ENCODING_GZIP = "gzip";
        public const int READ_BUFFER_SIZE = 1024 * 4;

        public const string DEFUALTTAOKOULING = "";

        #region TAOBAO_SDK
        public const string TAOBAO_SDK_PID = "mm_124544751_34022481_121318545";
        public const long ADZONE_ID = 121394480;

        public const string SERVER_URL = "https://eco.taobao.com/router/rest";
        public const string APP_KEY = "24556509";
        public const string APP_SECRET = "fa349a984749b791b11865a28caa9479";
        public const string FORMAT = "json";
        public const long PLATFORM = 2;

        public const string APP_KEY_BAICHUAN = "24531502";
        public const string APP_SECRET_BAICHUAN = "e207a9f548555af60ea05f6be6d3f450";

        #endregion

        #region TAOBAO_API
        public const string TAOBAO_API_PID = "mm_124544751_34022481_121394480";

        public const string TAOBAO_API_COUPONLIST = "https://uland.taobao.com/cp/coupon_list";

        public const string TAOBAO_API_COUPONCHECK = "https://uland.taobao.com/cp/coupon";

        public const string TAOBAO_API_COUPONSEARCH = "https://pub.alimama.com/items/search.json";

        public const string TAOBAO_API_PRODUCTDESCX = "https://hws.m.taobao.com/cache/mtop.wdetail.getItemDescx/4.1/";

        public const string TAOBAO_API_PRODUCTDETAIL = "https://hws.m.taobao.com/cache/wdetail/5.0/";

        public const string TAOBAO_API_PRODUCTDETAIL1 = "https://unszacs.m.taobao.com/gw/mtop.taobao.detail.getdetail/6.0/";

        public const string TAOBAO_API_PRODUCTDETAIL2 = "https://acs.m.taobao.com/h5/mtop.taobao.detail.getdetail/6.0/";
        #endregion

        #region QINGTAOKE_API
        public const string QINGTAOKE_PID = "mm_124544751_34116440_122972603";

        public const string QINGTAOKE_APP_KEY = "5xXWWuuO";

        public const string QINGTAOKE_SEARCH = "http://openapi.qingtaoke.com/search?v=1.0&s_type=1";

        public const string QINGTAOKE_LIST = "http://openapi.qingtaoke.com/qingsoulist?v=1.0";

        public const string QINGTAOKE_BAOKUAN = "http://openapi.qingtaoke.com/baokuan?v=1.0";

        #endregion

        #region DATAOKE_API
        public const string DATAOKE_PID = "mm_124544751_34116440_121214962";

        public const string DATAOKE_APP_KEY = "dw11okeue2";
        //全站领券API接口
        public const string DATAOKE_ALL = "http://api.dataoke.com/index.php?r=Port/index&type=total&v=2";
        //TOP100人气榜
        public const string DATAOKE_TOP100 = "http://api.dataoke.com/index.php?r=Port/index&type=top100&v=2";
        //实时销量榜
        public const string DATAOKE_PAOLIANG = "http://api.dataoke.com/index.php?r=Port/index&type=paoliang&v=2";
        /// <summary>
        /// QQ群专用API接口
        /// </summary>
        public const string DATAOKE_QQ = "http://api.dataoke.com/index.php?r=goodsLink/qq&type=qq_quan&v=2";

        #endregion

        #region TAOKEZHUSHOU_API
        public const string TAOKEZHUSHOU_PID = "mm_124544751_31444007_115482389";
        public const string TAOKEZHUSHOU_KEY = "3120c1fb7c4b04a8";
        /// <summary>
        /// 两小时销量榜API (app_key,page)
        /// </summary>
        public const string TAOKEZHUSHOU_TOPHOUR = "http://api.taokezhushou.com/api/v1/top_hour";
        /// <summary>
        /// 全天销量榜API (app_key,page)
        /// </summary>
        public const string TAOKEZHUSHOU_TOPDAY = "http://api.taokezhushou.com/api/v1/top_day";
        /// <summary>
        /// 商品搜索API
        /// </summary>
        public const string TAOKEZHUSHOU_SEARCH = "http://api.taokezhushou.com/api/v1/search";
        /// <summary>
        /// 全网商品
        /// </summary>
        public const string TAOKEZHUSHOU_ALL = " http://api.taokezhushou.com/api/v1/all";
        #endregion

        #region TAOKEJIDI_API

        public const string TAOKEJIDI_PID = "mm_124544751_33334167_118808727";
        public const string TAOKEJIDI_KEY = "2e647d2b90d02e01cdcb56afa0e222b8";
        public const string TAOKEJIDI_TOP100 = "http://api.tkjidi.com/getGoodsLink?type=top100";
        public const string TAOKEJIDI_DAPAI = "http://api.tkjidi.com/getGoodsLink?type=dapai";
        public const string TAOKEJIDI_BIPAI = "http://api.tkjidi.com/getGoodsLink?type=bipai";
        public const string TAOKEJIDI_SEARCH = "http://api.tkjidi.com/getGoodsLink?type=so";
        public const string TAOKEJIDI_CATEGORY = "http://api.tkjidi.com/getGoodsLink?type=classify";
        public const string TAOKEJIDI_CLASSLIST = "http://api.tkjidi.com/classList?appkey=2e647d2b90d02e01cdcb56afa0e222b8";
        public const string TAOKEJIDI_ALL = "http://api.tkjidi.com/getGoodsLink?type=www_lingquan";
        public const string TAOKEJIDI_QQ = "http://api.tkjidi.com/getGoodsLink?type=qq_qun_ling";

        #endregion
    }
}
