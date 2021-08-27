using Acr.UserDialogs;
using Tokenizer4GA.Mobile.Controls;
using Tokenizer4GA.Mobile.Pages;
using Tokenizer4GA.Shared.Localization;
using Tokenizer4GA.Shared.Logic.General;
using Tokenizer4GA.Shared.Models.Information;
using Tokenizer4GA.Shared.Models.Document;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.PlatformServices.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tokenizer4GA.Mobile.Pages.Certificate;
using Tokenizer4GA.Mobile.Pages.Login;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.PlatformServices
{
    class PlatformFunctionalityService : IPlatformFunctionalityService
    {
        public event EventHandler ConnectivityChanged;

        public PlatformFunctionalityService()
        {
            // Register for connectivity changes, be sure to unsubscribe when finished
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            //var access = e.NetworkAccess;
            //var profiles = e.ConnectionProfiles;

            ConnectivityChanged?.Invoke(this, e);
        }

        private IProgressDialog _dialog;

        public void Loading(string message)
        {
            _dialog = UserDialogs.Instance.Loading(message);
        }

        public void SetLoadingMessage(string message)
        {
            if (_dialog != null)
                _dialog.Title = message;
        }

        public void CloseLoading()
        {
            if (_dialog != null)
            {
                //_dialog.Hide();
                _dialog.Dispose();
            }
        }

        private readonly Dictionary<KeyboardType, Keyboard> _keyboards = new Dictionary<KeyboardType, Keyboard>()
        {
            { KeyboardType.Text, Keyboard.Text },
            { KeyboardType.Plain, Keyboard.Plain },
            { KeyboardType.Numeric, Keyboard.Numeric },
            { KeyboardType.Email, Keyboard.Email },
            { KeyboardType.Phone, Keyboard.Telephone }
        };

        public async Task DisplayAlertAsync(string message, string title = null) =>
            await Shell.Current.DisplayAlert(
                title ?? LocalizedStrings.Alert,
                message,
                LocalizedStrings.Accept);

        public async Task<bool> DisplayConfirmationAlertAsync(string message, string title = null,
            ConfirmationAlertType confirmationAlertType = ConfirmationAlertType.AcceptCancel) =>
            confirmationAlertType switch
            {
                ConfirmationAlertType.AcceptCancel =>
                    await Shell.Current.DisplayAlert(
                       title ?? LocalizedStrings.Alert,
                       message,
                       LocalizedStrings.Accept,
                       LocalizedStrings.Cancel),
                ConfirmationAlertType.YesNo =>
                    await Shell.Current.DisplayAlert(
                        title ?? LocalizedStrings.Alert,
                        message,
                        LocalizedStrings.Yes,
                        LocalizedStrings.No),
                _ => false,
            };

        public async Task<string> DisplayMenuNativeAsync(string message, string[] options, string title = null) =>
            await Application.Current.MainPage.DisplayActionSheet(
                message,
                LocalizedStrings.Cancel,
                null,
                options);

        public async Task<string> DisplayMenuAsync(string message, string[] options, string title = null) =>
            await Shell.Current.DisplayActionSheet(
                message,
                LocalizedStrings.Cancel,
                null,
                options);

        public async Task<string> DisplayPromptNativeAsync(string message, string title = null, string initialValue = null,
            int? maxLength = null, KeyboardType keyboardType = KeyboardType.Text) =>
            await Application.Current.MainPage.DisplayPromptAsync(title ?? LocalizedStrings.Alert, message, LocalizedStrings.Accept, LocalizedStrings.Cancel, null, maxLength ?? -1, Keyboard.Text, initialValue);

        public async Task DisplayAlertNativeAsync(string message, string title = null) =>
            await Application.Current.MainPage.DisplayAlert(
                title ?? LocalizedStrings.Alert,
                message,
                LocalizedStrings.Accept);

        public async Task<bool> DisplayConfirmationAlertNativeAsync(string message, string title = null,
            ConfirmationAlertType confirmationAlertType = ConfirmationAlertType.AcceptCancel) =>
            confirmationAlertType switch
            {
                ConfirmationAlertType.AcceptCancel =>
                    await Application.Current.MainPage.DisplayAlert(
                       title ?? LocalizedStrings.Alert,
                       message,
                       LocalizedStrings.Accept,
                       LocalizedStrings.Cancel),
                ConfirmationAlertType.YesNo =>
                    await Application.Current.MainPage.DisplayAlert(
                        title ?? LocalizedStrings.Alert,
                        message,
                        LocalizedStrings.Yes,
                        LocalizedStrings.No),
                ConfirmationAlertType.Custom =>
                    await Application.Current.MainPage.DisplayAlert(
                        title ?? LocalizedStrings.Alert,
                        message,
                        LocalizedStrings.Resend,
                        LocalizedStrings.Cancel),
                _ => false,
            };

        public async Task<string> DisplayPromptAsync(string message, string title = null, string initialValue = null,
            int? maxLength = null, KeyboardType keyboardType = KeyboardType.Text) =>
            await Shell.Current.DisplayPromptAsync(
                title ?? LocalizedStrings.Alert,
                message,
                LocalizedStrings.Accept,
                LocalizedStrings.Cancel,
                initialValue: initialValue,
                maxLength: maxLength ?? -1,
                keyboard: _keyboards[keyboardType]);

        public async Task ToasAlertAsync(string message, string title = null) =>
            await Task.Factory.StartNew(()=> UserDialogs.Instance.Toast(new ToastConfig(message)
                .SetDuration(TimeSpan.FromSeconds(9)).SetPosition(ToastPosition.Top).SetAction(x=>x.SetText("Ok").SetTextColor(Color.White)).SetBackgroundColor(Color.OrangeRed) )) ;

        public async Task NavigateToAsync(string resource, Dictionary<string, string> parameters = null)
        {
            var formattedResource = parameters == null ?
                    resource :
                    FormattersLogic.CreateResourcePathWithParameters(resource, parameters);
            if (Uri.TryCreate(formattedResource, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                    || uri.Scheme == Uri.UriSchemeHttps))
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            else
                await Shell.Current.GoToAsync(formattedResource);
        }

        public async Task NavigateToWebAsync(string url)
        {
            await Browser.OpenAsync(url, BrowserLaunchMode.External);
        }

        public void NavigateToToken() =>
            Application.Current.MainPage = new AppShell();

        public void NavigateToCertificate() =>
            Application.Current.MainPage = new CertificateSetPage();

        public void NavigateToLogin() =>
            Application.Current.MainPage = new LoginPage();

        public async Task NavigateCloseModal() =>
            await Application.Current.MainPage.Navigation.PopModalAsync();

        public async Task NavigateBackAsync() =>
            await Shell.Current.Navigation.PopAsync();

        public async Task NavigateBackToRootAsync() =>
            await Shell.Current.Navigation.PopToRootAsync();

        public string GetSetting(string key, string defaultValue = null) =>
            Preferences.Get(key, defaultValue);

        public void SetSetting(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                Preferences.Remove(key);
            else
                Preferences.Set(key, value);
        }

        public async Task<string> GetSecureDataAsync(string key, string defaultValue = null) =>
#if DEBUG
            await Task.FromResult(GetSetting(key, defaultValue));
#else
            await SecureStorage.GetAsync(key) ?? defaultValue;
#endif

        public async Task SetSecureDataAsync(string key, string value)
        {
#if DEBUG
            SetSetting(key, value);
            await Task.CompletedTask;
#else
            if (string.IsNullOrWhiteSpace(value))
                SecureStorage.Remove(key);
            else
                await SecureStorage.SetAsync(key, value);
#endif
        }

        public async Task<string> SaveBase64FileAsync(string name, string extension, string base64File, bool overwriteFile = true)
        {
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{name}.{extension}");
            if (File.Exists(filepath) && overwriteFile)
                File.Delete(filepath);

            if (!File.Exists(filepath))
                await File.WriteAllBytesAsync(filepath, Convert.FromBase64String(base64File));

            return filepath;
        }

        public async Task NavigateToBackModal() =>
            await Shell.Current.Navigation.PopModalAsync();

        public async Task ShowEmbeddedWebsiteAsync(string title, string uri) =>
            await Shell.Current.Navigation.PushAsync(new WebViewerPage(title, uri));

        public async Task ShowEmbeddedWebsiteModal(string title, string uri) =>
            await Application.Current.MainPage.Navigation.PushModalAsync(new WebViewerPage(title, uri));

        public string GetDevicePlatform() =>
            DeviceInfo.Platform.ToString();

        public async Task MakeCall(string phoneNumber)
        {
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception)
            {
                await DisplayAlertAsync(LocalizedStrings.CallError);
            }
        }

        public async Task MakeNativeCall(string phoneNumber)
        {
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception)
            {
                await DisplayAlertNativeAsync(LocalizedStrings.CallError);
            }
        }

        public async Task ShareUri(string uri, string title)
        {
            try
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Uri = uri,
                    Title = title
                });
            }
            catch (Exception)
            {
                await DisplayAlertAsync(LocalizedStrings.SharedError);
            }

        }

        public void SetFocusFirstElement(object control)
        {
            var stackAnnotations = (StackLayout)control;
            if (stackAnnotations != null && stackAnnotations.Children.Count > 0)
            {
                if (stackAnnotations.Children[0].GetType() == typeof(Entry))
                {
                    var firstEntry = (Entry)stackAnnotations.Children[0];
                    firstEntry.Focus();
                }
            }
        }

        public AppInformation GetVersion()
        {
            return new AppInformation
            {
                Version = AppInfo.VersionString,
                Build = AppInfo.Version.Build
            };
        }

        public bool CheckInternetConnection()
        {
            return !(Connectivity.NetworkAccess == NetworkAccess.None);
        }
        
        public async Task<DocumentSetFile> SelectCertificateAsBase64Async()
        {
            DocumentSetFile documentSetFile = new DocumentSetFile();
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.xml", "UTType.Xml" } },
                    { DevicePlatform.Android, new[] { "text/xml" } }
                });

                var pickResult = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = customFileType,
                    PickerTitle = "Selecciona el certificado"
                });

                if(pickResult == null) {
                    await DisplayAlertNativeAsync(LocalizedStrings.NullErrorCertificate);
                    return documentSetFile;
                }

                if (pickResult.FileName.EndsWith("xml", StringComparison.OrdinalIgnoreCase))
                {
                    documentSetFile.Description = pickResult.FileName;
                    documentSetFile.Url = pickResult.FullPath;
                    documentSetFile.Type = DocumentSetFileType.Xml;
                    var stream = await pickResult.OpenReadAsync();
                    byte[] bytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                    }
                    documentSetFile.Base64 = Convert.ToBase64String(bytes);
                }
                else
                {
                    await DisplayAlertNativeAsync(LocalizedStrings.ExtensionErrorCertificate);
                }
            }
            catch (Exception)
            {
                //ignored
            }
            return documentSetFile;
        }

        ~PlatformFunctionalityService()
        {
            // Unsubscribe when finished
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
        
        private async Task<bool> CheckAndRequestPermissionAsync<T>(T permission)
            where T : Permissions.BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status == PermissionStatus.Granted;
        }
    }
}
