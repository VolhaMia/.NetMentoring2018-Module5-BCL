using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    public class FileSystemCreateEventArgs: System.EventArgs
    {
        public FileModel CreatedFile { get; set; }
    }
}
