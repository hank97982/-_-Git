using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ESMP.STOCK.API.DTO.Statement
{
    [Serializable()]
    [XmlRoot("profit_sum")]
    public class AccsumErr
    {
        [JsonPropertyOrder(1)]
        [XmlElement("errcode")]
        [JsonPropertyName("errcode")]
        public string? Errcode { get; set; }             //錯誤代碼
        [JsonPropertyOrder(2)]
        [XmlElement("errmsg")]
        [JsonPropertyName("errmsg")]
        public string? Errmsg { get; set; }              //錯誤訊息
    }
    [XmlRoot("profit_sum")]
    public class Accsum: AccsumErr
    {
        [JsonPropertyOrder(3)]
        [XmlElement("netamt")]
        [JsonPropertyName("netamt")]
        public decimal Netamt { get; set; }         //淨收付金額
        [JsonPropertyOrder(4)]
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }            //手續費
        [JsonPropertyOrder(5)]
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }            //交易稅
        [JsonPropertyOrder(6)]
        [XmlElement("mqty")]
        [JsonPropertyName("mqty")]
        public decimal Mqty { get; set; }           //股數
        [JsonPropertyOrder(7)]
        [XmlElement("mamt")]
        [JsonPropertyName("mamt")]
        public decimal Mamt { get; set; }           //成交價金
        [JsonPropertyOrder(8)]
        [XmlElement("billSum")]
        [JsonPropertyName("billSum")]
        public BillSum? Sum { get; set; }            //對帳單匯總
        [JsonPropertyOrder(9)]
        [XmlElement("profile")]
        [JsonPropertyName("profile")]
        public List<Profile>? Profile { get; set; }  //對帳單 - 明細
    }
    [XmlRoot("billSum")]
    public class BillSum
    {
        [XmlElement("cnbamt")]
        [JsonPropertyName("cnbamt")]
        public decimal Cnbamt { get; set; }              //現買價金
        [XmlElement("cnsamt")]
        [JsonPropertyName("cnsamt")]
        public decimal Cnsamt { get; set; }              //現賣價金
        [XmlElement("cnfee")]
        [JsonPropertyName("cnfee")]
        public decimal Cnfee { get; set; }               //現股手續費
        [XmlElement("cntax")]
        [JsonPropertyName("cntax")]
        public decimal Cntax { get; set; }               //現股交易稅
        [XmlElement("cnnetamt")]
        [JsonPropertyName("cnnetamt")]
        public decimal Cnnetamt { get; set; }            //現股淨收付
        [XmlElement("bqty")]
        [JsonPropertyName("bqty")]
        public decimal Bqty { get; set; }                //買入股數
        [XmlElement("sqty")]
        [JsonPropertyName("sqty")]
        public decimal Sqty { get; set; }                //賣出股數
    }
    [XmlRoot("profile")]
    public class Profile {
        [XmlElement("bhno")]
        [JsonPropertyName("bhno")]
        public string? Bhno { get; set; }                   //分公司
        [XmlElement("cseq")]
        [JsonPropertyName("cseq")]
        public string? Cseq { get; set; }                   //帳號
        [XmlElement("name")]
        [JsonPropertyName("name")]
        public string? Name { get; set; }                   //姓名
        [XmlElement("stock")]
        [JsonPropertyName("stock")]
        public string? Stock { get; set; }                  //股票代碼
        [XmlElement("stocknm")]
        [JsonPropertyName("stocknm")]
        public string? Stocknm { get; set; }                //股票名稱
        [XmlElement("mdate")]
        [JsonPropertyName("mdate")]
        public string? Mdate { get; set; }                  //交易日期
        [XmlElement("dseq")]
        [JsonPropertyName("dseq")]
        public string? Dseq { get; set; }                   //委託書號
        [XmlElement("dno")]
        [JsonPropertyName("dno")]
        public string? Dno { get; set; }                    //分單號
        [XmlElement("ttype")]
        [JsonPropertyName("ttype")]
        public string? Ttype { get; set; }                  //交易別 0:現股 
        [XmlElement("ttypename")]
        [JsonPropertyName("ttypename")]
        public string? Ttypename { get; set; }              //交易類別名稱 現買/現賣/盤中零賣/盤後零賣
        [XmlElement("bstype")]
        [JsonPropertyName("bstype")]
        public string? Bstype { get; set; }                 //買賣別(B/S)
        [XmlElement("bstypename")]
        [JsonPropertyName("bstypename")]
        public string? Bstypename { get; set; }             //買賣別名稱
        [XmlElement("etype")]
        [JsonPropertyName("etype")]
        public string? Etype { get; set; }                  //盤別
        [XmlElement("mprice")]
        [JsonPropertyName("mprice")]
        public decimal Mprice { get; set; }                //成交價
        [XmlElement("mqty")]
        [JsonPropertyName("mqty")]
        public decimal Mqty { get; set; }                  //合計成交股數
        [XmlElement("mamt")]
        [JsonPropertyName("mamt")]
        public decimal Mamt { get; set; }                  //價金
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }                   //手續費
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                   //交易稅
        [XmlElement("netamt")]
        [JsonPropertyName("netamt")]
        public decimal Netamt { get; set; }                //淨收付
    }
}
