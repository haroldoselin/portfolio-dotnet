namespace CadastroCliente.Domain.ValueObjects;

public sealed record Cpf
{
    private Cpf(string valor) => Valor = valor;
    public string Valor { get; }

    public static bool TryCreate(string? valor, out Cpf? cpf)
    {
        cpf = null;
        var digits = new string((valor ?? string.Empty).Where(char.IsDigit).ToArray());

        if (digits.Length != 11 || digits.Distinct().Count() == 1)
        {
            return false;
        }

        cpf = new Cpf(digits);
        return true;
    }
}
