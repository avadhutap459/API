

using QuestaAdminApi.ServiceLayer.Model;

namespace QuestaAdminApi.ServiceLayer
{
    public interface IMailSender
    {
        void Dispose();
        Tuple<ClsMailBodyModel, ClsMailBodyModel> SendFinalEmail(int TestId, int ModuleId);
    }
}
