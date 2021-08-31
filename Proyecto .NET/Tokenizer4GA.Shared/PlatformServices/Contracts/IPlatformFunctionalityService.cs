using Tokenizer4GA.Shared.Models.Information;
using Tokenizer4GA.Shared.Models.Document;
using Tokenizer4GA.Shared.PlatformServices.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.PlatformServices.Contracts
{
    public interface IPlatformFunctionalityService
    {
        event EventHandler ConnectivityChanged;
        void Loading(string message);
        void SetLoadingMessage(string message);
        void CloseLoading();
        Task DisplayAlertAsync(string message, string title = null);
        Task DisplayAlertNativeAsync(string message, string title = null);
        Task<bool> DisplayConfirmationAlertAsync(string message, string title = null,
            ConfirmationAlertType confirmationAlertType = ConfirmationAlertType.AcceptCancel);
        Task<bool> DisplayConfirmationAlertNativeAsync(string message, string title = null,
            ConfirmationAlertType confirmationAlertType = ConfirmationAlertType.AcceptCancel);
        Task<string> DisplayMenuAsync(string message, string[] options, string title = null);
        Task<string> DisplayPromptNativeAsync(string message, string title = null, string initialValue = null,
            int? maxLength = null, KeyboardType keyboardType = KeyboardType.Text);
        Task<string> DisplayPromptAsync(string message, string title = null, string initialValue = null,
            int? maxLength = null, KeyboardType keyboardType = KeyboardType.Text);
        Task ToasAlertAsync(string message, string title = null);
        Task NavigateToAsync(string resource, Dictionary<string, string> parameters = null);
        Task NavigateToWebAsync(string url);
        void NavigateToToken();
        Task NavigateToBackModal();
        void NavigateToCertificate();
        Task NavigateCloseModal();
        Task NavigateBackAsync();
        Task NavigateBackToRootAsync();
        string GetSetting(string key, string defaultValue = null);
        void SetSetting(string key, string value);
        Task<string> GetSecureDataAsync(string key, string defaultValue = null);
        Task SetSecureDataAsync(string key, string value);
        Task<DocumentSetFile> SelectCertificateAsBase64Async();
        Task<string> SaveBase64FileAsync(string name, string extension, string base64File, string pathComplement, bool overwriteFile = true);
        Task ShowEmbeddedWebsiteAsync(string title, string uri);
        string GetDevicePlatform();
        Task ShareUri(string uri, string title);
        void SetFocusFirstElement(object control);
        AppInformation GetVersion();
        bool CheckInternetConnection();
    }
}
