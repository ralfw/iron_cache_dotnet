using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using io.iron.ironcache.Data;

namespace io.iron.ironcache
{
    public class Client
    {
        private const string HOST = "cache-aws-us-east-1.iron.io";
        private const int    PORT = 443;

        private readonly JavaScriptSerializer _serializer;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None, DefaultValueHandling = DefaultValueHandling.Ignore };
        private readonly RESTadapter _rest;


        public string Host { get; private set; }
        public int Port { get; private set; }


        public Client(string projectId, string token, string host = HOST, int port = PORT) : this(new Credentials(projectId, token), host, port) {}
        public Client(Credentials credentials, string host = HOST, int port = PORT)
        {
            this.Host = host;
            this.Port = port;

            _serializer = new JavaScriptSerializer();
            _rest = new RESTadapter(credentials, host, port, _serializer);
        }


        public Cache Cache(string name)
        {
            return new Cache(_rest, name);
        }

        public Cache this[string cacheNme]
        {
            get { return Cache(cacheNme); }
        }


        public string[] Caches(int page = 0, int perPage = 30)
        {
            var jsonResp = _rest.Get("caches?page=" + page + "&per_page=" + perPage);
            var listCachesResp = JsonConvert.DeserializeObject<ListCachesResponse[]>(jsonResp, _jsonSerializerSettings);
            return listCachesResp.Select(lcr => lcr.name).ToArray();
        }
    }
}
