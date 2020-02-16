using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using WolcenFileManagers;

namespace WolcenSaveEditor.VMS
{
    public class FileBackupViewModel : ViewModelBase
    {
        private readonly IFileBackup _fileBackup;

        public FileBackupViewModel(IFileBackup fileBackup)
        {
            _fileBackup = fileBackup;
        }
    }
}
