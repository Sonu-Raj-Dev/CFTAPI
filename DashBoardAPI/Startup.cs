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

        #region Security Headers Properties
        //headers to add
        private static List<KeyValuePair<string, string>> InclusionHeaderList = new List<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>("Content-Security-Policy", "default-src https: 'unsafe-inline' 'unsafe-eval'; script-src https: 'unsafe-inline' 'unsafe-eval'; style-src https: 'unsafe-inline'; img-src https: data:; font-src https:; object-src 'none'; frame-src 'none'; upgrade-insecure-requests;"),
            new KeyValuePair<string, string>("X-Content-Type-Options", "nosniff"),
            new KeyValuePair<string, string>("X-Xss-Protection", "1; mode=block"),
            new KeyValuePair<string, string>("X-Frame-Options", "SAMEORIGIN"),
            new KeyValuePair<string, string>("Strict-Transport-Security", "max-age=31536000;"),
            new KeyValuePair<string, string>("X-Content-Type", "nosniff"),
            new KeyValuePair<string, string>("Access-Control-Allow-Headers", "Content-Type, Authorization"),
            new KeyValuePair<string, string>("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS"),
            new KeyValuePair<string, string>("Feature-Policy", "accelerometer 'none'; camera 'none'; microphone 'none'"),
            new KeyValuePair<string, string>("Referrer-Policy", "no-referrer, strict-origin-when-cross-origin"),
            new KeyValuePair<string, string>("Cross-Origin-Opener-Policy", "same-origin"),
            new KeyValuePair<string, string>("Cross-Origin-Resource-Policy", "same-origin"),
            new KeyValuePair<string, string>("Cache-Control", "no-cache, must-revalidate, no-store"),
            new KeyValuePair<string, string>("Access-Control-Allow-Credentials", "true")
        };

        //headers to remove
        public static List<string> ExclusionHeaderList = new List<string>() {
            "X-Powered-By",
            "Server"
        };

        //CORS - allowed domains
        private static string[] AllowedDomains = new string[] {
            "http://localhost:3000",
            "http://localhost:8080",
            "http://localhost:6178",
            "https://localhost:6178",
            "https://CFTManagement.somee.com",
            "https://cftmanagementr.vercel.app"
        };

        //CORS - allowed methods
        public static string[] AllowedMethods = new string[] { "POST", "GET", "PUT", "DELETE", "OPTIONS" };
        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add CORS configuration
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

            // Add other services as needed
            services.AddSwaggerGen(); // If using Swagger
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Add security headers middleware
            app.Use(async (context, next) =>
            {
                // Remove unwanted headers
                foreach (var header in ExclusionHeaderList)
                {
                    context.Response.Headers.Remove(header);
                }

                // Add security headers
                var headers = GetInclusiveHeaders(context);
                foreach (var header in headers)
                {
                    context.Response.Headers[header.Key] = header.Value;
                }

                await next();
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            // Use CORS - Must be after UseRouting() and before UseAuthorization()
            app.UseCors("AllowReactApp");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Helper Methods
        public static List<KeyValuePair<string, string>> GetInclusiveHeaders(HttpContext context)
        {
            var headers = new List<KeyValuePair<string, string>>(InclusionHeaderList);

            // Add CORS origin header dynamically
            var origin = context.Request.Headers["Origin"].ToString();
            if (AllowedDomains.Contains(origin, StringComparer.OrdinalIgnoreCase))
            {
                headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", origin));
            }
            else
            {
                // Use environment variable or fallback
                var webOrigin = Environment.GetEnvironmentVariable("WebOrigin") ?? AllowedDomains.First();
                headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", webOrigin));
            }

            return headers;
        }

        public static string[] GetAllowedDomains()
        {
            return AllowedDomains.Select(ad => ad.ToLower()).ToArray();
        }

        public static string GetBaseUrl(HttpContext context)
        {
            string baseUrl = $"https://{context.Request.Host.Value}{context.Request.PathBase.Value}".ToLower();
            return baseUrl;
        }

        public static Stream GetStreamFromString(string str)
        {
            var newStream = new MemoryStream();
            var sw = new StreamWriter(newStream);
            sw.Write(str);
            sw.Flush();
            newStream.Position = 0; // Reset stream position
            return newStream;
        }
        #endregion
    }
}