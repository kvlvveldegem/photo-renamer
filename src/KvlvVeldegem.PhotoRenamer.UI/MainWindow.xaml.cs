using System.IO;
using System.Windows;
using Avalon.Windows.Dialogs;

namespace KvlvVeldegem.PhotoRenamer.UI
{
    public partial class MainWindow : Window
    {
        private readonly PhotoProcessor _photoProcessor = new PhotoProcessor();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSelectFolderClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            var dialog = new FolderBrowserDialog
            {
                Title = "Select the folder containing all your photos"
            };

            var dialogOutput = dialog.ShowDialog();
            if (dialogOutput == true)
            {
                FolderPath.Text = dialog.SelectedPath;
            }
        }

        private void OnStartProcessingClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            ProcessPhotosInFolder();
        }

        private void ProcessPhotosInFolder()
        {
            var folderPath = FolderPath.Text;
            if (Directory.Exists(folderPath) == false)
            {
                MessageBox.Show(this, $"Specified folder '{folderPath}' does not exist", "Folder does not exist", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _photoProcessor.Rename(folderPath, AlbumName.Text);
                MessageBox.Show(this, "All photos were renamed succesfully!", "Rename finished", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnAlbumNameTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateInputState();
        }

        private void ValidateInputState()
        {
            if (string.IsNullOrWhiteSpace(AlbumName.Text) == false && string.IsNullOrWhiteSpace(FolderPath.Text) == false)
            {
                StartProcessingButton.IsEnabled = true;
            }
        }
    }
}