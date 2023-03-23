using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ESMP.STOCK.API.DTO
{
    [Serializable()]

    [XmlRoot("Symbols")]
    public class QuoteBean
    {
        [XmlElement("Symbol")]
        public List<Symbol> SymbolList { get; set; }

    }

    public class Symbol
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }             //股票代號
        [XmlAttribute("dealprice")]
        public string? DealPrice { get; set; }      //現價
        [XmlAttribute("shortname")]
        public string? ShortName { get; set; }      //股票中文名稱
        [XmlAttribute("refprice")]
        public string? RefPrice { get; set; }       //參考價
        [XmlAttribute("moddate")]
        public string? ModDate { get; set; }        //更新日期
        [XmlAttribute("modtime")]
        public string ModTime { get; set; }         //更新時間
    }
}
