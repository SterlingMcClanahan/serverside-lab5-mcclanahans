using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace serverside_lab5_mcclanahans {
    internal class QueryBuilder {
        // https://github.com/jamesseanwright/ron-swanson-quotes#ron-swanson-quotes-api

        private static readonly HttpClient client = new HttpClient();        
        
        /// <summary>
        /// sends the base url to retrieve a single randome quote
        /// </summary>
        /// <returns>returns a string of a single quote</returns>
        public static async Task<string> GetSingleQuote() {
            string apiUrl = "https://ron-swanson-quotes.herokuapp.com/v2/quotes"; // the url to request from            
            HttpResponseMessage response = await client.GetAsync(apiUrl); // Gets the repsonse from the url
            string responseBody = await response.Content.ReadAsStringAsync(); // the response is Json but written as a string        
            string[] quotes = JsonSerializer.Deserialize<string[]>(responseBody); //breaks the json down to what you need
            return quotes[0]; // returns the type need, specified as 0 since we only need a single string
        }

        /// <summary>
        /// sends the url plus the number of random quotes you want to retrieve
        /// </summary>
        /// <param name="numberOfQuotes">user input of number of quotes to request</param>
        /// <returns>a string array of quotes</returns>
        public static async Task<string[]> GetMultipleQuotes(string numberOfQuotes) {
            string apiUrl = $"https://ron-swanson-quotes.herokuapp.com/v2/quotes/{numberOfQuotes}";            
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string responseBody = await response.Content.ReadAsStringAsync();
            string[] quotes = JsonSerializer.Deserialize<string[]>(responseBody);
            return quotes;            
        }

        /// <summary>
        /// sends a search term to the specified url for searching 
        /// and they search for quotes containing that term.
        /// uses built in function of api
        /// </summary>
        /// <param name="searchTerm">the term to search</param>
        /// <returns></returns>
        public static async Task<string[]> SearchQuotes(string searchTerm) {
            string apiUrl = $"https://ron-swanson-quotes.herokuapp.com/v2/quotes/search/{Uri.EscapeDataString(searchTerm)}";
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string responseBody = await response.Content.ReadAsStringAsync();
            string[] quotes = JsonSerializer.Deserialize<string[]>(responseBody);
            return quotes;            
        }

    }
}
