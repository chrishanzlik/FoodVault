﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Application.Commands
{
    /// <summary>
    /// In which state the command is.
    /// </summary>
    public enum CommandResultState
    {
        Processed, Created, Error, BadParameters
    }
}