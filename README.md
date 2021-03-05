# <img src="https://what3words.com/assets/images/w3w_square_red.png" width="64" height="64" alt="what3words">&nbsp;w3w-dotnet-wrapper

An .NET library to use the [what3words v3 API](https://docs.what3words.com/api/v3/).

API methods are grouped into a single service object which can be centrally managed by a What3WordsV3 instance. It will act as a factory for all of the API endpoints and will automatically initialize them with your API key.

To obtain an API key, please visit [https://what3words.com/select-plan](https://what3words.com/select-plan) and sign up for an account.

## Installation

The artifact is available through NuGet Package [![NuGet version (what3words.dotnet.wrapper)](https://img.shields.io/nuget/v/what3words.dotnet.wrapper.svg?style=flat-square)](https://www.nuget.org/packages/what3words.dotnet.wrapper/)

## Documentation

See the what3words public API [documentation](https://docs.what3words.com/api/v3/)

## Usage

- **ConvertTo3WA()** - Convert a coordinate (latitude and longitude) to a 3 word address:
```C#
var wrapper = new What3WordsV3("YOUR_API_KEY_HERE");
var result = await wrapper.ConvertTo3WA(new Coordinates(51.222011, 0.152311)).RequestAsync();
```

- **ConvertToCoordinates()** - Convert a 3 word address to a coordinate (latitude and longitude):
```C#
var wrapper = new What3WordsV3("YOUR_API_KEY_HERE");
var result = await wrapper.ConvertToCoordinates("filled.count.soap").RequestAsync();
```

- **Autosuggest()** - AutoSuggest can take a slightly incorrect 3 word address, and suggest a list of valid 3 word addresses. For more autosuggest proprieties similar to *focus* below go to our [documentation](https://developer.what3words.com/public-api/docs#autosuggest)
```C#
var wrapper = new What3WordsV3("YOUR_API_KEY_HERE");
var result = await wrapper.Autosuggest("index.home.r").Focus(51.502,-0.12345).RequestAsync();
```
- **GridSection()** - Returns a section of the 3m x 3m what3words grid for a bounding box.
```C#
var wrapper = new What3WordsV3("YOUR_API_KEY_HERE");
var result = await wrapper.GridSection(new Square(new Coordinates(51.222011, 0.152311), new Coordinates(51.222609, 0.152898))).RequestAsync();
```

- **AvailableLanguages()** - Retrieves a list all available 3 word address languages.
```C#
var wrapper = new What3WordsV3("YOUR_API_KEY_HERE");
var result = await wrapper.AvailableLanguages().RequestAsync();
```

If you run our Enterprise Suite API Server yourself, you may specify the URL to your own server like so:

```C#
var wrapper = What3Words("YOUR_API_KEY_HERE", "https://api.yourserver.com")  
```
