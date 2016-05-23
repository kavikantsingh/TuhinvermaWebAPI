using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using LAPP.ENTITY;
using LAPP.WS.App_Helper.Common;
using LAPP.BAL;
using LAPP.GlobalFunctions;

namespace LAPP.WS.App_Helper
{
    public class Validations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string GetErrorListFromModelState
                                        (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return Newtonsoft.Json.JsonConvert.SerializeObject(errorList);
        }

        #region Max Lnegth
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>

        /// <returns></returns>
        public static List<ResponseReason> IsValidateLengthProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(ValidateLength(PropertyValue, Maxlength)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + " " + " Data too long. " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }

        public static List<ResponseReason> CompareStrings(string PropertyName1, string PropertyName2, string string1, string string2, List<ResponseReason> objResponseList)
        {
            string Code = "IR";
            if (string1 != string2)
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName1 + "," + PropertyName2;
                objValidationResponse.Message = GeneralFunctions.GetSplitedCamelCase(PropertyName2).ToLower() + " " + " and  " + GeneralFunctions.GetSplitedCamelCase(PropertyName1).ToLower() + " does not match.";

                objResponseList.Add(objValidationResponse);
            }

            return objResponseList;
        }

        public static List<ResponseReason> IsIntGreaterThanZero(string PropertyName, int Value, List<ResponseReason> ObjReasonList)
        {
            string Code = "IR";
            if (Value <= 0)
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + " " + "is required and should be greater than 0. ";

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;
        }

        private static string ValidateLength(string propertyValue, int maxlength)
        {
            string Result = "";

            try
            {


                if (maxlength > 0)
                {
                    if (propertyValue.Length > maxlength)
                    {
                        Result = "Maximum length of the property  is " + maxlength;
                    }

                }
                return Result;
            }
            catch (Exception ex)
            {
                Result = "Max length can not be validated of null property.";
                return Result;
            }
        }

        private static string ValidateLength(string propertyValue, int maxlength, string PropertyName)
        {
            string Result = "";

            try
            {


                if (maxlength > 0)
                {
                    if (propertyValue.Length > maxlength)
                    {
                        Result = "Maximum length of the " + PropertyName + " is " + maxlength;
                    }

                }
                return Result;
            }
            catch (Exception ex)
            {
                Result = "Max length can not be validated of null property.";
                return Result;
            }
        }

        #endregion

        #region Required
        /// <summary>
        /// This method is used to validate required string type property
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsRequiredProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (string.IsNullOrEmpty(PropertyValue))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + " " + "is required. " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }

        public static List<ResponseReason> IsRequiredPropertyMaxLength(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (string.IsNullOrEmpty(PropertyValue) || PropertyValue.Length > Maxlength)
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + " " + "is required. " + ValidateLength(PropertyValue, Maxlength, PropertyName);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }

        public static List<ResponseReason> IsValidOnlyMaxLength(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (string.IsNullOrEmpty(PropertyValue) || PropertyValue.Length > Maxlength)
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = ValidateLength(PropertyValue, Maxlength, PropertyName);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }

        #endregion

        #region Email Validation
        public static List<ResponseReason> IsValidEmailProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidEmails(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". eg: john@example.com " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        public static List<ResponseReason> IsValidEmailFromUser(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList)
        {

            string Code = "FR";
            Users objUser = new Users();
            UsersBAL objUBAL = new UsersBAL();

            objUser = objUBAL.Get_Users_by_Email(PropertyValue);

            if (objUser != null)
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "The email entered is already registered with us.";

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }


        private static string IsValidEmails(string inputEmail, string message = "error")
        {
            string result = "";
            Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
            if (!String.IsNullOrEmpty(inputEmail.Trim()) && re.IsMatch(inputEmail))
                return result;
            else
                return message;
        }

        public static bool IsValidEmail(string inputEmail)
        {
            string result = "";
            Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
            if (!String.IsNullOrEmpty(inputEmail.Trim()) && re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }


        #endregion

        #region Only Alphabet
        public static List<ResponseReason> IsOnlyAlphabetProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsOnlyAlphabet(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter only alphabet values in ." + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsOnlyAlphabet(string inputReq, string message = "error")
        {
            string result = "";
            Regex re = new Regex(@"^[a-zA-Z\s\.\,\'\-]+$", RegexOptions.IgnoreCase);
            if (!String.IsNullOrEmpty(inputReq.Trim()) && re.IsMatch(inputReq))
                return result;
            else
                return message;
        }
        #endregion

        #region Password Validation
        private static string StrongPasswordValidation(string password)
        {
            //string regex =  (?=^.{8,25}$)(?=(?:.*?\d){1})(?=.*[a-z])(?=(?:.*?[A-Z]){2})(?=(?:.*?[!@#$%*()_+^&}{:;?.]){1})(?!.*\s)[0-9a-zA-Z!@#$%*()_+^&]*$

            string result = "";
            Regex re = new Regex(@"(?=^.{8,25}$)(?=(?:.*?\d){1})(?=.*[a-z])(?=(?:.*?[A-Z]){1})(?=(?:.*?[!@#$%*()_+^&}{:;?.,';~`]){1})(?!.*\s)[0-9a-zA-Z!@#$%*()_+^&}{:;?.,';~`^&]*$");
            if (!String.IsNullOrEmpty(password.Trim()) && re.IsMatch(password))
                return result;
            else
                return "Password does not follow the password rule. Please see the password instructions for the password rule.";
        }
        #endregion

        #region Url Validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidUrlProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidUrl(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". eg: http(s)://www.example.com " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidUrl(string inputUrl)
        {
            string result = "";
            string url = inputUrl;

            //Regex RgxUrl = new Regex("(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?");
            //if (RgxUrl.IsMatch(url))
            //{
            //    //url is valid
            //}
            //else
            //{
            //    //url is not valid
            //}

            //Uri uriResult;
            //bool result1 = Uri.TryCreate(inputUrl, UriKind.Absolute, out uriResult)
            //              && (uriResult.Scheme == Uri.UriSchemeHttp
            //                  || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!String.IsNullOrEmpty(inputUrl.Trim()) && Regex.IsMatch(url, @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"))
                return result;
            else if (!String.IsNullOrEmpty(inputUrl.Trim()) && Regex.IsMatch(url, @"((ftp|http|https):\/\/)?[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"))
                return result;
            else
                return "Invalid Url";

            //else if (!String.IsNullOrEmpty(inputUrl.Trim()) && Regex.IsMatch(url, @"((http|https)://)?[a-zA-Z]\w*(\.\w+)+(/\w*(\.\w+)*)*(\?.+)*"))
            //   return result;

            //if (!String.IsNullOrEmpty(inputUrl.Trim()) && Regex.IsMatch(url, @"(([\w]+:)?//)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,63}(:[\d]+)?(/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?"))


            // if (!String.IsNullOrEmpty(inputUrl.Trim()) && Regex.IsMatch(url, @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"))
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="objResponseList"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidCreditCardNumber(string PropertyName, string PropertyValue, List<ResponseReason> objResponseList)
        {
            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidCreditCardNumber(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter a valid card number(numeric value only).";

                objResponseList.Add(objValidationResponse);
            }

            return objResponseList;
        }

        private static string IsValidCreditCardNumber(string inputValue, string message = "eer")
        {
            string result = "";
            string data = inputValue.Trim();
            char[] inputCharactors = data.ToCharArray();
            if (inputCharactors.Length > 17)
            {
                return message;
            }
            if (inputCharactors.Length < 13)
            {
                return message;
            }

            if (!String.IsNullOrEmpty(data.ToString()) && Regex.IsMatch(data.ToString(), @"^[0-9]+$"))
            {
                // nothing
            }
            else
            {
                result = message;
            }



            return result;

        }
        #endregion

        #region Date Validation Format: Any Type of Date
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidDateProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidDate(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidDate(string inputDate)
        {
            string result = "";
            string date = inputDate;
            if (!String.IsNullOrEmpty(inputDate.Trim()) && Regex.IsMatch(date, @"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"))
                return result;
            else
                return "Invalid Date";
        }
        #endregion

        #region Date Validation MM/DD/YYYY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidDateMMDDYYYYProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidDateMMDDYYYY(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". eg: mm/dd/yyyy " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidDateMMDDYYYY(string inputDate, string errormessage = "error")
        {


            string result = "";
            string date = inputDate;
            if (!String.IsNullOrEmpty(inputDate) && Regex.IsMatch(date, @"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"))
            {
                return result;
            }
            if (!String.IsNullOrEmpty(inputDate) && Regex.IsMatch(date, @"^(((((0[13578])|([13578])|(1[02]))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(3[01])))|((([469])|(11))[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9])|(30)))|((02|2)[\-\/\s]?((0[1-9])|([1-9])|([1-2][0-9]))))[\-\/\s]?\d{4})(\s(((0[1-9])|([1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$"))
            {
                return result;
            }
            else if (!String.IsNullOrEmpty(inputDate) && Regex.IsMatch(date, @"^(\d{1,2})/(\d{1,2})/(\d{4})?$"))
            {
                return result;
            }
            else if (!String.IsNullOrEmpty(inputDate) && Regex.IsMatch(date, @"^(0?[1-9]|[12][0-9]|3[01])/(0?[1-9]|1[012])/((19|20)\d\d)?$"))
            {
                return result;
            }
            else
                return errormessage;

        }

        private static string IsValideDateYear(string inputDate, string errormessage)
        {
            string result = "";
            string date = inputDate;
            if (date != "" && Regex.IsMatch(date, @"^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"))
            {
                DateTime inputDateValue = Convert.ToDateTime(inputDate);
                if (!String.IsNullOrEmpty(inputDate) && inputDateValue.Date.Year <= DateTime.Now.AddYears(100).Year)
                    return result;
                else
                    return errormessage;
            }
            else
            {
                return errormessage;
            }
        }


        #endregion

        #region Decimal Valdiation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidIntDecimalProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidIntDecimal(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidIntDecimal(string inputInt, string errormessag = "error")
        {
            string result = "";
            string date = inputInt;
            if (!String.IsNullOrEmpty(date) && Regex.IsMatch(date, @"\d+(\.\d{1,2})?$"))
                return result;
            else
                return errormessag;
        }
        #endregion

        #region Year Validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidYearProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidYear(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidYear(string year, string errormessag = "error")
        {
            string result = "";

            if (!String.IsNullOrEmpty(year) && Regex.IsMatch(year, @"^(19|20)\d{2}$"))
                return result;
            else
                return errormessag;
        }


        #endregion

        #region Month Validation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidMonthProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidMonth(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidMonth(string month, string errormessag = "error")
        {
            string result = "";

            if (!String.IsNullOrEmpty(month) && Regex.IsMatch(month, @"^(0?[1-9]|1[012])$"))
                return result;
            else
                return errormessag;
        }
        #endregion

        #region Int Validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidIntProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidInt(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidInt(string inputInt)
        {
            string result = "";
            string date = inputInt;
            if (!String.IsNullOrEmpty(inputInt.Trim()) && Regex.IsMatch(date, @"^\d+$"))
                return result;
            else
                return "Invalid Number";
        }

        private static string IsValidInt(string inputInt, string message)
        {
            string result = "";
            string date = inputInt;
            if (!String.IsNullOrEmpty(inputInt.Trim()) && Regex.IsMatch(date, @"^\d+$"))
                return result;
            else
                return message;
        }
        #endregion

        #region US Zip validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidUSZIPProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidUSZIP(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". eg: XXXXX-XXXX, XXXXX, " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidUSZIP(string inputInt, string errormessage = "error")
        {
            string result = "";
            string date = inputInt;
            if (!String.IsNullOrEmpty(inputInt) && Regex.IsMatch(date, @"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$)"))
                return result;
            else
                return errormessage;
        }
        #endregion


        #region US Zip validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="ObjReasonList"></param> /// <param name="Maxlength"></param>
        /// <returns></returns>
        public static List<ResponseReason> IsValidUSPhoneNoProperty(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList, int Maxlength = 0)
        {

            string Code = "FR";
            if (!string.IsNullOrEmpty(IsValidUSPhoneNo(PropertyValue)))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = "Please enter valid " + GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + ". eg: (XXX) XXX-XXXX,  " + ValidateLength(PropertyValue, Maxlength);

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }
        private static string IsValidUSPhoneNo(string inputInt, string Message = "error")
        {
            string result = "";
            string date = inputInt;
            if (!String.IsNullOrEmpty(inputInt.Trim()) && Regex.IsMatch(date, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                return result;
            else
                return Message;
        }

        #endregion

        public static List<ResponseReason> IsValidbool(string PropertyName, string PropertyValue, List<ResponseReason> ObjReasonList)
        {

            string Code = "FR";
            if (string.IsNullOrEmpty(PropertyValue))
            {
                ResponseReason objValidationResponse = new ResponseReason();
                objValidationResponse.Code = Code;
                objValidationResponse.PropertyName = PropertyName;
                objValidationResponse.Message = GeneralFunctions.GetSplitedCamelCase(PropertyName).ToLower() + " " + "is required. ";

                ObjReasonList.Add(objValidationResponse);
            }

            return ObjReasonList;

        }



        // }


    }


}
