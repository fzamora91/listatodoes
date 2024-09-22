using Application;
using Infrastructure;
using Shared.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
EngineContext.Create();
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddSwaggerGen();


var app = builder.Build();

EngineContext.Current.Configure(app.Services);


app.UseSwagger();
app.UseSwaggerUI();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
