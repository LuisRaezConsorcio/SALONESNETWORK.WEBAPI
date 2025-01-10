using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.BLL.Services;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.DAL.Interfaces;
using SALONESNETWORK.DAL.Repositories;
using SALONESNETWORK.MODELS.Entities;
using System.Diagnostics.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SalonesDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("conexion"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("conexion"))
)
);

builder.Services.AddScoped<IPaisRepository<Pais>, PaisRepository>();
builder.Services.AddScoped<IPaisService, PaisService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
