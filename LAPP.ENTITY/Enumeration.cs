using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY.Enumeration
{



    public enum eSeverity
    {
        Verbose = 1,
        Information,
        Warning,
        Error,
        Critical
    }
    public enum eSource
    {
        WS = 1

    }

    public static class Validatations
    {

        public static string UsZipRegex()
        {
            return @"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)";
        }
    }

    public enum ResponseStatusCode
    {
        Success = 0,
        Validation,
        Exception,
        ValidateToken

    }
}
