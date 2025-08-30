using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo.Order
{
    public class CargoCassMallEntity
    {
        public int CID { get; set; }
        public int SourceType { get; set; }
        public string SourceAction { get; set; }
        public DateTime OPDATE { get; set; }
        public string ResJson { get; set; }
        public string orderId { get; set; }
    }
}
