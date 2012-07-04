﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

using io.iron.ironcache.Data;

namespace io.iron.ironcache
{
    public class Client
    {
        private const string HOST = "cache-aws-us-east-1.iron.io";
        private const int    PORT = 443;

        private readonly JavaScriptSerializer _serializer;
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


        public string[] Caches(int page = 100)
        {
            throw new NotImplementedException();
        }
    }
}
