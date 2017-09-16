using System.IO;
using System.Linq;

namespace KvlvVeldegem.PhotoRenamer
{
    public class PhotoProcessor
    {
        /// <summary>
        ///     Renames all the images in a specific folder
        /// </summary>
        /// <param name="folderPath">Folder containing all the photos to rename</param>
        /// <param name="photoPrefix">Prefix of the photo</param>
        public void Rename(string folderPath, string photoPrefix)
        {
            var photoUris = Directory.EnumerateFiles(folderPath)
                .Where(file => file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".jpg"));

            var counter = 1;

            foreach (var photoUri in photoUris)
            {
                var newFileName = ComposeNewFileName(photoPrefix, counter, photoUri);
                var newFileUri = $"{folderPath}\\{newFileName}";

                File.Move(photoUri, newFileUri);

                counter++;
            }
        }

        private static string ComposeNewFileName(string photoPrefix, int counter, string photoUri)
        {
            var photoExtension = Path.GetExtension(photoUri).ToLower();
            var newFileName = $"{photoPrefix} #{counter}{photoExtension}";
            return newFileName;
        }
    }
}