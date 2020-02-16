using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using WolcenFileManagers;

namespace SaveEditorTest
{
    public class FileBackupTests
    {
        private static readonly string BasePath = $"{Directory.GetCurrentDirectory()}/IntegrationTestsFiles";

        [SetUp]
        public void Setup()
        {
        }

        [Category("unit")]
        [Test]
        public void FileBackup_Creation_ShouldBeInCreatedStatus()
        {
            var fileBackup = new FileBackup(BasePath);
            Assert.AreEqual(FileBackupStatus.Created, fileBackup.Status);
        }

        [Category("unit")]
        [Test]
        public void FileBackup_LoadDirectory_OnRandomPath_ShouldReturnFalse()
        {
            var fileBackup = new FileBackup(BasePath + "NIMP");
            Assert.AreEqual(false,fileBackup.LoadDirectory());
            Assert.AreEqual(FileBackupStatus.Error, fileBackup.Status);
        }

        [Category("integration")]
        [Test]
        public void FileBackup_LoadDirectory_OnBaseFiles_ShouldDetect1Character()
        {
            var fileBackup = new FileBackup(BasePath);
            Assert.AreEqual(true, fileBackup.LoadDirectory());
            Assert.AreEqual(FileBackupStatus.Loaded,fileBackup.Status);
            Assert.AreEqual(1, fileBackup.Characters.Count);
            Assert.AreEqual("Bamboulorc", fileBackup.Characters.First());
        }

        [Category("integration")]
        [Test]
        public void FileBackup_BackupDirectory_OnBaseFiles_ShouldCreateAZipFile()
        {
            var fileBackup = new FileBackup(BasePath);
            Assert.AreEqual(true, fileBackup.LoadDirectory());
            Assert.AreEqual(FileBackupStatus.Loaded, fileBackup.Status);
            Assert.AreEqual(1, fileBackup.Characters.Count);

            var saveName = $"bu{DateTime.Now:yy-MM-dd-mm-ss}";
            var output = "./output";
            Assert.AreEqual(true,fileBackup.BackUpDirectory(output, saveName).Result);
            Assert.AreEqual(true, File.Exists($"{output}/{saveName}.zip"));
        }


    }
}