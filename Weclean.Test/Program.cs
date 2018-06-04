using System.Collections.Generic;
using System.IO;
using RestSharp;
using Newtonsoft.Json;

namespace Weclean.Test
{
    static class Program
    {

        #region private method 方法

        /// <summary>
        /// file to stream
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>stream</returns>
        public static Stream thisFileToStream(this string fileName)
        {
            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                return new MemoryStream(bytes);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        /// <summary>
        /// stream to byte
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns>byte</returns>
        public static byte[] thisStreamToBytes(this Stream stream)
        {
            try
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// WCF 地址
        /// </summary>
        public static RestClient restClientWeClean = new RestClient("http://localhost:65519/WeClean/WeCleanBackstageService.svc");

        static void Main(string[] args)
        {
            //----------- 员工信息 -----------
            var model = JsonConvert.SerializeObject(new
            {
                CompanyName = "Weclean",
                StreamFile = ((@"D:\weclean.xlsx").thisFileToStream()).thisStreamToBytes(),
                OperatorName = "senlin.huang"
            });
        }


    }
}
