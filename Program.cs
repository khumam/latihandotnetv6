using Microsoft.EntityFrameworkCore;
using BookManagement.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BookContext>(opt => opt.UseNpgsql(configuration.GetValue<string>("DatabaseConnectionString")));
builder.Services.AddDbContext<TodoContext>(opt => opt.UseNpgsql(configuration.GetValue<string>("DatabaseConnectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
