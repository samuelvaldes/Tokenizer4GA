using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Localization;
using Tokenizer4GA.Shared.Models.Information;
using Tokenizer4GA.Shared.Models.Document;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.PlatformServices.Enums;
using Tokenizer4GA.Shared.WebServices;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Tokenizer4GA.Shared.Services.Sqlite;

namespace Tokenizer4GA.Shared.ViewModels
{
    public abstract class PageViewModel : BaseViewModel
    {
        private readonly IRequestManagerService _requestManager;
        private readonly ICommandFactoryService _commandFactory;
        private readonly IPlatformFunctionalityService _platformFunctionality;
        private readonly IDeviceService _deviceService;
        private readonly IPathService _pathService;

        public bool ConfirmedGoingBack { get; set; } = true;

        public PageViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality,
            IPathService pathService)
        {
            _requestManager = requestManager;
            _commandFactory = commandFactory;
            _platformFunctionality = platformFunctionality;
            _pathService = pathService;

            Initialize();

            // Register for connectivity changes, be sure to unsubscribe when finished
            _platformFunctionality.ConnectivityChanged += PlatformFunctionality_ConnectivityChanged;
        }

        public PageViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality,
            IDeviceService deviceService,
            IPathService pathService)
        {
            _requestManager = requestManager;
            _commandFactory = commandFactory;
            _platformFunctionality = platformFunctionality;
            _deviceService = deviceService;
            _pathService = pathService;

            Initialize();

            // Register for connectivity changes, be sure to unsubscribe when finished
            _platformFunctionality.ConnectivityChanged += PlatformFunctionality_ConnectivityChanged;
        }

        private void PlatformFunctionality_ConnectivityChanged(object sender, EventArgs e)
        {
            IsNotConnected = !CheckInternetConnection();
        }

        protected abstract void Initialize();

        public override void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (ConfirmedGoingBack)
                ConfirmedGoingBack = false;
            base.OnPropertyChanged(name);
        }

        public async Task<bool> IsStorageUser() =>
            !string.IsNullOrWhiteSpace(await GetSecureData(StorageKeys.IsStorageUser));

        public bool existCertificateValidated() =>
            _pathService.ExistCertificate();

        public async Task NavigateBackWithConfirmation()
        {
            ConfirmedGoingBack = await DisplayConfirmationAlert(LocalizedStrings.PreventDataLossBackConfirmation);
            if (ConfirmedGoingBack)
                await NavigateBack();
        }

        public virtual async Task<BaseRequest> CreateBaseRequest()
        {
            var request = _requestManager.CreateBaseRequest();
            SetAppVersionResponse(request);
            await SetBearerTokenToRequest(request);
            return request;
        }

        public virtual async Task<BaseRequest<TData>> CreateBaseRequest<TData>(TData data)
        {
            var request = _requestManager.CreateBaseRequest(data);
            SetAppVersionResponse(request);
            await SetBearerTokenToRequest(request);
            return request;
        }

        public virtual async Task<BaseRequest<TData>> CreateBaseRequest<TData>(TData data, string appVersion)
        {
            var request = _requestManager.CreateBaseRequest(data, appVersion);
            await SetBearerTokenToRequest(request);
            return request;
        }

        public virtual async Task<BaseResponse> ExecuteRequest(Task<BaseResponse> request, bool disableLoader = false)
        {
            var response = await _requestManager.ExecuteRequestAsync(request, disableLoader);
            if (response.Success)
                await SaveBearerTokenFromResponse(response);
            return response;
        }

        public virtual async Task<BaseResponse<TData>> ExecuteRequest<TData>(Task<BaseResponse<TData>> request, bool disableLoader = false)
        {
            var response = await _requestManager.ExecuteRequestAsync(request, disableLoader);
            if (response.Success)
                await SaveBearerTokenFromResponse(response);
            return response;
        }

        public virtual ICommand CreateCommand(Action execute) =>
            _commandFactory.Create(execute);

        public virtual ICommand CreateCommand<T>(Action<T> execute) =>
            _commandFactory.Create(execute);

        public virtual ICommand CreateCommand(Action execute, Func<bool> canExecute) =>
            _commandFactory.Create(execute, canExecute);

        public virtual ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> canExecute) =>
            _commandFactory.Create(execute, canExecute);

        public virtual void NotifyOfCommandAvailability(ICommand command) =>
            _commandFactory.NotifyOfCommandAvailability(command);

        public virtual async Task DisplayAlert(string message, string title = null) =>
            await _platformFunctionality.DisplayAlertAsync(message, title);
        public virtual async Task DisplayAlertNativeAsync(string message, string title = null) =>
            await _platformFunctionality.DisplayAlertNativeAsync(message, title);

        public virtual async Task<bool> DisplayConfirmationAlert(string message, string title = null,
            ConfirmationAlertType confirmationAlertType = ConfirmationAlertType.AcceptCancel) =>
            await _platformFunctionality.DisplayConfirmationAlertAsync(message, title, confirmationAlertType);

        public virtual async Task<bool> DisplayConfirmationNativeAlertAsync(string message, string title = null,
            ConfirmationAlertType confirmationAlertType = ConfirmationAlertType.AcceptCancel) =>
            await _platformFunctionality.DisplayConfirmationAlertNativeAsync(message, title, confirmationAlertType);

        public virtual async Task<string> DisplayMenu(string message, string[] options, string title = null) =>
            await _platformFunctionality.DisplayMenuAsync(message, options, title);

        public virtual async Task<string> DisplayPrompt(string message, string title = null, string initialValue = null,
            int? maxLength = null, KeyboardType keyboardType = KeyboardType.Text) =>
            await _platformFunctionality.DisplayPromptAsync(message, initialValue, title, maxLength, keyboardType);

        public virtual async Task ToasAlertAsync(string message, string title = null) =>
            await _platformFunctionality.ToasAlertAsync(message, title);

        public virtual async Task<string> DisplayPromptNativeAsync(string message, string title = null, string initialValue = null,
            int? maxLength = null, KeyboardType keyboardType = KeyboardType.Text) =>
            await _platformFunctionality.DisplayPromptNativeAsync(message, initialValue, title, maxLength, keyboardType);

        public virtual async Task NavigateTo(string resource, Dictionary<string, string> parameters = null) =>
            await _platformFunctionality.NavigateToAsync(resource, parameters);

        public virtual async Task NavigateWebTo(string url) =>
            await _platformFunctionality.NavigateToWebAsync(url);

        public virtual void NavigateToToken() =>
            _platformFunctionality.NavigateToToken();

        public virtual void NavigateToCertificate() =>
            _platformFunctionality.NavigateToCertificate();

        public virtual async Task NavigateCloseModal() =>
            await _platformFunctionality.NavigateCloseModal();
        public virtual async Task NavigateToBackModal() =>
            await _platformFunctionality.NavigateToBackModal();

        public virtual async Task NavigateBack()
        {
            if (!ConfirmedGoingBack)
                ConfirmedGoingBack = true;
            await _platformFunctionality.NavigateBackAsync();
        }

        public virtual async Task NavigateBackToRoot()
        {
            if (!ConfirmedGoingBack)
                ConfirmedGoingBack = true;
            await _platformFunctionality.NavigateBackToRootAsync();
        }

        public virtual string GetSetting(string key, string defaultValue = null) =>
            _platformFunctionality.GetSetting(key, defaultValue);

        public virtual void SetSetting(string key, string value) =>
            _platformFunctionality.SetSetting(key, value);

        public virtual async Task<string> GetSecureData(string key, string defaultValue = null) =>
            await _platformFunctionality.GetSecureDataAsync(key, defaultValue);

        public virtual async Task SetSecureData(string key, string value) =>
            await _platformFunctionality.SetSecureDataAsync(key, value);

        public virtual async Task<DocumentSetFile> SelectCertificate() =>
            await _platformFunctionality.SelectCertificateAsBase64Async();

        public virtual async Task<string> SaveBase64File(string name, string extension, string base64File, string pathComplement, bool overwriteFile = true) =>
            await _platformFunctionality.SaveBase64FileAsync(name, extension, base64File, pathComplement, overwriteFile);

        public virtual async Task ShowEmbeddedWebsite(string title, string uri) =>
            await _platformFunctionality.ShowEmbeddedWebsiteAsync(title, uri);

        public virtual string GetDevicePlatform() =>
            _platformFunctionality.GetDevicePlatform();

        private async Task SetBearerTokenToRequest(BaseRequest request) =>
            request.BearerToken = await _platformFunctionality.GetSecureDataAsync(StorageKeys.BearerToken);

        private async Task SaveBearerTokenFromResponse(BaseResponse response) =>
            await _platformFunctionality.SetSecureDataAsync(StorageKeys.BearerToken, response.BearerToken);

        private void SetAppVersionResponse(BaseRequest request) =>
            request.AppVersion = _deviceService?.GetApplicationVersion();

        public async Task ShareUri(string uri, string title) => await _platformFunctionality.ShareUri(uri, title);
        public void SetFocusFirstElement(object control) => _platformFunctionality.SetFocusFirstElement(control);
        public AppInformation GetVersionAsync() => _platformFunctionality.GetVersion();
        public bool CheckInternetConnection() => _platformFunctionality.CheckInternetConnection();
        protected async Task<string> SetupDeviceGuid()
        {
            var guid = await GetSecureData(StorageKeys.DeviceGuid);
            if (string.IsNullOrWhiteSpace(guid))
            {
                guid = Guid.NewGuid().ToString();
                await SetSecureData(StorageKeys.DeviceGuid, guid);
            }
            return guid;
        }

        ~PageViewModel() { _platformFunctionality.ConnectivityChanged -= PlatformFunctionality_ConnectivityChanged; }
    }
}
