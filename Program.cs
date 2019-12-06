namespace IMDB_DATABASE
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Call loop, accepts the readable files - so far only basics
            SearchLoop searchLoop = new SearchLoop
                (FileHandler.MakeReadableBasics(),
                 FileHandler.MakeReadableRatings());

            searchLoop.Loop();
        }
    }
}
