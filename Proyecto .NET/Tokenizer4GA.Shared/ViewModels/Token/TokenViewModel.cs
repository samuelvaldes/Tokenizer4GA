using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.PlatformServices.Enums;
using Tokenizer4GA.Shared.PlatformServices.ServiceEnviroment;
using Tokenizer4GA.Shared.WebServices.Contracts;
using OtpNet;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Tokenizer4GA.Shared.Services.Sqlite;
using Message;

namespace Tokenizer4GA.Shared.ViewModels.Token
{
    public class TokenViewModel : PageViewModel
    {
        private const int DefaultIntervalOtp = 30;
        private readonly IProfileService _profile;
        private readonly IDeviceService _deviceService;
        private readonly Timer timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds); // se ejecutara cada segundo

        private Totp _otp;

        public TokenViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality,
            IProfileService profile,
            IDeviceService deviceService, IPathService pathService)
            : base(requestManager, commandFactory, platformFunctionality, deviceService, pathService)
        {
            _profile = profile;
            _deviceService = deviceService;

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

            //TODO temp
            ValidateCertificate();
        }

        protected override void Initialize()
        {
            RefreshCommand = CreateCommand(async () => await Refresh());

            InitOtp();
        }

        private void InitOtp()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            //var base32String = Base32Encoding.ToString(key);
            //var base32Bytes = Base32Encoding.ToBytes(base32String);
            _otp = new Totp(key);
            GenerateToken();
            IsValidOtpToken();
            timer.Elapsed += OnTimedEvent;
        }

        private bool IsValidOtpToken()
        {
            var window = new VerificationWindow(previous: 1, future: 1);
            var isValidOtpKey = _otp.VerifyTotp(_totpCode, out _timeWindowUsed, window: window);
            Debug.WriteLine($"isValidOtpKey :: {isValidOtpKey}");
            Debug.WriteLine($"timeWindowUsed :: {_timeWindowUsed}");
            return isValidOtpKey;
        }

        private void GenerateToken()
        {
            TotpCode = _otp.ComputeTotp();
            RemainingTime = _otp.RemainingSeconds();
            timer.Enabled = true;
            Debug.WriteLine($"totpCode :: {_totpCode}");
            Debug.WriteLine($"remainingTime :: {_remainingTime}");
        }
        
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            timer.Enabled = RemainingTime > 1;
            if(!timer.Enabled)
                GenerateToken();

            RemainingTime = _otp.RemainingSeconds();
        }

        private Task Refresh()
        {
            switch (EnviromentManager.Environment)
            {
                case Environments.Dev:
                    VersionEnvironment = $"V{_deviceService.GetApplicationVersion()}d";
                    break;
                case Environments.Qa:
                    VersionEnvironment = $"V{_deviceService.GetApplicationVersion()}d";
                    break;
                case Environments.Prod:
                    VersionEnvironment = $"V{_deviceService.GetApplicationVersion()}p";
                    break;
                default:
                    break;
            }

            IsRefreshing = false;

            return Task.CompletedTask;
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

        private bool _isRefreshing = true;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        private int _remainingTime = DefaultIntervalOtp;
        public int RemainingTime
        {
            get => _remainingTime;
            set
            {
                if (_remainingTime == value)
                    return;

                _remainingTime = value;
                OnPropertyChanged();
            }
        }

        private long _timeWindowUsed;
        public long TimeWindowUsed
        {
            get => _timeWindowUsed;
            set
            {
                if (_timeWindowUsed == value)
                    return;
                _timeWindowUsed = value;
                OnPropertyChanged();
            }
        }

        private string _totpCode;
        public string TotpCode
        {
            get => _totpCode;
            set
            {
                if (_totpCode == value)
                    return;
                _totpCode = value;
                OnPropertyChanged();
            }
        }

        private void ValidateCertificate()
        {
            var encryptedMessage = new EncryptedMessage
            {
                SymmetricKeyEncryptedBase64 = "Me+hX8E1eam5Oej2w3Naa2dvO3teCN7ESAaRB96mSxp0y8+uqSywiehLlM1u+Kkwcw7rM4fzJaCeX/gLLeXIpFBTUgZyQT01TRDc8/aqyIIivzRQz2powUYdi0SeXoQKq1tuP+TGcc18vpU4wnGgR8H1k4ZWUQs8xSga8Bnb0mqzH1YaDkgcNCMeytpbdOs4hfhpRH7u/SMq0lTROdp4mj0xRukKkYkc4ksagSBVOQHav2kb78TbRRrYQ3/G4c1vs6fmofvVo8B9Lz7ZGc2qEUH3g0avxMi976mTSjv11fcRSdQ5W9l9bwKXb6zkNLTu/YHbyRmVVY+oqfrJ3gw5EA==",
                InitializationVectorBase64 = "IVYCdBbALiVJdWmhkeRlYw==",
                CipherTextBase64 = "hHhKp3yMNesa2vX+2up2yLH/kWkUjncG+yEOruk+ow1uFnVgmh5pTrqKBzqJ+qgZKPHb2p8XtZVhc7ZTC9kJSE5RxJP2wp4GZNz/5EV61t8=",
                AsymmetricKeyId = Guid.Parse("011d6063-0f7d-41ee-a93f-6be779824388")
            };
            
            //TODO Implementar lógina para integrar proyecto MESSAGE
            //var cryptoHelper = new CryptoHelper(null,
                //encryptedMessage.SymmetricKeyEncryptedBase64);
            
            //(ISymmetricEncryptionService symmetricEncryptionService,
            //    IXmlBasedAsymmetricEncryptionService xmlBasedAsymmetricEncryptionService)
        }

        public ICommand RefreshCommand { get; private set; }
    }
}