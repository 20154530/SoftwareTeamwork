using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;

namespace ConsoleApp1
{
    class WebLoginInfSet //网站信息串
    {
        public bool NeedLogin = false;
        public bool Compressed = false;
        public bool Verify = false;
        public String CharSet;
        public String VerifyCode;
        private List<String> uris;
        public List<String> Uris
        {
            get { return uris; }
            set { uris = value; }
        }
        private List<KeyValuePair<String, String>> keyValuePairs;
        public List<KeyValuePair<String, String>> KeyValuePairs
        {
            get { return keyValuePairs; }
            set { keyValuePairs = value; }
        }
        private KeyValuePair<String, String> idcodes;
        public KeyValuePair<String, String> IdCodes
        {
            get { return idcodes; }
            set { idcodes = value; }
        }

        public WebLoginInfSet()
        {
            uris = new List<string>();
            KeyValuePairs = new List<KeyValuePair<string, string>>();
            idcodes = new KeyValuePair<string, string>(null,null);
        }

        public void CheckUris()
        {
            if (uris.Count == 1)
                uris.Add(uris[0]);
        }

        public void Printf()
        {
            foreach (string i in Uris)
                Console.WriteLine(String.Format("< {0} >", i));
            Console.WriteLine(String.Format("< CharSet = {0} : Compressed = {1} : NeedLogin = {2} : NeedVerifyCode = {3}>", CharSet, Compressed, NeedLogin, Verify));
            if (NeedLogin)
                foreach (KeyValuePair<String, String> i in KeyValuePairs)
                    Console.WriteLine(String.Format("< {0} : {1} >", i.Key.PadRight(20), i.Value.PadRight(20)));
            if (idcodes.Key != null)
                Console.WriteLine(String.Format("< {0} : {1} >", idcodes.Key.PadRight(20), idcodes.Value.PadRight(20)));
        }
    }

    class XmlAnalyze
    {
        private XmlDocument xmlDocument = null;

        public XmlAnalyze()
        {

        }

        public WebLoginInfSet GetInfWithName(String name)
        {
            StreamReader reader = new StreamReader("/Web-config.xml", System.Text.Encoding.UTF8);
            xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Configs/Web");
            XmlNodeList cxnl = null;

            WebLoginInfSet loginInfSet = new WebLoginInfSet();

            foreach (XmlNode i in xnl)
                if (i.Attributes["name"].Value.Equals(name))
                {
                    cxnl = i.ChildNodes;
                    loginInfSet.NeedLogin = i.Attributes["login"].Value.Equals("true");
                    loginInfSet.Compressed = i.Attributes["gzip"].Value.Equals("true");
                    loginInfSet.Verify = i.Attributes["needverifycode"].Value.Equals("true");
                    loginInfSet.CharSet = i.Attributes["charset"].Value;
                    break;
                }

            if (cxnl != null)
            {
                foreach (XmlNode i in cxnl)
                {
                    if (i.Attributes["type"].Value.Equals("uri"))
                        loginInfSet.Uris.Add(i.Attributes["uri"].Value);
                    else if (i.Attributes["type"].Value.Equals("action"))
                        loginInfSet.KeyValuePairs.Add(new KeyValuePair<string, string>(i.Attributes["name"].Value, i.Attributes["value"].Value));
                    else if (i.Attributes["type"].Value.Equals("verify"))
                        loginInfSet.VerifyCode = (i.Attributes["value"].Value);
                    else if (i.Attributes["type"].Value.Equals("idcode"))
                        loginInfSet.IdCodes = new KeyValuePair<string, string>(i.Attributes["name"].Value, i.Attributes["value"].Value);
                }
                return loginInfSet;
            }
            else return null;
        }
    }
}
