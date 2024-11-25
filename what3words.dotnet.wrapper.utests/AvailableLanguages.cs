using System;
using System.Threading.Tasks;
using Xunit;

namespace what3words.dotnet.wrapper.utests
{
    public class AvailableLanguages
    {
        private readonly What3WordsV3 api;

        public AvailableLanguages()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Fact]
        public async Task AvailableLanguages_Success()
        {
            var result = await api.AvailableLanguages().RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.True(result.Data.Languages.Count > 0);
        }

        [Fact]
        public async Task ConvertTo3WA_InvalidKey()
        {
            var api = new What3WordsV3("YOUR_API_KEY_HERES");
            var result = await api.AvailableLanguages().RequestAsync();
            Assert.False(result.IsSuccessful);
        }
    }
}