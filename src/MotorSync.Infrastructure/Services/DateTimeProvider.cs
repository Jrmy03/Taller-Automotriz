using MotorSync.Application.Interfaces.Services;

namespace MotorSync.Infrastructure.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}
