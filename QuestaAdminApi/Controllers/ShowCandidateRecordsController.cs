using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestaAdminApi.ServiceLayer.Model;

namespace QuestaAdminApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ShowCandidateRecordsController : ControllerBase
    {
        private ICandidateDetails CandidateDetails { get; set; }
        public ShowCandidateRecordsController(ICandidateDetails _CandidateDetails)
        {
            CandidateDetails = _CandidateDetails;
        }

        [HttpGet]
        [Route("GetListOfCandidate")]
        public IActionResult GetListOfCandidate()
        {
            try
            {
                List<ClsviewlistofcandidateModel> lstofcandidaterecords = CandidateDetails.Getlistofcandidate();

                return Ok(new { CandidateRecords = lstofcandidaterecords });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("UpdateCandidateStatus/{TestId}/{Status}")]
        public IActionResult UpdateCandidateStatus(int TestId,bool Status)
        {
            try
            {
                CandidateDetails.Updatecandidatestatus(TestId,Status);

                return Ok(new { IsSucess = true });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("CandidateScoreCard/{TestId}")]
        public IActionResult GetCandidateScoreCard(int TestId)
        {
            try
            {
                return Ok();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("CandidateReport/{TestId}")]
        public IActionResult GetCandidateReport(int TestId)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
