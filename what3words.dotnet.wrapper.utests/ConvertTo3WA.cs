using NUnit.Framework;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.utests
{
    public class ConvertTo3WA
    {
        private What3WordsV3 api;

        [SetUp]
        public void Setup()
        {
            api = new What3WordsV3("XXXXXXXX");
        }

        [Test]
        public async Task ConvertTo3WA_Success()
        {
            var result = await api.ConvertTo3WA().Coordinates(new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.AreEqual("blame.deflection.hills", result.Data.Words);
        }

        [Test]
        public async Task ConvertTo3WA_SuccessWithLanguage()
        {
            var result = await api.ConvertTo3WA().Coordinates(new Coordinates(51.222011, 0.152311)).Language("es").RequestAsync();
            Assert.AreEqual("ronca.largos.vegetales", result.Data.Words);
        }

        [Test]
        public async Task ConvertTo3WA_InvalidKey()
        {
            var api = new What3WordsV3("TCRPZKEES");
            var result = await api.ConvertTo3WA().Coordinates(new Coordinates(51.222011, 0.152311)).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.InvalidKey, result.Error.Error);
        }

        [Test]
        public async Task ConvertTo3WA_BadCoordinates()
        {
            var result = await api.ConvertTo3WA().Coordinates(new Coordinates(100, 100)).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadCoordinates, result.Error.Error);
        }
    }
}