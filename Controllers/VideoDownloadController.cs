using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Entity;
using DownLoadHaoKanVideoAPI.Entity.Video;
using Microsoft.AspNetCore.Mvc;

namespace DownLoadHaoKanVideoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class VideoDownloadController:ControllerBase
    {
        public static List<VideoInfo> Downloadlist = new List<VideoInfo>();
        Downloads downloads = new Downloads();
        public HttpClient Client { get; set; }
        public VideoDownloadController()
        {
            Client = downloads.RunClient(8, webProxy: new WebProxy("127.0.0.1", 1080));
        }

        /// <summary>
        /// 获取下载视频信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetUrl(string url)
        {
            if (!url.Contains("http") || !url.Contains("https") || !url.Contains("www"))
            {
                return new ResultModel<Employee> { State = ResultType.Error, Message = "url格式错误" };
            }
            var info= await downloads.AutomationGo(url);
            return new ResultModel<VideoInfo> { State = ResultType.Success, Message = "查询成功",Data = info};
        }
    }
}
