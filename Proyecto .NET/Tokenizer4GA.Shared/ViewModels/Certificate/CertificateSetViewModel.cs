using Tokenizer4GA.Shared.Models.Document;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tokenizer4GA.Shared.Services.Sqlite;
using Tokenizer4GA.Shared.Constants;

namespace Tokenizer4GA.Shared.ViewModels.Certificate
{
    public partial class CertificateSetViewModel : PageViewModel
    {
        private IPathService _pathService;

        public CertificateSetViewModel(
            IRequestManagerService requestManager,
            ICommandFactoryService commandFactory,
            IPlatformFunctionalityService platformFunctionality
            , IPathService pathService)
            : base(requestManager, commandFactory, platformFunctionality, pathService)
        {
            _pathService = pathService;
        }

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

                    //TODO guardar el certificado en localApp -> "_pathService.GetCertificatePath()"
                    var pathCertificate = await SaveCertificateLocal(insuranceBase64);

                    Console.Out.WriteLine($"EXISTE XML {_pathService.ExistCertificate()}");

                    //TODO agregar el certificado a la ruta previo a ValidateCertificate
                    ValidateCertificate(pathCertificate);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.StackTrace);
            }
        }

        private async Task<string> SaveCertificateLocal(DocumentSetFile insuranceBase64)
        {
            var pathCertificate = "";

            try
            {
                pathCertificate = await SaveBase64File(AppSettings.CertificateName,
                        Strings.XmlFileExtension,
                        insuranceBase64.Base64,
                        AppSettings.PathComplementCertificate);
                Console.Out.WriteLine($"Ruta certificado::{pathCertificate}");

                
            }
            catch (Exception e)
            {
                Console.Out.WriteLine($"Error::{e}");
            }

            return pathCertificate;
        }

        private void ValidateCertificate(string pathCertificate)
        {
            
            //TODO INIT Leer certificado TEMP
            //using (TextReader reader = new StreamReader(_pathService.GetCertificatePath())) {
            //    XmlSerializer serializer = new XmlSerializer(typeof(XmlCertificate));
            //    var xml = (XmlCertificate)serializer.Deserialize(reader);
            //    Console.Out.WriteLine($"XML :: {xml}");
            //}
            //TODO END Leer certificado TEMP




            
            //// The path to the certificate.
            //string Certificate = pathCertificate == null ? _pathService.GetCertificatePath() : pathCertificate;
            //
            //// Load the certificate into an X509Certificate object.
            //X509Certificate cert = new X509Certificate(Certificate);
            //
            //// Get the value.
            //string resultsTrue = cert.ToString(true);
            //
            //// Display the value to the console.
            //Console.WriteLine(resultsTrue);
            //
            //// Get the value.
            //string resultsFalse = cert.ToString(false);
            //
            //// Display the value to the console.
            //Console.WriteLine(resultsFalse);


            NavigateToToken();
        }
    }
}
