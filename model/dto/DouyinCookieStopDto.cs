namespace dy.net.model.dto
{
    public class DouyinCookieSwitchDto
    {
        public string Id { get; set; }

        public int Status { get; set; }
    }

    public class DouyinCollectCateSwitchDto
    {
        public string Id { get; set; }

        public bool Sync { get; set; }

        public string SaveFolder { get; set; }

        /// <summary>
        /// 合集/短剧文件命名方式:0=S01E集数 1=S01E集数+标题 2=仅标题
        /// </summary>
        public int? NameMode { get; set; }
    }
}
