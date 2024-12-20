using ContactCrudAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Allow the Angular app's origin
              .AllowAnyMethod()                    // Allow all HTTP methods
              .AllowAnyHeader();                   // Allow all headers
    });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<JsonFileService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAngularApp");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
