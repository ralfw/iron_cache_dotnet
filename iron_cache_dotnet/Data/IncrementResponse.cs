using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace io.iron.ironcache.Data
{
    [Serializable]
    public class IncrementResponse
    {
        public string msg { get; set; }
        public long value { get; set; }
    }
}
