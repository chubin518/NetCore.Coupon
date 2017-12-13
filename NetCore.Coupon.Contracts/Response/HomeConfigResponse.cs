using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Coupon.Contracts.Response
{
  public  class HomeConfigResponse
    {
        public HomeConfigResponse()
        {
            this.Cats = new List<ConfigItem>();
            this.Favorites = new List<ConfigItem>();
            this.Banners = new List<BannerItem>();
        }
        public int v { get; set; }
        public string Title { get; set; }
        public List<BannerItem> Banners { get; set; }
        public List<ConfigItem> Cats { get; set; }
        public List<ConfigItem> Favorites { get; set; }
    }

    public class ConfigItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class BannerItem
    {
        public int ID { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }
    }
}
