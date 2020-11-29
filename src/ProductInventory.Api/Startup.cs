using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductInventory.Domain;
using ProductInventory.Domain.Providers;
using ProductInventory.Domain.Queries.CalculateTrolley;
using ProductInventory.Domain.Queries.GetSortedProduct;
using ProductInventory.Domain.Services;

namespace ProductInventory.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<GetSortedProductQueryHandler>();
            services.AddTransient<CalculateTrolleyQueryHandler>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IShopperHistoryService, ShopperHistoryService>();
            services.AddTransient<RecommendationProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context => { await context.Response.WriteAsync("Woolies X Product Inventory v1"); });

                endpoints.MapControllers();
            });
        }
    }
}