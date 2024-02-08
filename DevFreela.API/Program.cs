using Autofac.Core;
using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("OpeningTime"));

//builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma instância para toda a aplicação enquanto estiver no ar
//builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma instância para cada requisição

//builder.Services.AddSingleton<DevFreelaDbContext>();  // uma instância para toda a aplicação enquanto estiver no ar (Singleton)

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs"); // DevFreelaCs está declarado no appsettings.json
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase(connectionString)); // cria um banco de dados em memória (com EntityFrameWorkCore), usado para situações que o banco de dados ainda não foi criado ou não foi realizada a migration


builder.Services.AddScoped<IProjectRepository, ProjectRepository>();   // padrão repository
builder.Services.AddScoped<ISkillRepository, SkillRepository>();       // padrão repository
builder.Services.AddScoped<IUserRepository, UserRepository>();         // padrão repository

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();     // usado no FluentValidation

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));  // usado no padrão CQRS (MediatR)

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });
});

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
