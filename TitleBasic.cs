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
        public ushort StartYear { get; }

        /// <summary>
        /// End Year - endYear
        /// </summary>
        public ushort EndYear { get; }

        /// <summary>
        /// Run Time in Minutes - runtimeMinutes
        /// </summary>
        public ushort RunTimeMin { get; }

        /// <summary>
        /// Array of Genres - genres
        /// </summary>
        public ICollection<string> Genres { get; }


        /// <summary>
        /// Average Rating of title - averageRating
        /// </summary>
        public float AvgRating { get;}

        /// <summary>
        /// Number of votes - numVotes
        /// </summary>
        public ushort NumVotes { get;}

        /// <summary>
        /// Constructor that uses params to set values to all properties.
        /// </summary>
        /// <param name="id"> Title's ID. </param>
        /// <param name="type"> Title's type. </param>
        /// <param name="primTitle"> Title's primary title. </param>
        /// <param name="origiTitle"> Title's original title. </param>
        /// <param name="isAdult"> If title has adult content. </param>
        /// <param name="startYear"> Year that title was released. </param>
        /// <param name="endYear"> Year that title ended, if it was a series.
        /// </param>
        /// <param name="runTime"> Title's length. </param>
        /// <param name="genres"> Title's genre/genres. </param>
        /// 
        ///
        ///
        public TitleBasic(string id, string type,
            string primTitle, string origiTitle,
            bool isAdult, ushort startYear,
            ushort endYear, ushort runTime,
            ICollection<string> genres, float avgRat, ushort numVotes)
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

            // Fase 2
            AvgRating = avgRat;
            NumVotes = numVotes;
        }
    }
}
