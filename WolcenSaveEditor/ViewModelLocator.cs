using GalaSoft.MvvmLight.Ioc;
using WolcenFileManagers;
using WolcenSaveEditor.VMS;

namespace WolcenSaveEditor
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IFileBackup, FileBackup>();
            SimpleIoc.Default.Register<FileBackupViewModel>();
        }

        public FileBackupViewModel FileBackupViewModel => SimpleIoc.Default.GetInstance<FileBackupViewModel>();
    }
}