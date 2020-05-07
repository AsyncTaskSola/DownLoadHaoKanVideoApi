using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Entity;
using DownLoadHaoKanVideoAPI.Entity.Video;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DownLoadHaoKanVideoAPI.Controllers
{
    /// <summary>
    /// 视频下载部分
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class VideoDownloadController:ControllerBase
    {
        public static List<VideoInfo> Downloadlist = new List<VideoInfo>();
        Downloads downloads = new Downloads();
        public VideoInfo info { get; set; }
        public HttpClient Client { get; set; }
        public VideoDownloadController()
        {
            Client = downloads.RunClient(8, webProxy: new WebProxy("127.0.0.1", 1080));
        }

        /// <summary>
        /// 获取下载视频信息 (这里不加权限可以直接用)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetUrlInfo(string url)
        {
            if (!url.Contains("https") || !url.Contains("http") )
            {
                return new ResultModel<Employee> { State = ResultType.Error, Message = "url格式错误" };
            }
            info= await downloads.AutomationGo(Client,url);
            return new ResultModel<VideoInfo> { State = ResultType.Success, Message = "查询成功",Data = info};
        }

        /// <summary>
        /// 建立本地文件  前端要请求两次
        /// </summary>
        [HttpPost]
        public Task<object> CreatFile([FromQuery]string Title, [FromQuery] long Filesize)
        {
            if (!string.IsNullOrEmpty(Title) && Filesize > 0)
            {

            }
        }
    }
}
