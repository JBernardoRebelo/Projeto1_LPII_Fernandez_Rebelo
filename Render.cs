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
            // REVIEW USER MESSAGES
            Console.WriteLine("Welcome to a console-based IMDB database. \n" +
                        "Please select the type of search you want to do: \n");
            Console.WriteLine("To search for a title press t; \n" +
                              "To search for a actor/actress press a; \n" +
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
            // Formated string that shows search categories
            Console.WriteLine($"{"Type",-20} {"Title",-60}" +
                $"{"Release date",-20}\n");
        }

        /// <summary>
        /// Method to print title information for general search.
        /// </summary>
        /// <param name="tb"> TitleBasic instance. </param>
        public void ShowTitles(TitleBasic tb)
        {
            // Format output information
            Console.Write($"\n{tb.Type,-15}" +
                $"{tb.PrimTitle,-70} " +
                $"{tb.StartYear,-20}");
        }

        /// <summary>
        /// Method to show user possible filter options
        /// </summary>
        public void ShowFilterOptions()
        {
            Console.WriteLine("\nPress any key to see the next 20 results\n" +
                "Write detail see detailed info for a title,\n" +
                "Write date to see titles organized by date\n" +
                "Write genre to see title with a specific genre.\n" +
                "Write back to return to previous menu.");
        }

        public void AskForDateToFilterBy()
        {

        }

        public void AskForGenreToFilterBy()
        {

        }

        public void AskForTitleToDetail()
        {
            Console.Write(
                "Please input the title you want detailed information about:");
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
        }

        /// <summary>
        /// Method to inform the user his/her input is invalid
        /// </summary>
        public void ErrorMessage()
        {
            Console.WriteLine("\n" +
                "Invalid input. Press any key to try again...");
            Console.ReadKey();
            Console.Clear();
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
