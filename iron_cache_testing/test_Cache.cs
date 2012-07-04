using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;
using io.iron.ironcache;

namespace iron_cache_testing
{
    [TestFixture]
    public class test_Cache
    {
        [Test]
        public void Get_for_missing_key()
        {
            var sut = new Client(CredentialsRepository.LoadFrom("ironmq.credentials.txt"));
            var c = sut.Cache("test_cache");

            var wex = Assert.Throws<WebException>(() => c.Get("xyz"));
            Assert.IsTrue(wex.Message.ToLower().IndexOf("not found") >= 0);
        }


        [Test]
        public void Add_get()
        {
            var sut = new Client(CredentialsRepository.LoadFrom("ironmq.credentials.txt"));
            var c = sut.Cache("test_cache");

            c.Add("k", "hello");
            try
            {
                Assert.AreEqual("hello", c.Get<string>("k"));
            }
            finally
            {
                c.Delete("k");
            }
        }

        [Test]
        public void Replace_get()
        {
            var sut = new Client(CredentialsRepository.LoadFrom("ironmq.credentials.txt"));
            var c = sut.Cache("test_cache");

            c.Add("k", "hello");
            try
            {
                c.Replace("k", "world");
                Assert.AreEqual("world", c.Get("k"));
            }
            finally
            {
                c.Delete("k");
            }
        }


        [Test]
        public void Put_get()
        {
            var sut = new Client(CredentialsRepository.LoadFrom("ironmq.credentials.txt"));
            var c = sut.Cache("test_cache");

            c.Put("k", "hello");
            try
            {
                Assert.AreEqual("hello", c.Get("k"));

                c["k"] = "world";
                Assert.AreEqual("world", c.Get("k"));
            }
            finally
            {
                c.Delete("k");
            }
        }


        [Test]
        public void Delete()
        {
            var sut = new Client(CredentialsRepository.LoadFrom("ironmq.credentials.txt"));
            var c = sut.Cache("test_cache");
   
            c.Add("k", "hello");
            c.Delete("k");

            var wex = Assert.Throws<WebException>(() => c.Get("k"));
            Assert.IsTrue(wex.Message.ToLower().IndexOf("not found")>=0);
        }


        [Test]
        public void Increment()
        {
            var sut = new Client(CredentialsRepository.LoadFrom("ironmq.credentials.txt"));
            var c = sut.Cache("test_cache");

            c.Add("k", 41);
            try
            {
                Assert.AreEqual(42, c.Increment("k"));
                Assert.AreEqual(41, c.Increment("k", -1));
            }
            finally
            {
                c.Delete("k");
            }
        }


        [Test]
        public void List_caches()
        {
            var sut = new Client(CredentialsRepository.LoadFrom("ironmq.credentials.txt"));
            var c = sut.Cache("test_cache");
            c.Add("k", 0);
            c.Delete("k");

            var cacheNames = sut.Caches();
            Assert.That(cacheNames, Is.EquivalentTo(new[] {"test_cache"}));
        }
    }
}
