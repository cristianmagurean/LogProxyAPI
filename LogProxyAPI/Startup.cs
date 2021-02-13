using LogProxyAPI.Interfaces;
using LogProxyAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LogProxyAPI.Helpers;
using NSwag;
using System.Linq;
using AutoMapper;
using LogProxyAPI.Mappers;

namespace LogProxyAPI
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
            services.AddHttpClient();

            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddTransient<IAirTableService, AirTableService>();
            services.AddScoped<IUserService, UserService>();

            //automapper
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            //NSwag
            services.AddOpenApiDocument(document =>
            {
                document.Title = "LogProxyAPI";
                document.AddSecurity("Basic", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Basic,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Provide Basic Authentication"
                });
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           app.UseOpenApi();
           app.UseSwaggerUi3(c => c.DocumentTitle = "LogProxyAPI");
        }
    }
}
