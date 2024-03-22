using DevFreela.API;
using DevFreela.API.Filters;
using DevFreela.API.Models;
using DevFreela.Application.Consumers;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.MessageBus;
using DevFreela.Infrastructure.Payments;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("OpeningTime"));

//builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma instância para toda a aplicação enquanto estiver no ar
//builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });  // uma instância para cada requisição

//builder.Services.AddSingleton<DevFreelaDbContext>();  // uma instância para toda a aplicação enquanto estiver no ar (Singleton)

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs"); // DevFreelaCs está declarado no appsettings.json
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase(connectionString)); // cria um banco de dados em memória (com EntityFrameWorkCore), usado para situações que o banco de dados ainda não foi criado ou não foi realizada a migration

builder.Services.AddHostedService<PaymentApprovedConsumer>();

builder.Services.AddHttpClient();    // para usar o http client factory


builder.Services.AddScoped<IProjectRepository, ProjectRepository>();   // padrão repository
builder.Services.AddScoped<ISkillRepository, SkillRepository>();       // padrão repository
builder.Services.AddScoped<IUserRepository, UserRepository>();         // padrão repository

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IMessageBusService, MessageBusService>();

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));   // validationFilter

builder.Services.AddApplication();    // classe de configuração

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
