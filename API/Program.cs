using API.Database;
using API.Hubs;
using API.Middlewares;
using API.Repository;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddLogging();
builder.Services.AddControllers();

//db
builder.Services.AddDbContext<AppDatabaseContext>(options =>
    options.UseInMemoryDatabase("AppDatabase"));

//invoke migrations
//testy!!!!!!
//design!!!


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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IChatService, ChatService>();

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
