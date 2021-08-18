using Tokenizer4GA.Shared.PlatformServices.Contracts;

namespace Tokenizer4GA.Shared.ViewModels.General
{
    public class WebViewerViewModel : PageViewModel
    {
        public WebViewerViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality) 
            : base(requestManager, commandFactory, platformFunctionality)
        {}
        
        protected override void Initialize() {}
    }
}