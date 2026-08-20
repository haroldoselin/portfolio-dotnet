namespace CSharp14.Showcase.Application.Services
{
    public delegate bool TryParseTransaction(string input,
        out decimal value);

    public sealed class TransactionAmountParser
    {
        private readonly TryParseTransaction _parser;

        public TransactionAmountParser()
        {
            _parser = (input, out value) =>
                decimal.TryParse(input, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out value);
        }

        public bool TryParse(string input, out decimal value)
        {
            ArgumentNullException.ThrowIfNull(input);

            return _parser(input, out value);
        }
    }
}
