﻿using Application.Common.Interfaces;
using Domain.Entities.User;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAPI;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();
            //InMemory Cache
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, InMemoryCacheService>();
            //Redis Cache
            //services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddScoped<ICommonService, CommonService>();
            services.ConfigureIdentity(configuration);
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddSocialNetworkAPI(configuration);
            return services;
        }
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
                o.Lockout.AllowedForNewUsers= true;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(int.Parse(configuration["ApplicationSettings:LockoutDurationInMinutes"] ?? "5"));
                o.Lockout.MaxFailedAccessAttempts = int.Parse(configuration["ApplicationSettings:MaxLoginFailedCount"] ?? "5");
            })
            .AddRoles<IdentityRole>()
            .AddSignInManager()
            ;
            // builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }
    }
}
