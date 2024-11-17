using LicenseeRecords.WebAPI.Services;
using LicenseeRecords.WebAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IJSONService, JSONService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DataInitializer.InitializeData();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
