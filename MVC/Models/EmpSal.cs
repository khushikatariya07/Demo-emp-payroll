using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class EmpSal
    {
        public int SalaryId {get;set;}
        public int EmpId {get;set;}

        public string Name{get;set;}
        public int Month {get;set;}
        public int Year {get;set;}
        public decimal Basic {get;set;}
        public decimal Allow {get;set;}
        public decimal Deduct {get;set;}
        public decimal Net {get;set;}
    }
}