using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.Repository;
using LibreriaAdmin.Services;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAdmin
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
            services.AddControllersWithViews();

            //����LibreriaContext
            services.AddDbContext<LibreriaDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LibreriaDataBaseContext")));

            //����MemberService
            services.AddTransient<IManagerService, ManagerService>();

            services.AddTransient<IMemberService, MemberService>();
            //
            services.AddTransient<ICategoryService, CategoryService>();


            //����ProductService
            services.AddTransient<IProductService, ProductService>();

            //�`�JOrderService
            services.AddTransient<IOrderService, OrderService>();

            //repository signup
            services.AddTransient<IRepository, LibreriaRepository>();


            services.AddSwaggerDocument(config =>
            {
                var apiSchema = new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "���v���",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "�бNtoken ��J���:Bearer{Token}"
                };
                config.AddSecurity("JWT Token", Enumerable.Empty<string>(), apiSchema);
                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT Token"));
            });

            services.AddAuthentication()
                     .AddCookie(options =>
                     {
                         options.LoginPath = "/Manager/login";
                         options.LogoutPath = "/Manager/login";
                         options.AccessDeniedPath = "/Account/Forbidden/";
                     })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

                        };
                    });

            services.AddTransient<IExhibitonService, ExhibitonService>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthentication();

            #region �Y�n����Server response������A�Ш������Ѩíק惡�q�{��
            //app.UseStatusCodePages(async context =>
            //{
            //    var request = context.HttpContext.Request;
            //    var response = context.HttpContext.Response;

            //    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            //    {
            //        response.Redirect("/Manager/login");
            //    }
            //});
            #endregion 

            app.UseCookiePolicy();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
