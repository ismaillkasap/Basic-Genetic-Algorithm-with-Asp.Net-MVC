using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneticAlgorithm.Models
{
    public class Log
    {
        public virtual int iteration { get; set; }
        public virtual string a { get; set; }
        public virtual string b { get; set; }
        public virtual string c { get; set; }
        public virtual string d { get; set; }
        public virtual DateTime log_time { get; set; }
    }
}