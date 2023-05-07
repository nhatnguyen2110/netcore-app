using Application;
using Application.Common.Interfaces;
using Domain;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;
using WebAPI.Extensions;
using WebAPI.Middlewares;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// Early init of NLog to allow startup and exception logging, before host is built
#if !DEBUG
var databaseTarget = (NLog.Targets.DatabaseTarget)LogManager.Configuration.FindTargetByName("ownDB-web");
databaseTarget.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
LogManager.ReconfigExistingLoggers();
#endif
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{


    // Add services to the container.

    builder.Services.AddControllers();
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureIISIntegration();
    builder.Services.AddSwaggerDocumentation(builder.Configuration);
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.ConfigureCors();
    builder.Services.AddAuthentication();
    builder.Services.ConfigureJWT(builder.Configuration);
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    //builder.Services.AddControllers(config =>
    //{
    //    config.RespectBrowserAcceptHeader = true;
    //    config.ReturnHttpNotAcceptable = true;
    //}).AddNewtonsoftJson()
    //.AddXmlDataContractSerializerFormatters();

    var app = builder.Build();
    app.UseSwaggerDocumentation(builder.Configuration);
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        // Initialise and seed database
        using (var scope = app.Services.CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseCors(Constants.CorsPolicy);

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}



