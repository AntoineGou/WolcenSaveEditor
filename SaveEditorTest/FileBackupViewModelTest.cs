using NUnit.Framework;
using WolcenFileManagers;
using WolcenSaveEditor.VMS;

namespace SaveEditorTest
{
    [TestFixture]
    public class FileBackupViewModelTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Category("integration")]
        [Test]
        public void FileBackupViewModel_OnCreation_ShouldLocateSaveFolder_IfDefault()
        {
            var fileBackup = new FileBackup();
            var viewModel = new FileBackupViewModel(fileBackup);

            Assert.AreEqual(FileBackupStatus.Loaded, fileBackup.Status);
            Assert.AreEqual(true, viewModel.IsDirectoryValid);
        }
    }
}