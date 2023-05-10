using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAssistant
{
    internal class BugCatcher
    {
        public BugCatcher() { 
        
        }

        public string TreatmentBug(string userMessage)
        {
            if(userMessage=="" || userMessage == " ")
                return "Вы ничего не ввели";
            return null;
        }
    }
}
