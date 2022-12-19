using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ESMP.STOCK.API.DTO.RealizedProfitAndLoss
{
    [Serializable()]
    [XmlRoot("root")]
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
    [XmlRoot("root")]
    public class Accsum : AccsumErr
    {
        [JsonPropertyOrder(3)]
        [XmlElement("cqty")]
        [JsonPropertyName("cqty")]
        public decimal Cqty { get; set; }               //沖銷股數
        [JsonPropertyOrder(4)]
        [XmlElement("cost")]
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }               //付出成本
        [JsonPropertyOrder(5)]
        [XmlElement("income")]
        [JsonPropertyName("income")]
        public decimal Income { get; set; }             //賣出收入
        [JsonPropertyOrder(6)]
        [XmlElement("profit")]
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }             //損益
        [JsonPropertyOrder(7)]
        [XmlElement("pl_ratio")]
        [JsonPropertyName("pl_ratio")]
        public string? PlRatio { get; set; }             //獲利率
        [JsonPropertyOrder(8)]
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }                //手續費
        [JsonPropertyOrder(9)]
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                //交易稅
        [JsonPropertyOrder(10)]
        [XmlArray("profit_sum")]
        [XmlArrayItem("profit_sum", typeof(Sum))]
        [JsonPropertyName("profit_sum")]
        public List<Sum>? ProfitSum { get; set; }        //已實現損益-個股彙總資料
    }
    [XmlRoot("profit_sum")]
    public class Sum
    {
        [XmlElement("bhno")]
        [JsonPropertyName("bhno")]
        public string? Bhno { get; set; }                //分公司
        [XmlElement("cseq")]
        [JsonPropertyName("cseq")]
        public string? Cseq { get; set; }                //帳號
        [XmlElement("tdate")]
        [JsonPropertyName("tdate")]
        public string? Tdate { get; set; }               //交易日期
        [XmlElement("dseq")]
        [JsonPropertyName("dseq")]
        public string? Dseq { get; set; }                //委託書號
        [XmlElement("dno")]
        [JsonPropertyName("dno")]
        public string? Dno { get; set; }                 //分單號
        [XmlElement("ttype")]
        [JsonPropertyName("ttype")]
        public string? Ttype { get; set; }               //交易別 0:現股 1,3:融資 2,4:融券
        [XmlElement("ttypename")]
        [JsonPropertyName("ttypename")]
        public string? Ttypename { get; set; }           //交易類別名稱 現股/融資/融券
        [XmlElement("bstype")]
        [JsonPropertyName("bstype")]
        public string? Bstype { get; set; }              //買賣別(B/S)
        [XmlElement("stock")]
        [JsonPropertyName("stock")]
        public string? Stock { get; set; }               //股票代碼
        [XmlElement("stocknm")]
        [JsonPropertyName("stocknm")]
        public string? Stocknm { get; set; }             //股票名稱
        [XmlElement("cqty")]
        [JsonPropertyName("cqty")]
        public decimal Cqty { get; set; }               //沖銷股數
        [XmlElement("mprice")]
        [JsonPropertyName("mprice")]
        public string? Mprice { get; set; }              //成交單價
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }                //手續費
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                //交易稅
        [XmlElement("cost")]
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }               //投資成本
        [XmlElement("income")]
        [JsonPropertyName("income")]
        public decimal Income { get; set; }             //賣出金額
        [XmlElement("profit")]
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }             //損益
        [XmlElement("pl_ratio")]
        [JsonPropertyName("pl_ratio")]
        public string? PlRatio { get; set; }            //報酬率
        [XmlElement("ctype")]
        [JsonPropertyName("ctype")]
        public string? Ctype { get; set; }                  //沖銷類別  0:現 1:資 2:券 3:當沖
        [XmlElement("ttypename2")]
        [JsonPropertyName("ttypename2")]
        public string? Ttypename2 { get; set; }             //交易別名稱2 現賣/資賣/券買/信沖/現沖
        [XmlArray("profit_detail")]
        [XmlArrayItem("profit_detail", typeof(Detail))]
        [JsonPropertyName("profit_detail")]
        public List<Detail>? ProfitDetail { get; set; }      //未實現損益-個股明細資料 (買入)
        [XmlElement("profit_detail_out")]
        [JsonPropertyName("profit_detail_out")]
        public DetailOut? profitDetailOut { get; set; }      //已實現損益 - 個股明細資料 (賣出)
    }
    [XmlRoot("profit_detail")]
    public class Detail
    {
        [XmlElement("tdate")]
        [JsonPropertyName("tdate")]
        public string? Tdate { get; set; }                  //交易日期
        [XmlElement("dseq")]
        [JsonPropertyName("dseq")]
        public string? Dseq { get; set; }                   //原委託書號
        [XmlElement("dno")]
        [JsonPropertyName("dno")]
        public string? Dno { get; set; }                    //原委託分單號
        [XmlElement("mqty")]
        [JsonPropertyName("mqty")]
        public decimal Mqty { get; set; }                  //股數
        [XmlElement("cqty")]
        [JsonPropertyName("cqty")]
        public decimal Cqty { get; set; }                  //已沖銷股數
        [XmlElement("mprice")]
        [JsonPropertyName("mprice")]
        public string? Mprice { get; set; }                 //成交單價
        [XmlElement("mamt")]
        [JsonPropertyName("mamt")]
        public string? Mamt { get; set; }                   //成交價金
        [XmlElement("cost")]
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }                  //付出成本
        [XmlElement("income")]
        [JsonPropertyName("income")]
        public decimal Income { get; set; }                //收入
        [XmlElement("netamt")]
        [JsonPropertyName("netamt")]
        public decimal Netamt { get; set; }                //淨收付
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }                   //手續費
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                   //交易稅
        [XmlElement("adjdate")]
        [JsonPropertyName("adjdate")]
        public string? Adjdate { get; set; }                //調整日期
        [XmlElement("ttype")]
        [JsonPropertyName("ttype")]
        public string? Ttype { get; set; }                  //交易別 0:整股 1,3:融資 2,4:融券 
        [XmlElement("ttypename")]
        [JsonPropertyName("ttypename")]
        public string? Ttypename { get; set; }              //交易類別名稱
        [XmlElement("bstype")]
        [JsonPropertyName("bstype")]
        public string? Bstype { get; set; }                 //買賣別(B:買入,S:賣出)
        [XmlElement("wtype")]
        [JsonPropertyName("wtype")]
        public string? Wtype { get; set; }                  //異動別
        [XmlElement("profit")]
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }                //損益
        [XmlElement("pl_ratio")]
        [JsonPropertyName("pl_ratio")]
        public string? PlRatio { get; set; }                //報酬率
        [XmlElement("ctype")]
        [JsonPropertyName("ctype")]
        public string? Ctype { get; set; }                  //沖銷類別 0:現 1:資 2:券 3:當沖
        [XmlElement("Ioflag")]
        [JsonPropertyName("Ioflag")]
        public string? Ioflag { get; set; }                 //匯撥集保代碼
        [XmlElement("ioname")]
        [JsonPropertyName("ioname")]
        public string? Ioname { get; set; }                 //匯撥集保名稱
        [XmlElement("ttypename2")]
        [JsonPropertyName("ttypename2")]
        public string? Ttypename2 { get; set; }             //交易別名稱2 現買
    }
    [XmlRoot("profit_detail_out")]
    public class DetailOut
    {
        [XmlElement("tdate")]
        [JsonPropertyName("tdate")]
        public string? Tdate{ get; set; }                  //交易日期
        [XmlElement("dseq")]
        [JsonPropertyName("dseq")]
        public string? Dseq { get; set; }                  //委託書號
        [XmlElement("dno")]
        [JsonPropertyName("dno")]
        public string? Dno { get; set; }                   //分單號
        [XmlElement("mqty")]
        [JsonPropertyName("mqty")]
        public decimal Mqty { get; set; }                 //股數
        [XmlElement("cqty")]
        [JsonPropertyName("cqty")]
        public decimal Cqty { get; set; }                 //已沖銷股數
        [XmlElement("mprice")]
        [JsonPropertyName("mprice")]
        public string? Mprice { get; set; }                //成交單價
        [XmlElement("mamt")]
        [JsonPropertyName("mamt")]
        public string? Mamt { get; set; }                  //成交價金
        [XmlElement("cost")]
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }                 //付出成本
        [XmlElement("income")]
        [JsonPropertyName("income")]
        public decimal Income { get; set; }               //收入
        [XmlElement("netamt")]
        [JsonPropertyName("netamt")]
        public decimal Netamt { get; set; }               //淨收付
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }                  //手續費
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                  //交易稅
        [XmlElement("ttype")]
        [JsonPropertyName("ttype")]
        public string? Ttype { get; set; }                 //交易別
        [XmlElement("ttypename")]
        [JsonPropertyName("ttypename")]
        public string? Ttypename { get; set; }             //交易類別名稱
        [XmlElement("bstype")]
        [JsonPropertyName("bstype")]
        public string? Bstype { get; set; }                //買賣別(B:買入,S:賣出)
        [XmlElement("wtype")]
        [JsonPropertyName("wtype")]
        public string? Wtype { get; set; }                 //異動別
        [XmlElement("profit")]
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }               //損益
        [XmlElement("pl_ratio")]
        [JsonPropertyName("pl_ratio")]
        public string? PlRatio { get; set; }               //報酬率
        [XmlElement("ctype")]
        [JsonPropertyName("ctype")]
        public string? Ctype { get; set; }                 //沖銷類別0：現
        [XmlElement("ttypename2")]
        [JsonPropertyName("ttypename2")]
        public string? Ttypename2 { get; set; }            //交易別名稱 2 現賣/賣沖
    }
}




