using Tokenizer4GA.Mobile.PlatformServices;
using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Data;
using Tokenizer4GA.Shared.JobServices.Sync;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.Services.Sqlite;
using Tokenizer4GA.Shared.ViewModels;
using Tokenizer4GA.Shared.ViewModels.Environment;
using Tokenizer4GA.Shared.ViewModels.Token;
using Tokenizer4GA.Shared.WebServices.Contracts;
using Tokenizer4GA.Shared.WebServices.Mocks;
using Tokenizer4GA.Shared.WebServices.RestServices;
using Autofac;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Tokenizer4GA.Shared.ViewModels.General;
using Tokenizer4GA.Shared.ViewModels.Certificate;

namespace Tokenizer4GA.Mobile
{
    public class ViewModelLocator
    {
        private static bool UseMockServices = false;

        private static readonly Dictionary<Type, Type> _platformServicesImplementations = new Dictionary<Type, Type>
        {
            { typeof(CommandFactoryService), typeof(ICommandFactoryService) },
            { typeof(PlatformFunctionalityService), typeof(IPlatformFunctionalityService) },
            { typeof(RequestManagerService), typeof(IRequestManagerService) },
            { typeof(DeviceServices), typeof(IDeviceService) }
        };

        private static readonly Type[] _viewModels = {
            typeof(HomeViewModel),
            typeof(CertificateSetViewModel),
            
            
            typeof(WebViewerViewModel)
        };

        private static readonly Type[] _singletonViewModels = 
        {
            typeof(AppShellViewModel),
            typeof(TitleViewModel)
        };

        private static readonly Dictionary<Type, Type> _webServicesImplementations = new Dictionary<Type, Type>
        {
            { typeof(PathService), typeof(IPathService) },
            { typeof(ContextGA), typeof(IDbContext) },
            { typeof(InformationRestService), typeof(IInformationService) },
            { typeof(SyncInformation), typeof(ISyncInformation) },
            { typeof(SyncContexGa), typeof(ISyncContextGa) }
        };

        private static readonly Dictionary<Type, Type> _webServicesMocks = new Dictionary<Type, Type>
        {
            {  typeof(InformationMock), typeof(IInformationService) }
        };

        private static ViewModelLocator _instance;

        public static ViewModelLocator Instance
        {
            get
            {
                if (_instance == null)
                    Initialize();
                return _instance;
            }
        }

        public static void Initialize() =>
            _instance = new ViewModelLocator();

        private static void RegisterServices(ContainerBuilder builder, IDictionary<Type, Type> servicesWithInterface, bool singleton)
        {
            foreach (var serviceWithInterface in servicesWithInterface)
                if (singleton)
                    builder.RegisterType(serviceWithInterface.Key)
                        .As(serviceWithInterface.Value).SingleInstance();
                else
                    builder.RegisterType(serviceWithInterface.Key)
                        .As(serviceWithInterface.Value);
        }

        private static void RegisterServices(ContainerBuilder builder, IEnumerable<Type> services, bool singleton)
        {
            foreach (var service in services)
                if (singleton)
                    builder.RegisterType(service).SingleInstance();
                else
                    builder.RegisterType(service);
        }

        private readonly IContainer _container;

        private ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder, _platformServicesImplementations, true);
            RegisterServices(builder, _viewModels, false);
            RegisterServices(builder, _singletonViewModels, true);

            if (UseMockServices)
                RegisterServices(builder, _webServicesMocks, true);
            else
                RegisterServices(builder, _webServicesImplementations, true);

            _container = builder.Build();

            ClearMockOrAuthenticDataIfRequired();
        }

        public TViewModel Resolve<TViewModel>()
            where TViewModel : BaseViewModel =>
            _container.Resolve<TViewModel>();

        public TViewModel ResolveSyncContextJob<TViewModel>()
           where TViewModel : ISyncInformation =>
           _container.Resolve<TViewModel>();

        private void ClearMockOrAuthenticDataIfRequired()
        {
            var usedMockServicesAsString = Preferences.Get(StorageKeys.UsedMockServices, null);
            if (!string.IsNullOrWhiteSpace(usedMockServicesAsString)
                && bool.TryParse(usedMockServicesAsString, out var usedMockServices)
                && usedMockServices != UseMockServices)
            {
#if DEBUG
                Preferences.Remove(StorageKeys.BearerToken);
                Preferences.Remove(StorageKeys.UserId);
#else
                SecureStorage.Remove(StorageKeys.BearerToken);
                SecureStorage.Remove(StorageKeys.UserId);
#endif
            }
            Preferences.Set(StorageKeys.UsedMockServices, UseMockServices.ToString());
        }
    }
}
