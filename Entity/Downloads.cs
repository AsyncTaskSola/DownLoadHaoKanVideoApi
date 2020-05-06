﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Entity.Video;

namespace DownLoadHaoKanVideoAPI.Entity
{
    public class Downloads
    {
        private int _threadCound;
        private int _byteArraySize;
        public string _basePash = "Videos/";
        public HttpClient Client { get; set; }
        /// <summary>
        /// 当前的视频路径
        /// </summary>
        public string _currentVideoPath { get; set; }
        /// <summary>
        /// 已下载
        /// </summary>
        public long _download { get; set; }

        /// <summary>
        /// Ctor 下载客户端配置
        /// </summary>
        /// <param name="threadCount">线程数</param>
        /// <param name="byteArraySize">缓存的字节 1G？</param>
        /// <param name="webProxy">代理</param>
        /// <param name="basePath">基础下载路径</param>
        //public Downloads()
        //{
        //}

        public HttpClient RunClient(int threadCount = 8, int byteArraySize = 1048576, WebProxy webProxy = null,
            string basePath = null)
        {
            _threadCound = threadCount;
            _byteArraySize = byteArraySize;
            if (!string.IsNullOrEmpty(basePath))
            {
                _basePash = basePath;
            }

            var count = byteArraySize / 1024 / 1024; //1mb
            Console.WriteLine($"当前的线程数为{threadCount}，每秒缓存{count}MB");
            var httpclientheard = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.All,
                Proxy = webProxy
            };
            httpclientheard.UseProxy = httpclientheard.Proxy != null;
            var client = new HttpClient(httpclientheard);
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,zh-CN;q=0.8,zh;q=0.7");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
            Client = client;
            return client;
        }
        public async Task<VideoInfo> AutomationGo(string url)
        {
            var source = await GetVideoInfo(url);
            return source;
            //GetFile(source);
        }

        public async Task<VideoInfo> GetVideoInfo(string url)
        {
            var response = await Client.GetAsync(url);
            return null;
        }

    }
}