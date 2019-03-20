using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calculator.Common.Evaluator;
using Calculator.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Calculator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDependencies(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<ILogStorage<EvaluatorLog>, InMemoryLogStorage<EvaluatorLog>>();
            services.AddScoped<StringEvaluator>();
            services.AddScoped<IStringEvaluator, LoggingStringEvaluatorDecorator>(provider=> new LoggingStringEvaluatorDecorator(provider.GetRequiredService<StringEvaluator>(), provider.GetRequiredService<ILogStorage<EvaluatorLog>>()));
            services.AddCors(o => o.AddPolicy("myPolicy", 
                b=>b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("myPolicy");
            app.UseMvc();
        }
    }
}