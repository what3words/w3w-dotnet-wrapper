using NUnit.Framework;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.utests
{

    public class ConvertToCoordinates
    {
        private What3WordsV3 api;

        [SetUp]
        public void Setup()
        {
            api = new What3WordsV3("TCRPZKEE");
        }

        [Test]
        public async Task ConvertToCoordinates_Success()
        {
            var result = await api.ConvertToCoordinates("blame.deflection.hills")
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(51.222011, result.Data.Coordinates.Lat);
            Assert.AreEqual(0.152311, result.Data.Coordinates.Lng);
        }

        [Test]
        public async Task ConvertToCoordinates_SuccessWithDifferentLanguage()
        {
            var result = await api.ConvertToCoordinates("ronca.largos.vegetales")
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(51.222011, result.Data.Coordinates.Lat);
            Assert.AreEqual(0.152311, result.Data.Coordinates.Lng);
        }

        [Test]
        public async Task ConvertToCoordinates_InvalidKey()
        {
            var api = new What3WordsV3("TCRPZKEES");
            var result = await api.ConvertToCoordinates("blame.deflection.hills")
                 .RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.InvalidKey, result.Error.Error);
        }

        [Test]
        public async Task ConvertToCoordinates_BadWords()
        {
            var result = await api.ConvertToCoordinates("123.aaa.asda").RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadWords, result.Error.Error);
        }
    }
}
