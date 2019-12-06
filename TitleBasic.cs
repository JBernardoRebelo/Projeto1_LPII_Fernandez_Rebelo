using System.Collections.Generic;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Class for basic Title File
    /// </summary>
    public struct TitleBasic : ITitle
    {
        /// <summary>
        /// ID - tconst
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Type - titleType
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Primary Title - primaryTitle
        /// </summary>
        public string PrimTitle { get; }

        /// <summary>
        /// Original Title - originalTitle
        /// </summary>
        public string OrigiTitle { get; }

        /// <summary>
        /// Is Adult - isAdult
        /// </summary>
        public bool IsAdult { get; }

        /// <summary>
        /// Start year - startYear
        /// </summary>
        public int StartYear { get; }

        /// <summary>
        /// End Year - endYear
        /// </summary>
        public int EndYear { get; }

        /// <summary>
        /// Run Time in Minutes - runtimeMinutes
        /// </summary>
        public int RunTimeMin { get; }

        /// <summary>
        /// "Array" of Genres - genres
        /// </summary>
        public HashSet<string> Genres { get; }

        /// <summary>
        /// Accepts as argument all properties to assign
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="primTitle"></param>
        /// <param name="origiTitle"></param>
        /// <param name="isAdult"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="runTime"></param>
        /// <param name="genres"></param>
        public TitleBasic(string id, string type,
            string primTitle, string origiTitle,
            bool isAdult, int startYear,
            int endYear, int runTime,
            HashSet<string> genres)
        {
            ID = id;
            Type = type;
            PrimTitle = primTitle;
            OrigiTitle = origiTitle;
            IsAdult = isAdult;
            StartYear = startYear;
            EndYear = endYear;
            RunTimeMin = runTime;
            Genres = genres;
        }

        public int Compare(ITitle x, ITitle y)
        {
            throw new System.NotImplementedException();
        }

        public int CompareTo(ITitle other)
        {
            throw new System.NotImplementedException();
        }
    }
}
