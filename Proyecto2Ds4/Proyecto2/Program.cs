using CalculadoraWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var connectionString = builder.Configuration.GetConnectionString("CalculadoraDB") 
    ?? "Server=localhost,1433;Database=CalculadoraDB;User Id=sa;Password=TuPassword123;TrustServerCertificate=True;";

builder.Services.AddSingleton(new CalculoService(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();


app.MapGet("/", () => new
{
    proyecto = "Web API Calculadora - Proyecto #2",
    curso = "Desarrollo de Software IV",
    profesor = "Regis Rivera",
    endpoints = new
    {
        todos = "GET /api/calculos",
        sumas = "GET /api/calculos/sumas",
        restas = "GET /api/calculos/restas",
        multiplicaciones = "GET /api/calculos/multiplicaciones",
        divisiones = "GET /api/calculos/divisiones",
        recientes = "GET /api/calculos/recientes?dias=7",
        guardar = "POST /api/calculos"
    },
    testing = "Usar Postman para consumir la API"
});

app.Run();
