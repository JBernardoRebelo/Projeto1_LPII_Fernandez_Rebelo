using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Class to instantiate titles and store them in Collections
    /// </summary>
    public class TitleLoader
    {
        /// <summary>
        /// Instantiates basic titles in a file
        /// </summary>
        /// <param name="filename"> Accepts a file name </param>
        /// <returns> Returns an IEnumerable of ITitle </returns>
        public IEnumerable<ITitle> LoadTitlesBasic(string filename)
        {
            // Title basic params
            string id = default;
            string type = default;
            string primTitle = default;
            string origiTitle = default;
            bool isAdult = default;
            int startYear = default;
            int endYear = default;
            int runTime = default;
            HashSet<string> genres = default;

            // Line
            string line;
            string[] splitLine;
            string firstCharsOfLine;

            // List of titlesBasic
            ICollection<TitleBasic> titlesBasic = new List<TitleBasic>();

            // Instantiate Stream reader
            StreamReader file = new StreamReader(filename);

            // Sort file
            while ((line = file.ReadLine()) != null)
            {
                firstCharsOfLine = line[0].ToString() + line[1].ToString();
                Console.WriteLine(firstCharsOfLine);

                // Split lines in tabs
                splitLine = line.Split('\t').ToArray();

                if (firstCharsOfLine == "tt")
                {
                    // Split line to corresponding title properties
                    // Etc
                }
            }

            // Close the file
            file.Close();

            // Returns the list
            return titlesBasic;
        }
    }
}
