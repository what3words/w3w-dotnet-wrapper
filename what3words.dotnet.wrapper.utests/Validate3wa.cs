using NUnit.Framework;
using System;
using System.Linq;

namespace what3words.dotnet.wrapper.utests
{
    public class Validate3wa
    {
        private What3WordsV3 api;

        [SetUp]
        public void Setup()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Test]
        public void IsPossible3wa_Returns_True()
        {
            var result = api.IsPossible3wa("filled.count.soap");
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPossible3wa_Returns_False()
        {
            var result = api.IsPossible3wa("I'm not a 3wa");
            Assert.IsFalse(result);
        }

        [Test]
        public void FindPossible3wa_Returns_3wa_Results()
        {
            var result = api.FindPossible3wa("From index.home.raft to filled.count.soap");
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void FindPossible3wa_Returns_Empty_Results()
        {
            var result = api.FindPossible3wa("There are no 3wa in this sentence");
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void IsValid3wa_Returns_True()
        {
            var result = api.IsValid3wa("filled.count.soap");
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValid3wa_Returns_False()
        {
            var result = api.IsValid3wa("aaaa.aaaa.aaaa");
            Assert.IsFalse(result);
        }
    }
}