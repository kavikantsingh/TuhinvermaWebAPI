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

    //public static class Validatations
    //{

    //    public static string UsZipRegex()
    //    {
    //        return @"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)";
    //    }
    //}

    public enum ResponseStatusCode
    {
        Success = 0,
        Validation,
        Exception,
        ValidateToken,
        PaymentGatewayPaymentFail,
        RenewalDenied,
        InvalidRequestObject,
        TransactionFailed

    }

    public enum eContactType
    {
        C = 1,
        H,
        W,
        A,
        F,
        P,
        S,
        E,

    }

    public enum eIndividualNameStatus
    {
        Current = 9,
        Previous,
        Other
    }

    public enum eIndividualNameType
    {
        Individual = 12,
        Sponsor,
        Supervisor
    }

    public enum eCommunicationStatus
    {
        Success = 'S',
        Fail ='F' 
    }
    public enum eCommunicationType
    {
        Email = 'E',
        SystemMessage = 'S'
    }
    public enum eCommentType
    {
        Log = 'L',
        Comment = 'C'
    }
}
