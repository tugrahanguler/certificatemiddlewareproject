// This is the entry point of the User Management API application.
// The application is built using ASP.NET Core and provides CRUD operations for user management.
// Middleware for logging and error handling is configured in this file.

using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Add the simple logging middleware to the pipeline
app.UseSimpleLoggingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();