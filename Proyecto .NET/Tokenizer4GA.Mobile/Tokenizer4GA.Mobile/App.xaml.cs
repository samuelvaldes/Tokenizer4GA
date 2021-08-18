using Tokenizer4GA.Mobile.Pages.Certificate;
using Tokenizer4GA.Shared.ViewModels.Environment;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using Tokenizer4GA.Mobile.Pages;
using Xamarin.Forms;
using Tokenizer4GA.Shared.JobServices;
using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Device = Xamarin.Forms.Device;

namespace Tokenizer4GA.Mobile
{
    public partial class App : Application
    {
        private readonly AppShellViewModel _vm;
        private readonly Shared.JobServices.Sync.ISyncInformation _job;

        public App()
        {
            Device.SetFlags(new []{ "AppTheme_Experimental" });
            Current.UserAppTheme = OSAppTheme.Light;

            InitializeComponent();
            AndroidSpecific.Application.SetWindowSoftInputModeAdjust(this, AndroidSpecific.WindowSoftInputModeAdjust.Resize);
            _vm = ViewModelLocator.Instance.Resolve<AppShellViewModel>();
            
            //TODO Validar si el certificado ya existe en el dispositivo
            //TODO ubtener del storage la huella digital que debió guardarse cuando se cargó el certificado
            if (_vm.IsStorageUser().Result)
                MainPage = new AppShell();
            else
                MainPage = new CertificateSetPage();

            
            CrossMedia.Current.Initialize();

            //Init Jobs Sync
            _job = ViewModelLocator.Instance.ResolveSyncContextJob<Shared.JobServices.Sync.ISyncInformation>();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=2bbfe22a-254a-4ae8-a2df-8a1a26289109;" +
                  "android=111a3513-b59c-4a1c-b15b-8d1d54a41894;",
                  typeof(Analytics), typeof(Crashes));

            //Init Jobs Sync
            _ = StartJob.Start(_job);
        }
    }
}
