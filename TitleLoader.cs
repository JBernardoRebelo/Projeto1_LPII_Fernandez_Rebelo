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
        /// Instantiates basic titles in a file
        /// </summary>
        /// <param name="filename"> Accepts a file name. </param>
        /// <returns> Returns an ICollection of ITitle title basic info. 
        /// </returns>
        public ICollection<ITitle> LoadTitlesBasic(StreamReader file)
        {
            // List of titlesBasic
            List<ITitle> titlesBasic = new List<ITitle>();

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

            int debug = 0;

            // Line
            string line;
            string[] splitLine;

            // Sort file
            while ((line = file.ReadLine()) != null)
            {
                genres = new List<string>(3);

                // Split lines in tabs
                splitLine = line.Split('\t');

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

                    // Instatiate title
                    TitleBasic title = new TitleBasic(id, type,
                        primTitle, origiTitle,
                        isAdult, startYear,
                        endYear, runTime, genres);

                    // Add the title to the collection
                    titlesBasic.Add(title);

                    debug++;
                    if(debug > 50)
                    {
                        
                    }
                }
            }

            // Close the file
            file.Close();

            // Returns the collection
            return titlesBasic;
        }

        /// <summary>
        /// Instanciates title ratings in a file
        /// </summary>
        /// <param name="file"> Accepts a file name. </param>
        /// <returns> Returns an ICollection of ITitle title ratings. </returns>
        public ICollection<ITitle> LoadTitlesRating(StreamReader file)
        {

            NumberStyles numberStyles = NumberStyles.Any;
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");

            // Line
            string line;
            string[] splitLine;

            // List of titlesBasic
            List<ITitle> titlesRating = new List<ITitle>();

            // Sort file
            while ((line = file.ReadLine()) != null)
            {
                splitLine = line.Split('\t');

                // Title Id
                if (splitLine[0] != "tconst")
                {
                    // Block variables to assign
                    int numVotes;
                    string id = splitLine[0];

                    // Title Type
                    float.TryParse(
                       splitLine[1], numberStyles, cultureInfo,
                       out float avgRating);

                    // Primary title
                    numVotes = Convert.ToInt32(splitLine[2]);

                    // Create a new instance of TitleRating
                    TitleRating titleR = new TitleRating
                        (id, avgRating, numVotes);

                    // Add instance to the collection
                    titlesRating.Add(titleR);
                }
            }

            // Close file
            file.Close();

            // Returns the collection
            return titlesRating;
        }

        // CREATE METH(HEAD)OD TO COMBINE COLLECTIONS

        //public ICollection<ITitle> CombineTitleCollections(
        //    ICollection<ITitle> basic, ICollection<ITitle> rating)
        //{
        //    var query 
        //        = rating.Join(basic,
        //                title => title,

        //                (fullTitle, rate) =>
        //            new { fullTit = person.Name, Pet = pet.Name });

        //    foreach (ITitle title in query)
        //    {
        //        Console.WriteLine(
        //            "{0} - {1}",
        //            title.,
        //            title.Pet);
        //    }

        //    return 
        //}

        //Join(IEnumerable<TOuter>, IEnumerable<TInner>, Func<TOuter, TKey>, Func<TInner, TKey>, Func<TOuter, TInner, TResult>)
    }
}
