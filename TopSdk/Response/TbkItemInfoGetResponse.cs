using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TbkItemInfoGetResponse.
    /// </summary>
    public class TbkItemInfoGetResponse : TopResponse
    {
        /// <summary>
        /// 淘宝客商品
        /// </summary>
        [XmlArray("results")]
        [XmlArrayItem("n_tbk_item")]
        public List<Top.Api.Domain.NTbkItem> Results { get; set; }

    }
}
