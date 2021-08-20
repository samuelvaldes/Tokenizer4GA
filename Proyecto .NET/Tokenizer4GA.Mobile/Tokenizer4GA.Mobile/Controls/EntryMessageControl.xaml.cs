using Tokenizer4GA.Shared.PlatformServices.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tokenizer4GA.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryMessageControl : StackLayout
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
                                                                                              typeof(ImageSource),
                                                                                              typeof(EntryMessageControl));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                       typeof(string),
                                                                                       typeof(EntryMessageControl));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty MessageTypesProperty = BindableProperty.Create(nameof(MessageTypes),
                                                                                              typeof(MessageType),
                                                                                              typeof(EntryMessageControl),
                                                                                              defaultValue: MessageType.Success,
                                                                                              defaultBindingMode: BindingMode.TwoWay,
                                                                                              propertyChanged: OnMessageTypePropertyChanged);

        private static void OnMessageTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EntryMessageControl)bindable;
            switch ((MessageType)newValue)
            {
                case MessageType.Error:
                    control.MessageTxt.TextColor = Color.FromHex("#ff3333");
                    break;
                case MessageType.Warning:
                    control.MessageTxt.TextColor = Color.FromHex("#ffc107");
                    break;
                case MessageType.Success:
                    control.MessageTxt.TextColor = Color.Green;
                    break;
            }
        }

        public MessageType MessageTypes
        {
            get => (MessageType)GetValue(MessageTypesProperty);
            set => SetValue(MessageTypesProperty, value);
        }

        public EntryMessageControl()
        {
            InitializeComponent();
            MessageTypes = MessageType.Success;
        }
    }
}
