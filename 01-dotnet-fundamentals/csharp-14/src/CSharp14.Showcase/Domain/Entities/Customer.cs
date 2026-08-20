namespace CSharp14.Showcase.Domain.Entities
{
    public sealed class Customer
    {
        public required string Name
        {
            get;
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                field = value.Trim();
            }
        }
    }
}
