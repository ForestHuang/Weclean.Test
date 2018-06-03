using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace Weclean.Test
{
    class Program
    {

        #region Private 方法

        public static Stream FileToStream(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        #endregion

        public static RestClient restClient = new RestClient("http://localhost:65519/WeClean/WeCleanBackstageService.svc");

        static void Main(string[] args)
        {

            HTTPCommon.HttpGet(restClient, "WeCleanLogin", new List<string>() { "2", "a" });
            string excelPath = @"D:\weclean.xlsx";
            Stream stream = FileToStream(excelPath);
            var model = JsonConvert.SerializeObject(new
            {
                CompanyName = "Weclean",
                StreamFile = StreamToBytes(stream),
                OperatorName = "senlin.huang"
            });
        }


    }
}
