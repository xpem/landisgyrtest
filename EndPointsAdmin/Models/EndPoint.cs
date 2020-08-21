using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsAdmin.Models
{
   public class EndPoint : IEndPoint
    {
        public string SerialNumber { get; set; }
        public int MeterModelId { get; set; }
        public int MeterNumber { get; set; }
        public string MeterFirmwareVersion { get; set; }
        public int SwitchState { get; set; }
    }
}
