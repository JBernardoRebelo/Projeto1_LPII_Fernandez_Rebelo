using System;
using System.IO;
using System.Linq;

namespace IMDB_DATABASE
{
    public class Program
    {
        private static void Main(string[] args)
        {
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
