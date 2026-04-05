using Bridge.Backend.Data;
using Bridge.Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services
builder.Services.AddControllers();

// 🔹 Database
builder.Services.AddDbContext<BridgeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BridgeConnectionString")));

// 🔹 Python Service
builder.Services.AddScoped<PythonService>();

// 🔹 Swagger (for testing API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 CORS (important for frontend later)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// 🔹 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthorization();

// 🔹 Map Controllers
app.MapControllers();

// Test route
app.MapGet("/", () => "Vision Backend Running 🚀");

app.Run();