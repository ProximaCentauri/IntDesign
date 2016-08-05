﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Cryptography;

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

        public static bool CheckConnection(String URL, ref string exception)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
            catch(WebException ex)
            {
                exception = ex.Message;
                return false;
            }
        }

        public static string Protect(string str)
        {
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            byte[] data = Encoding.ASCII.GetBytes(str);
            string protectedData = Convert.ToBase64String(ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser));
            return protectedData;
        }
    }
}
