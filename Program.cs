using Serilog;
using TaskTrackerAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext());

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddlewarePipeline();

app.Run();