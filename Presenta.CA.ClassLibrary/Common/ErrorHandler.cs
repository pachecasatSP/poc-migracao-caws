using System;
using System.Collections.Generic;
using System.Text;

namespace Presenta.CA.ClassLibrary.Common
{
    public class ErrorHandler
    {
        public static string GetError(Exception ex)
        {
            return
                String.Concat(
                    ex.Source,
                    " - ",
                    ex.Message,
                    " - ",
                    ex.InnerException != null ? " - " + ex.InnerException.Message : String.Empty
                    );
        }
    }
}
