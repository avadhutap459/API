using QuestaAdminApi.ServiceLayer.Model;

namespace QuestaAdminApi.ServiceLayer
{
    public interface ICandidateDetails
    {
        List<ClsviewlistofcandidateModel> Getlistofcandidate();
        void Updatecandidatestatus(int TestId, bool flg);
        void Dispose();
    }
}
