using Dapper;
using QuestaAdminApi.DatabaseLayer;
using QuestaAdminApi.ServiceLayer.Model;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using static QuestaAdminApi.ServiceLayer.Model.ClsEnum;

namespace QuestaAdminApi.ServiceLayer.Service
{
    public class ClsMailSender : IDisposable, IMailSender
    {
        ClsDbConnection Connectionmgr;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        private bool isDisposed = false;
        ClsAwsConsole ObjAwsConsole = new ClsAwsConsole();

        public ClsMailSender()
        {
            Connectionmgr = ClsDbConnection.Instance;
        }

        ~ClsMailSender()
        {
            Dispose(false);
        }

        public Tuple<ClsMailBodyModel, ClsMailBodyModel> SendFinalEmail(int TestId,int ModuleId)
        {
            try
            {
                ClsMailBodyModel ObjMailBodyForCandidate = SendEmailBodyForCandidate(TestId, "sp_getmailbodyforcandidate");
                ClsMailBodyModel ObjMailBodyForHr = SendEmailBodyForCandidate(TestId, "sp_getmailbodyforHr");

                string In_TestId = Convert.ToString(TestId);

                SendEmail(ObjMailBodyForCandidate, In_TestId, ModuleId, ObjMailBodyForCandidate.SendToCandidate).GetAwaiter().GetResult();
                SendEmail(ObjMailBodyForHr, In_TestId, ModuleId, ObjMailBodyForCandidate.SendToHr).GetAwaiter().GetResult();
                /*
                var tasks = new Task[] {
                    SendEmail(ObjMailBodyForCandidate,TestId.ToString(),ModuleId,ObjMailBodyForCandidate.SendToCandidate),
                    SendEmail(ObjMailBodyForHr,TestId.ToString(),ModuleId,ObjMailBodyForCandidate.SendToHr)
                };

                Task.WaitAll(tasks);
                */

                return new Tuple<ClsMailBodyModel, ClsMailBodyModel>(ObjMailBodyForCandidate, ObjMailBodyForHr);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }




        private async Task SendEmail(ClsMailBodyModel ObjMailBody, string TestId, int ModuleId,bool IsReportRequireToSend)
        {
           // DateTime DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            try
            {
                string FROM = ObjMailBody.SenderEmailAddress;
                string FROMNAME = ObjMailBody.SenderName;

                string TO = ObjMailBody.RecevierEmailAddress;

                string[] BCC = ObjMailBody.BCCEmail.Split(';');

                string RecevierName = ObjMailBody.RecevierName;

                string SMTP_USERNAME = ObjMailBody.SMTP_USERNAME;

                string SMTP_PASSWORD = ObjMailBody.SMTP_PASSWORD;

                string CONFIGSET = ObjMailBody.CONFIGSET;

                string HOST = ObjMailBody.HOST;

                int PORT = Convert.ToInt32(ObjMailBody.PORT);

                string SUBJECT = "Questa Enneagram Assessment - Complete Personality Assessment";

                // The body of the email
                string HTMLContent = "<html><head><style>.image{margin - left: auto; margin - right: auto;height: 55px;width: 103px;}p{font-size: 12px;font-family: Arial, Helvetica, sans-serif;text-align: justify;text-align-last: left;-moz-text-align-last: left;}";
                HTMLContent = HTMLContent + ".image - container {justify - content: center;}li{font-size: 12px;font-family: Arial, Helvetica, sans-serif;text-align: justify;text-align-last: left;-moz-text-align-last: left;}.border{border: 1px solid black;}</style></head><body>";
                string HTMLEndContent = "</body></html> ";
                string BODY = HTMLContent + ObjMailBody.BODY + HTMLEndContent;
                BODY = BODY.Replace("@RecevierName", RecevierName);

                // Create and build a new MailMessage object
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                //create Alrternative HTML view
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(BODY, null, "text/html");
                var buildDir = Environment.CurrentDirectory;


                // string FooterImagePath = buildDir + "\\Images\\" + "QuestaMailFooterLogo.png";
                byte[] FooterMailFooterImageBytes = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questaenneagramassest", "images", "QuestaMailFooterLogo.png");//System.IO.File.ReadAllBytes(FooterImagePath);
                System.IO.MemoryStream FooterMailFooterImagestreamBitmap = new System.IO.MemoryStream(FooterMailFooterImageBytes);
                LinkedResource theEmailImage1 = new LinkedResource(FooterMailFooterImagestreamBitmap, MediaTypeNames.Image.Jpeg);
                theEmailImage1.ContentId = "myFooterID";

                //Add the Image to the Alternate view
                htmlView.LinkedResources.Add(theEmailImage1);

               // string FacebookImagePath = buildDir + "\\Images\\" + "facebook.png";
                byte[] FacebookMailFooterImageBytes = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questaenneagramassest", "images", "facebook.png"); //System.IO.File.ReadAllBytes(FacebookImagePath);
                System.IO.MemoryStream FacebookMailFooterImagestreamBitmap = new System.IO.MemoryStream(FacebookMailFooterImageBytes);
                LinkedResource theEmailImage2 = new LinkedResource(FacebookMailFooterImagestreamBitmap, MediaTypeNames.Image.Jpeg);
                theEmailImage2.ContentId = "myFacebookID";

                //Add the Image to the Alternate view
                htmlView.LinkedResources.Add(theEmailImage2);

                //string ATImagePath = buildDir + "\\Images\\" + "AtLogo.png";
                byte[] ATImageBytes = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questaenneagramassest", "images", "AtLogo.png"); //System.IO.File.ReadAllBytes(ATImagePath);
                System.IO.MemoryStream ATImagestreamBitmap = new System.IO.MemoryStream(ATImageBytes);
                LinkedResource theEmailImage4 = new LinkedResource(ATImagestreamBitmap, MediaTypeNames.Image.Jpeg);
                theEmailImage4.ContentId = "myAtID";

                //Add the Image to the Alternate view
                htmlView.LinkedResources.Add(theEmailImage4);


                //string LinkedImagePath = buildDir + "\\Images\\" + "linkedin.png";
                byte[] LinkedImageBytes = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questaenneagramassest", "images", "linkedin.png");//System.IO.File.ReadAllBytes(LinkedImagePath);
                System.IO.MemoryStream LinkedImagestreamBitmap = new System.IO.MemoryStream(LinkedImageBytes);
                LinkedResource theEmailImage5 = new LinkedResource(LinkedImagestreamBitmap, MediaTypeNames.Image.Jpeg);
                theEmailImage5.ContentId = "myLinkedInID";

                //Add the Image to the Alternate view
                htmlView.LinkedResources.Add(theEmailImage5);

                //Add view to the Email Message
                message.AlternateViews.Add(htmlView);

                message.From = new MailAddress(FROM, FROMNAME);

                if(!string.IsNullOrEmpty(TO))
                {
                    message.To.Add(new MailAddress(TO));
                }
                message.Subject = SUBJECT;
                message.Body = BODY;

                foreach (var bcc in BCC)
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }

                if (IsReportRequireToSend)
                {
                    byte[] pdfResult = GenerateReport(ModuleId, TestId);

                    if(pdfResult != null)
                    {
                        var memStream = new MemoryStream(pdfResult);
                        memStream.Position = 0;
                        var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                        var reportAttachment = new Attachment(memStream, contentType);

                        int _InTestId = Convert.ToInt32(TestId);

                        string NameOfCandidate = getcandidatenamebaseontestid(_InTestId);

                        reportAttachment.ContentDisposition.FileName = NameOfCandidate + "-Questa Enneagram Assessment Profile.pdf";
                        message.Attachments.Add(reportAttachment);
                    }
                   
                }

                message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
                {
                    // Pass SMTP credentials
                    client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                    // Enable SSL encryption
                    client.EnableSsl = true;

                    client.Send(message);

                    client.Dispose();

                }

                message.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        private byte[] GenerateReport(int ModuleId, string TestId)
        {
            try
            {
                string filename = TestId + "-Questa Enneagram Assessment Profile.pdf";
                byte[] pdfResult = null;
                switch (ModuleId)
                {
                    case (int)enumModule.H1PartA:
                        pdfResult = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questareportpdfformat", "Qsser", filename);
                        return pdfResult;

                        break;
                    case (int)enumModule.StandardReport:
                        pdfResult = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questareportpdfformat", "Standard", filename);
                        return pdfResult;
                        
                        break;
                    case (int)enumModule.QMap:
                        pdfResult = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questareportpdfformat", "Qmap", filename);
                        return pdfResult;

                        break;
                    case (int)enumModule.QLeap:
                        pdfResult = ObjAwsConsole.DownloadFileFromAwsS3Bucket("questareportpdfformat", "Qleap", filename);
                        return pdfResult;

                        break;
                    default:
                        return pdfResult;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ClsMailBodyModel SendEmailBodyForCandidate(int TestId,string StoreProcedureName)
        {
            try
            {
                using (IDbConnection cn = Connectionmgr.connection)
                {
                    var dynamicParam = new DynamicParameters();
                    dynamicParam.Add("TestId", TestId);

                    ClsMailBodyModel ObjMailBody = cn.Query<ClsMailBodyModel>(StoreProcedureName, dynamicParam, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return ObjMailBody;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string getcandidatenamebaseontestid(int testid)
        {
            try
            {
                string fullname = string.Empty;
                using(IDbConnection cn = Connectionmgr.connection)
                {
                    fullname = cn.Query<string>("select TC.FirstName + ' ' + TC.LastName from txnCandidate TC inner join txnUserTestDetails TU on TC.UserId = TU.UserId Where TU.TestId = @TestId", new
                    {
                        TestId = testid
                    }).FirstOrDefault();
                }
                return fullname;
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
