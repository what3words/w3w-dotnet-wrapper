using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.request;

namespace what3words.dotnet.wrapper.utests
{
    public class AutosuggestWithCoordinates
    {
        private What3WordsV3 api;

        [SetUp] 
        public void Setup()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"));
        }

        [Test]
        public async Task AutosuggestWithCoordinates_ValidFocus()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(51.2, 0.2));
            var result = await api.AutosuggestWithCoordinates("blame.deflection.hil", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Coordinates.Lat == 51.222011 && x.Coordinates.Lng == 0.152311));
            Assert.IsTrue(result.Data.Suggestions.Any(x => x.Words == "blame.deflection.hills"));
        }

        [Test]
        public async Task AutosuggestWithCoordinates_Selection()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(51.2, 0.2));
            var result = await api.AutosuggestWithCoordinates("blame.deflection.hil", options).RequestAsync();
            Assert.IsTrue(result.IsSuccessful);
            var selection = result.Data.Suggestions.FirstOrDefault(x => x.Coordinates.Lat == 51.222011 && x.Coordinates.Lng == 0.152311);
            Assert.IsNotNull(selection);
            var submitSelection = await api.AutosuggestSelection("blame.deflection.hil", "text", selection.Words, selection.Rank, options).RequestAsync();
            Assert.IsTrue(submitSelection.IsSuccessful);
        }
    }
}