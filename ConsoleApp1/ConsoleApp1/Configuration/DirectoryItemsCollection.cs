using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Configuration
{
    public class DirectoryItemsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryItem)element).Path;
        }
    }
}
