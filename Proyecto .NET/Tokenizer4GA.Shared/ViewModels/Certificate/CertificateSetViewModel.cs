using Tokenizer4GA.Shared.Models.Document;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.ViewModels.Certificate
{
    public partial class CertificateSetViewModel : PageViewModel
    {
        public CertificateSetViewModel(IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality)
            : base(requestManager, commandFactory, platformFunctionality)
        { }

        protected override void Initialize()
        {
            SelectCertificateCommand = CreateCommand(async () => await SelectCertificateFromDevice());
        }

        private async Task SelectCertificateFromDevice()
        {
            try
            {
                var insuranceBase64 = await SelectCertificate();

                if (!string.IsNullOrEmpty(insuranceBase64.Base64))
                {
                    var fileList = new List<DocumentSetFile>();
                    if (FileDocument != null)
                    {
                        fileList = FileDocument.ToList();
                    }
                    fileList.Add(insuranceBase64);
                    FileDocument = fileList.ToArray();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
