using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PranavM.WxTechChallengeService.WebApi.Constants;
using PranavM.WxTechChallengeService.WebApi.Controllers.Implementation;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.Configurations;

namespace PranavM.WxTechChallengeService.WebApi
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
            services.AddControllers();
            services.AddScoped<IWxTechChallengeController, WxTechChallengeControllerImplementation>();

            var wooliesRecruitmentServiceConfigSection = Configuration.GetSection($"{ConfigConstants.WOOLES_RECRUITMENT_SERVICE_SECTION}");
            services.Configure<WooliesRecruitmentServiceConfig>(wooliesRecruitmentServiceConfigSection);

            var wooliesRecruitmentConfig = wooliesRecruitmentServiceConfigSection.Get<WooliesRecruitmentServiceConfig>();
            Log.Information("Woolies Recruitment Service Base URL: {WooliesRecruitmentBaseUrl}", wooliesRecruitmentConfig?.BaseUrl);
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
