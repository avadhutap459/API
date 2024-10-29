

namespace QuestaAdminApi.ServiceLayer
{
    public interface IMaster
    {
        string GetAsessmentUrlBaseOnDns(string DnsName);
        List<ClsAssessmentModel> GetAllAssessmentDetailsBaseCompanyId(int CompanyId);
        List<ClsCompanyModel> GetAllCompanyDetail();
        List<ClsHumanResouceModel> GetHrDetailsBaseOnCompany(int CompanyId, int AssessmentId);
        Tuple<List<ClsMailConfigByAssessmentModel>, List<ClsMailConfigByAssessmentModel>> GetMailConfigDetailBaseOnAssessmentId(int AssessmentId);
        void Dispose();
    }
}
