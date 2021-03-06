﻿using System;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace SoftwareTeamwork {

    class LoginAgent {
        private WebLoginInfSet infSet;
        public WebLoginInfSet InfSet {
            get => infSet;
            set { value.CheckUris(); infSet = value; }
        }
        private String result = null;
        private String ID = "";
        private static HttpClient httpClient;
        private static HttpClientHandler clientHandler;
        private HttpResponseMessage response;
        private List<KeyValuePair<String, String>> paramList;

        public static LoginAgent Instence = new LoginAgent();

        static LoginAgent() {
            clientHandler = new HttpClientHandler { UseCookies = true };
            httpClient = new HttpClient(clientHandler);
            httpClient.MaxResponseContentBufferSize = 25600;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            httpClient.Timeout = TimeSpan.FromDays(1);
        }

        public int SetInfset(WebLoginInfSet infset) {
            InfSet = infset;
            try { GetOrignResult(); }
            catch (Exception e) {
                MessageService.Instence.ShowError(null, "请检查网络连接");
                return -1;
            }
            if (infset.NeedLogin)
                paramList = infset.KeyValuePairs;
            if (infset.IdCodes.Key != null)
                GetLoginID();
            return 0;
        }

        private void GetLoginID() {
            while (result == null) ;//等待请求数据
            ID = Regex.Matches(result, InfSet.IdCodes.Value)[0].Groups[1].Value;
            
        }

        private void ParamListCheck()//请求键值对检查
        {
            for (int i = 0; i < paramList.Count; i++) {
                if (paramList[i].Value == "" ){//若为空进行补全
                    MessageService.Instence.ShowError(null, "请完善登陆信息");
                    break;
                }
            }
            if (InfSet.Cookies.Count != 0) {
                foreach (KeyValuePair<string, string> kv in InfSet.Cookies) {
                    Cookie cookie = new Cookie(kv.Key, kv.Value) {
                        Expires = DateTime.MaxValue
                    };
                    clientHandler.CookieContainer.Add(new Uri(InfSet.Uris[0]), cookie);
                }
                InfSet.HasCookie = true;
            }
        }

        private void GetOrignResult()//Http头填写
        {
            result = httpClient.GetStringAsync(InfSet.Uris[0]).Result;
        }

        public BitmapImage GetVerify() {
            var url = InfSet.Uris[0] + new Regex(InfSet.VerifyCode).Match(result).Groups[1].Value;
            response = httpClient.GetAsync(new Uri(url)).Result;
            BitmapImage bmp = null;
            try {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(response.Content.ReadAsByteArrayAsync().Result);
                bmp.EndInit();
            } catch {
                bmp = null;
            }
            return bmp;
        }

        private void RefrashinfSet(string name) {
            InfSet = XmlHelper.GetInfWithName(name is null ? infSet.name : name);
            if (InfSet.NeedLogin) {
                paramList = InfSet.KeyValuePairs;
                ParamListCheck();
            }
        }

        public void Post()//发送请求
        {
            RefrashinfSet(null);
            if (InfSet.NeedLogin)
                try {
                    response = httpClient.PostAsync(InfSet.Uris[0] + ID, new FormUrlEncodedContent(paramList)).Result;
                    if (!infSet.HasCookie && InfSet.Verify)
                        SetCookies(InfSet.Uris[0]);
                }
                catch (AggregateException) {
                    MessageService.Instence.ShowError(null,"请检查网络连接");
                }
        }

        public void Post(string name) {
            RefrashinfSet(name);
            if (InfSet.NeedLogin)
                try {
                    response = httpClient.PostAsync(InfSet.Uris[0] + ID , new FormUrlEncodedContent(paramList)).Result;
                    if (!infSet.HasCookie && InfSet.Verify)
                        SetCookies(InfSet.Uris[0]);
                }
                catch (AggregateException) {
                    MessageService.Instence.ShowError(null, "请检查网络连接");
                }
        }

        public void Post(string name,Dictionary<string,string> dic) {
            RefrashinfSet(name);
            for (int kvp = 0; kvp < paramList.Count; kvp++) {
                if (dic.ContainsKey(paramList[kvp].Key)) {
                    paramList[kvp] = new KeyValuePair<string, string>(paramList[kvp].Key, dic[paramList[kvp].Key]);
                }
            }
            if (InfSet.NeedLogin)
                try {
                    response = httpClient.PostAsync(InfSet.Uris[0] + ID, new FormUrlEncodedContent(paramList)).Result;
                    if (!infSet.HasCookie && InfSet.Verify)
                        SetCookies(InfSet.Uris[0]);
                }
                catch (AggregateException) {
                    MessageService.Instence.ShowError(null, "请检查网络连接");
                }
        }

        public String GetData(string name)//请求字符数据
        {
            if (!(name is null))
                RefrashinfSet(name);
            if (InfSet.Compressed)
                return GetDatasetByString(GetDataAsStream(null));
            else
                try {
                    return httpClient.GetStringAsync(InfSet.Uris[1]).Result;
                }
                catch (AggregateException) {
                    MessageService.Instence.ShowError(null, "请检查网络连接");
                    return null;
                }
        }

        public Stream GetDataAsStream(string name) {
            if (!(name is null))
                RefrashinfSet(name);
            Stream temp;
            try {
                temp = httpClient.GetStreamAsync(InfSet.Uris[1]).Result;
            }
            catch (AggregateException) {
                MessageService.Instence.ShowError(null, "请检查网络连接");
                return null;
            }
            return temp;
        }

        #region 转成图片

        #endregion

        #region 获取cookies
        private void SetCookies(string uri) {
            try {
                var responseCookies = clientHandler.CookieContainer.GetCookies(new Uri(uri)).Cast<Cookie>().ToArray();
                string[][] pairs = new string[clientHandler.CookieContainer.Count][];
                for (int i = 0; i < clientHandler.CookieContainer.Count; i++) {
                    pairs[i] = FormatCookie(responseCookies[i]);
                }
                XmlHelper.CreatWebNode(infSet.name, pairs);
            }
            catch {  }
        }

        private string[] FormatCookie(Cookie c) {
           return new string[] {
                String.Format("type:cookie"),
                String.Format("name:{0}",c.Name),
                String.Format("value:{0}",c.Value)
            };
        }
        #endregion

        #region 网页解压 
        private String GetDatasetByString(Stream Value) {
            string strHTML = "";
            GZipStream gzip = new GZipStream(Value, CompressionMode.Decompress);//解压缩
            using (StreamReader reader = new StreamReader(gzip, Encoding.GetEncoding(InfSet.CharSet)))//中文编码处理
            {
                strHTML = reader.ReadToEnd();
            }
            return strHTML;
        }
        #endregion

    }
}