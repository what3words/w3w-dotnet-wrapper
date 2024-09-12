using Xunit;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.utests
{
    public class ConvertTo3WA
    {
        private What3WordsV3 api;

        public ConvertTo3WA()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Fact]
        public async Task ConvertTo3WA_Success()
        {
            var result = await api.ConvertTo3WA(new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Equal("blame.deflection.hills", result.Data.Words);
        }

        [Fact]
        public async Task ConvertTo3WA_CultureVariant_Success()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            var result = await api.ConvertTo3WA(new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Equal("blame.deflection.hills", result.Data.Words);
        }

        [Fact]
        public async Task ConvertTo3WA_SuccessWithLanguage()
        {
            var result = await api.ConvertTo3WA(new Coordinates(51.222011, 0.152311)).Language("es").RequestAsync();
            Assert.Equal("ronca.largos.vegetales", result.Data.Words);
        }

        [Fact]
        public async Task ConvertTo3WA_InvalidKey()
        {
            var api = new What3WordsV3("YOUR_API_KEY_HERE");
            var result = await api.ConvertTo3WA(new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.InvalidKey, result.Error.Error);
        }

        [Fact]
        public async Task ConvertTo3WA_BadCoordinates()
        {
            var result = await api.ConvertTo3WA(new Coordinates(100, 100)).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadCoordinates, result.Error.Error);
        }
    }
}