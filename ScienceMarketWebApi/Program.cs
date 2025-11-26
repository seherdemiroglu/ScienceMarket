

using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ScienceMarketData;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddCors();

// Add services to the container.
builder
    .Services
    .AddDbContext<ScienceMarketDbContext>(config =>
    {
        //config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        config.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), p =>
        {
            p.MigrationsAssembly("ScienceMarketMigration");
        });
    });
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(config => config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapScalarApiReference(config =>
{

    config.Theme = ScalarTheme.Purple;
});

app.Run();
