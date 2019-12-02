using System;
using System.Collections.Generic;
using System.Text;

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
        public Action<TitleBasic> PrintTitleInfo = tb
            => Console.WriteLine($"ID -> {tb.ID} " +
                $"Type: {tb.Type} PrimTitle: {tb.PrimTitle} " +
                $"OriginalTytle: {tb.PrimTitle}");

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

        /// <summary>
        /// Method to inform the user his/her input is invalid
        /// </summary>
        public void ErrorMessage()
        {
            Console.WriteLine("Invalid input. Please try again.");
        }

        /// <summary>
        /// Method to inform the user has exited program
        /// </summary>
        public void GoodByeMessage()
        {
            Console.WriteLine("Thank you for using the pro");
        }

    }
}
