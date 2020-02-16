using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace WolcenFileManagers
{
    public class FileBackup : IFileBackup
    {
        private const string PlayerChestFileName = "playerchest.json";
        private const string PlayerDataFileName = "playerdata.json";
        private const string CharactersDirectoryName = "characters";

        public DirectoryInfo SaveDirectory { get; set; }

        public FileBackup()
        {
        }

        public FileBackupStatus Status { get; private set; } = FileBackupStatus.Created;
        public List<string> Characters { get; set; } = new List<string>();

        public bool LoadDirectory()
        {
            SaveDirectory.Refresh();
            if (SaveDirectory.Exists)
            {
                var files = SaveDirectory.GetFiles("*.json");
                if (files.Any(x => x.Name == PlayerChestFileName) && files.Any(x => x.Name == PlayerDataFileName))
                {
                    if (SaveDirectory.GetDirectories().Any(x => x.Name == CharactersDirectoryName))
                    {
                        var charFiles = SaveDirectory.EnumerateDirectories().Single(x => x.Name == CharactersDirectoryName)
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

        public (bool Result, string Message) BackUpDirectory(string fileName)
        {
            if (!fileName.EndsWith(".zip"))
                fileName = $"{fileName}.zip";
            try
            {
                ZipFile.CreateFromDirectory(SaveDirectory.FullName, fileName);
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
                ZipFile.ExtractToDirectory(source, SaveDirectory.FullName);
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
