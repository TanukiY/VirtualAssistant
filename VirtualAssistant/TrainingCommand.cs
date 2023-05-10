namespace VirtualAssistant
{
    internal class TrainingCommand
    {
        public TrainingCommand(string reference, string synonym)
        {
            this.Reference = reference;
            this.Synonym = synonym;
        }
        public string Reference { get; private set; }
        public string Synonym { get; private set; }
    }
}
