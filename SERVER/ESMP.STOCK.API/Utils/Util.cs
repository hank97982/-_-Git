using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;

namespace ESMP.STOCK.API.Utils
{
    public static class Util
    {
        private static Object lockObj = new Object();
        public static string Serialize(object o)
        {

            //加上Namespaces
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");


            XmlSerializer ser = new XmlSerializer(o.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, o, ns);

            //格式化
            XDocument doc = XDocument.Parse(sb.ToString());

            return doc.ToString();
        }

        public static T Deserialize<T>(string s)
        {
            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);

                return (T)obj;
            }
            catch (Exception ex)
            {
                //throw ex;
                return default(T);
            }
        }

        //public static bool TryParseJson<T>(this string @this)
        //{
        //    bool success = true;
        //    var settings = new JsonSerializerSettings
        //    {
        //        Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
        //        MissingMemberHandling = MissingMemberHandling.Error
        //    };
        //    //序列化結果可以透過out指向記憶體位置丟出
        //    JsonConvert.DeserializeObject<T>(@this, settings);

        //    return success;
        //}
        public static bool TryParseJson(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                var obj = JToken.Parse(value);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
        public static bool TryParseXml(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                var obj = XDocument.Parse(value);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }


        private static void ShowThreadInformation(String taskName)
        {
            String msg = null;
            Thread thread = Thread.CurrentThread;
            lock (lockObj)
            {
                msg = String.Format("{0} thread information\n", taskName) +
                      String.Format("   Background: {0}\n", thread.IsBackground) +
                      String.Format("   Thread Pool: {0}\n", thread.IsThreadPoolThread) +
                      String.Format("   Thread ID: {0}\n", thread.ManagedThreadId);
            }
            Console.WriteLine(msg);
        }

        public static void Log(string str)
        {
            //using (StreamWriter stream = new StreamWriter(@"E:\Systex_edu\test10Unit3\WinForm\Service\ServiceLog\Log.txt"))
            //{
            //    stream.WriteLine(DateTime.Now + " : " + str);
            //}

            StringBuilder sb = new StringBuilder();

            sb.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " : " + str);
            sb.Append("\r\n");
            File.AppendAllText(@"Log.txt", sb.ToString());
            sb.Clear();
        }

    }
}
