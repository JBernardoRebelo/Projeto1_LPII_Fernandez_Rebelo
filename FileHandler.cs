using System;
using System.IO;
using System.IO.Compression;

namespace IMDB_DATABASE
{
    /// <summary>
    /// Class that handles file decompression
    /// </summary>
    public class FileHandler
    {
        // Class consts
        /// <summary>
        /// Folder name where files exist
        /// </summary>
        private const string appName = "MyIMDBSearcher";

        /// <summary>
        /// Basic title compressed file name
        /// </summary>
        private const string fileTitleBasics = "title.basics.tsv.gz";

        /// <summary>
        /// Title ratings compressed file name
        /// </summary>
        private const string fileTitleRatings = "title.ratings.tsv.gz";

        /// <summary>
        /// Method to decompress basic information file.
        /// </summary>
        /// <returns> Decompressed basic title file. </returns>
        public StreamReader MakeReadableBasics()
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

        /// <summary>
        /// Method to decompress ratings file.
        /// </summary>
        /// <returns> Decompressed title ratings file. </returns>
        public StreamReader MakeReadableRatings()
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
