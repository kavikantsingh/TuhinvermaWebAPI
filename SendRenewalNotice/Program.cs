using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.WS;
using LAPP.GlobalFunctions;
using LAPP.LOGING;
using LAPP.ENTITY.Enumeration;

namespace SendRenewalNotice
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool emailSent = EmailHelper.SendMail("test@ebsoftsolutions.com", "test", "test", true);
                if (emailSent)
                {
                    LogHelper.LogCommunication(2, 2, eCommunicationType.Email, "License Renewal ONLINE is now open", eCommunicationStatus.Success, "Public", "License Renewal ONLINE is now open email has been sent", EmailHelper.GetSenderAddress(), "test@ebsoftsolutions.com", null, null, 0, null, null, null);
                }
                else
                {
                    LogHelper.LogCommunication(2, 2, eCommunicationType.Email, "License Renewal ONLINE is now open", eCommunicationStatus.Fail, "Public", "License Renewal ONLINE is now open email sending failed.", EmailHelper.GetSenderAddress(), "test@ebsoftsolutions.com", null, null, 0, null, null, null);
                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}
