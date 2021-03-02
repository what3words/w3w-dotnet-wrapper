using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.utests
{
    public class Autosuggest
    {
        private What3WordsV3 api;

        [SetUp] 
        public void Setup()
        {
            api = new What3WordsV3("TCRPZKEE");
        }

        [Test]
        public async Task Autosuggest_ValidFocus()
        {
            var result = await api.Autosuggest("index.home.ra")
                .Focus(new Coordinates(51.2, 0.2))
                .RequestAsync();
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_BadFocus()
        {
            var result = await api.Autosuggest("index.home.ra")
                .Focus(new Coordinates(151.2, 0.2))
                .RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadFocus, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToCircle()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToCircle(new Coordinates(-90.000000, 360.0), 100)
                .RequestAsync();
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
            Assert.IsTrue(result.IsSuccessful);
        }

        [Test]
        public async Task Autosuggest_BadClipToCircle()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToCircle(new Coordinates(-91.000000, 360.0), 100)
                .RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToCircle, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBox()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToBoundingBox(new Square(new Coordinates(50, -5), new Coordinates(53, 2)))
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingInfinitelySmall()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToBoundingBox(new Square(new Coordinates(50, -5), new Coordinates(50, -5)))
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBoxLng()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToBoundingBox(new Square(new Coordinates(50, -5), new Coordinates(53, 3544)))
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBoxThatWrapsAroundWorldButExcludesLondon()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToBoundingBox(new Square(new Coordinates(50, 2), new Coordinates(53, -5 + 360)))
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBoxThatWrapsAroundPolesButExcludesLondon()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToBoundingBox(new Square(new Coordinates(53, -5), new Coordinates(50 + 180, 2)))
                .RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToBoundingBox, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToCountryThatDoesNotExist()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToCountry(new List<string>() { "ZX" })
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipInvalidCountry()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToCountry(new List<string>() { "ZXC" })
                .RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToCountry, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToPolygon()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToPolygon(new List<Coordinates>() { new Coordinates(51, -1), new Coordinates(53, 0), new Coordinates(51, 1), new Coordinates(51, -1) })
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToPolygonWithFewPoints()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToPolygon(new List<Coordinates>() { new Coordinates(51, -1), new Coordinates(53, 0), new Coordinates(51, 1) })
                .RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToPolygon, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToPolygonWithHugeLongitude()
        {
            var result = await api.Autosuggest("index.home.ra")
                .ClipToPolygon(new List<Coordinates>() { new Coordinates(51, -1 - 180), new Coordinates(53, 0), new Coordinates(51, 1 + 180), new Coordinates(51, -1 - 180) })
                .RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

    }
}