using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using what3words.dotnet.wrapper;
using what3words.dotnet.wrapper.models;
using Xamarin.Forms;

namespace sample
{
    public partial class MainPage : ContentPage
    {
        private What3WordsV3 api;

        public MainPage()
        {
            InitializeComponent();
            api = new What3WordsV3("YOUR_API_KEY_HERE");
            ButtonConvertTo3wa.Clicked += ButtonConvertTo3wa_Clicked;
            ButtonConvertToCoordinates.Clicked += ButtonConvertToCoordinates_Clicked;
            ButtonAutosuggest.Clicked += ButtonAutosuggest_Clicked;
        }

        private async void ButtonAutosuggest_Clicked(object sender, EventArgs e)
        {
            var autosuggestResult = await api.Autosuggest(EntryAutosuggest.Text).RequestAsync();
            if (autosuggestResult.IsSuccessful)
            {
                LabelAutosuggest.Text = "Suggestions: " + string.Join(", ", autosuggestResult.Data.Suggestions.Select(x=>x.Words));
            }
            else
            {
                LabelAutosuggest.Text = autosuggestResult.Error.Code + " - " + autosuggestResult.Error.Message;
            }
        }

        private async void ButtonConvertToCoordinates_Clicked(object sender, EventArgs e)
        {
            var convertToCoordinatesResult = await api.ConvertToCoordinates(EntryConvertToCoordinates.Text).RequestAsync();
            if (convertToCoordinatesResult.IsSuccessful)
            {
                LabelConvertToCoordinates.Text = "Coordinates: " + convertToCoordinatesResult.Data.Coordinates.Lat + ", " + convertToCoordinatesResult.Data.Coordinates.Lng;
            }
            else
            {
                LabelConvertToCoordinates.Text = convertToCoordinatesResult.Error.Code + " - " + convertToCoordinatesResult.Error.Message;
            }
        }

        private async void ButtonConvertTo3wa_Clicked(object sender, EventArgs e)
        {
            var latLong = EntryConvertTo3wa.Text.Replace(" ", "").Split(',').Where(x => x != "");
            if(double.TryParse(latLong.ElementAt(0),out var lat) && double.TryParse(latLong.ElementAt(1), out var lng))
            {
                var convertTo3waResult = await api.ConvertTo3WA().Coordinates(new Coordinates(lat, lng)).RequestAsync();
                if(convertTo3waResult.IsSuccessful)
                {
                    LabelConvertTo3wa.Text = "3 word address: " + convertTo3waResult.Data.Words;
                }
                else
                {
                    LabelConvertTo3wa.Text = convertTo3waResult.Error.Code + " - " + convertTo3waResult.Error.Message;
                }
            }
        }
    }
}
