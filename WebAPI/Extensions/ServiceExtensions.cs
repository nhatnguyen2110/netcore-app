using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Text;
using System.Threading.RateLimiting;

namespace WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        private static string _apiVersion = $"{Constants.ApiVersionName.MajorVersion}.{Constants.ApiVersionName.MinorVersion}";
        private static string _apiName = Constants.ApiVersionName.Name;
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetSection("secretKey").Value;

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        SaveSigninToken = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? ""))
                    };
                });
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Constants.CorsPolicy, configurePolicy: builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            bool.TryParse(configuration["ApplicationSettings:IsUseSwagger"], out bool isUseSwagger);
            if (!isUseSwagger)
                return services;
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = _apiName;
                configure.Version = _apiVersion;
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            return services;
        }
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            var isUseSwagger = false;
            bool.TryParse(configuration["ApplicationSettings:IsUseSwagger"], out isUseSwagger);
            if (!isUseSwagger)
                return app;
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            return app;
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(Constants.ApiVersionName.MajorVersion, Constants.ApiVersionName.MinorVersion);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");

                //opt.Conventions.Controller<CompaniesController>().HasApiVersion(new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0));
                //opt.Conventions.Controller<CompaniesV2Controller>().HasApiVersion(new Microsoft.AspNetCore.Mvc.ApiVersion(2, 0));

            });
        }
        public static void ConfigureRateLimiter(this IServiceCollection services, IConfiguration configuration)
        {
            //Window Rate Limiter
            services.AddRateLimiter(options => {
                options.RejectionStatusCode = 429;
                options.AddFixedWindowLimiter("Fixed", opt => {
                    opt.Window = TimeSpan.FromSeconds(int.Parse(configuration["WindowRateLimiter:WindowBySeconds"]??"60"));
                    opt.PermitLimit = int.Parse(configuration["WindowRateLimiter:PermitLimit"] ?? "10000");
                    opt.QueueLimit = int.Parse(configuration["WindowRateLimiter:QueueLimit"] ?? "10");
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });
        }
    }
}
