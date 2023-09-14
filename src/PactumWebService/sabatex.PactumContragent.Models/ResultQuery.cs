using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.PactumContragent.Models
{
    public class ResultQuery<T> where T:class
    {
        public T Result { get; set; }
        public string Error { get; set; }
        public bool IsSucces { get; set; }
        public bool IsCashed { get; set; }
        public int Attempt { get; set; }
        public DateTime Date { get; set; }
    }
}
