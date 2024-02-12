using DevFreela.API;
using DevFreela.API.Filters;
using DevFreela.API.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));   // validationFilter

builder.Services.AddApplication();    // classe de configuração

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
