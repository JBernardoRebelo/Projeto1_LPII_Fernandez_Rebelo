using System;
using System.Collections.Generic;

namespace IMDB_DATABASE
{
    public struct TitleRating : ITitle
    {
        /// <summary>
        /// ID - tconst
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Average Rating of title - averageRating
        /// </summary>
        public float AvgRating { get; }

        /// <summary>
        /// Number of votes - numVotes
        /// </summary>
        public int NumVotes { get; }

        /// <summary>
        /// TitleRating constructor
        /// </summary>
        /// <param name="id"> Accepts an ID (tconst) </param>
        /// <param name="avgRating"> Average rating </param>
        /// <param name="numVotes"> Number of votes </param>
        public TitleRating(string id, float avgRating, int numVotes)
        {
            ID = id;
            AvgRating = avgRating;
            NumVotes = numVotes;
        }
    }
}
