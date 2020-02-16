using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace WolcenFileManagers
{
    public class FileBackup
    {
        private const string PlayerChestFileName = "playerchest.json";
        private const string PlayerDataFileName = "playerdata.json";
        private const string CharactersDirectoryName = "characters";

        private readonly DirectoryInfo _directory;

        public FileBackup(string basePath)
        {
            _directory = new DirectoryInfo(basePath);
        }

        public FileBackupStatus Status { get; private set; } = FileBackupStatus.Created;
        public List<string> Characters { get; set; } = new List<string>();

        public bool LoadDirectory()
        {
            _directory.Refresh();
            if (_directory.Exists)
            {
                var files = _directory.GetFiles("*.json");
                if (files.Any(x => x.Name == PlayerChestFileName) && files.Any(x => x.Name == PlayerDataFileName))
                {
                    if (_directory.GetDirectories().Any(x => x.Name == CharactersDirectoryName))
                    {
                        var charFiles = _directory.EnumerateDirectories().Single(x => x.Name == CharactersDirectoryName)
                            .GetFiles("*.json");
                        foreach (var charFile in charFiles)
                        {
                            LoadCharacter(charFile);
                        }
                        Status = FileBackupStatus.Loaded;
                        return true;
                    }
                }
            }
            Status = FileBackupStatus.Error;
            return false;
        }

        private void LoadCharacter(FileInfo charFile)
        {
            if (charFile.Exists)
            {
                Characters.Add(charFile.Name.Replace(".json",""));
            }
        }

        public (bool Result, string Message) BackUpDirectory(string outputDirectory, string fileName)
        {
            var dir = new DirectoryInfo(outputDirectory);
            if (!dir.Exists)
                dir.Create();
            try
            {
                ZipFile.CreateFromDirectory(_directory.FullName, $"{outputDirectory}/{fileName}.zip");
            }
            catch (Exception e)
            {
                return (false, e.Message);
                
            }
            return (true, string.Empty);
        }

        public (bool Result, string Message) RestoreDirectory(string source)
        {
            try
            {
                ZipFile.ExtractToDirectory(source, _directory.FullName);
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }

    public enum FileBackupStatus
    {
        Created,
        Loaded,
        Error
    }
}
