
namespace QuestaAdminApi.ServiceLayer
{
    public interface ILinkGeneration
    {
        int GenerateTestIdBaseonRequireDetails(int AssessmentId, int CompanyId, int HrId, int InitialMailId, int FinalMailId, bool IsReportSendToHr, bool IsReportSendToCandidate);
        void Dispose();
    }
}
