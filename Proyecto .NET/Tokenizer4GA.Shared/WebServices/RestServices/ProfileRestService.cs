using System.Threading.Tasks;
using Tokenizer4GA.Shared.Models.Login;
using Tokenizer4GA.Shared.PlatformServices.ServiceEnviroment;
using Tokenizer4GA.Shared.WebServices.Contracts;
using Tokenizer4GA.Shared.PlatformServices.Contracts;

namespace Tokenizer4GA.Shared.WebServices.RestServices
{
    public class ProfileRestService : BaseRestService, IProfileService
    {
        protected override string Server => EnviromentManager.Configurations.ProfileService;

        public async Task<BaseResponse<Access>> LoginAsync(BaseRequest<Login> loginRequest)
        {
            var response = await PostAsync<Access, Login>($"{Server}Login", loginRequest);
            
            return response;
        }
    }
}
