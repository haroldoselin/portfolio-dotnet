namespace MinimalApi.ShowCase.Domain.Entities;

public sealed class Produto
{
    private Produto(Guid id, string nome, decimal preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }

    public Guid Id { get; }
    public string Nome { get; }
    public decimal Preco { get; }

    public static Produto Criar(string nome, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        }

        if (preco <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(preco), "Preço deve ser maior que zero.");
        }

        return new Produto(Guid.NewGuid(), nome.Trim(), preco);
    }
}
