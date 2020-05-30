using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMockWebServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var req = context.Request;

                var output = new List<string>
                {
                    "-------------Request Information Start-------------",
                    $"Time[{DateTime.Now}]",
                    $"Scheme[{req.Scheme}]",
                    $"Path[{req.Path}]",
                    $"QueryString[{req.QueryString}]",
                    $"Method[{req.Method}]",
                    $"ContentLength[{req.ContentLength}]",
                    $"ContentType[{req.ContentType}]",

                    "-------------Body-------------"
                };
                using (var reader = new StreamReader(req.Body))
                {
                    var body = reader.ReadToEnd();
                    output.Add(body);
                }

                output.Add("-------------Headers--------------");

                output.Add($"Headers Count[{req.Headers?.AsEnumerable().Count() ?? 0}]");

                if (req.Headers != null)
                {
                    foreach (var h in req.Headers.AsEnumerable())
                    {
                        output.Add($"{h.Key}[{h.Value}]");
                    }
                }

                output.Add("-------------Request Information End-------------");

                logger.LogInformation(string.Join(Environment.NewLine, output));

                await context.Response.WriteAsync("Request Success");
                await context.Response.WriteAsync(Environment.NewLine);
                await context.Response.WriteAsync(string.Join(Environment.NewLine, output));
            });
        }
    }
}
