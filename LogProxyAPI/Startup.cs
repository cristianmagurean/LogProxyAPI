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
using System.Reflection;
using Lamar;
using MediatR;
using LogProxyAPI.Behaviors;
using LogProxyAPI.CQRS;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace LogProxyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ServiceRegistry services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.TryAddTransient<IValidator<SaveMessageCommand>, SaveMessageCommandValidator>();
        }       

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();           

            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            });

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
