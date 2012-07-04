using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace io.iron.ironcache.Data
{
    [Serializable]
    public class IncrementRequest
    {
        public long amount { get; set; }
    }
}
