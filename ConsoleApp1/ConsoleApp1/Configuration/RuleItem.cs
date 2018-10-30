using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Configuration
{
    public class RuleItem : ConfigurationElement
    {
        [ConfigurationProperty("fileTemplate", IsRequired = true, IsKey = true)]
        public string FileTemplate => (string)this["fileTemplate"];

        [ConfigurationProperty("toFolder", IsRequired = true)]
        public string ToFolder => (string)this["toFolder"];

        [ConfigurationProperty("isOrderAdded", IsRequired = false, DefaultValue = false)]
        public bool IsOrderAdded => (bool)this["isOrderAdded"];

        [ConfigurationProperty("isDateAdded", IsRequired = false, DefaultValue = false)]
        public bool IsDateAdded => (bool)this["isDateAdded"];
    }
}
