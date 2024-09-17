using Xunit;
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

        public AutosuggestWithCoordinates()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Fact]
        public async Task AutosuggestWithCoordinates_ValidFocus()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(51.2, 0.2));
            var result = await api.AutosuggestWithCoordinates("blame.deflection.hil", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Contains(result.Data.Suggestions, x => x.Coordinates.Lat == 51.222011 && x.Coordinates.Lng == 0.152311);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "blame.deflection.hills");
        }

        [Fact]
        public async Task AutosuggestWithCoordinates_Selection()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(51.2, 0.2));
            var result = await api.AutosuggestWithCoordinates("blame.deflection.hil", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            var selection = result.Data.Suggestions.FirstOrDefault(x => x.Coordinates.Lat == 51.222011 && x.Coordinates.Lng == 0.152311);
            Assert.NotNull(selection);
            var submitSelection = await api.AutosuggestSelection("blame.deflection.hil", "text", selection.Words, selection.Rank, options).RequestAsync();
            Assert.True(submitSelection.IsSuccessful);
        }
    }
}