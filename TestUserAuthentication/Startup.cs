using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using FluentAssertions.Common;
using TestUserAuthentication.DataLayer;

namespace TestUserAuthentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            try
            {
                Configuration = configuration;
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in startupclass :  " + ex.Message);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
       
                services.AddControllers();

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestUserAuthentication", Version = "v1" });
                });

                string accessKey=string.Empty;
                string secretKey= string.Empty;

                accessKey = Configuration.GetValue<string>("AWS:AccessKey");
                secretKey = Configuration.GetValue<string>("AWS:SecretKey");
                var credentials = new BasicAWSCredentials(accessKey, secretKey);

                var config = new AmazonDynamoDBConfig()
                {
                    RegionEndpoint = Amazon.RegionEndpoint.APSouth1
                };

                var client = new AmazonDynamoDBClient(credentials, config);
                services.AddSingleton<IAmazonDynamoDB>(client);
                services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
                services.AddSingleton<IDynamoDBService, DynamoDBService>();
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in ConfigureServices in startupclass :  " + ex.Message);              

            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestUserAuthentication v1"));
                }

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in Configure in startupclass :  " + ex.Message);
            }
}
    }
}
