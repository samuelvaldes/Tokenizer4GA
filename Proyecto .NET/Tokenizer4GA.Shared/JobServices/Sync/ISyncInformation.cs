using Tokenizer4GA.Shared.Models.Information;
using Tokenizer4GA.Shared.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
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
