using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.GlobalFunctions
{
    public class EmailHelper
    {

        private static string SenderText = "inLumon";

        #region Email Helper

        public static bool SendMail(string toAddress, string subject, string mailContent)
        {
            mailContent = SetLogo(mailContent);
            bool result = false;

            SmtpClient smtpClient = GetBackupSMTPClientObject();
            MailMessage message = new MailMessage();


            try
            {
                smtpClient = GetSMTPClientObject();
                MailAddress senderAddress = new MailAddress(GetSenderAddress());
                message.From = new MailAddress("\"" + SenderText + "\" <" + senderAddress + ">");
                message = GetEmails(toAddress, message);

                message.Subject = subject;
                message.IsBodyHtml = false;
                message.Body = mailContent;

                if (!String.IsNullOrEmpty(toAddress))
                {
                    smtpClient.Send(message);
                }
                else
                {
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                    return false;

                }

                result = true;
                //Config_GNF.LogSentEmail(toAddress, subject);

            }
            catch (Exception ex)
            {
                //Try to send using Backup SMTP In the case if SMTP failes
                if (!SendUsingBackupSMTP(message))
                {
                    result = false;
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                }

            }

            return result;
        }

        public static bool SendMail(string toAddress, string subject, string mailContent, bool IsBodyHtml)
        {
            mailContent = SetLogo(mailContent);
            bool result = false;

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();


            try
            {
                smtpClient = GetSMTPClientObject();
                MailAddress senderAddress = new MailAddress(GetSenderAddress());
                message.From = new MailAddress("\"" + SenderText + "\" <" + senderAddress + ">");
                message = GetEmails(toAddress, message);

                message.Subject = subject;
                message.IsBodyHtml = IsBodyHtml;
                message.Body = mailContent;



                if (!String.IsNullOrEmpty(toAddress))
                {
                    smtpClient.Send(message);
                }
                else
                {
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                    return false;

                }

                result = true;
                //Config_GNF.LogSentEmail(toAddress, subject);

            }
            catch (Exception ex)
            { //Try to send using Backup SMTP In the case if SMTP failes
                if (!SendUsingBackupSMTP(message))
                {
                    result = false;
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                }
            }

            return result;
        }

        public static bool SendMail(string toAddress, string subject, string mailContent, bool IsBodyHtml, string attachment)
        {
            mailContent = SetLogo(mailContent);
            bool result = false;

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();


            try
            {
                smtpClient = GetSMTPClientObject();
                MailAddress senderAddress = new MailAddress(GetSenderAddress());
                message.From = new MailAddress("\"" + SenderText + "\" <" + senderAddress + ">");
                message = GetEmails(toAddress, message);

                message.Subject = subject;
                message.IsBodyHtml = IsBodyHtml;
                message.Body = mailContent;

                System.Net.Mail.Attachment inline = new System.Net.Mail.Attachment(attachment);
                message.Attachments.Add(inline);

                if (!String.IsNullOrEmpty(toAddress))
                {
                    smtpClient.Send(message);
                }
                else
                {
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                    return false;

                }

                result = true;
                //Config_GNF.LogSentEmail(toAddress, subject);

            }
            catch (Exception ex)
            { //Try to send using Backup SMTP In the case if SMTP failes
                if (!SendUsingBackupSMTP(message))
                {
                    result = false;
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                }
            }

            return result;
        }

        private static string SetLogo(string mailContent)
        {
            try
            {
                string msg = mailContent; //.Replace("#LogoUrl#", Lapp_Configuration.Get_LogoUrl());
                return msg;
            }
            catch (Exception eX)
            {
                return mailContent;
            }
        }

        public static bool SendMailWithMultipleAttachment(string toAddress, string subject, string mailContent, bool IsBodyHtml, List<System.Net.Mail.Attachment> lstAttachment)
        {
            mailContent = SetLogo(mailContent);
            bool result = false;

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                smtpClient = GetSMTPClientObject();
                MailAddress senderAddress = new MailAddress(GetSenderAddress());
                message.From = new MailAddress("\"" + SenderText + "\" <" + senderAddress + ">");
                message = GetEmails(toAddress, message);

                message.Subject = subject;
                message.IsBodyHtml = IsBodyHtml;
                message.Body = mailContent;

                foreach (Attachment a in lstAttachment)
                {
                    message.Attachments.Add(a);

                }


                if (!String.IsNullOrEmpty(toAddress))
                {
                    smtpClient.Send(message);
                }
                else
                {
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                    return false;

                }

                result = true;
                //Config_GNF.LogSentEmail(toAddress, subject);

            }
            catch (Exception ex)
            { //Try to send using Backup SMTP In the case if SMTP failes
                if (!SendUsingBackupSMTP(message))
                {
                    result = false;
                    //Config_GNF.LogFailedEmail(toAddress, subject);
                }
            }

            return result;



        }

        #endregion


        #region Configuration
        private static string GetSenderAddress()
        {
            //return "info@inlumon.com";
            //if (Lapp_Configuration.IsApplicationUnderDevelopment())
            //{
                return ConfigurationManager.AppSettings["FromAddress"].ToString();//
            //}
            //else
            //{
            //    return Lapp_Configuration.EmailFrom().Value;
            //}
        }

        private static SmtpClient GetSMTPClientObject()
        {

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = ConfigurationManager.AppSettings["SMTPHost"];
            smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Timeout = 60000;
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SenderAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
            smtpClient.EnableSsl = false; // Convert.ToBoolean(Lapp_Configuration.SmtpEnableSsl().Value);

            //if (Lapp_Configuration.IsApplicationUnderDevelopment())
            //{
            //    smtpClient.Host = ConfigurationManager.AppSettings["SMTPHost"];
            //    smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            //    smtpClient.UseDefaultCredentials = false;
            //    smtpClient.Timeout = 60000;
            //    smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SenderAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
            //    smtpClient.EnableSsl = false; // Convert.ToBoolean(Lapp_Configuration.SmtpEnableSsl().Value);

            //}
            //else
            //{
            //    smtpClient.Host = Lapp_Configuration.SmtpServer().Value;
            //    smtpClient.Port = Convert.ToInt32(Lapp_Configuration.SmtpPort().Value);
            //    smtpClient.UseDefaultCredentials = Lapp_Configuration.UseDefaultCredentials();
            //    smtpClient.Timeout = 60000;
            //    smtpClient.Credentials = new NetworkCredential(Lapp_Configuration.SmtpUsername().Value, Lapp_Configuration.SmtpPassword().Value);
            //    smtpClient.EnableSsl = Convert.ToBoolean(Lapp_Configuration.SmtpEnableSsl().Value);
            //}

            return smtpClient;
        }

        private static SmtpClient GetBackupSMTPClientObject()
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = ConfigurationManager.AppSettings["SMTPHost"];
            smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Timeout = 60000;
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SenderAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
            smtpClient.EnableSsl = false; // Convert.ToBoolean(Lapp_Configuration.SmtpEnableSsl().Value);


            //SmtpClient smtpClient = new SmtpClient();
            //smtpClient.Host =  ConfigurationManager.AppSettings["SMTPHost"];
            //smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            //smtpClient.UseDefaultCredentials = Lapp_Configuration.UseDefaultCredentials();
            //smtpClient.Timeout = 60000;
            //smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SenderAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
            //smtpClient.EnableSsl = Convert.ToBoolean(Lapp_Configuration.BackupSmtpEnableSsl().Value);

            //if (Lapp_Configuration.IsApplicationUnderDevelopment())
            //{
            //    smtpClient.Host = ConfigurationManager.AppSettings["SMTPHost"];
            //    smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            //    smtpClient.UseDefaultCredentials = Lapp_Configuration.UseDefaultCredentials();
            //    smtpClient.Timeout = 60000;
            //    smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SenderAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
            //    smtpClient.EnableSsl = Convert.ToBoolean(Lapp_Configuration.BackupSmtpEnableSsl().Value);

            //}
            //else
            //{
            //    smtpClient.Host = Lapp_Configuration.BackupSmtpServer().Value;
            //    smtpClient.Port = Convert.ToInt32(Lapp_Configuration.BackupSmtpPort().Value);
            //    smtpClient.UseDefaultCredentials = Lapp_Configuration.UseDefaultCredentials();
            //    smtpClient.Timeout = 60000;
            //    smtpClient.Credentials = new NetworkCredential(Lapp_Configuration.BackupSmtpUsername().Value, Lapp_Configuration.BackupSmtpPassword().Value);
            //    smtpClient.EnableSsl = Convert.ToBoolean(Lapp_Configuration.BackupSmtpEnableSsl().Value);
            //}

            return smtpClient;
        }

        private static System.Net.Mail.MailMessage GetEmails(string toAddress, System.Net.Mail.MailMessage message)
        {
            message.To.Add(toAddress);
            string CCEmail = "";// Lapp_Configuration.EmailCopy().Value;
            if (CCEmail.Trim() != "")
            {
                message.CC.Add(CCEmail);
            }
            string BCCEmail = ""; // Lapp_Configuration.EmailBlindCopy().Value;
            if (BCCEmail.Trim() != "")
            {
                message.Bcc.Add(BCCEmail);


            }
            // message.Bcc.Add(new MailAddress("info@ebsoftsolutions.com"));
            return message;
        }

        private static bool SendUsingBackupSMTP(MailMessage message)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient = GetBackupSMTPClientObject();
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }

}
