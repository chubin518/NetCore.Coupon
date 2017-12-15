using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCore.Coupon.Utility;
using System.IO;
using NetCore.Coupon.Service;
using NetCore.Coupon.Data.Taobao.Api;
using NetCore.Coupon.Data.Taobao.SDK;
using Top.Api;
using NetCore.Coupon.API.Extensions;
using NetCore.Coupon.Data.Qingtaoke;
using NetCore.Coupon.Data.Dataoke;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NetCore.Coupon.Data.TaokeZhushou;
using NetCore.Coupon.Data.TaokeJidi;

namespace NetCore.Coupon.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            LogUtils.Init(Directory.GetCurrentDirectory());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(cfg =>
            {
                cfg.Filters.Add<ApiErrorHandleAttribute>();
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddResponseCompression();
            services.AddResponseCaching();
            services.AddMemoryCache();

            services.AddSingleton(Configuration);

            services.AddScoped<ITopClient>(d => new DefaultTopClient(ConstantsUtils.SERVER_URL, ConstantsUtils.APP_KEY, ConstantsUtils.APP_SECRET));

            services.AddScoped<ITaobaoApiDataRepository, TaobaoApiDataRepository>();
            services.AddScoped<ITaobaoSdkDataRepository, TaobaoSdkDataRepository>();
            services.AddScoped<IQingtaokeApiDataRepository, QingtaokeApiDataRepository>();
            services.AddScoped<IDataokeApiDataRepository, DataokeApiDataRepository>();
            services.AddScoped<ITaokeZhushouApiDataRepository, TaokeZhushouApiDataRepository>();
            services.AddScoped<ITaokeJidiApiDataRepository, TaokeJidiApiDataRepository>();

            services.AddScoped<ITaokooulingService, TaokooulingService>();
            services.AddScoped<IProductDetailService, ProductDetailService>();
            services.AddScoped<IProductTopicService, ProductTopicService>();
            services.AddScoped<IProductConfigService, ProductConfigService>();
            services.AddScoped<IProductSearchService, ProductSearchService>();
            services.AddScoped<IProductClassifyService, ProductClassifyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"images")),
                RequestPath = new PathString("/images")
            });
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseMvc();
        }
    }
}
