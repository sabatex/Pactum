using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sabatex.PactumContragent.Models
{
    public class ServiceState
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AllowToUse { get; set; }
        public int TotalUsage { get; set; }
        public int MonthlyUsage { get; set; }
        public int YearlyUsage { get; set; }
        public double Balance { get; set; }
        public int DailyUsage { get; set; }
        public string UserSubscriptionId { get; set; }
        public bool IsDiactivated { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public double RequestPrice { get; set; }
        public int? DailyLimit { get; set; }
        public int? MonthLimit { get; set; }
        public bool IsActive { get; set; }
        public string Id { get; set; }
        public int? YearRequestLimit { get; set; }
        public int DurationInDays { get; set; }
        public Double Price { get; set; }
        public int RequestsLimit { get; set; }

    }

}
