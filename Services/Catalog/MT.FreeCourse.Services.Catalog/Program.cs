﻿using Microsoft.Extensions.Options;
using MT.FreeCourse.Services.Catalog.Dtos;
using MT.FreeCourse.Services.Catalog.Settings;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(CategoryDto));// Class in baglı oldugu Assembly deki tum mapper lari tarayacak.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp=>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
})







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

app.UseAuthorization();

app.MapControllers();

app.Run();