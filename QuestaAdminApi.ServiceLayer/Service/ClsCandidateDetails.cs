using Dapper;
using QuestaAdminApi.DatabaseLayer;
using QuestaAdminApi.ServiceLayer.Model;
using System.Data;

namespace QuestaAdminApi.ServiceLayer
{
    public class ClsCandidateDetails : IDisposable , ICandidateDetails
    {
        ClsDbConnection Connectionmgr;


        private bool isDisposed = false;
        public ClsCandidateDetails()
        {
            Connectionmgr = ClsDbConnection.Instance;
        }

        ~ClsCandidateDetails()
        {
            Dispose(false);
        }


        public List<ClsviewlistofcandidateModel> Getlistofcandidate()
        {
            try
            {
                List<ClsviewlistofcandidateModel> listcandidaterecords = new List<ClsviewlistofcandidateModel>();

                using (IDbConnection cn = Connectionmgr.connection)
                {
                    listcandidaterecords = cn.Query<ClsviewlistofcandidateModel>("Select UTD.TestId TestId, TC.FirstName + ' ' + TC.LastName NameOfCandidate,MAS.AssessmentName NameOfAssessment,MC.CompanyName CampanyName, " +
                        "MHR.HrName HrName, TC.IsActive IsActive from txnCandidate TC " +
                        "INNER JOIN txnUserTestDetails UTD on TC.UserId = UTD.UserId " +
                        "LEFT JOIN mstAssessmentSet MAS on MAS.AssessmentId = TC.AssessmentId " +
                        "LEFT JOIN mstCompany MC on MC.CompanyId = TC.CompanyId " +
                        "LEFT JOIN mstHumanResourceRepo MHR on MHR.HrId = TC.HrId").ToList();
                }

                return listcandidaterecords;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Updatecandidatestatus(int TestId,bool flg)
        {
            try
            {
                using(IDbConnection cn = Connectionmgr.connection)
                {
                    cn.Execute("Update TC set TC.IsActive = @Flg from txnCandidate TC " +
                        "   inner join txnUserTestDetails UTD on TC.UserId = UTD.UserId where UTD.TestId = @TestId",
                        new
                        {
                            TestId = TestId,
                            Flg = flg
                        });
                }
            }
            catch(Exception ex)
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
