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

            var output = "./output";
            var saveName = $"bu{DateTime.Now:yy-MM-dd-mm-ss}";

            var filePath = $"{output}/{saveName}.zip";
            if (File.Exists(filePath))
                File.Delete(filePath);

            Assert.AreEqual(true,fileBackup.BackUpDirectory(output, saveName).Result);
            Assert.AreEqual(true, File.Exists($"{output}/{saveName}.zip"));
        }

        [Category("integration")]
        [Test]
        public void FileBackup_RestoreDirectory_OnBaseFiles_ShouldRestoreThemProperly()
        {
            var fileBackup = new FileBackup(BasePath);
            Assert.AreEqual(true, fileBackup.LoadDirectory());
            Assert.AreEqual(FileBackupStatus.Loaded, fileBackup.Status);
            Assert.AreEqual(1, fileBackup.Characters.Count);

            var output = "./output";
            var saveName = $"bu{DateTime.Now:yy-MM-dd-mm-ss}";

            var filePath = $"{output}/{saveName}.zip";
            if (File.Exists(filePath))
                File.Delete(filePath);

            
            Assert.AreEqual(true, fileBackup.BackUpDirectory(output, saveName).Result);
            Assert.AreEqual(true, File.Exists(filePath));

            new DirectoryInfo(BasePath).Delete(true);
            var reloadFb = new FileBackup(BasePath);
            Assert.AreEqual(false,reloadFb.LoadDirectory());
            Assert.AreEqual(true, fileBackup.RestoreDirectory(source: filePath).Result);

            Assert.AreEqual(true, reloadFb.LoadDirectory());
            Assert.AreEqual(FileBackupStatus.Loaded, reloadFb.Status);
            Assert.AreEqual(1, reloadFb.Characters.Count);
            Assert.AreEqual("Bamboulorc", reloadFb.Characters.First());
        }

    }
}