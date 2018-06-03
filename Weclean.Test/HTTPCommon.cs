using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weclean.Test
{
    public class HTTPCommon
    {
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="restClient">RestClient</param>
        /// <param name="dictionaryParamObj">参数</param>
        /// <param name="methodName">方法名</param>
        /// <returns>返回结果</returns>
        public static string HttpPost(RestClient restClient, Dictionary<string, object> dictionaryParamObj, string methodName)
        {
            try
            {
                var request = new RestRequest(methodName, Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(dictionaryParamObj);
                var _Response = restClient.Execute(request);
                return _Response.Content;
            }
            catch (WebException ex)
            {
                return "无法连接到服务器\r\n错误信息：" + ex.Message;
            }
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="restClient">RestClient</param>
        /// <param name="methodName">方法名</param>
        /// <param name="listParam">参数</param>
        /// <returns>返回结果</returns>
        public static string HttpGet(RestClient restClient, string methodName, List<string> listParam)
        {
            try
            {
                string paramObj = "/";
                listParam.ForEach((x) => { paramObj += "{" + x + "}/"; });
                var request = new RestRequest(methodName + paramObj.Substring(0, paramObj.Length - 1), Method.GET);
                var response = restClient.Execute(request);
                return response.Content;
            }
            catch (WebException ex)
            {
                return "无法连接到服务器\r\n错误信息：" + ex.Message;
            }
        }

    }
}
