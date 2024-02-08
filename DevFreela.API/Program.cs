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

//builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma inst�ncia para toda a aplica��o enquanto estiver no ar
//builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma inst�ncia para cada requisi��o

//builder.Services.AddSingleton<DevFreelaDbContext>();  // uma inst�ncia para toda a aplica��o enquanto estiver no ar (Singleton)

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs"); // DevFreelaCs est� declarado no appsettings.json
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase(connectionString)); // cria um banco de dados em mem�ria (com EntityFrameWorkCore), usado para situa��es que o banco de dados ainda n�o foi criado ou n�o foi realizada a migration


builder.Services.AddScoped<IProjectRepository, ProjectRepository>();   // padr�o repository
builder.Services.AddScoped<ISkillRepository, SkillRepository>();       // padr�o repository
builder.Services.AddScoped<IUserRepository, UserRepository>();         // padr�o repository

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();     // usado no FluentValidation

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));  // usado no padr�o CQRS (MediatR)

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
