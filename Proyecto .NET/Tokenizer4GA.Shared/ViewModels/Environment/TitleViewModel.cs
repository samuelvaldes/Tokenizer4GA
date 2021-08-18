using Tokenizer4GA.Shared.PlatformServices.Contracts;

namespace Tokenizer4GA.Shared.ViewModels.Environment
{
    public class TitleViewModel : PageViewModel
    {

        public TitleViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality)
            : base(requestManager, commandFactory, platformFunctionality)
        {}

        protected override void Initialize()
        {
        }
    }
}
