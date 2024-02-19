using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Signup_and_signin.Model;
using Signup_and_signin.Repository;
using Signup_and_signin.Services;
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
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Moinuddinshaikh"));
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,

    ValidIssuer = "authapiMoinuddin",
    ValidAudience = "userapi",
    IssuerSigningKey = key
});

var app = builder.Build();

app.UseCors("AllowAll");


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
