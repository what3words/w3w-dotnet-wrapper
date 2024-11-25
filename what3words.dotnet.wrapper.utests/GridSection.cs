using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;
using Xunit;

namespace what3words.dotnet.wrapper.utests
{
    public class GridSection
    {
        private readonly What3WordsV3 api;

        public GridSection()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Fact]
        public async Task GridSection_Success()
        {
            var result = await api.GridSection(new Coordinates(51.222011, 0.152311), new Coordinates(51.222609, 0.152898)).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.True(result.Data.Lines.Count > 0);
        }

        [Fact]
        public async Task GridSection_InvalidKey()
        {
            var api = new What3WordsV3("YOUR_API_KEY_HERES", Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
            var result = await api.GridSection(new Coordinates(51.222011, 0.152311), new Coordinates(51.222609, 0.152898)).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.InvalidKey, result.Error.Error);
        }

        [Fact]
        public async Task GridSection_BoundingBoxTooBig()
        {
            var result = await api.GridSection(new Coordinates(51.222609, -0.152898), new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadBoundingBoxTooBig, result.Error.Error);
        }

        [Fact]
        public async Task GridSection_BadBoundingBox()
        {
            var result = await api.GridSection(new Coordinates(100, 100), new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadBoundingBox, result.Error.Error);
        }
    }
}