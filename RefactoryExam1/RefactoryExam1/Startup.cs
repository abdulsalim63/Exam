using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefactoryExam1.Models;

namespace RefactoryExam1
{
    public class Startup
    {
        private readonly string connectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = "Host=localhost;Username=postgres;Password=docker;Database=exam1";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomersContext>(opt => opt.UseNpgsql(connectionString));
            services.AddDbContext<OrdersContext>(opt => opt.UseNpgsql(connectionString));
            services.AddDbContext<DriversContext>(opt => opt.UseNpgsql(connectionString));
            services.AddDbContext<ProductsContext>(opt => opt.UseNpgsql(connectionString));
            services.AddDbContext<OrderItemsContext>(opt => opt.UseNpgsql(connectionString));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
