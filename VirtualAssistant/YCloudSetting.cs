namespace VirtualAssistant
{
    internal class YCloudSetting
    {
        private static readonly YCloudSetting instance = new YCloudSetting();

        public string YandexCloudToken { get; private set; }
        public string YandexCloudFolderId { get; private set; }
        private YCloudSetting()
        {
            YandexCloudToken = "t1.9euelZrNyJWXj42Lmc3PnY2dlZaZye3rnpWajcaJzIqNmJ3NmZ6Rm8icjYrl8_ceRUFc-e82LxsR_d3z915zPlz57zYvGxH9.SU7c-l9ltstjarrGaJ8Ys4Vd5tWsj6VgdqGCGleZwBOsgsroIdH_ysezx6xok2FHcdv03YhoKkpRY9qGwSCWCg";
            YandexCloudFolderId = "b1gm46lqlkfoushj04at";
        }

        public static YCloudSetting GetInstance()
        {
            return instance;
        }
    }
}
