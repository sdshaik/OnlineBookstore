using Azure.Core;
using Azure.Identity;
using OnlineBookstore.Api.Authentication;
using OnlineBookstore.Api.Extensions;
using OnlineBookstore.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
TokenCredential tokenCredential = new ManagedIdentityCredential();
if (System.Environment.GetEnvironmentVariable("TF_BUILD") == "True")
{
    tokenCredential = new ChainedTokenCredential(new AzureCliCredential(), new ManagedIdentityCredential());
}
else if (builder.Environment.IsDevelopment())
{
    tokenCredential = new ChainedTokenCredential(new VisualStudioCredential(), new VisualStudioCodeCredential(), new AzureCliCredential());
}

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
//builder.Services.AddSingleton<ITelemetryInitializer>(new CustomTelemetryInitializer(builder.Configuration));
builder.Services.ConfigureAuthentication(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbServices(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.EnableAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
