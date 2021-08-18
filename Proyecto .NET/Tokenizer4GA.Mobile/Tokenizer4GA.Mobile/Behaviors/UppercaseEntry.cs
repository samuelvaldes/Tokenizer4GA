using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Behaviors
{
    public class UppercaseEntry:Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.NewTextValue))
                ((Entry)sender).Text = args.NewTextValue.ToUpper();
        }
    }
}
