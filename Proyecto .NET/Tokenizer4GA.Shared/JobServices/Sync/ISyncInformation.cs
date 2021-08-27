using Tokenizer4GA.Shared.WebServices;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.JobServices.Sync
{
   public interface ISyncInformation
    {
        Task<BaseResponse> SyncAll(BaseRequest request);

        //Menu
        Task<bool> JobHomeMenusAsync();
    }
}
