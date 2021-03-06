using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using persistence_nosql;
using persistence_relational;

namespace api
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
            // Add DbContext using SQL Server Provider
            services.AddDbContext<WvuFootballDataContext>(options =>
            {
                options.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=db_design_patterns_sql_dev;User Id=sa;Password=somethingStr0ng#;");
            });

            bool.TryParse(Configuration["UseDynamoDB"], out bool useDynamoDb);
            if (useDynamoDb)
            {
                services.AddTransient<IFootballRepository, DynamoDbFootballRepository>();
            }
            else
            {
                services.AddTransient<IFootballRepository, SqlServerFootballRepository>();
            }
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}