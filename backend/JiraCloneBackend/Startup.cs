using System;
using System.Collections.Generic;
using System.Linq;

using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JiraCloneBackend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace JiraCloneBackend
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
            services.AddControllers()
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.IgnoreNullValues = true;
                //    options.JsonSerializerOptions.ReferenceHandling = ReferenceHandling.Preserve;
                //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                //    options.JsonSerializerOptions.MaxDepth = 256;
                //});
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
                    
                
            
            
            //services.AddOptions()
            services.AddDbContextPool<JiraContext>(options =>
                //Local db for now
                options.UseMySql("Server=192.168.1.213;Database=jira_backend;User=jirauser;Password=2147;", mySqlOptions =>
                    mySqlOptions.ServerVersion(new Version(8,0,18), ServerType.MySql)
                )
            );
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
                endpoints.MapControllers();
            });
        }
    }
}
