using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddSerilogLogger();
builder.Host.UseSerilog();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();