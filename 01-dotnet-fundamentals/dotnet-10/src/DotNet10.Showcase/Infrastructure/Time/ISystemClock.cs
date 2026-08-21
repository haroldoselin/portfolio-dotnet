namespace DotNet10.Showcase.Infrastructure.Time
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }
}
