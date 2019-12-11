namespace IMDB_DATABASE
{
    public class Program
    {
        private static void Main()
        {
            // Declares and creates a new instance of SearchLoop class to run
            // search loop
            SearchLoop searchLoop = new SearchLoop
                (FileHandler.MakeReadableBasics(),
                 FileHandler.MakeReadableRatings());

            // Search loop method
            searchLoop.BeginSearch();
        }
    }
}
