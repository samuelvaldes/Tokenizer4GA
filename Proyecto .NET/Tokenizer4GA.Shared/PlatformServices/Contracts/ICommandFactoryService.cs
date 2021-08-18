﻿using System;
using System.Windows.Input;

namespace Tokenizer4GA.Shared.PlatformServices.Contracts
{
    public interface ICommandFactoryService
    {
        ICommand Create(Action execute);
        ICommand Create<T>(Action<T> execute);
        ICommand Create(Action execute, Func<bool> canExecute);
        ICommand Create<T>(Action<T> execute, Func<T, bool> canExecute);
        void NotifyOfCommandAvailability(ICommand command);
    }
}
