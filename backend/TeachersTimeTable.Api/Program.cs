using Microsoft.Extensions.Options;
using TeachersTimeTable.Application.Ai;
using TeachersTimeTable.Application.Ocr;
using TeachersTimeTable.Application.Services;
using TeachersTimeTable.Application.TimetableExtraction;
using TeachersTimeTable.Infrastructure.Ai;
using TeachersTimeTable.Infrastructure.Ocr;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Framework services
// --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// Application services
// --------------------
builder.Services.AddScoped<ITimetableExtractionService, TimetableExtractionService>();

// --------------------
// OCR (Infrastructure)
// --------------------
builder.Services.Configure<TesseractOptions>(
    builder.Configuration.GetSection("Tesseract"));
builder.Services.Configure<GeminiOptions>(
    builder.Configuration.GetSection("Gemini"));

builder.Services.AddHttpClient<IAiClient, GeminiAiClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddScoped<IOcrService, TesseractOcrService>();

// --------------------
// Gemini AI
// --------------------
builder.Services.AddHttpClient<IAiClient, GeminiAiClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(60);
});

// --------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
