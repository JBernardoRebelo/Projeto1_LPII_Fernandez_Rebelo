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
        public static ICollection<ITitle> LoadTitlesBasic(StreamReader file)
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
            HashSet<string> genres = new HashSet<string>();

            //int reps = 20;

            // Line
            string line;
            string[] splitLine;

            // List of titlesBasic
            ICollection<ITitle> titlesBasic = new List<ITitle>();

            // Sort file
            while ((line = file.ReadLine()) != null)
            {
                // Split lines in tabs
                splitLine = line.Split('\t').ToArray();

                if (splitLine[0] != "tconst")
                {
                    // Split line to corresponding title properties

                    // Title Id
                    id = splitLine[0];

                    // Title Type
                    type = splitLine[1];

                    // Primary title
                    primTitle = splitLine[2];

                    // Original title
                    origiTitle = splitLine[3];

                    // Adult - 1 == true and 0 == false
                    isAdult = splitLine[4] == "1";

                    // Start year
                    if (splitLine[5] != @"\N")
                    {
                        startYear = Convert.ToInt32(splitLine[5]);
                    }

                    // End year
                    if (splitLine[6] != @"\N")
                    {
                        endYear = Convert.ToInt32(splitLine[6]);
                    }

                    // Run time
                    if (splitLine[7] != @"\N")
                    {
                        runTime = Convert.ToInt32(splitLine[7]);
                    }

                    // Genres
                    // Add strings split with ", " to the collelction of genres
                    if (splitLine[8] != null)
                    {
                        string[] strg = splitLine[8].Split(',');
                        for (int i = 0; i < strg.Length; ++i)
                        {
                            genres.Add(strg[i]);
                        }
                    }

                    // Instatiate title
                    TitleBasic title = new TitleBasic(id, type,
                        primTitle, origiTitle,
                        isAdult, startYear,
                        endYear, runTime, genres);

                    // Add the title to the collection
                    titlesBasic.Add(title);

                    // Debug
                    //OutputTestFile(title);

                    //--reps;

                    //if (reps < 0)
                    //{
                    //    break;
                    //}
                    //// ********
                }
            }

            // Close the file
            file.Close();

            // Returns the collection
            return titlesBasic;
        }


        public static ICollection<ITitle> LoadTitlesRating(StreamReader file)
        {
            // Instance variables to assign
            string id = default;
            float avgRating = default;
            int numVotes = default;

            // Debug
            //int reps = 20;

            // Line
            string line;
            string[] splitLine;

            // List of titlesBasic
            ICollection<ITitle> titlesRating = new List<ITitle>();

            // Sort file
            while ((line = file.ReadLine()) != null)
            {
                splitLine = line.Split('\t').ToArray();

                // Title Id
                if (splitLine[0] != "tconst")
                {
                    id = splitLine[0];

                    // Title Type

                    _ = float.TryParse(splitLine[1], out avgRating);

                    // Primary title
                    numVotes = Convert.ToInt32(splitLine[2]);

                    TitleRating titleR = new TitleRating
                        (id, avgRating, numVotes);

                    // Add instance to the collection
                    titlesRating.Add(titleR);

                    //OutputTestFile(titleR);
                }
            }

            // Returns the collection
            return titlesRating;
        }

        // Debug method for output
        private static void OutputTestFile(TitleRating t)
        {
            Console.Write($"{t.ID} - ");
            Console.Write($"{t.AvgRating} - ");
            Console.Write($"{t.NumVotes} - \n");
        }

        // Debug method for out put
        private static void OutputTestFile(TitleBasic t)
        {
            Console.Write($"{t.ID} - ");
            Console.Write($"{t.Type} - ");
            Console.Write($"{t.PrimTitle} - ");
            Console.Write($"{t.OrigiTitle} - ");
            Console.Write($"{t.IsAdult} - ");
            Console.Write($"{t.StartYear} - ");
            Console.Write($"{t.EndYear} - ");
            Console.Write($"{t.RunTimeMin} - ");
            foreach (string s in t.Genres)
            {
                Console.Write($"{s}, ");
            }
            Console.WriteLine();
        }
    }
}
