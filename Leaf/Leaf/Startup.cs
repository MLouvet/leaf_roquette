﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Leaf.Models;
using Leaf.Services;
using NonFactors.Mvc.Grid;
using Microsoft.AspNetCore.Http;
using System.IO;
using Leaf.ScaffoldedModels;

namespace Leaf
{
    public class Startup
    {
        private string _contentRootPath = "";
        private string connection;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connection = Configuration["DbConnectionString"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddMvcGrid();
            var connection = @"Server=(localdb)\mssqllocaldb;Database=Leaf;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<LeafContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
