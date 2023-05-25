namespace VirtualAssistant
{
    internal class Setting
    {
        private static readonly Setting instance = new Setting();

        public string YandexCloudToken { get; private set; }
        public string YandexCloudFolderId { get; private set; }
        public string GPTToken { get; private set; }
        private Setting()
        {
            YandexCloudToken = "t1.9euelZrNyJWXj42Lmc3PnY2dlZaZye3rnpWajcaJzIqNmJ3NmZ6Rm8icjYrl8_ceRUFc-e82LxsR_d3z915zPlz57zYvGxH9.SU7c-l9ltstjarrGaJ8Ys4Vd5tWsj6VgdqGCGleZwBOsgsroIdH_ysezx6xok2FHcdv03YhoKkpRY9qGwSCWCg";
            YandexCloudFolderId = "b1gm46lqlkfoushj04at";
            GPTToken = "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";
        }

        public static Setting GetInstance()
        {
            return instance;
        }
    }
}
