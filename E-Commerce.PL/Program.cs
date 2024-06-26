using System.Security.Claims;
using System.Text;
using ConfigureManager;
using eCommerceApp.BLL.DTO;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Stripe;

try
{

    var builder = WebApplication.CreateBuilder(args);
    var logger = new LoggerConfiguration()
           .ReadFrom.Configuration(builder.Configuration)
           .Enrich.FromLogContext()
           .Enrich.WithMachineName()
           .Enrich.WithThreadName()
           .Enrich.WithThreadId()
           .Enrich.WithProcessId()
           .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    logger.Information("App starting");
    var emailConfig = builder.Configuration
         .GetSection("EmailConfiguration")
         .Get<EmailConfigurationDTO>();
    builder.Services.AddSingleton(emailConfig);

    // Add services to the container.


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.ConfigureDependency();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });
    builder.Configuration.GetValue<string>("StripeOptions:PublicKey");
    StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeOptions:SecretKey");
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    //builder.Services.AddDbContextFactory<DataContext>(options =>
    //{
    //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //});
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("V1", new OpenApiInfo
        {
            Version = "V1",
            Title = "WebAPI",
            Description = "eCommerceApp WebAPI"
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List <string>()
        }
    });
    });
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetSection("JWT").GetSection("ValidIssuer").Value,
                ValidAudience = builder.Configuration.GetSection("JWT").GetSection("ValidAudience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")
                .GetSection("SecretKey").Value)),
                NameClaimType = ClaimTypes.NameIdentifier


            };

        });
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetSection("RedisCacheServerURL").Value;
    });
    

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/V1/swagger.json", "eCommerceApp WebAPI");
        });
    }

    app.UseHttpsRedirection();
    app.UseExceptionMiddleware();
    app.UseAuthentication();
    app.UseCors("AllowAll");
    app.UseStaticFiles();


    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());   
}
finally
{
    Log.CloseAndFlush();
}
