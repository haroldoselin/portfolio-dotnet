namespace DotNet10.Showcase.Infrastructure.Time
{
    public sealed class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow
            => DateTimeOffset.UtcNow;
    }
}
