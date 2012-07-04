using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using io.iron.ironcache.Data;

namespace io.iron.ironcache
{
    class RESTadapter
    {
        private const string PROTO = "https";
        private const string API_VERSION = "1";


        private readonly JavaScriptSerializer _serializer;
        private readonly Credentials _credentials;
        private readonly string _host;
        private readonly int _port;


        public RESTadapter(Credentials credentials, string host, int port, JavaScriptSerializer serializer)
        {
            _credentials = credentials;
            _host = host;
            _port = port;
            _serializer = serializer;
        }


        public string Delete(string endpoint)
        {
            return Request("DELETE", endpoint, null);
        }

        public string Get(string endpoint)
        {
            return Request("GET", endpoint, null);
        }

        public string Post(string endpoint, string body)
        {
            return Request("POST", endpoint, body);
        }


        private string Request(string method, string endpoint, string body)
        {
            string path = "/" + API_VERSION + "/projects/" + _credentials.ProjectId + "/" + endpoint;
            string uri = PROTO + "://" + _host + ":" + _port + path;
            var request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "OAuth " + _credentials.Token);
            request.UserAgent = "IronMQ .Net Client";
            request.Method = method;
            if (body != null)
            {
                using (var write = new StreamWriter(request.GetRequestStream()))
                {
                    write.Write(body);
                    write.Flush();
                }
            }

            var response = (HttpWebResponse)request.GetResponse();
            var json = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = _serializer.Deserialize<Error>(json);
                throw new System.Web.HttpException((int)response.StatusCode, error.msg);
            }
            return json;
        }
    }
}
