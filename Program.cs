using MyApiApp.Services; // Assuming MongoDbService is in the Services folder

var builder = WebApplication.CreateBuilder(args);

// Read connection string and database name from environment variables or hardcoded for now
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
    ?? "mongodb://mongo:wcoejUokOtszAnyAmNLggJkkWAEfbVWz@autorack.proxy.rlwy.net:10353";
var databaseName = "test";

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The connection string 'DATABASE_URL' is not set.");
}

// Add services to the container.
builder.Services.AddControllers();

// Register MongoDbService with connection string and database name
builder.Services.AddSingleton(sp => new MongoDbService(connectionString, databaseName));

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
