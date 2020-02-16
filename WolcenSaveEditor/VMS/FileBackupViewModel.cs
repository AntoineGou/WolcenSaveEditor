using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WolcenFileManagers;

namespace WolcenSaveEditor.VMS
{
    public class FileBackupViewModel : ViewModelBase
    {
        private readonly IFileBackup _fileBackup;
        private bool _isDirectoryValid;
        private string _saveDirectory;

        public FileBackupViewModel(IFileBackup fileBackup)
        {
            _fileBackup = fileBackup;
            fileBackup.SaveDirectory = GetDefaultSaveDirectory();

            SaveDirectory = fileBackup.SaveDirectory.FullName;
            IsDirectoryValid = fileBackup.LoadDirectory();
        }

        public string SaveDirectory
        {
            get => _saveDirectory;
            set
            {
                _saveDirectory = value;
                RaisePropertyChanged();
            }
        }

        public bool IsDirectoryValid
        {
            get => _isDirectoryValid;
            set
            {
                _isDirectoryValid = value;
                RaisePropertyChanged();
            }
        }

        private DirectoryInfo GetDefaultSaveDirectory()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return new DirectoryInfo(@$"{basePath}/Saved Games/wolcen/savegames");
        }
    }
}
