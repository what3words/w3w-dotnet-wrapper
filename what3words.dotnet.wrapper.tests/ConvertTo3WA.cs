using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.tests
{
    [TestClass]
    public class ConvertTo3WA
    {
        [TestMethod]
        public async Task ConvertTo3WASuccess()
        {
            var api = new What3WordsV3("TCRPZKEE");
            var result = await api.ConvertTo3WA().Coordinates(51.222, 0.1523).RequestAsync();
            Assert.AreEqual("blame.deflection.hills", result.Words);
        }

        [TestMethod]
        public async Task ConvertTo3WAInvalidKey()
        {
            var api = new What3WordsV3("TCRPZKEES");
            var result = await api.ConvertTo3WA().Coordinates(51.222, 0.1523).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.InvalidKey, result.Error.Error);
        }

        [TestMethod]
        public async Task ConvertTo3WABadCoordinates()
        {
            var api = new What3WordsV3("TCRPZKEE");
            var result = await api.ConvertTo3WA().Coordinates(100, 100).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadCoordinates, result.Error.Error);
        }
    }
}
