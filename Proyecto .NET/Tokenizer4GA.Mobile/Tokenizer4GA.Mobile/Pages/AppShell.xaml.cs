using Tokenizer4GA.Mobile.Controls;
using Tokenizer4GA.Mobile.Pages.Token;
using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Logic.General;
using Tokenizer4GA.Shared.ViewModels;
using Tokenizer4GA.Shared.ViewModels.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokenizer4GA.Mobile.Pages.Certificate;
using Tokenizer4GA.Mobile.Pages.Login;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Pages
{
    public partial class AppShell
    {
        private static readonly Dictionary<string, Type> _pagesByLocation = new Dictionary<string, Type>
        {
            { Locations.TokenPage, typeof(TokenPage) },
            { Locations.CertificateSetPage, typeof(CertificateSetPage) },
            { Locations.LoginPage, typeof(LoginPage) }
        };

        private static readonly Type[] _dataLossSensitivePages = {
            typeof(CertificateSetPage)
        };

        private static readonly Type[] _sessionRequiredPages = {
            typeof(TokenPage)
        };

        private static readonly Type[] _noHistoryPages = {
            typeof(CertificateSetPage),
            typeof(LoginPage)
        };

        private static readonly Type[] _paginationPages = {
            typeof(TokenPage)
        };

        private readonly AppShellViewModel _vm;

        public AppShell()
        {
            InitializeComponent();
            _vm = ViewModelLocator.Instance.Resolve<AppShellViewModel>();
            BindingContext = _vm;
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            foreach (var resourceTypePair in _pagesByLocation)
                Routing.RegisterRoute(resourceTypePair.Key, resourceTypePair.Value);
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            if (Navigation.NavigationStack.Count > 1
                && (args.Source == ShellNavigationSource.Push
                    || args.Source == ShellNavigationSource.ShellSectionChanged))
            {
                var previousPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                if (previousPage != null)
                {
                    var previousPageType = previousPage.GetType();
                    if (_noHistoryPages.Contains(previousPageType)
                        && previousPageType != typeof(CertificateSetPage))//TODO Validar que exista certificado o no
                        Navigation.RemovePage(previousPage);
                }
            }
            base.OnNavigated(args);
        }

        protected override async void OnNavigating(ShellNavigatingEventArgs args)
        {
            if (args.CanCancel)
                if ((args.Source == ShellNavigationSource.Push
                    || args.Source == ShellNavigationSource.Unknown)
                    && args.Target != null
                    && args.Target.Location != null
                    && !string.IsNullOrWhiteSpace(args.Target.Location.OriginalString))
                {
                    var resourcePath = args.Target.Location.OriginalString;
                    var targetPage = FormattersLogic.GetResourcePathWithoutParameters(resourcePath);
                    if (targetPage != null
                        && _pagesByLocation.ContainsKey(targetPage))
                    {
                        var pageType = _pagesByLocation[targetPage];
                        if (_sessionRequiredPages.Contains(pageType))//TODO Validar que exista certificado o no
                        {
                            var parameters = new Dictionary<string, string>
                            {
                                { Locations.TargetPage, targetPage }
                            };

                            var originalParameters = FormattersLogic.GetResourcePathParameters(resourcePath);
                            if (originalParameters != null)
                                parameters.Add(Locations.TargetPageParameters, originalParameters);

                            args.Cancel();
                            await Current.GoToAsync(FormattersLogic.CreateResourcePathWithParameters(Locations.CertificateSetPage, parameters));
                        }
                    }
                }
                else if (args.Source == ShellNavigationSource.Pop)
                {
                    var lastPage = Navigation.NavigationStack.Last();

                    if (lastPage == null)
                        lastPage = (Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

                    if (lastPage != null)
                    {
                        if (lastPage is WebViewerPage webViewerPage
                            && webViewerPage.WebViewCanGoBack)
                        {
                            webViewerPage.WebViewGoBack();
                            args.Cancel();
                        }

                        if (!args.Cancelled
                            && _paginationPages.Contains(lastPage.GetType())
                            && lastPage.BindingContext is PageWithPaginationViewModel lastPageWithPaginationViewModel
                            && !lastPageWithPaginationViewModel.FirstPage)
                        {
                            _ = lastPageWithPaginationViewModel.PreviousPage();
                            args.Cancel();
                        }

                        if (!args.Cancelled
                            && _dataLossSensitivePages.Contains(lastPage.GetType())
                            && lastPage.BindingContext is PageViewModel lastPageViewModel
                            && !lastPageViewModel.ConfirmedGoingBack)
                        {
                            _ = lastPageViewModel.NavigateBackWithConfirmation();
                            args.Cancel();
                        }
                    }
                }
            base.OnNavigating(args);
        }
    }
}
