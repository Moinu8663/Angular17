using Consul;
using Microsoft.EntityFrameworkCore;
using Post.Config;
using Post.Model;
using Post.Repository;
using Post.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddDbContext<PostContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
