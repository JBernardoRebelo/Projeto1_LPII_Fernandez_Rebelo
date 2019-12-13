using System;
using System.Diagnostics;


namespace IMDB_DATABASE
{
    /// <summary>
    /// Class to display menus and navigate through the program and database
    /// </summary>
    public class Render
    {
        /// <summary>
        /// Method to introduce the user to the program and explain functions  
        /// </summary>
        public void Greetings()
        {
            // Guarantee console is clear when using this message
            Console.Clear();

            // REVIEW USER MESSAGES
            Console.WriteLine("Welcome to a console-based IMDB database. \n" +
                        "Please select the type of search you want to do: \n");
            Console.WriteLine("To search for a title press t; \n" +
                              "To quit the program press q.");
        }

        /// <summary>
        /// Method to show title search menu
        /// </summary>
        public void SearchForTitles()
        {
            Console.Clear();
            Console.WriteLine("You are now searching for titles. \n" +
            "Please input the title you want to search for.\n" +
            "If you want to return to the previous menu press b and Enter.");
        }

        /// <summary>
        /// Method to show general search categories.
        /// </summary>
        public void GeneralSearchGUI()
        {
            Console.Clear();

            // Formated string that shows search categories
            Console.WriteLine($"{"Type",-20} {"Title",-60}" +
                $"{"Release date",-20} {"Rating", -20}\n");
        }

        /// <summary>
        /// Method to print title information for general search.
        /// </summary>
        /// <param name="tb"> TitleBasic instance. </param>
        public void ShowGeneralTitlesSearch(TitleBasic tb)
        {
            // Format output information
            Console.WriteLine($"{tb.Type,-15}" +
                $"{tb.PrimTitle,-70}" +
                $"{tb.StartYear,-20}" +
                $"{tb.AvgRating,-10}");
        }

        /// <summary>
        /// Method to show user possible filter options
        /// </summary>
        public void ShowGeneralFilterOptions()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(
                "Press Enter to see the next 20 results\n" +
                "Write detail see detailed info for a title,\n" +
                "Date to see titles organized by date\n" +
                "Genre to see title with a specific genre.\n" +
                "Type to see title with a specific genre.\n" +
                "Rating to order by high to low or the inverse\n" +
                "Back to return to previous menu.");
        }

        /// <summary>
        /// Method to show user options within a filtered search
        /// </summary>
        public void ShowSpecificFilterOptions()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(
                "Press any key to see the next 20 results\n" +
                "Write detail see detailed info for a title,\n" +
                "Or write back to return to general search.");
        }

        /// <summary>
        /// 
        /// </summary>
        public void AskForDateToFilterBy()
        {
            Console.Write("Please write the date you want to filter the" +
                "search by: ");
        }

        /// <summary>
        /// 
        /// </summary>
        public void AskForGenreToFilterBy()
        {
            Console.Write("Please write the genre you want to filter the" +
                "search by: ");
        }

        /// <summary>
        /// 
        /// </summary>
        public void AskForTypeToFilterBy()
        {
            Console.Write("Please write the type you want to filter the" +
                "search by: ");
        }

        /// <summary>
        /// 
        /// </summary>
        public void AskForTitleToDetail()
        {
            Console.Write(
                "Please input the complete title name you want detailed " +
                "information about:");
        }

        /// <summary>
        /// 
        /// </summary>
        public void OrderByAscendingOrDescending()
        {
            Console.Write("Do you want to order by ascending?" +
                "Write Y or N ?");
        }

        /// <summary>
        /// Method to show on screen selected title detailed info.
        /// </summary>
        /// <param name="tb"> Title to show details about. </param>
        public void ShowDetailedInfo(TitleBasic tb)
        {
            byte i = 0;

            // Format output information
            Console.Write(
                $"{"Type",-10} {tb.Type,-5}\n" +
                $"{"Title",-10} {tb.PrimTitle,-10}\n" +
                $"{"For adults?",-10} {tb.IsAdult,-10}\n" +
                $"{"Release date",-10} {tb.StartYear,-10}\n" +
                $"{"End year",-10} {tb.EndYear,-10}\n" +
                $"{"Genres",-10}");

            foreach (string s in tb.Genres)
            {
                i++;

                Console.Write($"{s,-13}");

                if (i >= 3)
                {
                    break;
                }
            }
            Console.WriteLine($"---------------------------------------\n");

            Console.ReadKey(false);
        }

        /// <summary>
        /// Method to inform the user his/her input is invalid
        /// </summary>
        public void ErrorMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid input. Press any key to try again...");
            Console.ReadKey(false);
            Console.Clear();
        }

        /// <summary>
        /// Method to warn user if no title could be found
        /// </summary>
        public void NoTitleFoundMessage()
        {
            Console.WriteLine();
            Console.WriteLine("No titles were found... Press any key to try" +
                " again...");
            Console.ReadKey(false);
            Console.Clear();
        }

        /// <summary>
        /// Method to warn user if filter couldn't be used
        /// </summary>
        public void FilterErrorMessage()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Couldn't filter titles with inputed information...");
            Console.ReadKey(false);
            Console.Clear();
        }

        /// <summary>
        /// Warns user that there are no more results to be displayed
        /// </summary>
        public void EndOfSearchResultsWarning()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("There are no more results for the exact title" +
                " you entered... Press any key to return to the main menu...");
            Console.ReadKey(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void EndOfDateSearchResultsWarning()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("There are no more results for the exact date" +
                " you entered... Press any key to return to the general" +
                " search results...");
            Console.ReadKey(false);
        }

        /// <summary>
        /// Method to inform the user has exited program
        /// </summary>
        public void GoodByeMessage()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using the program!");
        }

        // DEBUG METHOD *******************************************************
        public void ShowRAMUsage()
        {
            Console.WriteLine("My app is occupying "
            + (Process.GetCurrentProcess().VirtualMemorySize64 / 1024 / 1024)
            + " megabytes of memory");
        }
        // END DEBUG METHOD ***************************************************
    }
}
