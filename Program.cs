using EcommerceApi.Data;
using EcommerceApi.Services;
using EcommerceApi.Converters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// --- SERVIÇOS ---
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ProdutoService>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; 
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new StatusPedidoConverter()); 
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
    
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        }
    )
);

builder.Services.AddHealthChecks();
var app = builder.Build();

// --- MIDDLEWARES ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce Api V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

// --- SEED / MIGRATION AUTOMÁTICA ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>(); 
        
        Console.WriteLine("Verificando conexão e aplicando Migrations no Docker...");
        context.Database.Migrate(); 
        Console.WriteLine("Banco de dados e Migrations aplicados com sucesso!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro crítico ao aplicar as migrations automaticamente.");
        Console.WriteLine($"ERRO CRÍTICO NO DOCKER: {ex.Message}"); 
    }
}

app.Run();