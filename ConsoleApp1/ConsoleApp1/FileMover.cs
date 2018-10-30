using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using ConsoleApp1.Resources;

namespace ConsoleApp1
{
    public class FileMover
    {
        private readonly List<Rule> _rules;
        private readonly string _defaultDirectory;
        private const int CheckingFileTimeout = 500;

        public FileMover(IEnumerable<Rule> rules, string defaultDirectory)
        {
            _rules = rules.ToList();
            _defaultDirectory = defaultDirectory;
        }

        public async Task MoveToAsync(FileModel file)
        {
            string fromDir = file.FullName;
            foreach (Rule rule in _rules)
            {
                var match = Regex.Match(file.Name, rule.FileTemplate);

                if (match.Success && match.Length == file.Name.Length)
                {
                    rule.MatchCount++;
                    string toDir = CreateDestination(file, rule);
                    Console.WriteLine(Strings.RuleFound);
                    await MoveFileToAsync(fromDir, toDir);
                    Console.WriteLine(string.Format(Strings.FileMovedTo, file.FullName, toDir));
                    return;
                }
            }

            string destination = Path.Combine(_defaultDirectory, file.Name);
            Console.WriteLine(Strings.RuleNotFound);
            await MoveFileToAsync(fromDir, destination);
            Console.WriteLine(string.Format(Strings.FileMovedTo, file.FullName, destination));
        }

        private string CreateDestination(FileModel file, Rule rule)
        {
            string extension = Path.GetExtension(file.Name);
            string fileName = Path.GetFileNameWithoutExtension(file.Name);
            StringBuilder destination = new StringBuilder();
            destination.Append(Path.Combine(rule.ToFolder, fileName));

            if (rule.IsDateAdded)
            {
                var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                dateTimeFormat.DateSeparator = ".";
                destination.Append($"_{DateTime.Now.ToString(dateTimeFormat.ShortDatePattern)}");
            }

            if (rule.IsOrderAdded)
            {
                destination.Append($"_{rule.MatchCount}");
            }

            destination.Append(extension);
            return destination.ToString();
        }

        private async Task MoveFileToAsync(string from, string to)
        {
            string directory = Path.GetDirectoryName(to);
            Directory.CreateDirectory(directory);
            bool cannotAccessFile = true;
            do
            {
                try
                {
                    if (File.Exists(to))
                    {
                        File.Delete(to);
                    }
                    File.Move(from, to);
                    cannotAccessFile = false;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine(Strings.FileNotFound);
                    break;
                }
                catch (IOException ex)
                {
                    var t = ex.GetType();
                    await Task.Delay(CheckingFileTimeout);
                }
            }
            while (cannotAccessFile);
        }
    }
}
