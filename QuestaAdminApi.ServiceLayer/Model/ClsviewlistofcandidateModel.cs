

namespace QuestaAdminApi.ServiceLayer.Model
{
    public class ClsviewlistofcandidateModel
    {
        public int TestId { get; set; }
        public string NameOfCandidate { get; set; } = string.Empty;
        public string NameOfAssessment { get; set; } = string.Empty;
        public string CampanyName { get; set; } = string.Empty;
        public string HrName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
