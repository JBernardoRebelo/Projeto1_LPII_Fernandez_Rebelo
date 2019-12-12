namespace IMDB_DATABASE
{
    /// <summary>
    /// Interface to be extended in diferent
    /// Types of Titles (basic, rating, etc)
    /// </summary>
    public interface ITitle // Should this implement IEqualityComparer<T>?!?!?
    {
        /// <summary>
        /// ID - tconst
        /// </summary>
        string ID { get; }

        // SHOULD MORE PROPERTIES BE INLCUDED? TO CREATE A TITLE WITH COMPLETE INFO?????
    }
}
