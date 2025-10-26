using DashBoardAPI.Repository;
using DashBoardAPI.Service.ComplaintService;
using DashBoardAPI.Service.CustomerService;
using DashBoardAPI.Service.DashBoardService;
using DashBoardAPI.Service.EngineerService;
using DashBoardAPI.Service.LoginService;
using DashBoardAPI.Service.RoleService;
using DashBoardAPI.Service.UserService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DashBoardAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Security Headers - REMOVED Content-Security-Policy that blocks CORS
        private static List<KeyValuePair<string, string>> InclusionHeaderList = new List<KeyValuePair<string, string>>() {
            // Remove or modify Content-Security-Policy that blocks cross-origin requests
            new KeyValuePair<string, string>("X-Content-Type-Options", "nosniff"),
            new KeyValuePair<string, string>("X-Xss-Protection", "1; mode=block"),
            new KeyValuePair<string, string>("X-Frame-Options", "SAMEORIGIN"),
            new KeyValuePair<string, string>("Strict-Transport-Security", "max-age=31536000;"),
            new KeyValuePair<string, string>("Referrer-Policy", "no-referrer, strict-origin-when-cross-origin"),
            new KeyValuePair<string, string>("Cache-Control", "no-cache, must-revalidate, no-store")
        };

        public static List<string> ExclusionHeaderList = new List<string>() {
            "X-Powered-By",
            "Server"
        };

        // Allowed Origins
        private static string[] AllowedDomains = new string[]
        {
            "http://localhost:3000",
            "https://cftmanagementr.vercel.app",
            "https://cftmanagement.somee.com" 
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Enhanced CORS configuration
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins(AllowedDomains)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Register dependencies here
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IComplaintService, ComplaintService>();
            services.AddScoped<IEngineerService, EngineerServeice>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // CORS middleware - MUST come before UseAuthorization and UseEndpoints
            app.UseCors("AllowReactApp");

            // Handle OPTIONS requests for preflight
            app.Use(async (context, next) =>
            {
                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    await context.Response.CompleteAsync();
                    return;
                }
                await next();
            });

            // Security Headers middleware - AFTER CORS
            app.Use(async (context, next) =>
            {
                // Remove unwanted headers
                foreach (var header in ExclusionHeaderList)
                {
                    context.Response.Headers.Remove(header);
                }

                // Add security headers (excluding CORS headers)
                foreach (var header in InclusionHeaderList)
                {
                    // Don't override CORS headers
                    if (!context.Response.Headers.ContainsKey(header.Key))
                    {
                        context.Response.Headers[header.Key] = header.Value;
                    }
                }

                await next();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // Helper methods
        public static Stream GetStreamFromString(string str)
        {
            var newStream = new MemoryStream();
            var sw = new StreamWriter(newStream);
            sw.Write(str);
            sw.Flush();
            newStream.Position = 0;
            return newStream;
        }
    }
}