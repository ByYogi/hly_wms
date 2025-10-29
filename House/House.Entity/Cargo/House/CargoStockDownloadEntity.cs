using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo.House
{
    public class CargoStockDownloadEntity
    {
        public long ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime OP_DATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
