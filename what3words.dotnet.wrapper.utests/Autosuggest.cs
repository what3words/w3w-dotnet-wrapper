using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.request;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.utests
{
    public class Autosuggest
    {
        private What3WordsV3 api;

        [SetUp] 
        public void Setup()
        {
            api = new What3WordsV3("YOUR_API_KEY_HERE");
        }

        [Test]
        public async Task Autosuggest_NoParams()
        {
            var result = await api.Autosuggest("index.home.raf").RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ValidFocus()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(51.2, 0.2));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_BadFocus()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(151.2, 0.2));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadFocus, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToCircle()
        {
            var options = new AutosuggestOptions().SetClipToCircle(new Coordinates(-90.000000, 360.0), 100);
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
            Assert.IsTrue(result.IsSuccessful);
        }

        [Test]
        public async Task Autosuggest_BadClipToCircle()
        {
            var options = new AutosuggestOptions().SetClipToCircle(new Coordinates(-91.000000, 360.0), 100);
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToCircle, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBox()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Square(new Coordinates(50, -5), new Coordinates(53, 2)));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingInfinitelySmall()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Square(new Coordinates(50, -5), new Coordinates(50, -5)));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBoxLng()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Square(new Coordinates(50, -5), new Coordinates(53, 3544)));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBoxThatWrapsAroundWorldButExcludesLondon()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Square(new Coordinates(50, 2), new Coordinates(53, -5 + 360)));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToBoundingBoxThatWrapsAroundPolesButExcludesLondon()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Square(new Coordinates(53, -5), new Coordinates(50 + 180, 2)));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToBoundingBox, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToCountryThatDoesNotExist()
        {
            var options = new AutosuggestOptions().SetClipToCountry(new List<string>() { "ZX" });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsFalse(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipInvalidCountry()
        {
            var options = new AutosuggestOptions().SetClipToCountry(new List<string>() { "ZXC" });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToCountry, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToPolygon()
        {
            var options = new AutosuggestOptions().SetClipToPolygon(new List<Coordinates>() { new Coordinates(51, -1), new Coordinates(53, 0), new Coordinates(51, 1), new Coordinates(51, -1) });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

        [Test]
        public async Task Autosuggest_ClipToPolygonWithFewPoints()
        {
            var options = new AutosuggestOptions().SetClipToPolygon(new List<Coordinates>() { new Coordinates(51, -1), new Coordinates(53, 0), new Coordinates(51, 1) });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(What3WordsError.BadClipToPolygon, result.Error.Error);
        }

        [Test]
        public async Task Autosuggest_ClipToPolygonWithHugeLongitude()
        {
            var options = new AutosuggestOptions().SetClipToPolygon(new List<Coordinates>() { new Coordinates(51, -1 - 180), new Coordinates(53, 0), new Coordinates(51, 1 + 180), new Coordinates(51, -1 - 180) });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "index.home.raft"));
        }

    }
}