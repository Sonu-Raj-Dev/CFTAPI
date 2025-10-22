using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using DashBoardAPI.Service.ComplaintService;
using DashBoardAPI.Service.CustomerService;
using DashBoardAPI.Service.DashBoardService;
using DashBoardAPI.Service.EngineerService;
using DashBoardAPI.Service.LoginService;
using DashBoardAPI.Service.UserService;
using Microsoft.EntityFrameworkCore;
using System.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// If you use EF Core, uncomment this line and set your connection string
// builder.Services.AddDbContext<DataContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
builder.Services.AddMemoryCache();

// ✅ CORS configuration for React
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp", policy =>
//        policy.WithOrigins("http://localhost:3000","http://CFTManagement.somee.com", "https://CFTManagement.somee.com", "https://cftmanagementr.vercel.app")  // React default port
//              .AllowAnyHeader()
//              .AllowAnyMethod());
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("SameOriginPolicy", policy =>
        policy
            .WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod());
});



var app = builder.Build();

// Middleware order matters — don’t rearrange unless you enjoy debugging nonsense
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Apply CORS before authorization
app.UseCors("SameOriginPolicy");

app.UseAuthorization();

// Map controllers (no need to overcomplicate routes)
app.MapControllers();

app.Run();
