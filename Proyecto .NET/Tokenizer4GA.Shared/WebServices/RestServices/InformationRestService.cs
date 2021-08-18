using Tokenizer4GA.Shared.PlatformServices.ServiceEnviroment;
using Tokenizer4GA.Shared.WebServices.Contracts;
using Tokenizer4GA.Shared.PlatformServices.Contracts;

namespace Tokenizer4GA.Shared.WebServices.RestServices
{
    public class InformationRestService : BaseRestService, IInformationService
    {
        protected override string Server => EnviromentManager.Configurations.InformationService;
        private readonly IPlatformFunctionalityService _platformFunctionality;
        public InformationRestService(IPlatformFunctionalityService platformFunctionality)
        {
            _platformFunctionality = platformFunctionality;
        }
    }
}
