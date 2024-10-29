using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuestaAdminApi.Controllers;
using QuestaAdminApi.ServiceLayer;
using QuestaAdminApi.UnitTestFreamWork.Dependancy;

namespace QuestaAdminApi.UnitTestFreamWork
{
    public class GenerateLinkTest
    {
        private readonly Mock<IMaster> mastersvc;
        private readonly Mock<ILinkGeneration> linkgenerationsvc;
        public GenerateLinkTest()
        {
            mastersvc = new Mock<IMaster>();
            linkgenerationsvc = new Mock<ILinkGeneration>();
        }

        [Fact]
        public void Should_Be_Return_StatusCode_Ok_GetCompanyDetailForGeneratingLink()
        {

            ClsMasterSvc objmaster = new ClsMasterSvc(new ClsMasterData());

            var allcompanydetails = objmaster.GetAllCompanyDetail();

            mastersvc.Setup(x=>x.GetAllCompanyDetail()).Returns(allcompanydetails);

            var CandidateLinkController = new GenerateCandidateLinkController(mastersvc.Object, linkgenerationsvc.Object);

            var companydetailresult = CandidateLinkController.GetAllCompanyDetailForGeneratingLink();
            Assert.NotNull(companydetailresult);

            var okResult = Assert.IsType<OkObjectResult>(companydetailresult);

            List<ClsCompanyModel> lstCompanyDetails = new List<ClsCompanyModel>();

            if(okResult.Value != null)
            {
                var json = JsonConvert.SerializeObject(okResult.Value);

                string data = JObject.Parse(json)["CompanyDetails"].ToString();

                lstCompanyDetails = JsonConvert.DeserializeObject<List<ClsCompanyModel>>(data);
            }

            var _json = companydetailresult.ToString();

            Assert.Equal(allcompanydetails.Count(), lstCompanyDetails.Count());


            Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.OK);

        }

        [Theory]
        [InlineData(29)]
        public void Should_Be_Return_Ok_Status_Code_GetAllAssessmentNHrDetailsBaseOnCompany(int CompanyId)
        {
            try
            {
                ClsMasterSvc objmaster = new ClsMasterSvc(new ClsMasterData());

                var AssessmentDetails = objmaster.GetAllAssessmentDetailsBaseCompanyId(CompanyId);

                mastersvc.Setup(x => x.GetAllAssessmentDetailsBaseCompanyId(It.Is<int>(x => x == CompanyId))).Returns(AssessmentDetails);


                var CandidateLinkController = new GenerateCandidateLinkController(mastersvc.Object, linkgenerationsvc.Object);

                var AssessmentResult = CandidateLinkController.GetAllAssessmentNHrDetailsBaseOnCompany(CompanyId);

                var okResult = Assert.IsType<OkObjectResult>(AssessmentResult);

                Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Fact]
        public void Should_Be_Return_False_GetCompanyDetailForGeneratingLink()
        {
            try
            {
                ClsMasterSvc objmaster = new ClsMasterSvc(new ClsMasterData());

                var allcompanydetails = objmaster.GetAllCompanyDetail();

                mastersvc.Setup(x => x.GetAllCompanyDetail()).Returns(allcompanydetails);

                var CandidateLinkController = new GenerateCandidateLinkController(mastersvc.Object, linkgenerationsvc.Object);

                mastersvc.Verify(x => x.GetAllCompanyDetail(), Times.Never);

                var companydetailresult = CandidateLinkController.GetAllCompanyDetailForGeneratingLink();

                
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Theory]
        [InlineData(29,2)]
        public void Should_Be_Return_True_GetOthesMasterDetailBaseOnCompanyAndAssessmentId(int CompanyId,int AssessmentId)
        {
            ClsMasterSvc objmaster = new ClsMasterSvc(new ClsMasterData());

            var _mailConfigDetailBaseAssessmentId = objmaster.GetMailConfigDetailBaseOnAssessmentId(AssessmentId);

            mastersvc.Setup(x => x.GetMailConfigDetailBaseOnAssessmentId(It.Is<int>(x => x == CompanyId))).Returns(_mailConfigDetailBaseAssessmentId);

            var CandidateLinkController = new GenerateCandidateLinkController(mastersvc.Object, linkgenerationsvc.Object);

            var GetCompanyAndAssessmentDetail = CandidateLinkController.GetOthesMasterDetailBaseOnCompanyAndAssessmentId(CompanyId,AssessmentId);

            var okResult = Assert.IsType<OkObjectResult>(GetCompanyAndAssessmentDetail);
            List<ClsMailConfigByAssessmentModel> lstinitialmaildetails = new List<ClsMailConfigByAssessmentModel>();
            List<ClsMailConfigByAssessmentModel> lstfinalmaildetails = new List<ClsMailConfigByAssessmentModel>();
            List<ClsHumanResouceModel> lsthrdetails = new List<ClsHumanResouceModel>();
            if (okResult.Value != null)
            {
                var json = JsonConvert.SerializeObject(okResult.Value);

                string InitialMailTemplateData = JObject.Parse(json)["InitialMailTemplate"].ToString();
                string FinalMailTemplateData = JObject.Parse(json)["FinalMailTemplate"].ToString();
                string HrDetailsData = JObject.Parse(json)["HrDetails"].ToString();

                lstinitialmaildetails = JsonConvert.DeserializeObject<List<ClsMailConfigByAssessmentModel>>(InitialMailTemplateData);
                lstfinalmaildetails = JsonConvert.DeserializeObject<List<ClsMailConfigByAssessmentModel>>(FinalMailTemplateData);
                lsthrdetails = JsonConvert.DeserializeObject<List<ClsHumanResouceModel>>(HrDetailsData);
            }

            Assert.Null(lsthrdetails);

            Assert.Equal(_mailConfigDetailBaseAssessmentId.Item1.Count(), lstinitialmaildetails.Count());
            Assert.Equal(_mailConfigDetailBaseAssessmentId.Item2.Count(), lstfinalmaildetails.Count());


        }
    }
}
