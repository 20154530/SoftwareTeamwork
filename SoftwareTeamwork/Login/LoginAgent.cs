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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace SoftwareTeamwork {

    class LoginAgent {
        private String dir = @"D:\DB\";
        private WebLoginInfSet InfSet;
        private String result = null;
        private String ID = "";
        private HttpClient httpClient;
        private HttpResponseMessage response;
        private List<KeyValuePair<String, String>> paramList;
        private CookieContainer Cookies;

        public static LoginAgent Instence = new LoginAgent();

        public LoginAgent() {
            
        }

        public int SetInfset(WebLoginInfSet infset) {
            infset.CheckUris();
            InfSet = infset;
            try {
                Login_Headers_config();
            }
            catch (Exception e) {
                ErrorMessageService.Instence.ShowError(App.Current.MainWindow, "请检查网络连接");
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
            int i = paramList.Count - 1;
            for (; i >= 0; i--) {
                if (paramList[i].Value == "" | paramList[i].Value == null){//若为空进行补全{
                    ErrorMessageService.Instence.ShowError(App.Current.MainWindow, "请完善登陆信息");
                    break;
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

        public BitmapImage GetVerify() {
            var url = InfSet.Uris[0] + InfSet.VerifyCode + new Regex(InfSet.VerifyCode + "(.*?)").Match(result).Groups[1].Value;
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

        public void SetVerify(string imgCode) {
            paramList.Add(new KeyValuePair<string, string>(paramList[paramList.Count - 1].Key, imgCode));
            paramList.RemoveAt(paramList.Count - 2);
        }

        public void Post()//发送请求
        {
            if (InfSet.NeedLogin)
                try {
                    ParamListCheck();
                    response = httpClient.PostAsync(InfSet.Uris[0] + ID, new FormUrlEncodedContent(paramList)).Result;
                }
                catch (AggregateException) {
                    ErrorMessageService.Instence.ShowError(App.Current.MainWindow,"请检查网络连接");
                }
        }

        public String GetData()//请求字符数据
        {
            if (InfSet.Compressed)
                return GetDatasetByString(GetDataAsStream());
            else
                try {
                    return httpClient.GetStringAsync(InfSet.Uris[1]).Result;
                }
                catch (AggregateException) {
                    ErrorMessageService.Instence.ShowError(App.Current.MainWindow, "请检查网络连接");
                    return null;
                }
        }

        public Stream GetDataAsStream() {
            Stream temp;
            try {
                ParamListCheck();
                temp = httpClient.GetStreamAsync(InfSet.Uris[1]).Result;
            }
            catch (AggregateException) {
                ErrorMessageService.Instence.ShowError(App.Current.MainWindow, "请检查网络连接");
                return null;
            }
            return temp;
        }

        public void SaveHtml() {
            Write("MyHtml.html", GetDatasetByString(GetDataAsStream()));
        }

        private void GetVerifyCode() {

        }

        #region 转成图片

        #endregion

        #region 获取cookies
        public static List<Cookie> GetAllCookies(CookieContainer cc) {


            return null;
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

        #region 写入本地
        private void Write(string fileName, string html) {
            try {
                FileStream fs = new FileStream(dir + fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(html);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Write(string fileName, byte[] html) {
            try {
                File.WriteAllBytes(dir + fileName, html);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion
    }
}
