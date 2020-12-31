using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Application.Mediator
{
    /// <summary>
    /// In which state the command is.
    /// </summary>
    public enum CommandResultState
    {
        Processed, Created, Error, BadParameters
    }
}
