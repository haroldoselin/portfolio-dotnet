namespace CSharp14.Showcase.Performance
{
    public static class TransactionParser
    {
        public static decimal ParseWithString(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            return decimal.Parse(input, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static decimal ParseWithSpan(ReadOnlySpan<char> input)
        {
            return decimal.Parse(input, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}