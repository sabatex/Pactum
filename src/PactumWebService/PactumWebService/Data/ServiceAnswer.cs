using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PactumWebService.Data
{
    public class ServiceAnswer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public int Attempt { get; set; }
        public string Answer { get; set; }
    }
}
