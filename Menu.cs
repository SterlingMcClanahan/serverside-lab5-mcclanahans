using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverside_lab5_mcclanahans {
    internal class Menu {
        /// <summary>
        /// Displays main menu with options and calls those methods
        /// </summary>
        public static async void MainMenu() {
            char borderSymbol = '~';
            string consoleBorder = "";
            while (Console.WindowWidth > consoleBorder.Length) consoleBorder += borderSymbol;
            Console.Clear();
            
            Console.WriteLine("Ron Swanson Quote Generator");
            Console.WriteLine(
                $"{consoleBorder}\n" +
                "Enter a number to select an option: \n" +
                "1. Get Single Quote\n" +
                "2. Get Multiple Quotes \n" +
                "3. Search for Quote \n" +
                "4. Exit \n" +
                $"{consoleBorder}\n");

            Dictionary<string, Action> options = new Dictionary<string, Action> {
                { "1", GetSingleQuote },
                { "2", GetMultipleQuotes },
                { "3", SearchQuotes},
                { "4", () => Environment.Exit(0) }
            };

            string input = Console.ReadLine();
            if (options.ContainsKey(input)) options[input]();
            else ReturnHome();
            MainMenu();
        }

        /// <summary>
        /// Gets a single ron swanson quote from the api and prints it out
        /// </summary>
        static async void GetSingleQuote() {
            Console.Clear();          
            string quote = await QueryBuilder.GetSingleQuote();            
            Console.WriteLine($"{quote} \n");
            ReturnHome();
        }

        /// <summary>
        /// gets user input on how many quotes to gather and prints them out
        /// there are either only 108 quotes or you can only request 108 at a time
        /// </summary>
        static async void GetMultipleQuotes() {
            Console.Clear();
            Console.WriteLine("how many quotes would you like? (max 108)");

            string num = Console.ReadLine();
            string[] quotes = await QueryBuilder.GetMultipleQuotes(num);

            for (int i = 1; i <= quotes.Count(); i++)
                Console.WriteLine($"{i}. {quotes[i - 1]}");             
         
            Console.WriteLine();
            ReturnHome();
        }

        /// <summary>
        /// searches the quotes by sending a search term, then prints them out
        /// if there are more than 108 quotes with the search term it breaks
        /// </summary>
        static async void SearchQuotes() {
            Console.Clear();
            Console.WriteLine("What would you like to search for?");

            string searchTerm = Console.ReadLine();
            string[] quotes = await QueryBuilder.SearchQuotes(searchTerm);

            Console.WriteLine($"Quotes containing '{searchTerm}':");
            foreach (var quote in quotes)
                Console.WriteLine(quote);
            
            Console.WriteLine();
        }

        /// <summary>
        /// Default method to return home
        /// </summary>
        static async void ReturnHome() {
            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
            MainMenu();
        }
    }
}
