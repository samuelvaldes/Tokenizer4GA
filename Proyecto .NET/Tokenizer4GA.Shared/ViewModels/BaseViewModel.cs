using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tokenizer4GA.Shared.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBussy;
        private bool messageVisible;
        private bool isNotConnected = true;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public bool IsBusy
        {
            get { return isBussy; }
            set { isBussy = value;OnPropertyChanged("IsBusy"); }
        }

        public bool MessageVisible
        {
            get { return messageVisible; }
            set { messageVisible = value; OnPropertyChanged("MessageVisible"); }
        }

        public bool IsNotConnected
        {
            get { return isNotConnected; }
            set { isNotConnected = value;OnPropertyChanged("IsNotConnected"); }
        }

        private string _pageTitle;
        public string PageTitle
        {
            get => PageTitle;
            set
            {
                if (_pageTitle == value)
                    return;
                _pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }

    }
}
