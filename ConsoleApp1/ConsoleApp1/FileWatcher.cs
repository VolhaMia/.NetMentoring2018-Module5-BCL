using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using ConsoleApp1.Resources;

namespace ConsoleApp1
{
    public class FileWatcher
    {
        private readonly List<FileSystemWatcher> _fileSystemWatchers;
        private readonly ILogger _logger;

        public FileWatcher(IEnumerable<string> directories, ILogger logger)
        {
            _logger = logger;
            _fileSystemWatchers = directories.Select(FileSystemCreateWatcher).ToList();
        }

        public event EventHandler<FileSystemCreateEventArgs> Created;

        private void OnCreated(FileModel file)
        {
            Created?.Invoke(this, new FileSystemCreateEventArgs { CreatedFile = file });
        }

        private FileSystemWatcher FileSystemCreateWatcher(string path)
        {
            FileSystemWatcher fileSystemWatcher =
                new FileSystemWatcher(path)
                {
                    NotifyFilter = NotifyFilters.FileName,
                    IncludeSubdirectories = false,
                    EnableRaisingEvents = true
                };
            fileSystemWatcher.Created += (sender, fileSystemEventArgs) =>
            {
                _logger.Log(string.Format(Strings.FileFound, fileSystemEventArgs.Name));
                OnCreated(new FileModel { FullName = fileSystemEventArgs.FullPath, Name = fileSystemEventArgs.Name });
            };

            return fileSystemWatcher;
        }
    }
}
