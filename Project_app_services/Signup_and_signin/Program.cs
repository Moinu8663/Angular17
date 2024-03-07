using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Signup_and_signin.Config;
using Signup_and_signin.Model;
using Signup_and_signin.Repository;
using Signup_and_signin.Services;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDBcontext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));
builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<Iservice, Service>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin()
              .WithOrigins("http://localhost:4200"));

});

//Add token
var secret = "Moinuddinshaikhmainproject";
var key = Encoding.UTF8.GetBytes(secret);
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
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
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
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

app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
