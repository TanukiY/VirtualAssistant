namespace VirtualAssistant
{
    internal class YCloudSetting
    {
        private static readonly YCloudSetting instance = new YCloudSetting();

        public string YandexCloudToken { get; private set; }
        public string YandexCloudFolderId { get; private set; }
        private YCloudSetting()
        {
            YandexCloudToken = "t1.9euelZqXxs2Jj8iUk5iVzc2Xz8zLjO3rnpWajcaJzIqNmJ3NmZ6Rm8icjYrl8_coREZc-e9IYTFc_t3z92hyQ1z570hhMVz-.dIpzVSLcT7LyzcO1ezpRn1ohDPRwMxJiQ_lHX7k7QcpsfMvouAIBcJR8gGpwjkmU5FG0y-z6tZjAwzFdOq-0BA";
            YandexCloudFolderId = "b1gm46lqlkfoushj04at";
        }

        public static YCloudSetting GetInstance()
        {
            return instance;
        }
    }
}
