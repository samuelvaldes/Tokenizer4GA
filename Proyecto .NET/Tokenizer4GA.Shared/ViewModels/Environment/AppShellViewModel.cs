using Tokenizer4GA.Shared.PlatformServices.Contracts;

namespace Tokenizer4GA.Shared.ViewModels.Environment
{
    public class AppShellViewModel : PageViewModel
    {
        
        public AppShellViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality)
            : base(requestManager, commandFactory, platformFunctionality)
        {
            _ = SetupDeviceGuid();
        }

        protected override void Initialize()
        {
        }
        
    }
}
