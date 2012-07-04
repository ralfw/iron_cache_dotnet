using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace io.iron.ironcache.Data
{
    [Serializable]
    [JsonObject]
    public class ItemAddRequest
    {
        public object value { get; set; }

        [DefaultValue(604800)]
        public long expires_in { get; set; }

        [DefaultValue(true)]
        public bool add { get; set; }
    }

    [Serializable]
    [JsonObject]
    public class ItemReplaceRequest
    {
        public object value { get; set; }

        [DefaultValue(604800)]
        public long expires_in { get; set; }

        [DefaultValue(true)]
        public bool replace { get; set; }
    }
}
