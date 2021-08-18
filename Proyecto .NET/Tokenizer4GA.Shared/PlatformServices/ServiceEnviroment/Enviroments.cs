using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer4GA.Shared.PlatformServices.ServiceEnviroment
{
    public class Enviroments : IEnviroments
    {
        public string InformationService { get; set; }
        public string ProfileService { get; set; }
        public string SyncService { get; set; }
    }
}
