using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Controls
{
    public partial class FilesGalleryControl : StackLayout
    {
        public FilesGalleryControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty DocumentListProperty = BindableProperty.Create(nameof(DocumentList),
                                                                                              typeof(ObservableCollection<string>),
                                                                                              typeof(FilesGalleryControl),
                                                                                              defaultBindingMode: BindingMode.TwoWay,
                                                                                              propertyChanged: OnDocumentListProperyChange);

        

        private static void OnDocumentListProperyChange(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                if (newValue != null)
                {
                    var control = (FilesGalleryControl)bindable;
                    var documentList = (ObservableCollection<string>)newValue;

                    control.Orientation = StackOrientation.Horizontal;

                    foreach (var item in documentList)
                    {
                        var documentIcon = new Image
                        {
                            Source = "document_icon",
                            HeightRequest = 50
                        };
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        documentIcon.GestureRecognizers.Add(tapGestureRecognizer);
                        tapGestureRecognizer.Tapped += (sender, e) =>
                        {
                            control.DocumentSelected = item;
                        };
                        control.Children.Add(documentIcon);
                    }
                }
            }
            catch (Exception)
            {
                //ignored

            }
        }

        public ObservableCollection<string> DocumentList
        {
            get => (ObservableCollection<string>)GetValue(DocumentListProperty);
            set => SetValue(DocumentListProperty, value);
        }

        public static readonly BindableProperty DocumentSelectedProperty = BindableProperty.Create(nameof(DocumentSelected),
                                                                                                   typeof(string),
                                                                                                   typeof(FilesGalleryControl),
                                                                                                   defaultBindingMode: BindingMode.TwoWay);

        public string DocumentSelected
        {
            get => (string)GetValue(DocumentSelectedProperty);
            set => SetValue(DocumentSelectedProperty, value);
        }
    }
}
