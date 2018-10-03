using Amazon.S3;
using Amazon.Rekognition;
using GotTalent_API.Data;
using GotTalent_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GotTalent_API
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
            string connectionString = Helpers.GetRDSConnectionString(this.Configuration);
            services.AddDbContext<DataContext>(x => x.UseMySql(connectionString));

            services.AddDefaultAWSOptions(this.Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddAWSService<IAmazonRekognition>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
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
                //app.UseHsts();
            }

           //app.UseHttpsRedirection();
           app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
           app.UseMvc();
        }
    }
}
