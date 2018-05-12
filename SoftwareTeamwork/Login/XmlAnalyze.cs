using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;

namespace SoftwareTeamwork {
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
