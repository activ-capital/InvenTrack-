using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Loggers/logs.txt", rollingInterval: RollingInterval.Month)
    .Filter.ByExcluding(logEvent =>
    {
        if (logEvent.Properties.TryGetValue("SourceContext", out var sourceContext))
        {
            var sourceContextValue = sourceContext.ToString();
            return sourceContextValue.Contains("Microsoft.EntityFrameworkCore.Database.Command");
        }

        return false;
    })
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); // Убрана дублирующая настройка SwaggerDoc
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization(); // <-- ВАЖНО добавить здесь!

app.MapControllers();


app.Run();