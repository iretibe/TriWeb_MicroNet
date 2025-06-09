namespace MicroNet.System.Core.Dtos
{
    public class NotificationSettingsDto
    {
        public string Mode { get; set; } = default!; // e.g., "Email", "SMS", "Popup", etc.
        public List<string> Recipients { get; set; } = new();
        public bool UseMakerChecker { get; set; }
        public bool RequireTransactionLimit { get; set; }
    }
}
