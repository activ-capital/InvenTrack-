using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
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


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}
app.UseRouting();
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();