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
        public string? Id { get; set; }
        [XmlAttribute("dealprice")]
        public string? DealPrice { get; set; }
        [XmlAttribute("shortname")]
        public string? ShortName { get; set; }
        [XmlAttribute("refprice")]
        public string? RefPrice { get; set; }
        [XmlAttribute("moddate")]
        public string? ModDate { get; set; }
        [XmlAttribute("modtime")]
        public string ModTime { get; set; }
    }
}
