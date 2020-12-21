using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quality_PiwebApp
{
   public class QualityData
    {        
        public string PartNumber { get; set; }
        public Nullable<int> OperationNum { get; set; }
        public Nullable<int> Op10 { get; set; }
        public Nullable<int> Op20 { get; set; }
        public Nullable<int> Op30 { get; set; }
        public Nullable<int> Op40 { get; set; } // final inspection operation
        public string PartIdentity { get; set; }
        public Nullable<int> MachineID { get; set; }
        public string CorrectedDate { get; set; }
        public int IsPiweb { get; set; }
        public int Status { get; set; }
        public Nullable<int> RejectedQty { get; set; }
        public Nullable<int> TotalQty { get; set; }
        public string WorkOrderNum { get; set; }
        public DateTime MeasureDateTime { get; set; }

    }

    public class QUALITY
    {
        public string PartNumber { get; set; }        
        public List<QualityData> QualityDatameasurements { get; set; }
    }
}
