using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Behaviors
{
    public class ListViewItemSelectedBehavior : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            bindable.BindingContextChanged += Bindable_BindingContextChanged;
            bindable.ItemSelected += Bindable_ItemSelected;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            bindable.ItemSelected -= Bindable_ItemSelected;
        }

        private void Bindable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (Command == null) return;

            if (Command.CanExecute(e))
            {
                Command.Execute(e);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        private void Bindable_BindingContextChanged(object sender, System.EventArgs e)
        {
            OnBindingContextChanged();
        }

        #region Properties
        public ListView AssociatedObject { get; private set; }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
                                                                                          typeof(ICommand),
                                                                                          typeof(ListViewItemSelectedBehavior),
                                                                                          null);
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        #endregion
    }
}
