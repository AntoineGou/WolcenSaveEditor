using System.Collections.Generic;
using System.IO;

namespace WolcenFileManagers
{
    public interface IFileBackup
    {
        DirectoryInfo SaveDirectory { get; set; }
        FileBackupStatus Status { get; }
        List<string> Characters { get; set; }
        bool LoadDirectory();
        (bool Result, string Message) BackUpDirectory(string fileName);
        (bool Result, string Message) RestoreDirectory(string source);
    }
}