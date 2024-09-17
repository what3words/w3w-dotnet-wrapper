using Xunit;
using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.utests
{

    public class ConvertToCoordinates
    {
        private What3WordsV3 api;

        public ConvertToCoordinates()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Fact]
        public async Task ConvertToCoordinates_Success()
        {
            var result = await api.ConvertToCoordinates("blame.deflection.hills")
                .RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Equal(51.222011, result.Data.Coordinates.Lat);
            Assert.Equal(0.152311, result.Data.Coordinates.Lng);
        }

        [Fact]
        public async Task ConvertToCoordinates_SuccessWithDifferentLanguage()
        {
            var result = await api.ConvertToCoordinates("ronca.largos.vegetales")
                .RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Equal(51.222011, result.Data.Coordinates.Lat);
            Assert.Equal(0.152311, result.Data.Coordinates.Lng);
        }

        [Fact]
        public async Task ConvertToCoordinates_InvalidKey()
        {
            var api = new What3WordsV3("XXXXXXXX", Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
            var result = await api.ConvertToCoordinates("blame.deflection.hills")
                 .RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.InvalidKey, result.Error.Error);
        }

        [Fact]
        public async Task ConvertToCoordinates_FreePlan()
        {
            var api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_FREE_PLAN_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
            var result = await api.ConvertToCoordinates("blame.deflection.hills")
                 .RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.QuotaExceeded, result.Error.Error);
        }

        [Fact]
        public async Task ConvertToCoordinates_BadWords()
        {
            var result = await api.ConvertToCoordinates("123.aaa.asda").RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadWords, result.Error.Error);
        }
    }
}
