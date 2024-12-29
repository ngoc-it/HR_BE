using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static HR_BEND.Controllers.LoginLogoutController;
using HR_BEND.Models.Service;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);  // Lắng nghe trên cổng 5000, có thể thay đổi cổng nếu cần
});
// Đảm bảo nạp tệp cấu hình
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "user_cookie";
        options.Cookie.HttpOnly = true; // Cookie chỉ có thể truy cập qua HTTP, không thể truy cập từ JavaScript
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Chỉ gửi cookie qua kết nối HTTPS
        options.Cookie.SameSite = SameSiteMode.None; // Chấp nhận cookie từ các nguồn khác
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Thời gian sống của cookie
        options.LoginPath = "/api/TaiKhoans/login"; // Đường dẫn đến API đăng nhập
        *//*        options.LoginPath = "/api/LoginLogout/login";*//*
        options.LogoutPath = "/api/TaiKhoans/logout";
*//*        options.LogoutPath = "/api/LoginLogout/logout";*//*
        options.AccessDeniedPath = "/api/TaiKhoans/accessdenied"; // Đường dẫn khi truy cập bị từ chối
        options.SlidingExpiration = true; // Gia hạn thời gian hết hạn khi có yêu cầu mới
    });*/


// Add services to the container.
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin() // Cho phép tất cả nguồn
                          .AllowAnyMethod() // Cho phép tất cả phương thức
                          .AllowAnyHeader()); // Cho phép tất cả header
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Sử dụng builder.Configuration
            ValidAudience = builder.Configuration["Jwt:Audience"], // Sử dụng builder.Configuration
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Sử dụng khóa bí mật// Khóa bí mật
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddControllers() ; 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<IChamCongService, ChamCongService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication(); // Đảm bảo việc xác thực
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();
