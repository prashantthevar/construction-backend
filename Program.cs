using Microsoft.EntityFrameworkCore;
using MyApiApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Read connection string from the environment variable DATABASE_URL
// var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
var connectionString = "Server=junction.proxy.rlwy.net;Database=railway;User=root;Password=IVYdSwxGvWbSvpHsQNzTQyPCXgQzIBtj;Port=12398;";

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The connection string 'DATABASE_URL' is not set.");
}

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext with the connection string from the environment variable
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
