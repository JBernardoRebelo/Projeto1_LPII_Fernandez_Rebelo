using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace IMDB_DATABASE
{
    public class Program
    {
        // Ao nível de uma classe
        const string appName = "MyIMDBSearcher";
        private const string fileTitleBasics = "title.basics.tsv.gz";
        private const string fileTitleRatings = "title.ratings.tsv.gz";

        private static void Main(string[] args)
        {
            // Static method call LoadTitlesBasic - static for debug
            Console.WriteLine("\nBASIC FILE\n");
            TitleLoader.LoadTitlesBasic(MakeReadableBasics());
            Console.WriteLine("\nRATING'S FILE\n");
            TitleLoader.LoadTitlesRating(MakeReadableRatings());

            // Call loop
        }

        // Returns a streamReader file to use - title basics
        private static StreamReader MakeReadableBasics()
        {
            string folderWithFiles
                = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData),
                appName);

            // Caminho completo de cada um dos ficheiros de dados
            string fileTitleBasicsFull = Path.Combine(folderWithFiles,
                fileTitleBasics);

            FileStream fs = new FileStream(fileTitleBasicsFull,
                FileMode.Open, FileAccess.Read);
            GZipStream zipFile = new GZipStream(fs,
                CompressionMode.Decompress);
            StreamReader file = new StreamReader(zipFile);

            return file;
        }

        // Returns a streamReader file to use - title ratings
        private static StreamReader MakeReadableRatings()
        {
            // Access needed folder
            string folderWithFiles
                = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData),
                appName);

            string fileTitleRatingsFull = Path.Combine(folderWithFiles,
                fileTitleRatings);

            FileStream fs = new FileStream(fileTitleRatingsFull,
                FileMode.Open, FileAccess.Read);
            GZipStream zipFile = new GZipStream(fs,
                CompressionMode.Decompress);
            StreamReader file = new StreamReader(zipFile);

            return file;
        }

    }
}
