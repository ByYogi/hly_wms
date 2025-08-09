using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 运单路线图
    /// </summary>
    [Serializable]
    public class AwbRoadEntity
    {
        public string BelongSystem { get; set; }
        public Int64 AwbID { get; set; }
        public string AwbNo { get; set; }
        public string Dep { get; set; }
        public string Dest { get; set; }
        public string Transit { get; set; }
        public int Piece { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public int AwbPiece { get; set; }
        public string ShipperName { get; set; }
        public string ShipperUnit { get; set; }
        public string AcceptUnit { get; set; }
        public string AcceptPeople { get; set; }
        public string CreateAwb { get; set; }
        public DateTime CreateDate { get; set; }
        public string ContractNum { get; set; }
        public string ReturnStatus { get; set; }
        public string ReturnInfo { get; set; }
        public string Signer { get; set; }
        public DateTime SignTime { get; set; }
        public DateTime ReturnDate { get; set; }
        public string SendReturnAwbStatus { get; set; }
        public DateTime SendReturnAwbDate { get; set; }
        public string ConfirmReturnAwbStatus { get; set; }
        public DateTime ConfirmReturnAwbDate { get; set; }
        public string TruckNum { get; set; }
        public DateTime StartTime { get; set; }
        public string Loader { get; set; }
        public string Manifester { get; set; }
        public string Driver { get; set; }
        public string DriverCellPhone { get; set; }
        public string DestPeople { get; set; }
        public string DestCellphone { get; set; }
        public DateTime ActArriveTime { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
