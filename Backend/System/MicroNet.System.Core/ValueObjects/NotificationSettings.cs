using MicroNet.System.Core.Enums;

namespace MicroNet.System.Core.ValueObjects
{
    public class NotificationSettings
    {
        public NotificationMode Mode { get; private set; }
        public List<string> Recipients { get; private set; }
        public bool UseMakerChecker { get; private set; }
        public bool RequireTransactionLimit { get; private set; }

        public NotificationSettings(
            NotificationMode mode,
            List<string> recipients,
            bool useMakerChecker,
            bool requireTransactionLimit)
        {
            Mode = mode;
            Recipients = recipients;
            UseMakerChecker = useMakerChecker;
            RequireTransactionLimit = requireTransactionLimit;
        }
    }
}
