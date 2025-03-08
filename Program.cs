using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PlayerDatabaseSettings>(
    builder.Configuration.GetSection("PlayerDatabase"));

builder.Services.AddSingleton<PlayerService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<PlayerContext>(opt => opt.UseInMemoryDatabase("GameData"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
