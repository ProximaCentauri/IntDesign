using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Helpers
{
    public static class UtilityHelper
    {
        public static string GetUtilityStatusText(DateTime? cutoffDate, bool withReceipt)
        {
            string status = string.Empty;            
            try
            {
                if (!cutoffDate.HasValue)
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
    }
}
