using System.Windows.Input;
using Tokenizer4GA.Shared.Localization;
using Tokenizer4GA.Shared.Models.Document;

namespace Tokenizer4GA.Shared.ViewModels.Certificate
{
    public partial class CertificateSetViewModel
    {
        private string _title = LocalizedStrings.CertificateTitlePage;

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }

        private DocumentSet _documentSet;

        public DocumentSet DocumentSet
        {
            get => _documentSet;
            set
            {
                if (_documentSet == value)
                    return;
                _documentSet = value;
                OnPropertyChanged();

                if (value != null)
                {
                    Title = value.DocumentType.Description;
                    //_ = FilterAndSavePdfs(value);
                    //FilterImages(value);
                }
                else
                {
                    Title = LocalizedStrings.CertificateTitlePage;
                    DocumentPdfUris = null;
                }
            }
        }

        private DocumentSetFile[] _fileDocument;

        public DocumentSetFile[] FileDocument
        {
            get => _fileDocument;
            set
            {
                if (_fileDocument == value)
                    return;
                _fileDocument = value;
                OnPropertyChanged();

                FileIsValid = value != null && value.Length > 0;
            }
        }

        private bool? _fileIsValid;

        public bool? FileIsValid
        {
            get => _fileIsValid;
            private set
            {
                if (_fileIsValid == value)
                    return;
                _fileIsValid = value;
                OnPropertyChanged();

                if (value.HasValue)
                {
                    ShowMasterPlaceholder = !value.Value;
                }
            }
        }

        private bool _showMasterPlaceholder = true;

        public bool ShowMasterPlaceholder
        {
            get => _showMasterPlaceholder;
            private set
            {
                if (_showMasterPlaceholder == value)
                    return;
                _showMasterPlaceholder = value;
                OnPropertyChanged();
            }
        }

        private string[] _documentPdfUris;

        public string[] DocumentPdfUris
        {
            get => _documentPdfUris;
            set
            {
                if (_documentPdfUris == value)
                    return;
                _documentPdfUris = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand SelectCertificateCommand { get; set; }
    }
}
