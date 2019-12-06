namespace IMDB_DATABASE
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // **********************************************************
            // Static method call LoadTitlesBasic - static for debug
            //Console.WriteLine("\nBASIC FILE\n");
            //TitleLoader.LoadTitlesBasic(MakeReadableBasics());
            //Console.WriteLine("\nRATING'S FILE\n");
            //TitleLoader.LoadTitlesRating(MakeReadableRatings());
            // **********************************************************


            // Call loop, accepts the readable files - so far only basics
            SearchLoop loop = new SearchLoop
                (FileHandler.MakeReadableBasics(),
                 FileHandler.MakeReadableRatings());

            loop.ActualLoop();

        }
    }
}
