using System;
using System.IO;
using System.Linq;

namespace KvlvVeldegem.PhotoRenamer
{
    internal class Program
    {
        private static void Main()
        {
            var folderLocation = PromptForInput("What folder do you want to process?");

            if (Directory.Exists(folderLocation))
            {
                var albumName = PromptForInput("What's the name of the album?");

                RenameImage(folderLocation, albumName);
            }
            else
            {
                Console.WriteLine("Folder does not exist.");
            }

            Console.WriteLine("Press any key to close the utility");
            Console.ReadLine();
        }

        private static string PromptForInput(string inputDescription)
        {
            Console.WriteLine(inputDescription);
            var folderLocation = Console.ReadLine();
            return folderLocation;
        }

        private static void RenameImage(string folderLocation, string albumName)
        {
            var photoUris = Directory.EnumerateFiles(folderLocation)
                .Where(file => file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".jpg"));

            var counter = 1;

            foreach (var photoUri in photoUris)
            {
                var oldFileName = Path.GetFileName(photoUri);
                var newFileName = ComposeNewFileName(albumName, counter, photoUri);
                var newFileUri = $"{folderLocation}\\{newFileName}";

                Console.WriteLine($"Renaming '{oldFileName}' to '{newFileName}'");

                File.Move(photoUri, newFileUri);

                counter++;
            }
        }

        private static string ComposeNewFileName(string albumName, int counter, string photoUri)
        {
            var photoExtension = Path.GetExtension(photoUri).ToLower();
            var newFileName = $"{albumName} #{counter}{photoExtension}";
            return newFileName;
        }
    }
}