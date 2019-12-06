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
        /// Accepts a title basic and prints all info about it
        /// </summary>
        public Action<TitleBasic> PrintSearchInfo = tb
            => Console.WriteLine($"Type: {tb.Type}, " +
                $"Primary Title: {tb.PrimTitle}, " +
                $"For Adults: {tb.IsAdult}, Start Year: {tb.StartYear}," +
                $"End Year: {tb.EndYear} Genres: {tb.Genres}");

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
                "Please input the title you want to search for:");
        }

        private void ShowTitles(TitleBasic tb, TitleRating tr)
        {
            Console.WriteLine($"Type: {tb.Type}, " +
                $"Primary Title: {tb.PrimTitle}, " +
                $"For Adults: {tb.IsAdult}, Start Year: {tb.StartYear}," +
                $"End Year: {tb.EndYear} Genres: {tb.Genres}");
        }

        /// <summary>
        /// Method to inform the user his/her input is invalid
        /// </summary>
        public void ErrorMessage()
        {
            Console.WriteLine("Invalid input. Please try again.");
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


        // DEBUG METHOD ******************************************************
        public void ShowRAMUsage()
        {
            Console.WriteLine("My app is occupying "
            + (Process.GetCurrentProcess().VirtualMemorySize64 / 1024 / 1024)
            + " megabytes of memory");
        }
    }
}
