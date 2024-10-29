
namespace QuestaAdminApi.ServiceLayer.Model
{
    public class ClsMailBodyModel
    {
        public string SenderEmailAddress { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string RecevierEmailAddress { get; set; } = string.Empty;
        public string RecevierName { get; set; } = string.Empty;
        public string SMTP_USERNAME { get; set; } = string.Empty;
        public string SMTP_PASSWORD { get; set; } = string.Empty;
        public string CONFIGSET { get; set; } = string.Empty;
        public string HOST { get; set; } = string.Empty;
        public string PORT { get; set; } = string.Empty;
        public string BODY { get; set; } = string.Empty;
        public string BCCEmail { get; set; } = string.Empty;
        public string CCEmail { get; set; } = string.Empty;
        public bool SendToHr { get; set; }
        public bool SendToCandidate { get; set; }
    }
}
