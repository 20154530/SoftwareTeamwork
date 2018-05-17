using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTeamwork
{
    class WebLoginInfSet //网站信息串
    {
        public bool NeedLogin = false;
        public bool Compressed = false;
        public bool Verify = false;
        public bool HasCookie = false;
        public String name;
        public String CharSet;
        public String VerifyCode;
        private List<String> uris;
        public List<String> Uris {
            get { return uris; }
            set { uris = value; }
        }
        private List<KeyValuePair<String, String>> keyValuePairs;
        public List<KeyValuePair<String, String>> KeyValuePairs {
            get { return keyValuePairs; }
            set { keyValuePairs = value; }
        }
        private List<KeyValuePair<string, string>> cookies;
        public List<KeyValuePair<string, string>> Cookies {
            get => cookies;
            set {
                cookies = value;
            }
        }
        private KeyValuePair<String, String> idcodes;
        public KeyValuePair<String, String> IdCodes {
            get { return idcodes; }
            set { idcodes = value; }
        }

        public WebLoginInfSet() {
            uris = new List<string>();
            KeyValuePairs = new List<KeyValuePair<string, string>>();
            Cookies = new List<KeyValuePair<string, string>>();
            idcodes = new KeyValuePair<string, string>(null, null);
        }

        public void CheckUris() {
            if (uris.Count == 1) {
                uris.Add(uris[0]);
                uris.Add(uris[0]);
            }
        }

        public void Printf() {
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
}
