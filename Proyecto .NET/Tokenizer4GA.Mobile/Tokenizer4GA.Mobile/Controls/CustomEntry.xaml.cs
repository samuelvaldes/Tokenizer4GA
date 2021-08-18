using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tokenizer4GA.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntry : StackLayout
    {
        public CustomEntry()
        {
            InitializeComponent();
            TitleLbl.IsVisible = false;
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
                                                                                        typeof(string),
                                                                                        typeof(CustomEntry),
                                                                                        string.Empty);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                       typeof(string),
                                                                                       typeof(CustomEntry),
                                                                                       string.Empty,
                                                                                       BindingMode.TwoWay,
                                                                                       propertyChanged: OnTextPropertyChanged);

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            if (newValue != null && !string.IsNullOrEmpty(newValue.ToString()))
                control.TitleLbl.IsVisible = true;
            else
                control.TitleLbl.IsVisible = false;
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType),
                                                                                             typeof(ReturnType),
                                                                                             typeof(CustomEntry),
                                                                                             ReturnType.Default);

        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        public static readonly BindableProperty ResourceColorProperty = BindableProperty.Create(nameof(ResourceColor),
                                                                                                typeof(Color),
                                                                                                typeof(CustomEntry),
                                                                                                Color.White);

        public Color ResourceColor
        {
            get => (Color)GetValue(ResourceColorProperty);
            set => SetValue(ResourceColorProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword),
                                                                                             typeof(bool),
                                                                                             typeof(CustomEntry),
                                                                                             false);

        public static readonly BindableProperty MaxLenghtProperty = BindableProperty.Create(nameof(MaxLenght),
                                                                                            typeof(int),
                                                                                            typeof(CustomEntry),
                                                                                            100);

        public int MaxLenght
        {
            get => (int)GetValue(MaxLenghtProperty);
            set => SetValue(MaxLenghtProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsEnabledEntryProperty = BindableProperty.Create(nameof(IsEnabledEntry),
                                                                                                 typeof(bool),
                                                                                                 typeof(CustomEntry),
                                                                                                 true);

        public bool IsEnabledEntry
        {
            get => (bool)GetValue(IsEnabledEntryProperty);
            set => SetValue(IsEnabledEntryProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly),
                                                                                             typeof(bool),
                                                                                             typeof(CustomEntry),
                                                                                             false);

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard),
                                                                                           typeof(Keyboard),
                                                                                           typeof(CustomEntry),
                                                                                           Keyboard.Default);

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }


        public static readonly BindableProperty ComplementaryCommandParameterProperty = BindableProperty.Create(nameof(ComplementaryCommandParameter),
                                                                                                                typeof(object),
                                                                                                                typeof(CustomEntry));

        public object ComplementaryCommandParameter
        {
            get => GetValue(ComplementaryCommandParameterProperty);
            set => SetValue(ComplementaryCommandParameterProperty, value);
        }

        public static readonly BindableProperty ActionCommandProperty = BindableProperty.Create(nameof(ActionCommand),
                                                                                                typeof(ICommand),
                                                                                                typeof(CustomEntry));

        public ICommand ActionCommand
        {
            get => (ICommand)GetValue(ActionCommandProperty);
            set => SetValue(ActionCommandProperty, value);
        }




        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor),
                                                                                                typeof(Color),
                                                                                                typeof(CustomEntry));

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
    }
}