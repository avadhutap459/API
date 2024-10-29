using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestaAdminApi.ServiceLayer
{
    public class ClsMasterModel
    {
    }

    public class ClsAssessmentModel
    {
        public int AssessmentId { get; set; }
        public string AssessmentName { get; set; } = string.Empty;
        public string CreateBy { get; set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public int TotalQuestion { get; set; }
    }

    public class ClsHumanResouceModel
    {
        public int HrId { get; set; }
        public string HrName { get; set; } = string.Empty;
        public string HrEmail { get; set; } = string.Empty;
        public string HrPhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class ClsCompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class ClsMailConfigByAssessmentModel
    {
        public int MailConfigId { get; set; }
        public string MailType { get; set; } = string.Empty;
        public string MailConfigName { get; set; } = string.Empty;
        public int AssessmentId { get; set; }
        public int MailTemplateId { get; set; }
    }
}
