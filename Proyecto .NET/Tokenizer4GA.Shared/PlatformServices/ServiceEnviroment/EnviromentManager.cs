using System;
using System.Diagnostics;
using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.PlatformServices.Enums;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace Tokenizer4GA.Shared.PlatformServices.ServiceEnviroment
{
    public class EnviromentManager
    {
        private static IEnviroments _configuration;

        public static IEnviroments Configurations
        {
            get
            {
                if (_configuration == null)
                    LoadEnviroment();

                return _configuration;
            }
        }

        private static Stream _enviromentResourceStream;

        public static Environments Environment = Environments.Dev;


        private static void LoadEnviroment()
        {
            switch (Environment)
            {
                case Environments.Dev:
                    _enviromentResourceStream = Assembly.GetAssembly(typeof(IEnviroments)).GetManifestResourceStream(EnvironmentKeys.EndpointDev);
                    break;
                case Environments.Qa:
                    _enviromentResourceStream = Assembly.GetAssembly(typeof(IEnviroments)).GetManifestResourceStream(EnvironmentKeys.EndpointQa);
                    break;
                case Environments.Prod:
                    _enviromentResourceStream = Assembly.GetAssembly(typeof(IEnviroments)).GetManifestResourceStream(EnvironmentKeys.EndpointProduction);
                    break;
            }

            if (_enviromentResourceStream == null)
                return;

            using var streamReader = new StreamReader(_enviromentResourceStream);
            var jsoString = streamReader.ReadToEnd();
            _configuration = JsonConvert.DeserializeObject<Enviroments>(jsoString);
        }
    }
}
