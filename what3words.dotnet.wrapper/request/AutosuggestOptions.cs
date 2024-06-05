using Refit;
using System.Collections.Generic;
using System.Globalization;
using what3words.dotnet.wrapper.models;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AutosuggestOptions
    {
        [AliasAs("n-results")]
        public string NResults { get; private set; }

        [AliasAs("focus")]
        public string Focus { get; private set; }

        [AliasAs("n-focus-results")]
        public string NFocusResults { get; private set; }

        [AliasAs("clip-to-country")]
        public string ClipToCountry { get; private set; }

        [AliasAs("clip-to-bounding-box")]
        public string ClipToBoundingBox { get; private set; }

        [AliasAs("clip-to-circle")]
        public string ClipToCircle { get; private set; }

        [AliasAs("clip-to-polygon")]
        public string ClipToPolygon { get; private set; }

        [AliasAs("input-type")]
        public string InputType { get; private set; }

        [AliasAs("prefer-land")]
        public string PreferLand { get; private set; }

        [AliasAs("language")]
        public string Language { get; private set; }

        /**
        * <summary>Set the number of AutoSuggest results to return. A maximum of 100 results can be specified, if a number greater than this is requested,
        * this will be truncated to the maximum. The default is 3</summary>
        *
        * <param name="n">the number of AutoSuggest results to return</param>
        * <returns>a <see cref="AutosuggestOptions"/> instance suitable for invoking a autosuggest API request.</returns>
        */
        public AutosuggestOptions SetNResults(int n)
        {
            NResults = n.ToString();
            return this;
        }

        /**
         * <summary>Specifies the number of results (must be &lt;= nResults) within the results set which will have a focus. Defaults to nResults.
         * This allows you to run autosuggest with a mix of focussed and unfocussed results, to give you a "blend" of the two. This is exactly what the old V2
         * standardblend did, and standardblend behaviour can easily be replicated by passing nFocusResults=1,
         * which will return just one focussed result and the rest unfocussed.</summary>
         *
         * <param name="n">number of results within the results set which will have a focus</param>
         * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
         */
        public AutosuggestOptions SetNFocusResults(int n)
        {
            NFocusResults = n.ToString();
            return this;
        }

        /**
        * <summary>For power users, used to specify voice input mode. Can be <see cref="AutosuggestInputType.TEXT"/> (default), <see cref="AutosuggestInputType.VOCON_HYBRID"/>
        * or <see cref="AutosuggestInputType.NMDP_ASR"/>. See voice recognition section within the developer docs for more details https://docs.what3words.com/api/v3/#voice.
        * </summary>
        * <param name="type">type the AutosuggestInputType</param>
        * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
        */
        public AutosuggestOptions SetInputType(AutosuggestInputType type)
        {
            switch (type)
            {
                case AutosuggestInputType.GENERIC_VOICE:
                    InputType = "generic-voice";
                    break;
                case AutosuggestInputType.TEXT:
                    InputType = "text";
                    break;
                case AutosuggestInputType.VOCON_HYBRID:
                    InputType = "vocon-hybrid";
                    break;
                case AutosuggestInputType.NMDP_ASR:
                    InputType = "nmdp-asr";
                    break;
                case AutosuggestInputType.SPEECHMATICS:
                    InputType = "speechmatics";
                    break;
                default:
                    InputType = null;
                    break;
            }
            return this;
        }

        /**
        * <summary>This is a location, specified as a latitude (often where the user making the query is). If specified, the results will be weighted to
        * give preference to those near the focus. For convenience, longitude is allowed to wrap around the 180 line, so 361 is equivalent to 1.</summary>
        *
        * <param name="coordinates">the focus to use</param>
        * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
        */
        public AutosuggestOptions SetFocus(Coordinates coordinates)
        {
            Focus = coordinates.Lat.ToString(CultureInfo.InvariantCulture) + "," + coordinates.Lng.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        /**
        * <summary>For normal text input, specifies a fallback language, which will help guide AutoSuggest if the input is particularly messy. If specified,
        * this parameter must be a supported 3 word address language as an ISO 639-1 2 letter code. For voice input (see voice section),
        * language must always be specified.</summary>
        *
        * <param name="language">the fallback language</param>
        * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
        */
        public AutosuggestOptions SetLanguage(string language)
        {
            Language = language;
            return this;
        }

        public AutosuggestOptions SetPreferLand(bool preferLand)
        {
            PreferLand = preferLand.ToString();
            return this;
        }

        /**
        * <summary>Restrict autosuggest results to a circle, specified by <see cref="Coordinates"/> <paramref name="centre"/> representing the centre of the circle, plus the
        * <paramref name="radius"/> in kilometres. For convenience, longitude is allowed to wrap around 180 degrees. For example 181 is equivalent to -179.</summary>
        *
        * <param name="centre">the centre of the circle</param>
        * <param name="radius">the radius of the circle in kilometres</param> 
        * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
        */
        public AutosuggestOptions SetClipToCircle(Coordinates centre, double radius)
        {
            ClipToCircle = centre.Lat.ToString(CultureInfo.InvariantCulture) + "," + centre.Lng.ToString(CultureInfo.InvariantCulture) + "," + radius.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        /**
        * <summary>Restrict autosuggest results to a polygon, specified by a collection of <see cref="Coordinates"/>. The polygon should be closed,
        * i.e. the first element should be repeated as the last element; also the list should contain at least 4 entries. The API is currently limited to
        * accepting up to 25 pairs.</summary>
        *
        * <param name="polygon">the polygon to clip results too</param>
        * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
        */
        public AutosuggestOptions SetClipToPolygon(List<Coordinates> polygon)
        {
            var coordinatesList = new List<string>();
            foreach (var coordinates in polygon)
            {
                coordinatesList.Add(coordinates.Lat.ToString(CultureInfo.InvariantCulture));
                coordinatesList.Add(coordinates.Lng.ToString(CultureInfo.InvariantCulture));
            }
            ClipToPolygon = string.Join(",", coordinatesList);
            return this;
        }

        /**
        * <summary>Restrict autosuggest results to a Bounding Box (2 coners, South West and North East.</summary>
        *
        * <param name="southWest">South West corner of the bounding box to clip results too</param>
        * <param name="northEast">North East corner of the bounding box to clip results too</param>
        * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
        */
        public AutosuggestOptions SetClipToBoundingBox(Coordinates southWest, Coordinates northEast)
        {
            ClipToBoundingBox = southWest.Lat.ToString(CultureInfo.InvariantCulture) + "," + southWest.Lng.ToString(CultureInfo.InvariantCulture) + "," +
                    northEast.Lat.ToString(CultureInfo.InvariantCulture) + "," + northEast.Lng.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        /**
        * <summary>
        * Restricts autosuggest to only return results inside the countries specified by comma-separated list of uppercase ISO 3166-1 alpha-2 country codes
        * (for example, to restrict to Belgium and the UK, use ClipToCountry("GB", "BE"). Will also accept lowercase
        * country codes. Entries must be two a-z letters. WARNING: If the two-letter code does not correspond to a country, there is no error: API simply returns no results.
        * </summary>
        *
        * <param name="countryCodes">countries to clip results too</param>
        * <returns>a <see cref="AutosuggestOptions"/> instance to continue adding options to your autosuggest request.</returns>
        */
        public AutosuggestOptions SetClipToCountry(List<string> countryCodes)
        {
            ClipToCountry = string.Join(",", countryCodes);
            return this;
        }

    }
}