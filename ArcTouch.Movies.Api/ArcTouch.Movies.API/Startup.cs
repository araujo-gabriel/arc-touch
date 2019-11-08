using ArchTouch.Movies.API.Filters;
using ArcTouch.Movies.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace ArchTouch.Movies.API
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
            services.AddDependenciesInjection(Configuration);

            services.AddCors();

            services.AddResponseCaching();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ARC TOUCH - MOVIES API", Version = "v1" });
            });

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(ExceptionFilter));
            })
           .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                opt.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
                opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                 builder.WithOrigins(Configuration.GetSection("FrontEndConfigurations").GetValue<string>("Url"))
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ARC TOUCH - MOVIES API");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
