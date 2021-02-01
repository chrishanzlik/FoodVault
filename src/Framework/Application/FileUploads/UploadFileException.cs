using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Framework.Application.FileUploads
{
    public class UploadFileException : Exception
    {
        public UploadFileException(string message) : base(message) 
        {

        }
    }
}
