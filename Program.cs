using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.Auth;
using FriendStuff.Features.GroupEvent;
using FriendStuff.Features.Group;
using FriendStuff.Features.Group.Member;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using FriendStuff.Features.EventExpense;
using FriendStuff.Features.EventExpense.ExpenseRefund;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

var rsa = RSA.Create();

string publicKeyPath = "/etc/secrets/public.pem";

string publicKey = await File.ReadAllTextAsync(publicKeyPath);

rsa.ImportFromPem(publicKey);

var rsaSecurityKey = new RsaSecurityKey(rsa);


builder.Services.AddDbContext<FriendStuffDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions =>
    {
        npgsqlOptions.CommandTimeout(60);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = rsaSecurityKey,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

builder.Services.AddControllers();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupMemberService, GroupMemberService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IRefundService, RefundService>();


builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
