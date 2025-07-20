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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FriendStuffDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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

builder.Services.AddOpenApi();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});

app.Run();
