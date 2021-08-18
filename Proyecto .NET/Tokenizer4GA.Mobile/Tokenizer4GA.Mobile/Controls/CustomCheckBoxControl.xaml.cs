using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tokenizer4GA.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomCheckBoxControl : StackLayout
    {
        public CustomCheckBoxControl()
        {
            InitializeComponent();

            ChckBx.CheckedChanged += (object sender, CheckedChangedEventArgs e) =>
            {
                if (CheckedCommand != null && CheckedCommand.CanExecute(null))
                {
                    CheckedCommand.Execute(null);
                }
            };

        }

        private void ChckBx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
                                                                                        typeof(string),
                                                                                        typeof(CustomCheckBoxControl),
                                                                                        string.Empty);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty CheckedCommandProperty = BindableProperty.Create(nameof(CheckedCommand),
                                                                                                 typeof(ICommand),
                                                                                                 typeof(CustomCheckBoxControl),
                                                                                                 null);

        public ICommand CheckedCommand
        {
            get => (ICommand)GetValue(CheckedCommandProperty);
            set => SetValue(CheckedCommandProperty, value);
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked),
                                                                                            typeof(bool),
                                                                                            typeof(CustomCheckBoxControl),
                                                                                            false);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
    }
}
