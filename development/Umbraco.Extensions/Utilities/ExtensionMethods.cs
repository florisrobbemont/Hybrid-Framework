using Eksponent.CropUp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using uComponents.DataTypes.UrlPicker;
using uComponents.DataTypes.UrlPicker.Dto;
using umbraco.BusinessLogic;
using Umbraco.Core;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Poco;
using Umbraco.Web;

namespace Umbraco.Extensions.Utilities
{
    using umbraco.cms.helpers;

    public static class ExtensionMethods
    {
        
        
        #region UmbracoHelper - Methods

        #region Email

        /// <summary>
        /// Send the e-mail.
        /// </summary>
        /// <param name="umbraco"> </param>
        /// <param name="emailFrom"></param>
        /// <param name="emailFromName"> </param>
        /// <param name="emailTo"></param>
        /// <param name="emailCc"></param>
        /// <param name="emailBcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        /// <param name="emailType">Type of email to set in the db</param>
        /// <returns></returns>
        public static void SendEmail(this UmbracoHelper umbraco, string emailFrom, string emailFromName, string emailTo, string subject, string body, string emailCc = "", string emailBcc = "", EmailType? emailType = null)
        {
            //Make the MailMessage and set the e-mail address.
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailFrom, emailFromName);

            //Split all the e-mail addresses on , or ;.
            char[] splitChar = { ',', ';' };

            //Remove all spaces.
            emailTo = emailTo.Trim();
            emailCc = emailCc.Trim();
            emailBcc = emailBcc.Trim();

            //Split emailTo.
            string[] toArray = emailTo.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string to in toArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(to))
                {
                    //Loop through all e-mail addressen in toArray and add them in the to.
                    message.To.Add(new MailAddress(to));
                }
            }

            //Split emailCc.
            string[] ccArray = emailCc.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string cc in ccArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(cc))
                {
                    //Loop through all e-mail addressen in ccArray and add them in the cc.
                    message.CC.Add(new MailAddress(cc));
                }
            }

            //Split emailBcc.
            string[] bccArray = emailBcc.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string bcc in bccArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(bcc))
                {
                    //Loop through all e-mail addressen in bccArray and add them in the bcc.
                    message.Bcc.Add(new MailAddress(bcc));
                }
            }

            //Set the rest of the e-mail data.
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            //Only send the email if there is a to address.
            if (message.To.Any())
            {
                if (emailType.HasValue)
                {
                    try
                    {
                        //Get the database.
                        var database = ApplicationContext.Current.DatabaseContext.Database;

                        //Create the email object and set all properties.
                        var emailPoco = new EmailPoco()
                        {
                            Type = emailType.Value.ToString(),
                            FromName = emailFromName,
                            FromEmail = emailFrom,
                            ToEmail = emailTo,
                            CCEmail = emailCc,
                            BCCEmail = emailBcc,
                            Date = DateTime.Now,
                            Subject = subject,
                            Message = body
                        };

                        //Insert the email into the database.
                        database.Insert(emailPoco);
                    }
                    catch (Exception ex)
                    {
                        Umbraco.LogException(ex);
                    }
                }

                //Make the SmtpClient.
                SmtpClient smtpClient = new SmtpClient();

                //Send the e-mail.
                smtpClient.Send(message);
            }

            //Clear the resources.
            message.Dispose();
        }

        #endregion

        #region Error

        ///// <summary>
        ///// Log an exception and send an email.
        ///// </summary>
        ///// <param name="ex"></param>
        ///// <param name="nodeId"></param>
        ///// <param name="type"></param>
        //public static void LogException(this UmbracoHelper umbraco, Exception ex)
        //{
        //    try
        //    {
        //        int nodeId = -1;
        //        if (System.Web.HttpContext.Current.Items["pageID"] != null)
        //        {
        //            int.TryParse(System.Web.HttpContext.Current.Items["pageID"].ToString(), out nodeId);
        //        }

        //        StringBuilder comment = new StringBuilder();
        //        StringBuilder commentHtml = new StringBuilder();

        //        commentHtml.AppendFormat("<p><strong>Url:</strong><br/>{0}</p>", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //        commentHtml.AppendFormat("<p><strong>Node id:</strong><br/>{0}</p>", nodeId);

        //        //Add the exception.
        //        comment.AppendFormat("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace);
        //        commentHtml.AppendFormat("<p><strong>Exception:</strong><br/>{0}</p>", ex.Message);
        //        commentHtml.AppendFormat("<p><strong>StackTrace:</strong><br/>{0}</p>", ex.StackTrace);

        //        if (ex.InnerException != null)
        //        {
        //            //Add the inner exception.
        //            comment.AppendFormat("- InnerException: {0} - InnerStackTrace: {1}", ex.InnerException.Message, ex.InnerException.StackTrace);
        //            commentHtml.AppendFormat("<p><strong>InnerException:</strong><br/>{0}</p>", ex.InnerException.Message);
        //            commentHtml.AppendFormat("<p><strong>InnerStackTrace:</strong><br/>{1}</p>", ex.InnerException.StackTrace);
        //        }

        //        //Log the Exception into Umbraco.
        //        Log.Add(LogTypes.Error, nodeId, comment.ToString());

        //        //Send an email with the exception.
        //        umbraco.SendEmail(umbraco.Config().ErrorFrom, umbraco.Config().ErrorFromName, umbraco.Config().ErrorTo, "Error log", commentHtml.ToString());
        //    }
        //    catch
        //    {
        //        //Do nothing because nothing can be done with this exception.
        //    }
        //}

        #endregion

        #region Property

        ///// <summary>
        ///// Return an instance of the Configuration class.
        ///// </summary>
        //public static Configuration Config(this UmbracoHelper umbraco)
        //{
        //    return Configuration.Instance;
        //}

        #endregion

    
        #region Other

        

        #endregion

        #endregion
    }
}