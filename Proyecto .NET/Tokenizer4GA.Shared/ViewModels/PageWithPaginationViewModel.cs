using Tokenizer4GA.Shared.PlatformServices.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tokenizer4GA.Shared.ViewModels
{
    public abstract class PageWithPaginationViewModel : PageViewModel
    {
        public PageWithPaginationViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality)
            : base(requestManager, commandFactory, platformFunctionality)
        {
        }

        public PageWithPaginationViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality,
            IDeviceService deviceService)
            : base(requestManager, commandFactory, platformFunctionality, deviceService)
        {
        }

        public abstract Task PreviousPage();

        public abstract Task NextPage();

        public override async Task NavigateBack()
        {
            if (Page > 1)
                Page = 1;
            await base.NavigateBack();
        }

        public override async Task NavigateBackToRoot()
        {
            if (Page > 1)
                Page = 1;
            await base.NavigateBack();
        }

        private int _page;

        public int Page
        {
            get => _page;
            protected set
            {
                if (_page == value)
                    return;
                _page = value;
                OnPropertyChanged();

                FirstPage = value == 1;
                LastPage = value >= TotalPages;
            }
        }

        private int _itemsPerPage;

        public int ItemsPerPage
        {
            get => _itemsPerPage;
            protected set
            {
                if (_itemsPerPage == value)
                    return;
                _itemsPerPage = value;
                OnPropertyChanged();
            }
        }

        private int _totalPages;

        public int TotalPages
        {
            get => _totalPages;
            protected set
            {
                if (_totalPages == value)
                    return;
                _totalPages = value;
                OnPropertyChanged();

                LastPage = Page >= value;
            }
        }

        private bool _firstPage = true;

        public bool FirstPage
        {
            get => _firstPage;
            private set
            {
                if (_firstPage == value)
                    return;
                _firstPage = value;
                OnPropertyChanged();
            }
        }

        private bool _lastPage = true;

        public bool LastPage
        {
            get => _lastPage;
            private set
            {
                if (_lastPage == value)
                    return;
                _lastPage = value;
                OnPropertyChanged();
            }
        }

        public ICommand PreviousPageCommand { get; protected set; }

        public ICommand NextPageCommand { get; protected set; }
    }
}
