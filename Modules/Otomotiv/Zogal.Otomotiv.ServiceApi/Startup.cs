using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zogal.Core;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.BusinessImplementation;

namespace Zogal.Otomotiv.ServiceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var keys = new[] { "connection.provider", "connection.connection_string", "show_sql", "dialect", "connection.driver_class" };
            var settings = keys.ToDictionary(k => k, v => Configuration.GetValue<string>($"zogal:oto:nh:{v}"));
            var assemblyIndex = 0;
            string assembly = null;
            var assemblies = new List<string>();
            while ((assembly = Configuration.GetValue<string>($"zogal:oto:nh:assembly:{assemblyIndex}")) != null)
            {
                assemblies.Add(assembly);
                assemblyIndex++;
            }
            services.AddScoped<INhHelper, NhHelper>(x => new NhHelper(settings, true, assemblies));

            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IOperationFacade, OperationFacade>();
            services.AddScoped<ICustomerFacade, CustomerFacade>();
            services.AddScoped<ISubscriberFacade, SubscriberFacade>();
            //services.AddSingleton<IRepository, RepositoryInMemory>();
            services.AddScoped<IRepository, NhRepository>();
            services.AddScoped<IReportFacade, ReportFacade>();
            services.AddScoped<ICounterFacade, CounterFacade>();
            services.AddScoped<IPriceMapFacade, PriceMapFacade>();
            services.AddScoped<IStockFacade, StockFacade>();
            services.AddScoped<ITipFacade, TipFacade>();
            services.AddScoped<IOperationTypeFacade, OperationTypeFacade>();
            services.AddScoped<IFileManagerFacade, FileManagerFacade>();
            services.AddScoped<ILookupFacade, LookupFacade>();
            services.AddCors(options =>
            {

              
                options.AddPolicy("test_",
                    builder =>
                    {

                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowCredentials();
                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("test_");
            app.UseMvc();
        }
    }
}
