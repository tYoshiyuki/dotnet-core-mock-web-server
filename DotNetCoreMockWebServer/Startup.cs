using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMockWebServer
{
    public class Startup
    {
        ILogger _logger;

        public Startup(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Startup>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(option =>
            {
                option.AddConsole();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var req = context.Request;

                var output = new List<string>();
                output.Add("-------------Request Information Start-------------");
                output.Add($"Time[{DateTime.Now}]");
                output.Add($"Scheme[{req.Scheme}]");
                output.Add($"Path[{req.Path}]");
                output.Add($"QueryString[{req.QueryString}]");
                output.Add($"Method[{req.Method}]");
                output.Add($"ContentLength[{req.ContentLength}]");
                output.Add($"ContentType[{req.ContentType}]");

                output.Add("-------------Body-------------");
                using (var reader = new StreamReader(req.Body))
                {
                    var body = reader.ReadToEnd();
                    output.Add(body);
                }

                output.Add("-------------Headers--------------");
                output.Add($"Headers Count[{req.Headers.AsEnumerable().Count()}]");

                foreach (var h in req.Headers.AsEnumerable())
                {
                    output.Add($"{h.Key}[{h.Value}]");
                }
                output.Add("-------------Request Information End-------------");
                                
                _logger.LogInformation(string.Join(Environment.NewLine, output));
                await context.Response.WriteAsync("Request Success");
            });
        }
    }
}
