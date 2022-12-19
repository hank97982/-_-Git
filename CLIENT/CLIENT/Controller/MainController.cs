using WinFormsAppClient.Model;
using WinFormsAppClient.Bean;

using System.Diagnostics;
using WinFormsAppClient.Utils;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Data;
using CLIENT;
using System.Text.Json;
using System.Reflection;

namespace WinFormsAppClient.Controller
{
    public class MainController
    {
        private MainForm _form;
        private MainModel _model;
        public MainController(MainForm mainForm)
        {
            _form = mainForm;
            _model = new MainModel(this);
        }

        public void AppendToListBox(string stringData)
        {
            _form.AppendToListBox(stringData);
        }

        public void MessageAlert(string messageStr)
        {
            _form.MessageAlert(messageStr);
        }

        public void AskForSearch(int selectItem, object requestBean)
        {
            //如何遍歷物件內的成員並賦予值?
            //T requestBean = new ();
            //UnrealizedGainsAndlLossesDTO requestBean = new UnrealizedGainsAndlLossesDTO();

            //這行看一下
            //foreach (var nw in requestBean.GetType().GetProperties().Zip(InputString, Tuple.Create))
            //{
            //    requestBean.GetType().GetProperty(nw.Item1.Name).SetValue(requestBean, nw.Item2);
            //}

            //var numbersAndWords = requestBean.GetType().GetProperties().Zip(InputString, (n, w) => new { Number = n, Word = w });
            //foreach (var nw in numbersAndWords)
            //{
            //    requestBean.GetType().GetProperty(nw.Number.Name).SetValue(requestBean, nw.Word);
            //}

            if (ConnectTypeItems.Json.GetHashCode() == selectItem)
            {
                //string sendJsonStr = JsonConvert.ObjectInJsonOut(requestBean);
                string sendJsonStr = JsonSerializer.Serialize(requestBean);
                //Debug.WriteLine("is sending now ");
                _model.RequestSender(sendJsonStr);

                return;
            }
            if (ConnectTypeItems.Xml.GetHashCode() == selectItem)
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer xmlSerializer = new XmlSerializer(requestBean.GetType());
                //StringBuilder,StringReader,StringWriter的差異?
                StringWriter sw = new StringWriter(new StringBuilder());
                xmlSerializer.Serialize(sw, requestBean, ns);
                _model.RequestSender(sw.ToString());

                return;
            }
            _form.MessageAlert("超出項目的發送格式!!");
            return;

        }
    }
}
