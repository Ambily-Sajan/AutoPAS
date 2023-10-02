using AutoPAS.Data;
using AutoPAS.Services;
using AutoPAS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("localhost")));

builder.Services.AddDbContext<AutoPasDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("AutoPas")));

builder.Services.AddScoped<ICustomerPortal, customerPortalRepository>();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
