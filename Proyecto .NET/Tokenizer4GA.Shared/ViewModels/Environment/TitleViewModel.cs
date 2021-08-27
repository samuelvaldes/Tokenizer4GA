using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.Services.Sqlite;

namespace Tokenizer4GA.Shared.ViewModels.Environment
{
    public class TitleViewModel : PageViewModel
    {

        public TitleViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality,
            IPathService pathService)
            : base(requestManager, commandFactory, platformFunctionality, pathService)
        {}

        protected override void Initialize()
        {
        }
    }
}
