using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:8000");
builder.Host.AddSerilogLogger();
builder.Host.UseSerilog();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.UseRouting();
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();