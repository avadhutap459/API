

namespace QuestaAdminApi.ServiceLayer
{
    public class ClsLinkGenerationModel
    {
        public int AssessmentID { get; set; }
        public int CompanyId { get; set; }
        public int HrId { get; set; }
        public int InitialMailId { get; set; }
        public int FinalMailId { get; set; }
        public bool IsReportSendToHr { get; set; }
        public bool IsReportSendToCandidate { get; set; }
        public int LinkCount { get; set; }
    }

    public class DisplayLinkGeneration
    {
        public string Url { get; set; } = string.Empty;
        public int TestId { get; set; }
    }
}
