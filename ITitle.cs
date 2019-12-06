using System;
using System.Collections.Generic;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Interface to be extended in diferent
    /// Types of Titles (basic, rating, etc)
    /// </summary>
    public interface ITitle : IComparer<ITitle>, IComparable<ITitle>
    {
        /// <summary>
        /// ID - tconst
        /// </summary>
        string ID { get; }
    }
}
