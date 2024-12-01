using FluentValidation;
using FluentValidation.AspNetCore;
using Core;
using Core.Entities.Validators;
using Core.Interfaces.Repository;
using Infrastructure.Repositories;
using Infrastructure;
using DotNet.DynamicInjector.Models;
using System.Data;
using DotNet.DynamicInjector;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<AssuntoValidator>();

builder.Services.AddScoped<DataContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddScoped<IAssuntoRepository, AssuntoRepository>();

var allowedInterfaceNamespaces = new List<string> { "UnitOfWork" };

var roles = new List<IoCRole>
 {
     new IoCRole
     {
         Dll = "Infrastructure.dll", //DLL name
         Implementation = "Infrastructure", // Implementation name, can be used for a control if you use several projects and wanted to separate them
         Priority = 1, // Priority that the dll should be loaded
         LifeTime = LifeTime.SCOPED, // Lifetime of your addiction injection
         Name = "Dapper", //Dependency name. It is used only for identification
         Active = true
     }
 };

var ioCConfiguration = new IoCConfiguration(roles, allowedInterfaceNamespaces);
builder.Services.RegisterDynamicDependencies(ioCConfiguration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAllOrigins");

app.Run();
