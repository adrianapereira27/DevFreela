using DevFreela.API.Models;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("OpeningTime"));

//builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma inst�ncia para toda a aplica��o enquanto estiver no ar
//builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma inst�ncia para cada requisi��o

//builder.Services.AddSingleton<DevFreelaDbContext>();  // uma inst�ncia para toda a aplica��o enquanto estiver no ar (Singleton)

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs"); // DevFreelaCs est� declarado no appsettings.json
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase(connectionString)); // cria um banco de dados em mem�ria (com EntityFrameWorkCore), usado para situa��es que o banco de dados ainda n�o foi criado ou n�o foi realizada a migration

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISkillService, SkillService>();

builder.Services.AddControllers();
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
