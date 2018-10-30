using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Configuration
{
    public class FilesConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("culture", DefaultValue = "en-EN", IsRequired = false)]
        public CultureInfo Culture => (CultureInfo)this["culture"];

        [ConfigurationCollection(typeof(DirectoryItem), AddItemName = "directory")]
        [ConfigurationProperty("directories", IsRequired = false)]
        public DirectoryItemsCollection Directories => (DirectoryItemsCollection)this["directories"];

        [ConfigurationCollection(typeof(RuleItem), AddItemName = "rule")]
        [ConfigurationProperty("rules", IsRequired = true)]
        public RuleItemsCollection Rules => (RuleItemsCollection)this["rules"];
    }
}
