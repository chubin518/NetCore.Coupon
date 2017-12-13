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
        public static readonly string TAOBAO_SDK_PID = "mm_124544751_34022481_121394480";
        public static readonly long ADZONE_ID = 121394480;

        public const string SERVER_URL = "https://eco.taobao.com/router/rest";
        public const string APP_KEY = "24556509";
        public const string APP_SECRET = "fa349a984749b791b11865a28caa9479";
        public const string FORMAT = "json";
        public const long PLATFORM = 2;

        public const string APP_KEY_BAICHUAN = "24531502";
        public const string APP_SECRET_BAICHUAN = "e207a9f548555af60ea05f6be6d3f450";

        #endregion

        #region TAOBAO_API
        public static readonly string TAOBAO_API_PID = "mm_124544751_34022481_121394480";

        public const string TAOBAO_API_COUPONLIST = "https://uland.taobao.com/cp/coupon_list";

        public const string TAOBAO_API_COUPONCHECK = "https://uland.taobao.com/cp/coupon";

        public const string TAOBAO_API_COUPONSEARCH = "http://pub.alimama.com/items/search.json";

        public const string TAOBAO_API_PRODUCTDESCX = "http://hws.m.taobao.com/cache/mtop.wdetail.getItemDescx/4.1/";

        public const string TAOBAO_API_PRODUCTDETAIL = "https://unszacs.m.taobao.com/gw/mtop.taobao.detail.getdetail/6.0";
        #endregion

        #region QINGTAOKE_API
        public static readonly string QINGTAOKE_PID = "mm_124544751_33334167_118612702";

        public static readonly string QINGTAOKE_APP_KEY = "5xXWWuuO";

        public static readonly string QINGTAOKE_URL_SEARCH = "http://openapi.qingtaoke.com/search?v=1.0&s_type=1";

        public static readonly string QINGTAOKE_URL_LIST = "http://openapi.qingtaoke.com/qingsoulist?v=1.0";

        public static readonly string QINGTAOKE_BAOKUAN = "http://openapi.qingtaoke.com/baokuan?v=1.0";

        #endregion

        #region DATAOKE_API
        public static readonly string DATAOKE_PID = "mm_124544751_33334167_118612702";

        public static readonly string DATAOKE_APP_KEY = "dw11okeue2";
        //全站领券API接口
        public static readonly string DATAOKE_URL_ALL = "http://api.dataoke.com/index.php?r=Port/index&type=total&v=2";
        //TOP100人气榜
        public static readonly string DATAOKE_URL_TOP = "http://api.dataoke.com/index.php?r=Port/index&type=top100&v=2";
        //实时销量榜
        public static readonly string DATAOKE_URL_PAOLIANG = "http://api.dataoke.com/index.php?r=Port/index&type=paoliang&v=2";

        #endregion

        #region TAOKEZHUSHOU_API
        public static readonly string TAOKEZHUSHOU_PID = "mm_124544751_33334167_118612702";
        public static readonly string TAOKEZHUSHOU_KEY = "3120c1fb7c4b04a8";
        /// <summary>
        /// 两小时销量榜API (app_key,page)
        /// </summary>
        public const string TAOKEZHUSHOU_URL_TOPHOUR = "http://api.taokezhushou.com/api/v1/top_hour";
        /// <summary>
        /// 全天销量榜API (app_key,page)
        /// </summary>
        public const string TAOKEZHUSHOU_URL_TOPDAY = "http://api.taokezhushou.com/api/v1/top_day";
        /// <summary>
        /// 商品搜索API
        /// </summary>
        public const string TAOKEZHUSHOU_URL_SEARCH = "http://api.taokezhushou.com/api/v1/search";
        #endregion
    }
}
