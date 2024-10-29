using QuestaAdminApi.DatabaseLayer;
using Dapper;
using System.Data;

namespace QuestaAdminApi.ServiceLayer.Service
{
    public class ClsLinkGeneration : IDisposable, ILinkGeneration
    {
        ClsDbConnection Connectionmgr;


        private bool isDisposed = false;
        public ClsLinkGeneration()
        {
            Connectionmgr = ClsDbConnection.Instance;
        }

        ~ClsLinkGeneration()
        {
            Dispose(false);
        }


        public int GenerateTestIdBaseonRequireDetails(int AssessmentId,int CompanyId,int HrId,int InitialMailId,int FinalMailId,bool IsReportSendToHr,bool IsReportSendToCandidate)
        {
            try
            {
                int TestId = 0;
                using (IDbConnection cn = Connectionmgr.connection)
                {
                    var dynamicParam = new DynamicParameters();
                    dynamicParam.Add("AssessmentId", AssessmentId);
                    dynamicParam.Add("CompanyId", CompanyId);
                    dynamicParam.Add("HrId", HrId);
                    dynamicParam.Add("InitialMailId", InitialMailId);
                    dynamicParam.Add("FinalMailId", FinalMailId);
                    dynamicParam.Add("IsReportSendToHr", IsReportSendToHr);
                    dynamicParam.Add("IsReportSendToCandidate", IsReportSendToCandidate);
                    dynamicParam.Add("OutputTestId",dbType:DbType.Int32,direction : ParameterDirection.Output);

                    cn.Query<int>("sp_generatelinkbaseonrequiredetails", dynamicParam, commandType: CommandType.StoredProcedure);

                    TestId = dynamicParam.Get<int>("OutputTestId");
                }

                return TestId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        #region Dispose


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {

                // Code to dispose the managed resources of the class
            }
            // Code to dispose the un-managed resources of the class
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
