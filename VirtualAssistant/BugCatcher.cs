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
