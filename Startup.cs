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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using PhotoWEB.Models.DBmodels;
using Dapper;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using PhotoWEB.Models.ViewsModels;
using PhotoWEB.Models.DBmodels.ViewsModels;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels.DBmodels;
using PhotoWEB.Controllers;
using System.ComponentModel;
using System.IO;

namespace PhotoWEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Index");
                });

            services.AddOptions();
           

            var builder = new ContainerBuilder();

            builder.Populate(services);

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();
            builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            builder.RegisterType<LogFactory>();
            builder.RegisterType<MsSQLConnectionFactory>().As<IConnectionFactory>();


           // builder.Register(context => new MsSQLConnectionFactory("Server=.\\SQLEXPRESS;Initial Catalog=PhotoWeb;Integrated Security=True;")).As<IConnectionFactory>();
            
            builder.Register(context => new MsSQLConnectionFactory(Configuration.GetConnectionString("PhotoWEB"))).As<IConnectionFactory>();

            AutofacContainer = builder.Build();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return new AutofacServiceProvider(AutofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseAuthentication();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
