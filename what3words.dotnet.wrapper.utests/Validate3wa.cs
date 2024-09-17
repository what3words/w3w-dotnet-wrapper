using Xunit;
using System;
using System.Linq;

namespace what3words.dotnet.wrapper.utests
{
    public class Validate3wa
    {
        private What3WordsV3 api;

        public Validate3wa()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Fact]
        public void IsPossible3wa_Returns_True()
        {
            var result = api.IsPossible3wa("filled.count.soap");
            Assert.True(result);
        }

        [Fact]
        public void IsPossible3wa_Returns_False()
        {
            var result = api.IsPossible3wa("I'm not a 3wa");
            Assert.False(result);
        }

        [Fact]
        public void FindPossible3wa_Returns_3wa_Results()
        {
            var result = api.FindPossible3wa("From index.home.raft to filled.count.soap");
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void FindPossible3wa_Returns_Empty_Results()
        {
            var result = api.FindPossible3wa("There are no 3wa in this sentence");
            Assert.Empty(result);
        }

        [Fact]
        public void IsValid3wa_Returns_True()
        {
            var result = api.IsValid3wa("filled.count.soap");
            Assert.True(result);
        }

        [Fact]
        public void IsValid3wa_Returns_False()
        {
            var result = api.IsValid3wa("aaaa.aaaa.aaaa");
            Assert.False(result);
        }
    }
}