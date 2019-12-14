namespace IMDB_DATABASE
{
    /// <summary>
    /// Interface to be extended in diferent
    /// Types of Titles (basic, rating, etc)
    /// to ensure every title has an ID and can be used.
    /// </summary>
    public interface ITitle
    {
        /// <summary>
        /// ID - tconst
        /// </summary>
        string ID { get; }
    }
}
