using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Helpers
{
    public static class UtilityStatusHelper
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
                int numDays = Math.Abs((DateTime.Today.Date - cutoffDate.Value).Days);
                if (result > 0)
                {
                    status = String.Format("Over-Due of: {0} day(s)", numDays);
                }
                else if (result == 0)
                {
                    status = "Due Today";
                }
                else
                {
                    status = String.Format("Due in: {0} day(s)", numDays); 
                }
            }
            catch (Exception)
            {
            }
            return status;
        }
    }
}
