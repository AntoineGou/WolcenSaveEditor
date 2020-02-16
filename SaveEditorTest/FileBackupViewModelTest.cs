﻿using NUnit.Framework;
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

        /// <summary>
        /// TODO: this test is crap
        /// </summary>
        [Category("integration")]
        [Test]
        public void FileBackupViewModel_Save_OnInvalidFolder_DoesNothing()
        {
            var fileBackup = new FileBackup();
            var viewModel = new FileBackupViewModel(fileBackup);
            viewModel.SaveDirectory = "ndfohsdfoihsd";

            Assert.AreEqual(FileBackupStatus.Error, fileBackup.Status);
            Assert.AreEqual(false, viewModel.IsDirectoryValid);

            viewModel.PerformSave();
        }


    }
}