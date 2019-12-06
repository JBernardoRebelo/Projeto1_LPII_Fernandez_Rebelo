using System;
using System.Collections.Generic;

namespace IMDB_DATABASE
{
    public struct TitleRating : ITitle, IComparer<TitleRating>, 
        IComparable<TitleRating>
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

        public int Compare(TitleRating x, TitleRating y)
        {
            if (x.AvgRating.CompareTo(y.AvgRating) != 0)
            {
                return x.AvgRating.CompareTo(y.AvgRating);
            }
            else
            {
                return 0;
            }
        }

        public int CompareTo(TitleRating other)
        {
            if (AvgRating > other.AvgRating)
            {
                return -1;
            }
            else if (AvgRating < other.AvgRating)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Compare(ITitle x, ITitle y)
        {
            return Compare((TitleRating)x, (TitleRating)y);
        }

        public int CompareTo(ITitle other)
        {
            return CompareTo((TitleRating)other);
        }
    }
}
