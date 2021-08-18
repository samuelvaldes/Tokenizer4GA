using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tokenizer4GA.Mobile.Controls.Models;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Controls
{
    public partial class ActionModalControl : Grid
    {
        public ActionModalControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ConfigurationProperty = BindableProperty.Create(nameof(Configuration),
                                                                                                typeof(ActionModal),
                                                                                                typeof(ActionModalControl),
                                                                                                propertyChanged: OnConfigurationPropertyChanged);

        private static void OnConfigurationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (ActionModalControl)bindable;
                var list = (ObservableCollection<ActionModal>)newValue;

                foreach (var item in list)
                {
                    var button = new Button
                    {
                        Text = item.Title,
                        ImageSource = item.Icon,
                        Command = item.Command
                    };
                    control.modalStckLyt.Children.Add(button);
                }
            }
            catch (Exception)
            {
                //ignored
            }
        }

        public ActionModal Configuration
        {
            get => (ActionModal)GetValue(ConfigurationProperty);
            set => SetValue(ConfigurationProperty, value);
        }
    }
}
