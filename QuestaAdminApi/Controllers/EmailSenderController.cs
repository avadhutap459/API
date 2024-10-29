using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuestaAdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private IMailSender MailSender { get; set; }
        public EmailSenderController(IMailSender _MailSender)
        {
            MailSender = _MailSender;
        }

        [HttpGet]
        [Route("FinalEmailSend/{TestId}/{ModuleId}")]
        public IActionResult FinalEmailSend(int TestId,int ModuleId)
        {
            try
            {
                var ObjMailBody = MailSender.SendFinalEmail(TestId, ModuleId);

                return Ok(new { IsSucess = true, CandidateModel = ObjMailBody.Item1, HrModel = ObjMailBody.Item2 });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
