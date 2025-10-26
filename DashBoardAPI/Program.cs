using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using DashBoardAPI.Service.ComplaintService;
using DashBoardAPI.Service.CustomerService;
using DashBoardAPI.Service.DashBoardService;
using DashBoardAPI.Service.EngineerService;
using DashBoardAPI.Service.LoginService;
using DashBoardAPI.Service.RoleService;
using DashBoardAPI.Service.UserService;
using Microsoft.EntityFrameworkCore;
using System.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bind ConnectionStrings section
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

// Register dependencies
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IDashBoardService, DashBoardService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<IEngineerService, EngineerServeice>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddMemoryCache();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ✅ CRITICAL: Add Static Files middleware for Somee.com
app.UseStaticFiles();

// ✅ CRITICAL: Add Default Files middleware
app.UseDefaultFiles();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Apply CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// Map controllers
app.MapControllers();

// ✅ ADD THESE ENDPOINTS FOR SOMEE.COM
app.MapGet("/", () => "Application is running successfully on Somee.com!");
app.MapGet("/api/health", () => new { status = "OK", message = "API is running" });

// ✅ Fallback route for SPA
app.MapFallbackToFile("index.html");

app.Run();