namespace DependencyInjection.Showcase.Domain;

public sealed record Order(int Id, decimal Amount, string Status = "Criado");