using CadastroCliente.Application.UseCases.CadastrarCliente;
using CadastroCliente.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddSingleton<CadastrarClienteValidator>();
builder.Services.AddScoped<CadastrarClienteHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CadastroCliente API",
        Version = "v1",
        Description = "API para cadastro de clientes com validação de CPF."
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CadastroCliente API v1");
    options.RoutePrefix = string.Empty;
});
app.MapControllers();
app.Run();

public partial class Program
{
}
