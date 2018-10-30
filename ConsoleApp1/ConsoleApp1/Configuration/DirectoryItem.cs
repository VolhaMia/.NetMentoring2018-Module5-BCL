using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Configuration
{
    public class DirectoryItem : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public string Path => (string)this["path"];
    }
}
