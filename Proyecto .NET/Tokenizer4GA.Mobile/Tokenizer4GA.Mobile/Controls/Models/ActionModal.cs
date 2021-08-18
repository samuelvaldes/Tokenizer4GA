using System;
using System.Windows.Input;

namespace Tokenizer4GA.Mobile.Controls.Models
{
    public class ActionModal
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public ICommand Command { get; set; }
    }
}
