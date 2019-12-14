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
        private bool _endSearch, _filterSearch;

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
        private readonly ICollection<ITitle> _titles;
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
            _endSearch = _filterSearch = false;
            _searchIndex = default;
            _searchedTitle = default;

            // Load titles to collection
            _titles = _titleLoader.LoadTitles(fileBasic, fileRating);
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
        /// <param name="titleName">
        /// User input title string
        /// </param>
        private void ShowTitlesGeneral(string titleName)
        {
            // This must pause every 20 iterations
            _searchIndex = 0;
            int shownResults = 0;
            int resultCount;

            // Query result colection
            IEnumerable<TitleBasic> results =
                from title in _titles.OfType<TitleBasic>()
                where title.PrimTitle.Contains(titleName)
                select title;

            resultCount = results.Count();

            if (resultCount == 0)
            {
                _render.NoTitleFoundMessage();
            }

            else
            {
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
                // Get detailled information for a title
                case "detail":
                    _uInput = null;
                    _render.AskForTitleToDetail();
                    ShowTitleDetails();
                    break;

                // Filter search by a certain date
                case "date":
                    _uInput = null;
                    _render.AskForDateToFilterBy();
                    FilterByDate();
                    break;

                // Filter search by a certain type
                case "type":
                    _uInput = null;
                    _render.AskForTypeToFilterBy();
                    FilterByType(GetTypeToFilter());
                    break;

                // Filter search by a certain genre
                case "genre":
                    _uInput = null;
                    _render.AskForGenreToFilterBy();
                    FilterByGenre(GetGenreToFilter());
                    break;

                case "rating":
                    _uInput = null;
                    _render.OrderByAscendingOrDescending();
                    OrderByRating(AscendingOrder());
                    break;

                // Stop current title search
                case "back":
                    _uInput = null;
                    _searchedTitle = null;
                    _endSearch = true;
                    break;

                // If user pressed enter
                default:
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
                     filterOption != "genre" && filterOption != "rating" &&
                     filterOption != "back");

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

            string filterOption = GetUserSpecificFilterOptions();

            switch (filterOption)
            {
                // In case user wants details about a title
                case "detail":
                    _render.AskForTitleToDetail();
                    ShowTitleDetails();
                    break;

                // Return to general title search results
                case "back":
                    _filterSearch = true;
                    break;

                // Continue using filter
                default:
                    break;
            }
        }

        /// <summary>
        /// Method to options while in a filtered search
        /// </summary>
        /// <returns> Chosen option. </returns>
        private string GetUserSpecificFilterOptions()
        {
            string filterOption;

            do
            {
                filterOption = Console.ReadLine().ToLower();

                if (filterOption != "" && filterOption != "back")
                {
                    _render.ErrorMessage();
                }

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
                chosenTitle = Console.ReadLine();

            } while (chosenTitle == "");

            return chosenTitle;
        }

        /// <summary>
        /// Method where user chosen title details are shown.
        /// </summary>
        /// <param name="title"> Specific title to be detailed. </param>
        private void ShowTitleDetails()
        {
            string uTitle = GetTitleForDetails();
            bool found = false;

            // Check wich titles have the name the user is looking for
            foreach (TitleBasic t in _titles)
            {
                if (t.PrimTitle == uTitle)
                {
                    // Show detailed info, sets found to true
                    _render.ShowDetailedInfo(t);
                    found = true;
                    break;
                }
            }
            if(found == false)
            {
                // When the input doens't match a title output error
                _render.NoTitleFoundMessage();
            }
        }

        /// <summary>
        /// Method to filter search by specific release date.
        /// </summary>
        private void FilterByDate()
        {
            string uDate = Console.ReadLine();

            if (ushort.TryParse(uDate, out ushort date))
            {
                int i = 0;
                int shownResults = 0;
                int resultCount;

                IEnumerable<TitleBasic> dateResults =
                    from title in _titles.OfType<TitleBasic>()
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
                            SpecificFilterOptions();
                            Console.Clear();
                            i = 0;
                            _render.GeneralSearchGUI();
                        }

                        if (_filterSearch)
                        {
                            _filterSearch = false;
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

        /// <summary>
        /// Method to filter search by a certain genre.
        /// </summary>
        /// <param name="genre"> Genre itself.</param>
        private void FilterByGenre(string genre)
        {
            int i = 0;
            int shownResults = 0;
            int resultCount;

            IEnumerable<TitleBasic> genreResults =
                from title in _titles.OfType<TitleBasic>()
                where title.PrimTitle.Contains(_searchedTitle) &&
                title.Genres.Contains(genre)
                select title;

            resultCount = genreResults.Count();

            if (resultCount == 0)
            {
                _render.FilterErrorMessage();
            }
            else
            {
                _render.GeneralSearchGUI();

                foreach (TitleBasic t in genreResults)
                {
                    ++i;
                    ++shownResults;

                    _render.ShowGeneralTitlesSearch(t);

                    if (i >= 20)
                    {
                        SpecificFilterOptions();
                        Console.Clear();
                        i = 0;
                        _render.GeneralSearchGUI();
                    }

                    if (_filterSearch)
                    {
                        _filterSearch = false;
                        break;
                    }

                    if (shownResults == resultCount)
                    {
                        _render.EndOfDateSearchResultsWarning();
                    }
                }
            }
        }

        /// <summary>
        /// Method to get user chosen genre to filter by
        /// </summary>
        /// <returns> String of selected genre. </returns>
        private string GetGenreToFilter()
        {
            string genreToFilter;

            do
            {
                genreToFilter = Console.ReadLine();

            } while (genreToFilter == "");

            return genreToFilter;
        }

        /// <summary>
        /// Method to filter search result by type
        /// </summary>
        /// <param name="type"> Type to use as filter. </param>
        private void FilterByType(string type)
        {
            int i = 0;
            int shownResults = 0;

            IEnumerable<TitleBasic> typeResults =
                from title in _titles.OfType<TitleBasic>()
                where title.PrimTitle.Contains(_searchedTitle)
                && title.Type.Contains(type)
                select title;

            if(typeResults.Count() > 0)
            {
                Console.WriteLine("HELLO");
                _render.GeneralSearchGUI();

                foreach (TitleBasic t in typeResults)
                {
                    ++i;
                    ++shownResults;

                    _render.ShowGeneralTitlesSearch(t);

                    if (i >= 20)
                    {
                        SpecificFilterOptions();
                        Console.ReadKey(false);
                        Console.Clear();
                        i = 0;
                        _render.GeneralSearchGUI();
                    }

                    if (_filterSearch)
                    {
                        _filterSearch = false;
                        break;
                    }

                    if (shownResults == typeResults.Count())
                    {
                        _render.EndOfDateSearchResultsWarning();
                    }
                }
            }
            else
            {
                _render.FilterErrorMessage();
            }
        }

        /// <summary>
        /// Method to get user chosen type to use as filter.
        /// </summary>
        /// <returns> Chosen user type. </returns>
        private string GetTypeToFilter()
        {
            string type;

            do
            {
                type = Console.ReadLine();

            } while (type == "");

            return type;
        }

        private void OrderByRating(bool ascending)
        {
            int i = 0;
            int shownResults = 0;
            int resultCount;

            if (ascending)
            {
                IEnumerable<TitleBasic> typeResults =
                    (from title in _titles.OfType<TitleBasic>()
                     where title.PrimTitle.Contains(_searchedTitle)
                     select title).OrderBy(title => title.AvgRating);

                resultCount = typeResults.Count();

                if (resultCount == 0)
                {
                    _render.FilterErrorMessage();
                }

                else
                {
                    _render.GeneralSearchGUI();

                    foreach (TitleBasic t in typeResults)
                    {
                        ++i;
                        ++shownResults;

                        _render.ShowGeneralTitlesSearch(t);

                        if (i >= 20)
                        {
                            SpecificFilterOptions();
                            Console.ReadKey(false);
                            Console.Clear();
                            i = 0;
                            _render.GeneralSearchGUI();
                        }

                        if (_filterSearch)
                        {
                            _filterSearch = false;
                            break;
                        }

                        if (shownResults == resultCount)
                        {
                            _render.EndOfDateSearchResultsWarning();
                        }
                    }

                }
            }
            else
            {
                IEnumerable<TitleBasic> typeResults =
                   (from title in _titles.OfType<TitleBasic>()
                    where title.PrimTitle.Contains(_searchedTitle)
                    select title).OrderByDescending(title => title.AvgRating);

                resultCount = typeResults.Count();

                if (resultCount == 0)
                {
                    _render.FilterErrorMessage();
                }

                else
                {
                    _render.GeneralSearchGUI();

                    foreach (TitleBasic t in typeResults)
                    {
                        ++i;
                        ++shownResults;

                        _render.ShowGeneralTitlesSearch(t);

                        if (i >= 20)
                        {
                            SpecificFilterOptions();
                            Console.ReadKey(false);
                            Console.Clear();
                            i = 0;
                            _render.GeneralSearchGUI();
                        }

                        if (_filterSearch)
                        {
                            _filterSearch = false;
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

        private bool AscendingOrder()
        {
            string order;

            do
            {
                order = Console.ReadLine().ToLower();

            } while (order == "");

            if (order == "y")
            {
                return true;
            }

            else
            {
                return false;
            }
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
