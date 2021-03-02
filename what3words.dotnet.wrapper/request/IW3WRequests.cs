using Refit;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;
using static what3words.dotnet.wrapper.request.AutosuggestRequest;
using static what3words.dotnet.wrapper.request.ConvertTo3WARequest;

public interface IW3WRequests
{
    [Get("/convert-to-3wa")]
    Task<Address> ConvertTo3WA(ConvertTo3WAParams queryParams);

    [Get("/convert-to-coordinates?words={words}")]
    Task<Address> ConvertToCoordinates(string words);

    [Get("/available-languages")]
    Task<AvailableLanguages> AvailableLanguages();

    [Get("/grid-section?bounding-box={boundingBox}")]
    Task<GridSection> GridSection(string boundingBox);

    [Get("/autosuggest")]
    Task<AutoSuggest> AutoSuggest(AutosuggestRequestParams queryParams);
}