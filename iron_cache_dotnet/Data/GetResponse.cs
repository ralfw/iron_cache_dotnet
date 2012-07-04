using System;

namespace io.iron.ironcache.Data
{
    [Serializable]
    public class GetResponse
    {
        public string cache { get; set; }
        public string key { get; set; }
        public object value { get; set; }
    }
}