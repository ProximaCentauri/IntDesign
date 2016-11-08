using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Cryptography;
using log4net;
using System.Net.Mail;


namespace Model.Helpers
{
    public static class UtilityHelper
    {

        

        public static string GetUtilityStatusText(DateTime? cutoffDate, bool withReceipt)
        {
            string status = string.Empty;            
            try
            {
                if (!cutoffDate.HasValue && !withReceipt)
                {
                    return status;
                }

                if (withReceipt)
                {
                    return "Paid";
                }

                int result = DateTime.Compare(DateTime.Today.Date, cutoffDate.Value);
                int daysDue = Math.Abs(UtilityHelper.GetUtilityDaysDue(cutoffDate.Value));                
                if (result > 0)
                {
                    status = String.Format("Over-Due: {0} day(s)", daysDue);
                }
                else if (result == 0)
                {
                    status = "Due Today";
                }
                else
                {
                    status = String.Format("Due in: {0} day(s)", daysDue);
                }
            }
            catch (Exception)
            {
            }
            return status;
        }


        public static int GetUtilityDaysDue(DateTime? cutoffDate)
        {
            int daysDue = 0;
            try
            {
                if (!cutoffDate.HasValue)
                {
                    return daysDue;
                }
                daysDue = (cutoffDate.Value - DateTime.Today.Date).Days;
            }            
            catch (Exception)
            {
            }
            return daysDue;
        }

        public static string GetUtilityAlerts(IEnumerable<Utility> utilities)
        {
            string alert = string.Empty;            
            if (utilities.Count() > 0)
            {
                alert = "You have unpaid bills";
            }
            return alert;
        }

        public static string GetApplianceWarrantyStatus(DateTime? warrantyEndDate)
        {            
            if(!warrantyEndDate.HasValue)
            {
                return string.Empty;
            }
            return (DateTime.Today.Date <= warrantyEndDate.Value) ? "On Warranty" : "Out of Warranty";            
        }

        public static string GetFitOutWarrantyStatus(DateTime? fitOutCompletionDate)
        {            
            if (!fitOutCompletionDate.HasValue)
            {
                return string.Empty;
            }
            DateTime warrantyEndDate = fitOutCompletionDate.Value.AddDays(45);
            return (DateTime.Today.Date <= warrantyEndDate) ? "On Warranty" : "Out of Warranty";
        }
    }

    public class Encryptor
    {
        public static string Encrypt(string str)
        {
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            byte[] data = Encoding.ASCII.GetBytes(str);
            string protectedData = Convert.ToBase64String(ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser));
            return protectedData;
        }

        public static string GenerateTempPIN()
        {
            var random = new Random();
            return random.Next(1000000).ToString("D6");
        }
    }

    public class EmailManager
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static bool CheckConnection(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) 
                    return true;
                else 
                    return false;
            }
            catch (WebException ex)
            {
                Log.ErrorFormat("No internet connection. Message: {0}", ex.ToString());
                return false;
            }
        }

        public static bool IsValidEmail(string emailAdd)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(emailAdd, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        public static bool Send(string TO, string CC, string subject, string body)
        {
            bool retVal = false;
            
            if (IsValidEmail(TO) && CheckConnection("http://www.google.com"))
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("proximacentauriofficial@gmail.com");
                msg.To.Add(TO);
                //msg.CC.Add(CC);
                msg.Subject = subject + " " + DateTime.Now.ToString();
                msg.Body = body;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 20000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;                
                client.Credentials = new NetworkCredential("proximacentauriofficial@gmail.com", "jasperp@ssw0rd");                
                try
                {
                    client.Send(msg);
                    retVal = true;
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("Exception in sending email. Message:{0}", ex.ToString());
                    retVal = false;
                }
                finally
                {
                    msg.Dispose();
                }
            }
            return retVal;
        }
    }
}
