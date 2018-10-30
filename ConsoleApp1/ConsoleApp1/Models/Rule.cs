using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Rule
    {
        public string FileTemplate { get; set; }
        public string ToFolder { get; set; }
        public bool IsOrderAdded { get; set; }
        public bool IsDateAdded { get; set; }

        public int MatchCount { get; set; }
    }
}
