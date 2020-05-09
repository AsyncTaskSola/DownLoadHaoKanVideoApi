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
        /// 建立本地文件  前端要请求两次（点击下载时候，=》和下载）
        /// </summary>
        [HttpGet]
        public ActionResult<object> GetCreatFile([FromQuery]string Title, [FromQuery] long Filesize)
        {
            if (!string.IsNullOrEmpty(Title) && Filesize > 0)
            {
                var resultdata= downloads.CreateFile(Title, Filesize);
                return new ResultModel<VideoInfo> {State = ResultType.Success, Message = $"创建目标文件成功+{resultdata}"};
            }
            return new ResultModel<VideoInfo> { State = ResultType.Error, Message = "创建目标文件失败" };
        }
        /// <summary>
        /// 请求下载
        /// </summary>
        /// <param name="Info"></param>    前端需要在请求GetUrlInfo接口时保存返回的下载信息
        /// <param name="Filesize"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> DownloadStart([FromForm] VideoInfo Info, long Filesize)
        {
           //var click= Info ?? throw new ApplicationException("请输入相关的视频查询信息内容");
           if (Info == null || Filesize <= 0)
           {
               return new ResultModel<VideoInfo> { State = ResultType.Error, Message = "请输入相关的视频查询信息内容" };
           }

        }
    }
}
