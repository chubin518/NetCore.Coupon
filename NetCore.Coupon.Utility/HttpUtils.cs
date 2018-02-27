using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace NetCore.Coupon.Utility
{
    public class HttpUtils
    {
        private int _timeout = 10000;
        private int _readWriteTimeout = 30000;
        private bool _ignoreSSLCheck = true;
        private bool _disableWebProxy = false;

        /// <summary>
        /// 等待请求开始返回的超时时间
        /// </summary>
        public int Timeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }

        /// <summary>
        /// 等待读取数据完成的超时时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return this._readWriteTimeout; }
            set { this._readWriteTimeout = value; }
        }

        /// <summary>
        /// 是否忽略SSL检查
        /// </summary>
        public bool IgnoreSSLCheck
        {
            get { return this._ignoreSSLCheck; }
            set { this._ignoreSSLCheck = value; }
        }

        /// <summary>
        /// 是否禁用本地代理
        /// </summary>
        public bool DisableWebProxy
        {
            get { return this._disableWebProxy; }
            set { this._disableWebProxy = value; }
        }

        public string Host { get; set; }

        public string Accept { get; set; }

        public string User_Agent { get; set; }

        public string ContentType { get; set; }

        public string Referer { get; set; }

        public CookieCollection RspCookies { get; private set; }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> textParams)
        {
            return DoPost(url, textParams, null);
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, string> headerParams)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(textParams));
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = GetResponseEncoding(rsp);
            //var cookies = rsp.Cookies;
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(string url, IDictionary<string, string> textParams)
        {
            return DoGet(url, textParams, null);
        }
        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="textParams"></param>
        /// <returns></returns>
        public T DoGet<T>(string url, IDictionary<string, string> textParams)
        {
            string response = DoGet(url, textParams, null);
            if (!string.IsNullOrWhiteSpace(response))
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(response);
                }
                catch (Exception ex)
                {
                    LogUtils.Error(ex, url);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(string url, IDictionary<string, string> textParams, IDictionary<string, string> headerParams)
        {
            try
            {
                if (textParams != null && textParams.Count > 0)
                {
                    url = BuildRequestUrl(url, textParams);
                }

                HttpWebRequest req = GetWebRequest(url, "GET", headerParams);
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                Encoding encoding = GetResponseEncoding(rsp);
                return GetResponseAsString(rsp, encoding);
            }
            catch (Exception ex)
            {
                LogUtils.Error(ex, url);
            }
            return string.Empty;
        }

        /// <summary>
        /// 执行带body体的POST请求。
        /// </summary>
        /// <param name="url">请求地址，含URL参数</param>
        /// <param name="body">请求body体字节流</param>
        /// <param name="contentType">body内容类型</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, byte[] body, string contentType, IDictionary<string, string> headerParams)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = contentType;
            if (body != null)
            {
                System.IO.Stream reqStream = req.GetRequestStream();
                reqStream.Write(body, 0, body.Length);
                reqStream.Close();
            }
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = GetResponseEncoding(rsp);
            return GetResponseAsString(rsp, encoding);
        }

        public HttpWebRequest GetWebRequest(string url, string method, IDictionary<string, string> headerParams)
        {
            HttpWebRequest req = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                if (this._ignoreSSLCheck)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(TrustAllValidationCallback);
                }
                req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(url);
            }

            if (this._disableWebProxy)
            {
                req.Proxy = null;
            }

            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (string key in headerParams.Keys)
                {
                    req.Headers.Add(key, headerParams[key]);
                }
            }
            var servicePoint = ServicePointManager.FindServicePoint(new Uri(url));
            servicePoint.Expect100Continue = false;
            //req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            //req.KeepAlive = true;
            req.Timeout = this._timeout;
            req.ReadWriteTimeout = this._readWriteTimeout;
            if (!string.IsNullOrWhiteSpace(Host))
            {
                req.Host = Host;
            }
            if (!string.IsNullOrWhiteSpace(Accept))
            {
                req.Accept = Accept;
            }
            else
            {
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            }
            if (!string.IsNullOrWhiteSpace(User_Agent))
            {
                req.UserAgent = User_Agent;
            }
            else
            {
                req.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1";
            }
            if (!string.IsNullOrWhiteSpace(ContentType))
            {
                req.ContentType = ContentType;
            }
            if (!string.IsNullOrWhiteSpace(Referer))
            {
                req.Referer = Referer;
            }
            
            return req;
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                if (ConstantsUtils.CONTENT_ENCODING_GZIP.Equals(rsp.ContentEncoding, StringComparison.OrdinalIgnoreCase))
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }
                reader = new StreamReader(stream, encoding);
                RspCookies = rsp.Cookies;
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        /// <summary>
        /// 组装含参数的请求URL。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数映射</param>
        /// <returns>带参数的请求URL</returns>
        public static string BuildRequestUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                return BuildRequestUrl(url, BuildQuery(parameters));
            }
            return url;
        }

        /// <summary>
        /// 组装含参数的请求URL。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="queries">一个或多个经过URL编码后的请求参数串</param>
        /// <returns>带参数的请求URL</returns>
        public static string BuildRequestUrl(string url, params string[] queries)
        {
            if (queries == null || queries.Length == 0)
            {
                return url;
            }

            StringBuilder newUrl = new StringBuilder(url);
            bool hasQuery = url.Contains("?");
            bool hasPrepend = url.EndsWith("?") || url.EndsWith("&");

            foreach (string query in queries)
            {
                if (!string.IsNullOrEmpty(query))
                {
                    if (!hasPrepend)
                    {
                        if (hasQuery)
                        {
                            newUrl.Append("&");
                        }
                        else
                        {
                            newUrl.Append("?");
                            hasQuery = true;
                        }
                    }
                    newUrl.Append(query);
                    hasPrepend = false;
                }
            }
            return newUrl.ToString();
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder query = new StringBuilder();
            bool hasParam = false;

            foreach (KeyValuePair<string, string> kv in parameters)
            {
                string name = kv.Key;
                string value = kv.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                    {
                        query.Append("&");
                    }

                    query.Append(name);
                    query.Append("=");
                    query.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    hasParam = true;
                }
            }

            return query.ToString();
        }

        private Encoding GetResponseEncoding(HttpWebResponse rsp)
        {
            string charset = rsp.CharacterSet;
            if (string.IsNullOrEmpty(charset))
            {
                charset = ConstantsUtils.CHARSET_UTF8;
            }
            return Encoding.GetEncoding(charset);
        }

        private static bool TrustAllValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; // 忽略SSL证书检查
        }
    }
}
