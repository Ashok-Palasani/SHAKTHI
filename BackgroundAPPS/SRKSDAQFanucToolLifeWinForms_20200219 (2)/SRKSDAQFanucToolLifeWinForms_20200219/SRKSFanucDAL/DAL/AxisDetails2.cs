using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRKSFanucDAL.DAL
{
    public class AxisDetails2
    {
        public int AD2ID { get; set; }
        public int MachineID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal FeedRate { get; set; }
        public decimal SpindleLoad { get; set; }
        public decimal SpindleSpeed { get; set; }
        public int IsDeleted { get; set; }
        public DateTime InsertedOn { get; set; }
        public string CorrectedDate { get; set; }
        public int SpindleTemperature { get; set; }
        public int AxisNo { get; set; }
        public string FeedRateUnit { get; set; }
    }
}
