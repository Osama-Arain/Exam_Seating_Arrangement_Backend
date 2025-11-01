using ESA.Environment.Register;
using ESA.Layers.ContextLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("mycors",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetPreflightMaxAge(TimeSpan.FromMinutes(30))
            );
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Payday.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});


// var LocalConnection = builder.Configuration.GetConnectionString("Local");
builder.Services.AddControllers();
builder.Services.ConnectionConfigure();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureAuthentication();
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.ConfigureCoresPolicy();
builder.Services.ConfigureProcessor();
builder.Services.ConfigureSwaggerGeneration();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//} 


app.UseCors("mycors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.MapControllers();
app.Run();

