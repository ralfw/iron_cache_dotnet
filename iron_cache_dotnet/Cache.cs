using System;
using System.Net;
using Newtonsoft.Json;
using io.iron.ironcache.Data;

namespace io.iron.ironcache
{
    public class Cache
    {
        private readonly RESTadapter _rest;
        private readonly string _name;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None, DefaultValueHandling = DefaultValueHandling.Ignore };


        internal Cache(RESTadapter rest, string name)
        {
            _rest = rest;
            _name = name;
        }


        public object this[string key]
        {
            get { return Get<object>(key); }
            set { Replace(key, value); }
        }


        public void Add(string key, object value, long expiresIn = 604800)
        {
            var jsonReq = JsonConvert.SerializeObject(new ItemAddRequest() { value = value, expires_in = expiresIn, add = true});
            _rest.Put("caches/" + _name + "/items/" + key, jsonReq);
        }

        public void Replace(string key, object value, long expiresIn = 604800)
        {
            var jsonReq = JsonConvert.SerializeObject(new ItemReplaceRequest() { value = value, expires_in = expiresIn, replace = true });
            _rest.Put("caches/" + _name + "/items/" + key, jsonReq);            
        }


        public T Get<T>(string key) { return (T)Get(key); }
        public object Get(string key)
        {
            var jsonResp = _rest.Get("caches/" + _name + "/items/" + key);
            var cacheResp = JsonConvert.DeserializeObject<GetResponse>(jsonResp, _jsonSerializerSettings);
            return cacheResp.value;
        }


        public void Delete(string key)
        {
            _rest.Delete("caches/" + _name + "/items/" + key);
        }


        public long Increment(string key) { return Increment(key, 1); }
        public long Increment(string key, long amount)
        {
            var jsonReq = JsonConvert.SerializeObject(new IncrementRequest() { amount = amount });
            var jsonResp = _rest.Post("caches/" + _name + "/items/" + key + "/increment", jsonReq);
            var incrementResp = JsonConvert.DeserializeObject<IncrementResponse>(jsonResp, _jsonSerializerSettings);
            return incrementResp.value;
        }
    }
}