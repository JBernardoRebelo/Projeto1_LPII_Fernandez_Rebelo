namespace IMDB_DATABASE
{
    public class Program
    {
        private static void Main()
        {
            // Instance of FileHandler to open files to be read
            FileHandler fh = new FileHandler();

            // Declares and creates a new instance of SearchLoop class to run
            // search loop
            SearchLoop searchLoop = new SearchLoop
                (fh.MakeReadableBasics(),
                 fh.MakeReadableRatings());

            // Search loop method
            searchLoop.BeginSearch();
        }
    }
}
