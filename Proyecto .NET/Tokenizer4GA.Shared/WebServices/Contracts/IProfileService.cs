using System.Threading.Tasks;
using Tokenizer4GA.Shared.Models.Login;

namespace Tokenizer4GA.Shared.WebServices.Contracts
{
    public interface IProfileService
    {
        Task<BaseResponse<Access>> LoginAsync(BaseRequest<Login> loginRequest);
    }
}
