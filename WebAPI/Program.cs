using Application;
using Application.Common.Interfaces;
using Domain;
using Infrastructure;
using Infrastructure.Persistence;
using NLog;
using WebAPI.Extensions;
using WebAPI.Services;



var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.

builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.ConfigureCors();
    builder.Services.AddAuthentication();
    builder.Services.ConfigureJWT(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        // Initialise and seed database
        using (var scope = app.Services.CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseCors(Constants.CorsPolicy);

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

app.Run();


