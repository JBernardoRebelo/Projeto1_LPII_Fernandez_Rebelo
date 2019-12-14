using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Class to instantiate titles and store them in Collections
    /// </summary>
    public class TitleLoader
    {
        /// <summary>
        /// Method to load read files into a single collection to be used as
        /// a database.
        /// </summary>
        /// <param name="fileBasic"> Basic title information file. </param>
        /// <param name="fileRatings"> Title rating information file. </param>
        /// <returns> A collection o title and their informations. </returns>
        public ICollection<ITitle> LoadTitles(StreamReader fileBasic,
            StreamReader fileRatings)
        {
            // Method variables
            NumberStyles numberStyles = NumberStyles.Any;
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            int ratingIndex = 0;

            // List of titles
            List<ITitle> titles = new List<ITitle>();

            // Title basic params
            string id;
            string type;
            string primTitle;
            string origiTitle;
            bool isAdult;
            ushort startYear = default;
            ushort endYear = default;
            ushort runTime = default;
            List<string> genres;

            // Line
            string lineBasic;
            string[] splitLine;
            // Line
            string lineRating;
            string[] splitLine2;

            // Each string is a line
            List<string> fileR = new List<string>(996500);
            while ((lineRating = fileRatings.ReadLine()) != null)
            {
                if (!lineRating.Contains("tconst"))
                {
                    fileR.Add(lineRating);
                    //Console.WriteLine(lineRating);
                }
            }


            // Sort files
            while ((lineBasic = fileBasic.ReadLine()) != null)
            {
                genres = new List<string>(3);

                // Split lines in tabs
                splitLine = lineBasic.Split('\t');

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
                        startYear = Convert.ToUInt16(splitLine[5]);
                    }

                    // End year
                    if (splitLine[6] != @"\N")
                    {
                        endYear = Convert.ToUInt16(splitLine[6]);
                    }

                    // Run time
                    if (splitLine[7] != @"\N")
                    {
                        runTime = Convert.ToUInt16(splitLine[7]);
                    }

                    // Genres
                    // Add strings split with ", " to the collection of genres
                    if (splitLine[8] != null)
                    {
                        string[] strg = splitLine[8].Split(',');

                        for (int i = 0; i < strg.Length; ++i)
                        {
                            genres.Add(strg[i]);
                        }
                    }


                    // Title rating params
                    float avgRating;
                    ushort numVotes;
                    
                    if (fileR.ElementAt(ratingIndex).Contains(id))
                    {
                        splitLine2 = fileR.ElementAt(ratingIndex).Split('\t');

                        float.TryParse(
                                splitLine2[1], numberStyles, cultureInfo,
                                out avgRating);

                        ushort.TryParse(splitLine2[2], numberStyles,
                            cultureInfo, out numVotes);

                        ratingIndex++;
                    }
                    else
                    {
                        avgRating = 0;
                        numVotes = 0;
                    }

                    // Instatiate title
                    TitleBasic title = new TitleBasic(id, type,
                    primTitle, origiTitle,
                    isAdult, startYear,
                    endYear, runTime, genres, avgRating, numVotes);

                    // Add the title to the collection
                    titles.Add(title);
                }
            }

            // Close the files
            fileRatings.Close();

            fileBasic.Close();

            // Returns the collection
            return titles;
        }
    }
}