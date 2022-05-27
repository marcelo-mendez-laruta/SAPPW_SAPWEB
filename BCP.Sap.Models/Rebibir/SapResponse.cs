using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class SapResponse
    {
        public string state { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public string operation { get; set; }
        public List<string> errors { get; set; }
    }
}
