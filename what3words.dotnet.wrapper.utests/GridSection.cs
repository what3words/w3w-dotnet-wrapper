using NUnit.Framework;
using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.utests
{
    public class GridSection
    {
        private What3WordsV3 api;

        [SetUp]
        public void Setup()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Test]
        public async Task GridSection_Success()
        {
            var result = await api.GridSection(new Coordinates(51.222011, 0.152311), new Coordinates(51.222609, 0.152898)).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Lines.Count > 0);
        }

        [Test]
        public async Task GridSection_InvalidKey()
        {
            var api = new What3WordsV3("YOUR_API_KEY_HERES");
            var result = await api.GridSection(new Coordinates(51.222011, 0.152311), new Coordinates(51.222609, 0.152898)).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.InvalidKey, result.Error.Error);
        }

        [Test]
        public async Task GridSection_BoundingBoxTooBig()
        {
            var result = await api.GridSection(new Coordinates(51.222609, -0.152898), new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadBoundingBoxTooBig, result.Error.Error);
        }

        [Test]
        public async Task GridSection_BadBoundingBox()
        {
            var result = await api.GridSection(new Coordinates(100,100), new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadBoundingBox, result.Error.Error);
        }
    }
}