using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace QuestaAdminApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class GenerateCandidateLinkController : ControllerBase
    {
        private IMaster MasterSvc { get; set; }
        private ILinkGeneration LinkGenerationSvc { get; set; }
        public GenerateCandidateLinkController(IMaster _MasterSvc, ILinkGeneration _LinkGenerationSvc)
        {
            MasterSvc = _MasterSvc;
            LinkGenerationSvc = _LinkGenerationSvc;
        }

        [HttpGet]
        [Route("GetAllCompanyDetailForGeneratingLink")]
        public IActionResult GetAllCompanyDetailForGeneratingLink()
        {
            try
            {
                List<ClsCompanyModel> lstcompanymodel = new List<ClsCompanyModel>();

                lstcompanymodel = MasterSvc.GetAllCompanyDetail();

                return Ok(new { IsSucess = true, CompanyDetails = lstcompanymodel });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllAssessmentDetailsBaseOnCompany/{CompanyId}")]
        public IActionResult GetAllAssessmentNHrDetailsBaseOnCompany(int CompanyId)
        {
            try
            {
                List<ClsAssessmentModel> lstassessmentdetails = new List<ClsAssessmentModel>();

                lstassessmentdetails = MasterSvc.GetAllAssessmentDetailsBaseCompanyId(CompanyId);

                

                return Ok(new {IsSucess = true ,AssessmentDetails = lstassessmentdetails});
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        [HttpGet]
        [Route("GetOthesMasterDetailBaseOnCompanyAndAssessmentId/{CompanyId}/{AssessmentId}")]
        public IActionResult GetOthesMasterDetailBaseOnCompanyAndAssessmentId(int CompanyId,int AssessmentId)
        {
            try
            {
                List<ClsMailConfigByAssessmentModel> lstinitialmaildetails = new List<ClsMailConfigByAssessmentModel>();
                List<ClsMailConfigByAssessmentModel> lstfinalmaildetails = new List<ClsMailConfigByAssessmentModel>();
                List<ClsHumanResouceModel> lsthrdetails = new List<ClsHumanResouceModel>();

                var mailtemplatedetails = MasterSvc.GetMailConfigDetailBaseOnAssessmentId(AssessmentId);
                lstinitialmaildetails = mailtemplatedetails.Item1;
                lstfinalmaildetails = mailtemplatedetails.Item2;
                lsthrdetails = MasterSvc.GetHrDetailsBaseOnCompany(CompanyId, AssessmentId);

                return Ok(new {IsSucess = true, InitialMailTemplate = lstinitialmaildetails , FinalMailTemplate = lstfinalmaildetails, HrDetails = lsthrdetails });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost("GenerateLinkBaseUserSelection")]
        public IActionResult GenerateLinkBaseUserSelection(ClsLinkGenerationModel linkgenerationmodel)
        {
            try
            {
                string ConfigName = "EmailFlg_" + this.Request.Host.Host.ToString();

                string Url = MasterSvc.GetAsessmentUrlBaseOnDns(ConfigName);
                List<DisplayLinkGeneration> lstLinkGeneration = new List<DisplayLinkGeneration>();

               // Dictionary<int, string> lstlinks = new Dictionary<int, string>();
               

                for (int i = 1;i<= linkgenerationmodel.LinkCount; i++)
                {
                    int TestId = LinkGenerationSvc.GenerateTestIdBaseonRequireDetails(linkgenerationmodel.AssessmentID, linkgenerationmodel.CompanyId,
                        linkgenerationmodel.HrId, linkgenerationmodel.InitialMailId, linkgenerationmodel.FinalMailId, linkgenerationmodel.IsReportSendToHr,
                        linkgenerationmodel.IsReportSendToCandidate);
                    string URL = Url + TestId;
                    lstLinkGeneration.Add(new DisplayLinkGeneration { Url = URL, TestId = TestId});

                }

                return Ok(new { IsSucess = true, Links = lstLinkGeneration });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
