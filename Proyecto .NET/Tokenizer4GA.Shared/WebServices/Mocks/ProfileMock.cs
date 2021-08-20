using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Models.Information;
using Tokenizer4GA.Shared.WebServices.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Tokenizer4GA.Shared.Models.Login;

namespace Tokenizer4GA.Shared.WebServices.Mocks
{
    public class ProfileMock : IProfileService
    {
        Task<BaseResponse<Access>> IProfileService.LoginAsync(BaseRequest<Login> loginRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
