using System;
using System.Collections.Generic;
using System.Text;

namespace KindergartenLogger.Models
{
    public class Guardian
    {
        public string Name { get; set; }
        public string OIB { get; set; }
        public byte[] Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool ReceiveArrivalText { get; set; }
        public bool ReceiveDepartureText { get; set; }


    }
}
