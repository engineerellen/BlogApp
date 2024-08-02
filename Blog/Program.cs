using BlogApp.Data;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração da services, criando e configurando contexto, swagger autenticação, autorização e implementando o middleware WebScoket
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogAPI", Version = "v1" });
});

builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromMinutes(2);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApp API v1"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//  WebSocket middleware
app.UseWebSockets();
app.UseMiddleware<WebSocketMiddleware>();

app.Run();