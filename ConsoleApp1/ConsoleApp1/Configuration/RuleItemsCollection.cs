using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Configuration
{
    public class RuleItemsCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultDirectory", IsRequired = true)]
        public string DefaultDirectory => (string)this["defaultDirectory"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleItem)element).FileTemplate;
        }
    }
}
