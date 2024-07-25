using API.Database;
using API.Hubs;
using API.Middlewares;
using API.Repository;
using API.Server;
using API.Services;
using EmptyAspMvcAuth.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddLogging();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

//db
builder.Services.AddDbContext<ChatDatabase>(options =>
    options.UseInMemoryDatabase("ChatRooms"));
builder.Services.AddDbContext<UsersDatabase>(options =>
    options.UseInMemoryDatabase("Users"));

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.SetIsOriginAllowed(_ => true)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });

});

//services
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<AuthService>(); // potential error witg scoped!!



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers(); 

app.MapHub<ChatHub>("/chatHub");

app.Run();
