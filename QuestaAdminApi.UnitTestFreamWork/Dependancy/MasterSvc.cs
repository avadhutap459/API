using QuestaAdminApi.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestaAdminApi.UnitTestFreamWork.Dependancy
{
    public class ClsMasterSvc
    {
        public IMaster mstsvc { get; set; }
        public ClsMasterSvc(IMaster _mstsvc) 
        { 
            mstsvc = _mstsvc;
        }

        public List<ClsCompanyModel> GetAllCompanyDetail()
        {
            try
            {
                return mstsvc.GetAllCompanyDetail();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public List<ClsAssessmentModel> GetAllAssessmentDetailsBaseCompanyId(int CompanyId)
        {
            try
            {
                return mstsvc.GetAllAssessmentDetailsBaseCompanyId(CompanyId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Tuple<List<ClsMailConfigByAssessmentModel>, List<ClsMailConfigByAssessmentModel>> GetMailConfigDetailBaseOnAssessmentId(int AssessmentId)
        {
            try
            {
                return mstsvc.GetMailConfigDetailBaseOnAssessmentId(AssessmentId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<ClsHumanResouceModel> GetHrDetailsBaseOnCompany(int CompanyId, int AssessmentId)
        {
            try
            {
                return mstsvc.GetHrDetailsBaseOnCompany(CompanyId, AssessmentId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
