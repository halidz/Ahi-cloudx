using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zogal.Core;
using Zogal.SystemManagement.Business;
using Zogal.SystemManagement.BusinessImplementation;

namespace Zogal.SystemManagement.ServiceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var keys = new[] { "connection.provider", "connection.connection_string", "show_sql", "dialect", "connection.driver_class" };
            var settings = keys.ToDictionary(k => k, v => Configuration.GetValue<string>($"zogal:sys:nh:{v}"));
            var assemblyIndex = 0;
            string assembly = null;
            var assemblies = new List<string>();
            while ((assembly = Configuration.GetValue<string>($"zogal:sys:nh:assembly:{assemblyIndex}"))!=null)
            {
                assemblies.Add(assembly);
                assemblyIndex++;
            }
            services.AddScoped<INhHelper, NhHelper>(x =>new NhHelper(settings, true, assemblies));
            services.AddScoped<IRepository, NhRepository>();
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddScoped<IRoleFacade, RoleFacade>();
            services.AddScoped<ITokenFacade, TokenFacade>();
            services.AddScoped<IMessageFacade, MessageFacade>();
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
