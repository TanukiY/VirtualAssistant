using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAssistant
{
    internal class DictionaryCmd
    {
        public DictionaryCmd(string reference, string synonym)
        {
            this.Reference = reference;
            this.Synonym = synonym;
        }
        public string Reference { get; private set; }
        public string Synonym { get; private set; }
    }
}
