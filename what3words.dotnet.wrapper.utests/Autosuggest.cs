using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.request;
using what3words.dotnet.wrapper.response;
using Xunit;

namespace what3words.dotnet.wrapper.utests
{
    public class Autosuggest
    {
        private readonly What3WordsV3 api;

        public Autosuggest()
        {
            api = new What3WordsV3(Environment.GetEnvironmentVariable("W3W_API_KEY"), Environment.GetEnvironmentVariable("W3W_API_ENDPOINT"));
        }

        [Fact]
        public async Task Autosuggest_NoParams()
        {
            var result = await api.Autosuggest("index.home.raf").RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_ValidFocus()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(51.2, 0.2));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_Culture_ValidFocus()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
            var options = new AutosuggestOptions().SetFocus(new Coordinates(51.2, 0.2));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }


        [Fact]
        public async Task Autosuggest_BadFocus()
        {
            var options = new AutosuggestOptions().SetFocus(new Coordinates(151.2, 0.2));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadFocus, result.Error.Error);
        }

        [Fact]
        public async Task Autosuggest_ClipToCircle()
        {
            var options = new AutosuggestOptions().SetClipToCircle(new Coordinates(-90.000000, 360.0), 100);
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Data.Suggestions);
            Assert.DoesNotContain(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_Culture_ClipToCircle()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
            var options = new AutosuggestOptions().SetClipToCircle(new Coordinates(-90.000000, 360.0), 100);
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Data.Suggestions);
            Assert.DoesNotContain(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_BadClipToCircle()
        {
            var options = new AutosuggestOptions().SetClipToCircle(new Coordinates(-91.000000, 360.0), 100);
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadClipToCircle, result.Error.Error);
        }

        [Fact]
        public async Task Autosuggest_ClipToBoundingBox()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Coordinates(50, -5), new Coordinates(53, 2));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_Culture_ClipToBoundingBox()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Coordinates(50.001, -5.001), new Coordinates(53.001, 2.001));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_ClipToBoundingInfinitelySmall()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Coordinates(50, -5), new Coordinates(50, -5));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Data.Suggestions);
            Assert.DoesNotContain(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_ClipToBoundingBoxLng()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Coordinates(50, -5), new Coordinates(53, 3544));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_ClipToBoundingBoxThatWrapsAroundWorldButExcludesLondon()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Coordinates(50, 2), new Coordinates(53, -5 + 360));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.DoesNotContain(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_ClipToBoundingBoxThatWrapsAroundPolesButExcludesLondon()
        {
            var options = new AutosuggestOptions().SetClipToBoundingBox(new Coordinates(53, -5), new Coordinates(50 + 180, 2));
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadClipToBoundingBox, result.Error.Error);
        }

        [Fact]
        public async Task Autosuggest_ClipToCountryThatDoesNotExist()
        {
            var options = new AutosuggestOptions().SetClipToCountry(new List<string>() { "ZX" });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.Empty(result.Data.Suggestions);
            Assert.DoesNotContain(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_ClipInvalidCountry()
        {
            var options = new AutosuggestOptions().SetClipToCountry(new List<string>() { "ZXC" });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadClipToCountry, result.Error.Error);
        }

        [Fact]
        public async Task Autosuggest_ClipToPolygon()
        {
            var options = new AutosuggestOptions().SetClipToPolygon(new List<Coordinates>() { new Coordinates(51.001, -1.001), new Coordinates(53.001, 0.001), new Coordinates(51.001, 1.001), new Coordinates(51.001, -1.001) });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_Culture_ClipToPolygon()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
            var options = new AutosuggestOptions().SetClipToPolygon(new List<Coordinates>() { new Coordinates(51.001, -1.001), new Coordinates(53.001, 0.001), new Coordinates(51.001, 1.001), new Coordinates(51.001, -1.001) });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

        [Fact]
        public async Task Autosuggest_ClipToPolygonWithFewPoints()
        {
            var options = new AutosuggestOptions().SetClipToPolygon(new List<Coordinates>() { new Coordinates(51.001, -1.001), new Coordinates(53.001, 0.001), new Coordinates(51.001, 1.001) });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.False(result.IsSuccessful);
            Assert.Equal(What3WordsError.BadClipToPolygon, result.Error.Error);
        }

        [Fact]
        public async Task Autosuggest_ClipToPolygonWithHugeLongitude()
        {
            var options = new AutosuggestOptions().SetClipToPolygon(new List<Coordinates>() { new Coordinates(51.001, -1.001 - 180.001), new Coordinates(53.001, .0010), new Coordinates(51.001, 1.001 + 180.001), new Coordinates(51.001, -1.001 - 180.001) });
            var result = await api.Autosuggest("index.home.ra", options).RequestAsync();
            Assert.True(result.IsSuccessful);
            Assert.NotEmpty(result.Data.Suggestions);
            Assert.Contains(result.Data.Suggestions, x => x.Words == "index.home.raft");
        }

    }
}