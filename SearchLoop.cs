using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Class to run the database search loop
    /// </summary>
    public class SearchLoop
    {
        #region Class variables
        /// <summary>
        /// Variable to be used as a index indentifier within a search
        /// </summary>
        private int _searchIndex;

        /// <summary>
        /// Bool to check if user wants end current search.
        /// </summary>
        private bool _endSearch;

        /// <summary>
        /// Variable to store user input.
        /// </summary>
        private string _uInput;

        /// <summary>
        /// Variable to store title that is being search for
        /// </summary>
        private string _searchedTitle;

        /// <summary>
        /// Instance of the Render class where all output messages exist
        /// </summary>
        private readonly Render _render;

        /// <summary>
        /// Instance of the TitleLoader class to instantiate titles from files
        /// to be stored in a collection.
        /// </summary>
        private readonly TitleLoader _titleLoader;

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
        //private readonly ICollection<ITitle> _titles; 
        #endregion

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
            _titleLoader = new TitleLoader();
            _uInput = default;
            _endSearch = false;
            _searchIndex = default;
            _searchedTitle = default;

            // Load titles to collection
            _titlesBasic = _titleLoader.LoadTitlesBasic(fileBasic);
            _titlesRating = _titleLoader.LoadTitlesRating(fileRating);

            // Instatiate List<T>
            //_titles = new List<ITitle>(_titlesBasic.Count());

            // INSERT JOIN METHOD TO HAVE A COLLECTION WITH COMPLETE TITLES
            // INFORMATION ?!?!? OR CREATE IT IN TITLELOADER CLASS ?!?!?

        }

        #region Initial Search Methods
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
        /// is chosen.
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
        #endregion

        #region General search methods
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
        /// Method to get the title to be searched for and stores
        /// a copy of the inouted title in a separate variable.
        /// If input is invalid,
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
            int shownResults = 0;
            int resultCount;

            // Query result colection
            IEnumerable<TitleBasic> results =
                from title in _titlesBasic.OfType<TitleBasic>()
                where title.PrimTitle.Contains(name)
                select title;

            resultCount = results.Count();

            _render.GeneralSearchGUI();

            foreach (TitleBasic tb in results)
            {
                _render.ShowGeneralTitlesSearch(tb);

                ++shownResults;

                ++_searchIndex;

                if (_searchIndex >= 20)
                {
                    GeneralFilterOptions();
                    Console.Clear();
                    _searchIndex = 0;
                    _render.GeneralSearchGUI();
                }

                if (_endSearch)
                {
                    _endSearch = false;
                    break;
                }

                if (shownResults == resultCount)
                {
                    _render.EndOfSearchResultsWarning();
                }
            }
        }

        /// <summary>
        /// Method to show organizing options when showing titles
        /// </summary>
        private void GeneralFilterOptions()
        {
            _render.ShowGeneralFilterOptions();

            _uInput = GetUserGeneralFilterOptions();

            switch (_uInput)
            {
                case "detail":
                    _uInput = null;
                    _render.AskForTitleToDetail();
                    ShowTitleDetails(GetTitleForDetails());
                    break;

                case "date":
                    _uInput = null;
                    _render.AskForDateToFilterBy();
                    FilterByDate();
                    break;

                case "type":
                    _uInput = null;
                    _render.AskForTypeToFilterBy();

                    break;

                case "genre":
                    _uInput = null;
                    _render.AskForGenreToFilterBy();
                    //FilterByGenre(/*ADD USER INPUT METHOD*/);
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
        private string GetUserGeneralFilterOptions()
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
        #endregion

        #region Specific search methods
        /// <summary>
        /// Method to show user options when within a filtered search
        /// </summary>
        private void SpecificFilterOptions()
        {
            // Pseudo copy of GeneralFilterOptions

            _render.ShowSpecificFilterOptions();

            _uInput = GetUserSpecificFilterOptions();

            switch (_uInput)
            {
                default:
                    break;
            }
        }

        private string GetUserSpecificFilterOptions()
        {
            string filterOption;

            do
            {
                filterOption = Console.ReadLine().ToLower();

            } while (filterOption != "" && filterOption != "back");

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

            } while (chosenTitle == "");

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
        /// Method to filter search by specific release date.
        /// </summary>
        private void FilterByDate()
        {
            if (ushort.TryParse(Console.ReadLine(), out ushort date))
            {
                int i = 0;
                int shownResults = 0;
                int resultCount;

                IEnumerable<TitleBasic> dateResults =
                    from title in _titlesBasic.OfType<TitleBasic>()
                    where title.PrimTitle.Contains(_searchedTitle) &&
                    title.StartYear.Equals(date)
                    select title;

                resultCount = dateResults.Count();

                if (resultCount == 0)
                {
                    _render.FilterErrorMessage();
                }

                else
                {
                    _render.GeneralSearchGUI();

                    foreach (TitleBasic t in dateResults)
                    {
                        ++i;
                        ++shownResults;

                        _render.ShowGeneralTitlesSearch(t);

                        if (i >= 20)
                        {
                            //SpecificFilterOptions();
                            Console.ReadKey(false);
                            Console.Clear();
                            i = 0;
                            _render.GeneralSearchGUI();
                        }

                        if (_endSearch)
                        {
                            _endSearch = false;
                            break;
                        }

                        if (shownResults == resultCount)
                        {
                            _render.EndOfDateSearchResultsWarning();
                        }
                    }
                }
            }
        }

        private void FilterByGenre(string genre)
        {

            //if
            //{
            IEnumerable<TitleBasic> results =
                from title in _titlesBasic.OfType<TitleBasic>()
                where title.PrimTitle.Contains(_searchedTitle)
                where title.Genres.Contains(genre)
                select title;
            //}
            //else
            //{
            //    _render.FilterErrorMessage();
            //}
        }
        #endregion

        /// <summary>
        /// Method to quit the program
        /// </summary>
        private void QuitProgram()
        {
            Environment.Exit(0);
        }
    }
}
