using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.PlatformServices.Enums;
using Tokenizer4GA.Shared.PlatformServices.ServiceEnviroment;
using Tokenizer4GA.Shared.WebServices.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Data;
using Tokenizer4GA.Shared.JobServices.Sync;
using Tokenizer4GA.Shared.Localization;
using Tokenizer4GA.Shared.Logic.General;
using Tokenizer4GA.Shared.Services.Sqlite;

namespace Tokenizer4GA.Shared.ViewModels.Login
{
    public partial class LoginViewModel : PageViewModel
    {
        public LoginViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality,
            IProfileService profile,
            IDeviceService deviceService,
            ISyncInformation syncInformation,
            IDbContext ctx,
            IPathService pathService)
            : base(requestManager, commandFactory, platformFunctionality, deviceService, pathService)
        {
            _deviceService = deviceService;
            _profile = profile;
            _syncInformation = syncInformation;
            _ctx = ctx;

            _ = GetLoginBinnacle();

            var email = GetSetting(nameof(Email), Email);
            if (!string.IsNullOrEmpty(email))
                Email = email;

            switch (EnviromentManager.Environment)
            {
                case Environments.Dev:
                    VersionEnvironment = $"V{_deviceService.GetApplicationVersion()}p";
                    break;
                case Environments.Qa:
                    VersionEnvironment = $"V{_deviceService.GetApplicationVersion()}p";
                    break;
                case Environments.Prod:
                    VersionEnvironment = $"V{_deviceService.GetApplicationVersion()}";
                    break;
                default:
                    break;
            }
        }

        private async Task GetLoginBinnacle()
        {
            if (StorageKeys.IsStorageUser == "IsStorageUser")
            {
                var loginBinnacleList = await _ctx.AppLogin.Get();

                if (loginBinnacleList.Any())
                {
                    if (loginBinnacleList.LastOrDefault().LastLogin.Date < DateTime.Now.Date)
                    {
                        Email = loginBinnacleList.LastOrDefault().Username;
                        Password = loginBinnacleList.LastOrDefault().Password;
                        await Login();
                    }
                    else
                    {
                        await Login();
                    }
                }
            }
        }

        protected override void Initialize()
        {
            LoginCommand = CreateCommand(async () => await Login(), ValidateFields);
        }

        private async Task Login()
        {
            var login = new Models.Login.Login
            {
                Email = Email.ToLower(),
                Password = Password
            };

            if (!CheckInternetConnection())
            {
                await DisplayAlertNativeAsync(LocalizedStrings.NoInternetLogin);
                return;
            }

            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)) return;

            var loginResponse =
                await ExecuteRequest(
                    _profile.LoginAsync(await CreateBaseRequest(login, _deviceService.GetApplicationVersion())));
            if (!loginResponse.Success)
            {
                switch (loginResponse.ResponseCode)
                {
                    case 408:
                    case 500:
                    case 777:
                        await DisplayAlertNativeAsync(
                            FormattersLogic.CreateCompleteErrorMessage(LocalizedStrings.LoginError, loginResponse));
                        break;
                    default:
                        await DisplayAlertNativeAsync(FormattersLogic.CreateCompleteErrorMessage("", loginResponse));
                        break;
                }

                return;
            }

            if (loginResponse.Data == null)
            {
                await DisplayAlertNativeAsync(LocalizedStrings.LoginError);
                return;
            }

            if (!loginResponse.Data.EmailConfirmed)
            {
                if (loginResponse.Data.Blocked)
                {
                    SetSetting(StorageKeys.FcmTokenPendingUpload, true.ToString());
                    await SetSecureData(StorageKeys.UserId, null);
                    var deviceGuid = await SetupDeviceGuid();
                    return;
                }
            }

            if (IsStorage) await SetSecureData(StorageKeys.IsStorageUser, "IsStorageUser");
            else await SetSecureData(StorageKeys.IsStorageUser, null);

            await SetSecureData(StorageKeys.MustChangePassword, null);
            SetSetting(nameof(Email), Email);
            SetSetting(StorageKeys.FcmTokenPendingUpload, true.ToString());
            await SetSecureData(StorageKeys.UserId, loginResponse.Data.UserId.ToString());

            await _syncInformation.SyncAll(await CreateBaseRequest());

            await _ctx.AppLogin.Insert(new Data.Models.AppLogin
            {
                Username = Email.ToLower(),
                Password = Password,
                Token = loginResponse.BearerToken,
                LastLogin = DateTime.Now
            });
            NavigateToCertificate();
        }

        private bool ValidateFields() =>
            EmailIsValid == true
            && PasswordIsValid == true;
    }
}