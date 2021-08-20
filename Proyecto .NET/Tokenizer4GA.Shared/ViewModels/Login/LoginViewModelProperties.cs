using Tokenizer4GA.Shared.PlatformServices.Contracts;
using System.Windows.Input;
using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Data;
using Tokenizer4GA.Shared.JobServices.Sync;
using Tokenizer4GA.Shared.Logic.General;
using Tokenizer4GA.Shared.WebServices.Contracts;

namespace Tokenizer4GA.Shared.ViewModels.Login
{
    public partial class LoginViewModel
    {
        private readonly IProfileService _profile;
        private readonly IDeviceService _deviceService;
        private readonly ISyncInformation _syncInformation;
        private readonly IDbContext _ctx;

        public string TargetPage { private get; set; } = Locations.TokenPage;

        public string TargetPageParameters { private get; set; }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value)
                    return;
                _email = value;
                OnPropertyChanged();

                EmailIsValid = !string.IsNullOrWhiteSpace(value)
                    && ValidatorsLogic.ValidateEmail(value);
            }
        }
        
        private bool? _emailIsValid;
        public bool? EmailIsValid
        {
            get => _emailIsValid;
            private set
            {
                if (_emailIsValid == value)
                    return;
                _emailIsValid = value;
                OnPropertyChanged();

                NotifyOfCommandAvailability(LoginCommand);
            }
        }
        
        private string _password;
        
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnPropertyChanged();
                PasswordIsValid = !string.IsNullOrWhiteSpace(value)
                    && ValidatorsLogic.ValidateExtendedAsciiCharacters(value)
                    && ValidatorsLogic.ValidatePasswordLogin(value);
            }
        }
        
        private bool? _passwordIsValid;
        
        public bool? PasswordIsValid
        {
            get => _passwordIsValid;
            private set
            {
                if (_passwordIsValid == value)
                    return;
                _passwordIsValid = value;
                OnPropertyChanged();

                NotifyOfCommandAvailability(LoginCommand);
            }
        }
        
        private bool _isStorage = true;
        
        public bool IsStorage
        {
            get => _isStorage;
            set
            {
                if (_isStorage == value)
                    return;
                _isStorage = value;
                OnPropertyChanged();
            }
        }
        
        private string _versionEnvironment;
        public string VersionEnvironment
        {
            get => _versionEnvironment;
            set
            {
                if (_versionEnvironment == value)
                    return;
                _versionEnvironment = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand LoginCommand { get; private set; }
    }
}