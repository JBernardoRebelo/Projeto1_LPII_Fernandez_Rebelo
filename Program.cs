using System;
using System.IO;
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
            // Ao nível de um método
            // Caminho completo da pasta contendo os ficheiros de dados
            string folderWithFiles = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                appName);

            // Caminho completo de cada um dos ficheiros de dados
            string fileTitleBasicsFull = Path.Combine(folderWithFiles, fileTitleBasics);
            string fileTitleRatingsFull = Path.Combine(folderWithFiles, fileTitleRatings);


            TitleLoader.OutputTestFile(fileTitleBasicsFull);

            // Call loop

            // Debug **************************************************
            string line;
            string[] splitLine;

            line = "ttas";

            string firstChars = line[0].ToString() + line[1].ToString();
            Console.WriteLine(firstChars);

            // Split lines in tabs
            splitLine = line.Split('\t').ToArray();

            if (firstChars == "tt")
            {

            }
        }
    }
}
