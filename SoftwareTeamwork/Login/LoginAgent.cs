using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.IO.Compression;
using System.Collections;

namespace ConsoleApp1
{
    class LoginAgent
    {
        private const String dir = @"D:\DB\";
        private WebLoginInfSet InfSet;
        private String result = null;
        private String ID = "";
        private HttpClient httpClient;
        private HttpResponseMessage response;
        private List<KeyValuePair<String, String>> paramList;
        private CookieContainer Cookies;

        public LoginAgent()
        {
            httpClient = new HttpClient();
        }

        public LoginAgent(WebLoginInfSet infset)
        {
            infset.CheckUris();
            InfSet = infset;
            Login_Headers_config();
            if (infset.NeedLogin)
                paramList = infset.KeyValuePairs;
            if (infset.IdCodes.Key != null)
                GetLoginID();
        }

        private void GetLoginID()
        {
            while (result == null) ;
            String s = @"action=""(\S*)""";
            Console.WriteLine(InfSet.IdCodes.Value + "    " + s + " " + s.Equals(InfSet.IdCodes.Value));
            ID = Regex.Matches(result, InfSet.IdCodes.Value)[0].Groups[1].Value;
        }

        private void paramListCheck()//请求键值对检查
        {
            if (InfSet.Verify)
                GetVerify();
            int i = paramList.Count - 1;
            for (; i >= 0; i--)
            {
                if (paramList[i].Value == "" | paramList[i].Value == null)//若为空进行补全
                {
                    Console.WriteLine("Please Enter " + paramList[i].Key);
                    paramList.Add(new KeyValuePair<string, string>(paramList[i].Key, Console.ReadLine()));
                    paramList.RemoveAt(i);
                }
            }
        }

        private void Login_Headers_config()//Http头填写
        {
            Cookies = new CookieContainer();
            httpClient = new HttpClient(new HttpClientHandler() { UseCookies = true, CookieContainer = Cookies });
            httpClient.MaxResponseContentBufferSize = 25600;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

            result = httpClient.GetStringAsync(InfSet.Uris[0]).Result;
        }

        private void GetVerify()
        {
            var url = InfSet.Uris[0] + InfSet.VerifyCode + new Regex(InfSet.VerifyCode + "(.*?)").Match(result).Groups[1].Value;

            response = httpClient.GetAsync(new Uri(url)).Result;
            Write("amosli.png", response.Content.ReadAsByteArrayAsync().Result);

            Console.WriteLine("输入图片验证码：");
            String imgCode = "";//验证码写到本地了，需要手动填写
            imgCode = Console.ReadLine();

            paramList.Add(new KeyValuePair<string, string>(paramList[paramList.Count - 1].Key, imgCode));
            paramList.RemoveAt(paramList.Count - 2);
        }

        public void Post()//发送请求
        {
            if (InfSet.NeedLogin)
                try
                {
                    paramListCheck();
                    foreach (KeyValuePair<string, string> i in paramList)
                        Console.WriteLine(String.Format("{0} -- {1}", i.Key, i.Value));
                    response = httpClient.PostAsync(InfSet.Uris[0] + ID, new FormUrlEncodedContent(paramList)).Result;
                }
                catch (AggregateException)
                {
                    Console.WriteLine("Post Message Error : Please check your web connection !");
                }
        }

        
        public String GetData()//请求字符数据
        {
            if (InfSet.Compressed)
                return GetDatasetByString(GetDataAsStream());
            else
                try
                {
                    return httpClient.GetStringAsync(InfSet.Uris[1]).Result;
                }
                catch (AggregateException)
                {
                    Console.WriteLine("Request Message Error : Please check your web connection !");
                    return null;
                }
        }

        public Stream GetDataAsStream()
        {
            Stream temp;
            try
            {
                paramListCheck();
                temp = httpClient.GetStreamAsync(InfSet.Uris[1]).Result;
            }
            catch (AggregateException)
            {
                Console.WriteLine("Request Message Error : Please check your web connection !");
                return null;
            }
            return temp;
        }

        public void SaveHtml()
        {
            Write("MyHtml.html", GetDatasetByString(GetDataAsStream()));
        }

        private void GetVerifyCode()
        {

        }

        #region 获取cookies
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {

            
            return null;
        }
        #endregion

        #region 网页解压 
        private String GetDatasetByString(Stream Value)
        {
            string strHTML = "";

            GZipStream gzip = new GZipStream(Value, CompressionMode.Decompress);//解压缩
            using (StreamReader reader = new StreamReader(gzip, Encoding.GetEncoding(InfSet.CharSet)))//中文编码处理
            {
                strHTML = reader.ReadToEnd();
            }

            return strHTML;
        }
        #endregion

        #region 写入本地
        private void Write(string fileName, string html)
        {
            try
            {
                FileStream fs = new FileStream(dir + fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(html);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Write(string fileName, byte[] html)
        {
            try
            {
                File.WriteAllBytes(dir + fileName, html);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion
    }
}
