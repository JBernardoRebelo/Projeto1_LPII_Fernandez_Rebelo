using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Class to run the database search loop
    /// </summary>
    public class SearchLoop
    {
        // Class variables
        /// <summary>
        /// Variable to store user input.
        /// </summary>
        private string _uInput;

        /// <summary>
        /// Variable to store title that is being search for
        /// </summary>
        private string _searchedTitle;

        /// <summary>
        /// Variable to be used as a index indentifier within a search
        /// </summary>
        private int _searchIndex;

        private bool _endSearch;

        /// <summary>
        /// Instance of the Render class where all output messages exist
        /// </summary>
        private readonly Render _render;

        /// <summary>
        /// Collection that contains basic title info
        /// </summary>
        private readonly ICollection<ITitle> _titlesBasic;

        /// <summary>
        /// Collection that contains title ratings
        /// </summary>
        private readonly ICollection<ITitle> _titlesRating;

        /// <summary>
        /// Collection that contains all information from
        /// other title collections
        /// </summary>
        private readonly ICollection<ITitle> _titles;

        /// <summary>
        /// Constructor to initiate the search loop
        /// </summary>
        /// <param name="fileBasic"> 
        /// File that constains basic title information
        /// </param>
        /// <param name="fileRating">
        /// File that contains title rating information
        /// </param>
        public SearchLoop(StreamReader fileBasic, StreamReader fileRating)
        {
            _render = new Render();
            _uInput = default;
            _endSearch = false;
            _searchIndex = default;
            _searchedTitle = default;

            // Load titles to collection
            _titlesBasic = TitleLoader.LoadTitlesBasic(fileBasic);
            _titlesRating = TitleLoader.LoadTitlesRating(fileRating);

            //// Check if there are duplicates
            //_titles = _titlesBasic.GroupBy(
            //    i => i.ID).Select(t => t.First()).ToList();

            //// Check if 
            //foreach (ITitle rating in _titlesRating)
            //{
            //    if (_titles.Where(x => x.ID == rating.ID).Count() == 0)
            //    {
            //        _titles.Add(rating);
            //    }
            //}
        }

        /// <summary>
        /// Main method where the search loop begins
        /// </summary>
        public void BeginSearch()
        {
            while (true)
            {
                _render.Greetings();

                // DEBUG METHOD **********************************************
                _render.ShowRAMUsage();
                // END DEBUG *************************************************

                UserSearchOption();
                TypeOfSearch();
            }
        }

        /// <summary>
        /// Method where the type of search to be done in the database
        /// is chosen
        /// </summary>
        private void TypeOfSearch()
        {
            switch (_uInput)
            {
                case "t":
                    _endSearch = false;
                    _uInput = null;
                    _render.SearchForTitles();
                    TitleSearch();
                    break;

                case "q":
                    _render.GoodByeMessage();
                    QuitProgram();
                    break;
            }
        }

        /// <summary>
        /// Method to get user search option input to be used in the main menu.
        /// If user input is invalid, calls Render's ErroMessage() and calls
        /// Render's Greetings() again.
        /// </summary>
        private void UserSearchOption()
        {
            do
            {
                _uInput = Console.ReadLine().ToLower();

                if (_uInput != "t" && _uInput != "q")
                {
                    _render.ErrorMessage();
                    _render.Greetings();
                }

            } while (_uInput != "t" && _uInput != "q");
        }

        /// <summary>
        /// Method where user inputs title name to be searched for.
        /// After input, it is shown the results. Calls ShowSearchTitle()
        /// </summary>
        private void TitleSearch()
        {
            string titleToSearch = GetTitle();

            _uInput = titleToSearch;

            if (_uInput == "b")
            {
                BeginSearch();
            }
            else
            {
                ShowTitlesGeneral(_uInput);
            }
        }

        /// <summary>
        /// Method to get the title to be searched for. If input is invalid,
        /// calls Render's ErrorMessage() and calls Render's SearchForTitles().
        /// </summary>
        /// <returns> User title choice. </returns>
        private string GetTitle()
        {
            string titleToSearch;

            do
            {
                titleToSearch = Console.ReadLine();

                if (titleToSearch == null || titleToSearch == "")
                {
                    _render.ErrorMessage();
                    _render.SearchForTitles();
                }

            } while (titleToSearch == null || titleToSearch == "");

            _searchedTitle = titleToSearch;

            return titleToSearch;
        }


        /// <summary>
        /// Method that prints title that contain user input string
        /// </summary>
        /// <param name="name">
        /// User input title string
        /// </param>
        private void ShowTitlesGeneral(string name)
        {
            // This must pause every 20 iterations
            _searchIndex = 0;

            // Query result colection
            IEnumerable<TitleBasic> results =
                from title in _titlesBasic.OfType<TitleBasic>()
                where title.PrimTitle.Contains(name)
                select title;

            while (_searchIndex < results.Count() && !_endSearch)
            {
                _render.GeneralSearchGUI();

                for (byte i = 0; i < 20 && i < results.Count(); i++)
                {
                    ++_searchIndex;
                    _render.ShowTitles(results.ElementAt(_searchIndex));
                }

                FilterOptions();
            }

            //foreach (TitleBasic tb in results)
            //{
            //    _render.ShowTitles(tb);

            //    //++j;

            //    if (j >= 20)
            //    {
            //        // Wait for user input
            //        FilterOptions();
            //        Console.Clear();
            //        j = 0;
            //        _render.GeneralSearchGUI();
            //    }
        }

        /// <summary>
        /// Method to show organizing options when showing titles
        /// </summary>
        private void FilterOptions()
        {
            _render.ShowFilterOptions();

            _uInput = GetUserFilterOptions();

            switch (_uInput)
            {
                case "detail":
                    _uInput = null;
                    _render.AskForTitleToDetail();
                    ShowTitleDetails(GetTitleForDetails());
                    break;

                case "date":
                    break;

                case "type":
                    break;

                case "genre":
                    _render.AskForGenreToFilterBy();

                    break;

                case "back":
                    _uInput = null;
                    _searchedTitle = null;
                    _endSearch = true;
                    break;

                default:
                    Console.Clear();
                    break;
            }
        }

        /// <summary>
        /// Method to get user filter option of choice, 
        /// or none and keep showing other titles
        /// </summary>
        /// <returns> String with selected user choice. </returns>
        private string GetUserFilterOptions()
        {
            string filterOption;

            do
            {
                filterOption = Console.ReadLine().ToLower();

            } while (filterOption != "" && filterOption != "detail" &&
                     filterOption != "date" && filterOption != "type" &&
                     filterOption != "genre" && filterOption != "back");

            return filterOption;
        }

        /// <summary>
        /// Method to get user title of choice to get 
        /// detailed information about.
        /// </summary>
        /// <returns> String with chosen title. </returns>
        private string GetTitleForDetails()
        {
            string chosenTitle;

            do
            {
                chosenTitle = Console.ReadLine().ToLower();

            } while (chosenTitle == null);

            return chosenTitle;
        }

        /// <summary>
        /// Method where user chosen title details are shown.
        /// </summary>
        /// <param name="title"> Specific title to be detailed. </param>
        private void ShowTitleDetails(string title)
        {
            // FAZER AMANHA (11/12), PRIMEIRO DESPACHAR MENUS E ORGANIZAÇÃO
        }

        /// <summary>
        /// Method to quit the program
        /// </summary>
        private void QuitProgram()
        {
            Environment.Exit(0);
        }
    }
}
