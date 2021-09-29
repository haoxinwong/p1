using System;
using System.Collections.Generic;
using P0_M.Models;

namespace DL.Entities
{
    public class LineItem
    {
        public string Name{get;set;}
        public decimal Price{get;set;}
        public int Quantity{get;set;}
        public int Id{get;set;}
        public int OrderId{get;set;}

        public virtual Order Order { get; set; }
    }
}