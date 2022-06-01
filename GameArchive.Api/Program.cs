using Microsoft.EntityFrameworkCore;
using GameArchive.Api.Services;
using GameArchive.Api.Services.Implementation;
using GameArchive.Api.Models.Mapping;
using GameArchive.Api.Models.Data;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IGameService, GameService>();

builder.Services.AddAutoMapper(typeof(GameMappingProfile));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context?.Features
        ?.Get<IExceptionHandlerPathFeature>()
        ?.Error;

    var response = exception switch
    {
        DbUpdateException => exception?.InnerException?.Message,
        _ => exception?.Message
    };

    await context.Response.WriteAsJsonAsync(response);
}));
app.MapControllers();

app.Run();
