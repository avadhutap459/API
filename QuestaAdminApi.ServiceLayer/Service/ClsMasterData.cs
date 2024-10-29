using Dapper;
using QuestaAdminApi.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestaAdminApi.ServiceLayer
{
    public class ClsMasterData : IDisposable , IMaster
    {
        ClsDbConnection Connectionmgr;


        private bool isDisposed = false;
        public ClsMasterData()
        {
            Connectionmgr = ClsDbConnection.Instance;
        }

        ~ClsMasterData()
        {
            Dispose(false);
        }


        public string GetAsessmentUrlBaseOnDns(string DnsName)
        {
            try
            {
                string Url = string.Empty;
                using(IDbConnection cn = Connectionmgr.connection)
                {
                    Url = cn.Query<string>("select ConfigValue from MstConfig where ConfigName = @ConfigName", new
                    {
                        ConfigName = DnsName
                    }).FirstOrDefault();
                }

                return Url;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ClsAssessmentModel> GetAllAssessmentDetailsBaseCompanyId(int CompanyId)
        {
            try
            {
                List<ClsAssessmentModel> lstAssessmentModel = new List<ClsAssessmentModel>();

                using(IDbConnection cn = Connectionmgr.connection)
                {
                    lstAssessmentModel = cn.Query<ClsAssessmentModel>("select MA.AssessmentId,MA.AssessmentName,MA.CreateBy,MA.LastModifiedBy," +
                        "MA.CreateAt,MA.LastModifiedAt,MA.TotalQuestion from [dbo].[mstCompany] MC " +
                        "inner join [dbo].[txnCampanyMapToAssessment] TCA on MC.CompanyId = TCA.CompanyId " +
                        "Left join [dbo].[mstAssessmentSet] MA on MA.AssessmentId = TCA.AssessmentId " +
                        "Where TCA.CompanyId = @CompanyId and TCA.isactive = 1", new
                        {
                            CompanyId = CompanyId
                        }).ToList();
                }

                return lstAssessmentModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ClsCompanyModel> GetAllCompanyDetail()
        {
            try
            {
                List<ClsCompanyModel> lstCompanyModel = new List<ClsCompanyModel>();

                using(IDbConnection cn = Connectionmgr.connection)
                {
                    lstCompanyModel = cn.Query<ClsCompanyModel>("select * from mstCompany where IsActive = 1").ToList();
                }

                return lstCompanyModel;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<ClsHumanResouceModel> GetHrDetailsBaseOnCompany(int CompanyId,int AssessmentId)
        {
            try
            {
                List<ClsHumanResouceModel> lstHrModel = new List<ClsHumanResouceModel>();

                using(IDbConnection cn = Connectionmgr.connection)
                {
                    lstHrModel = cn.Query<ClsHumanResouceModel>("select MH.HrId,MH.HrName,MH.HrEmail,MH.HrPhoneNumber,MH.IsActive FROM " +
                        "[dbo].[txnHrMapToCompanyAndAssessment] HCA " +
                        "INNER JOIN [dbo].[mstCompany] MC on HCA.CompanyId = MC.CompanyId" +
                        " INNER JOIN [dbo].[mstAssessmentSet] MA on HCA.AssessmentId = MA.AssessmentId" +
                        " INNER JOIN [dbo].[mstHumanResourceRepo] MH on MH.HrId = HCA.HrId" +
                        " where MC.CompanyId = @CompanyId and MA.AssessmentId = @AssessmentId and HCA.isactive = 1", new
                        {
                            CompanyId = CompanyId,
                            AssessmentId = AssessmentId
                        }).ToList();
                }
                return lstHrModel;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Tuple<List<ClsMailConfigByAssessmentModel>,List<ClsMailConfigByAssessmentModel>> GetMailConfigDetailBaseOnAssessmentId(int AssessmentId)
        {
            try
            {
                List<ClsMailConfigByAssessmentModel> lstInitialMailModel = new List<ClsMailConfigByAssessmentModel>();
                List<ClsMailConfigByAssessmentModel> lstFinalMailModel = new List<ClsMailConfigByAssessmentModel>();

                using (IDbConnection cn = Connectionmgr.connection)
                {
                    lstInitialMailModel = cn.Query<ClsMailConfigByAssessmentModel>("select * from [dbo].[mst_mailConfigByAssessment] where AssessmentId = @AssessmentId and MailType = @MailType", new
                    {
                        AssessmentId = AssessmentId,
                        MailType = "Initial"
                    }).ToList();

                    lstFinalMailModel = cn.Query<ClsMailConfigByAssessmentModel>("select * from [dbo].[mst_mailConfigByAssessment] where AssessmentId = @AssessmentId and MailType = @MailType", new
                    {
                        AssessmentId = AssessmentId,
                        MailType = "Final"
                    }).ToList();
                }

                    return new Tuple<List<ClsMailConfigByAssessmentModel>, List<ClsMailConfigByAssessmentModel>>(lstInitialMailModel, lstFinalMailModel);
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
