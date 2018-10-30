using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.Configuration;
using ConsoleApp1.Models;
using ConsoleApp1.Resources;

namespace ConsoleApp1
{
    public class Program
    {
        private static FileMover _fileMover;
        private static List<Rule> _rules;
        private static List<string> _directories;

        public static void Main(string[] args)
        {
            try
            {
                FilesConfigSection config = ConfigurationManager.GetSection("filesConfigSection") as FilesConfigSection;

                if (config != null)
                {
                    ConfigRead(config);
                }
                else
                {
                    Console.WriteLine(Strings.ConfigNotFound);
                    return;
                }

                Console.WriteLine(config.Culture.DisplayName);

                _fileMover = new FileMover(_rules, config.Rules.DefaultDirectory);
                var watcher = new FileWatcher(_directories);

                watcher.Created += OnCreated;

                CancellationTokenSource source = new CancellationTokenSource();

                Console.CancelKeyPress += (o, e) =>
                {
                    watcher.Created -= OnCreated;
                    source.Cancel();
                };

                Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token).Wait(source.Token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static async void OnCreated(object sender, FileSystemCreateEventArgs args)
        {
            await _fileMover.MoveToAsync(args.CreatedFile);
        }

        private static void ConfigRead(FilesConfigSection config)
        {
            _directories = new List<string>(config.Directories.Count);
            _rules = new List<Rule>();

            foreach (DirectoryItem dir in config.Directories)
            {
                _directories.Add(dir.Path);
            }

            foreach (RuleItem rule in config.Rules)
            {
                _rules.Add(new Rule
                {
                    FileTemplate = rule.FileTemplate,
                    ToFolder = rule.ToFolder,
                    IsDateAdded = rule.IsDateAdded,
                    IsOrderAdded = rule.IsOrderAdded
                });
            }

            CultureInfo.DefaultThreadCurrentCulture = config.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = config.Culture;
            CultureInfo.CurrentUICulture = config.Culture;
            CultureInfo.CurrentCulture = config.Culture;
        }
    }
}
