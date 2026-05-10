using docs_garage_api.Interface;
using docs_garage_api.Service;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IMergePdfService, MergePdfService>();
builder.Services.AddScoped<IFileValidationService, FileValidationService>();
//builder.Services.Configure<FormOptions>(options =>
//{
//    options.MultipartHeadersLengthLimit = 1024 * 1024;           // 1 MB
//    options.MultipartBodyLengthLimit = 200 * 1024 * 1024;        // 200 MB
//    options.ValueLengthLimit = 200 * 1024 * 1024;                // 200 MB
//});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 200 * 1024 * 1024;       // 200 MB
});

// Sirf ek AddCors rakho
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // ← dev ke liye easiest
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("AllowAll");  // ← "AllowAngular" se "AllowAll" karo
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();   
}

//app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();

app.MapControllers();

app.Run();
