using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace io.iron.ironcache.Data
{
    [Serializable]
    public class ListCachesResponse
    {
        public string project_id { get; set; }
        public string name { get; set; }
    }
}
