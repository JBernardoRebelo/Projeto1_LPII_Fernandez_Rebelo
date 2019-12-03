using System;
using System.Collections.Generic;
using System.IO;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Class to run the database search loop
    /// </summary>
    public class SearchLoop
    {
        // Class variables

        /// <summary>
        /// Variable to store user input
        /// </summary>
        private string _uInput;

        /// <summary>
        /// Instance of the Render class where all output messages exist
        /// </summary>
        private Render _render;

        private ICollection<ITitle> _titlesBasic;
        private ICollection<ITitle> _titlesRating;

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

            // Load titles to collection
            _titlesBasic = TitleLoader.LoadTitlesBasic(fileBasic);
            _titlesRating = TitleLoader.LoadTitlesRating(fileRating);
        }

        // Menu tipo de pesquisa

        // Menu de pesquisa por titulos

        // Receber keyword

        // Apresentar resultados (20 por "pagina")

        // Menu de pesquisa por pessoas

        // Receber keyword

        // Apresentar resultados

        // Menu de saida

        // Menu de acesso a titulos

        // Receber opção

        // Opção de aceder a info detalhada do titulo

        // Apresentar detalhes do titulo

        // Voltar ao titulo "pai" (no caso de uma serie)

        // Voltar à pesquisa de titulos

        // Voltar ao menu anterior

        // Opção de saida do programa

        /// <summary>
        /// Main method where the search loop occours
        /// </summary>
        public void ActualLoop()
        {
            while (true)
            {
                _render.Greetings();
                GetUserInput();
                TypeOfSearch();
            }
        }

        /// <summary>
        /// Method where the type of search in the database is selected
        /// </summary>
        private void TypeOfSearch()
        {
            switch (_uInput)
            {
                case "a":
                    break;

                case "t":
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
        /// Method to get user input to be used in menus.
        /// </summary>
        private void GetUserInput()
        {
            do
            {
                _uInput = Console.ReadLine().ToLower();

                if (_uInput == null || _uInput != "t" && _uInput != "q")
                {
                    _render.ErrorMessage();
                }

            } while (_uInput == null && _uInput != "t" && _uInput != "q");
        }

        /// <summary>
        /// Method to search user selected title to search for.
        /// </summary>
        private void TitleSearch()
        {
            string titleToSearch = GetTitle();
            _uInput = titleToSearch;

            OutputWantedTitles(_uInput);
        }

        /// <summary>
        /// Method to get the title to be searched for.
        /// </summary>
        /// <returns> User title choice. </returns>
        private string GetTitle()
        {
            string titleToSearch;

            do
            {
                titleToSearch = Console.ReadLine();

            } while (titleToSearch == null);

            return titleToSearch;
        }

        /// <summary>
        /// Method to quit the program
        /// </summary>
        private void QuitProgram()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Method that prints title that contain user input string
        /// </summary>
        /// <param name="name">
        /// User input title string
        /// </param>
        private void OutputWantedTitles(string name)
        {
            // This must pause every 20 iterations
            int i = 0;

            foreach (TitleBasic tb in _titlesBasic)
            {
                if (tb.PrimTitle.Contains(name) ||
                    tb.OrigiTitle.Contains(name))
                {
                    _render.PrintTitleInfo(tb);
                    ++i;
                }

                if (i >= 20)
                {
                    // Wait for user input
                    Console.ReadLine();
                    i = 0;
                    Console.Clear();
                }
            }
        }
    }
}
