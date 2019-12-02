﻿using System;
using System.Collections.Generic;
using System.Text;

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

        /// <summary>
        /// Constructor to initialize class variables
        /// </summary>
        public SearchLoop()
        {
            _render = new Render();
            _uInput = default;
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
        /// <param name="input"> String to be stored user input.</param>
        /// <returns> User input, if valid.</returns>
        private void GetUserInput()
        {
            
            do
            {
                _uInput = Console.ReadLine().ToLower();

                if (_uInput == null && _uInput != "t" && _uInput != "q")
                {
                    _render.ErrorMessage();
                }

            } while (_uInput == null && _uInput != "t" && _uInput != "q");
        }

        /// <summary>
        /// Method to search user selected title to search for.
        /// </summary>
        /// <param name="_uInput"> Class variable where title name was stored.</param>
        private void TitleSearch()
        {
            string titleToSearch = GetTitle();

            _uInput = titleToSearch;


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
    }
}