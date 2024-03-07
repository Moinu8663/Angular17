using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using UserAccount.Config;
using UserAccount.Model;
using UserAccount.Repository;
using UserAccount.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddDbContext<AccountDBcontext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

//Add token
var secret = "Moinuddinshaikhmainproject";
var key = Encoding.UTF8.GetBytes(secret);
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
{
    RoleClaimType = ClaimTypes.Role,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,

    ValidIssuer = "authapiMoinuddin",
    ValidAudience = "userapi",
    IssuerSigningKey = new SymmetricSecurityKey(key)
});
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
        .RequireAuthenticatedUser().Build());
});

//Add cors
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin());

});

// Register multiple instances based on configuration
var consulClient = new ConsulClient();
var consulConfiguration = builder.Configuration.GetSection("Consul:Instances").Get<List<Service_Config>>();

foreach (var instanceConfig in consulConfiguration)
{
    var registration = new AgentServiceRegistration()
    {
        ID = instanceConfig.Id,
        Name = instanceConfig.Name,
        Address = instanceConfig.Address,
        Port = instanceConfig.Port,
        Tags = new[] { "authorized:true" },
        Check = new AgentServiceCheck()
        {
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
            Interval = TimeSpan.FromSeconds(15),
            HTTP = $"https://{instanceConfig.Address}:{instanceConfig.Port}/{instanceConfig.Name}",
            Timeout = TimeSpan.FromSeconds(15),
        }
    };
    await consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(false);
    await consulClient.Agent.ServiceRegister(registration).ConfigureAwait(false);
}


var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Map("/health", async context =>
{
    Console.WriteLine("Health check request received.");
    // Your authorization logic here
    if (context.Request.Headers["Authorization"] != "Bearer YourToken")
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }
    else
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("OK");
    }
    Console.WriteLine($"Health check response: {context.Response.StatusCode}");
});
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
