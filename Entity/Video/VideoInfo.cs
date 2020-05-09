using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownLoadHaoKanVideoAPI.Entity.Video
{
    public class VideoInfo
    {
        public int Id { get; set; }
        //原始网站url
        public string Url { get; set; }
        public string Title { get; set; }
        public string DownLoadUrl { get; set; }
        public long FileSize { get; set; }
        public string Info { get; set; }

        public long Startposition { get; set; }
        public long Endpositon { get; set; }
    }
}
