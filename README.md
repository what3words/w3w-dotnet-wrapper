![what3words](https://avatars.githubusercontent.com/u/170637712?s=110 'what3words')

# w3w-dotnet-wrapper

An .NET library to use the [what3words v3 API](https://docs.what3words.com/api/v3/).

API methods are grouped into a single service object which can be centrally managed by a What3WordsV3 instance. It will act as a factory for all of the API endpoints and will automatically initialize them with your API key.

## Installation

The artifact is available through NuGet Package [![NuGet version (what3words.dotnet.wrapper)](https://img.shields.io/nuget/v/what3words.dotnet.wrapper.svg?style=flat-square)](https://www.nuget.org/packages/what3words.dotnet.wrapper/)

## Documentation

See the what3words public API [documentation](https://docs.what3words.com/api/v3/)

## Setup

To obtain an API key, please visit [https://what3words.com/select-plan](https://what3words.com/select-plan) and sign up for an account.

```C#
var wrapper = new What3WordsV3("YOUR_API_KEY_HERE");
```

If you run our Enterprise Suite API Server yourself, you may specify the URL to your own server like so:

```C#
var wrapper = new What3Words("YOUR_API_KEY_HERE", "https://api.yourserver.com")
```

## Usage

- **ConvertTo3WA()** - Convert a coordinate (latitude and longitude) to a 3 word address:

```C#
var result = await wrapper.ConvertTo3WA(new Coordinates(51.222011, 0.152311)).RequestAsync();
```

- **ConvertToCoordinates()** - Convert a 3 word address to a coordinate (latitude and longitude):

```C#
var result = await wrapper.ConvertToCoordinates("filled.count.soap").RequestAsync();
```

- **Autosuggest()** - AutoSuggest can take a slightly incorrect 3 word address, and suggest a list of valid 3 word addresses. For more autosuggest proprieties similar to _focus_ below go to our [documentation](https://developer.what3words.com/public-api/docs#autosuggest)

```C#
var result = await wrapper.Autosuggest("index.home.r", new AutosuggestOptions().SetFocus(51.502,-0.12345)).RequestAsync();
```

- **GridSection()** - Returns a section of the 3m x 3m what3words grid for a bounding box (Coordinats SW, Coordinates NE).

```C#
var result = await wrapper.GridSection(new Coordinates(51.222011, 0.152311), new Coordinates(51.222609, 0.152898)).RequestAsync();
```

- **AvailableLanguages()** - Retrieves a list all available 3 word address languages.

```C#
var result = await wrapper.AvailableLanguages().RequestAsync();
```

- **IsPossible3wa()** - This method takes a string as a parameter and returns whether the string is in the format of a 3WA (eg "filled.count.soap"). Return type is boolean.
  > :warning: NOTE: Does not check if it is an actual existing 3WA.

```C#
var result = wrapper.IsPossible3wa("filled.count.so"); // true
```

- **FindPossible3wa()** - This method takes a string as a parameter and searches the string for any possible instances of a 3WA - e.g. "leave in my porch at word.word.word." Likely to be the main method that is called on the delivery notes. Returns an array of matched items. Returns an empty array if no matches are found.
  > :warning: NOTE: Does not check if it is an actual existing 3WA.

```C#
var result = wrapper.FindPossible3wa("from index.home.raft to filled.count.soap"); // IEnumerable<string>[ "index.home.raft", "filled.count.soap"]
```

- **IsValid3wa()** - This method takes a string as a parameter and first passes it through the W3W regex filter (akin to calling isPossible3wa() on the string) and then calls the W3W api to verify it is a real 3WA.

```C#
var result = wrapper.IsValid3wa("filled.count.soap"); // true
```

## Tests

To run the tests, you need to provide the following environment variables:

- `W3W_API_KEY` - A valid API Key (with autosuggest with coordinates enabled - otherwise tests related to `v3/autosuggest-with-endpoint` will fail)
- `W3W_API_ENDPOINT` - (optional) You can override the default endpoint which is https://api.what3words.com (perhaps a self hosted or a local instance of the what3words api).

```bash
dotnet test what3words.dotnet.wrapper.utests/what3words.dotnet.wrapper.utests.csproj

Test run for /w3w-dotnet-wrapper/what3words.dotnet.wrapper.utests/bin/Debug/net5.0/what3words.dotnet.wrapper.utests.dll(.NETCoreApp,Version=v5.0)
Microsoft (R) Test Execution Command Line Tool Version 16.11.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 42
     Passed: 42
 Total time: 7.0222 Seconds
```

## Changes

### v4.0.0

- **breaking change**: moved `AutosuggestInputType` and `Request<T>` to `what3words.dotnet.wrapper.request` namespace

### v3.1.1

- fix: revert Refit v6.0.24

### v3.1.0

- feat: validation helper methods

### v3.0.5

- fix: updated Line class visibility

### v3.0.4

- fix: culture variant on coordinates double to URL
