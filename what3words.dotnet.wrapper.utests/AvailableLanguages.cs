using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace what3words.dotnet.wrapper.utests
{
    public class AvailableLanguages
    {
        private What3WordsV3 api;

        [SetUp]
        public void Setup()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Test]
        public async Task AvailableLanguages_Success()
        {
            var result = await api.AvailableLanguages().RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Languages.Count > 0);
        }

        [Test]
        public async Task ConvertTo3WA_InvalidKey()
        {
            var api = new What3WordsV3("YOUR_API_KEY_HERES");
            var result = await api.AvailableLanguages().RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
        }
    }
}