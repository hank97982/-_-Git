using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ESMP.STOCK.API.DTO.UnrealizedGainsAndlLosses
{
    [Serializable()]
    [XmlRoot("unoffset_qtype_accsum")]
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

    [XmlRoot("unoffset_qtype_accsum")]
    public class Accsum : AccsumErr
    {
        [JsonPropertyOrder(3)]
        [XmlElement("bqty")]
        [JsonPropertyName("bqty")]
        public decimal Bqty { get; set; }               //昨日總庫存股數
        [JsonPropertyOrder(4)]
        [XmlElement("cost")]
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }               //總付出成本
        [JsonPropertyOrder(5)]
        [XmlElement("marketvalue")]
        [JsonPropertyName("marketvalue")]
        public decimal Marketvalue { get; set; }        //總現值(市值)
        [JsonPropertyOrder(6)]
        [XmlElement("profit")]
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }             //損益試算
        [JsonPropertyOrder(7)]
        [XmlElement("pl_ratio")]
        [JsonPropertyName("pl_ratio")]
        public string? PlRatio { get; set; }             //報酬率(估)
        [JsonPropertyOrder(8)]
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }                //手續費
        [JsonPropertyOrder(9)]
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                //交易稅
        [JsonPropertyOrder(10)]
        [XmlElement("estimateAmt")]
        [JsonPropertyName("estimateAmt")]
        public decimal EstimateAmt { get; set; }        //預估賣出價金
        [JsonPropertyOrder(11)]
        [XmlElement("estimateFee")]
        [JsonPropertyName("estimateFee")]
        public decimal EstimateFee { get; set; }        //預估賣出手續費
        [JsonPropertyOrder(12)]
        [XmlElement("estimateTax")]
        [JsonPropertyName("estimateTax")]
        public decimal EstimateTax { get; set; }        //預估賣出交易稅

        //======================================

        [JsonPropertyOrder(13)]
        [XmlElement("cramt")]
        [JsonPropertyName("cramt")]
        public decimal Cramt { get; set; }              //融資金額

        [JsonPropertyOrder(14)]
        [XmlElement("bcramt")]
        [JsonPropertyName("bcramt")]
        public decimal Bcramt { get; set; }             //未償還融資金額

        [JsonPropertyOrder(15)]
        [XmlElement("gtamt")]
        [JsonPropertyName("gtamt")]
        public decimal Gtamt { get; set; }              //融券保證金

        [JsonPropertyOrder(16)]
        [XmlElement("bgtamt")]
        [JsonPropertyName("bgtamt")]
        public decimal Bgtamt { get; set; }             //未償還融券保證金

        [JsonPropertyOrder(17)]
        [XmlElement("dnamt")]
        [JsonPropertyName("dnamt")]
        public decimal Dnamt { get; set; }             //融券擔保品

        [JsonPropertyOrder(18)]
        [XmlElement("bdnamt")]
        [JsonPropertyName("bdnamt")]
        public decimal Bdnamt { get; set; }             //未償還融券擔保品

        [JsonPropertyOrder(19)]
        [XmlElement("interest")]
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }           //資券利息

        [JsonPropertyOrder(20)]
        [XmlElement("dbfee")]
        [JsonPropertyName("dbfee")]
        public decimal Dbfee { get; set; }              //融資手續費

        //======================================

        [JsonPropertyOrder(21)]
        [XmlArray("unoffset_qtype_sum")]
        [XmlArrayItem("unoffset_qtype_sum", typeof(Sum))]
        [JsonPropertyName("unoffset_qtype_sum")]
        public List<Sum>? UnoffsetQtypeSum { get; set; }        //個股未實現損益
    }
    [XmlRoot("Unoffset_qtype_sum")]
    public class Sum
    {
        [XmlElement("stock")]
        [JsonPropertyName("stock")]
        //[Column("STOCK")]
        public string? Stock { get; set; }               //股票代碼
        [XmlElement("stocknm")]
        [JsonPropertyName("stocknm")]
        //[Column("CNAME")]
        public string? Stocknm { get; set; }             //股票名稱
        [XmlElement("ttype")]
        [JsonPropertyName("ttype")]
        public string? Ttype { get; set; }               //交易別 0:現股 
        [XmlElement("ttypename")]
        [JsonPropertyName("ttypename")]
        public string? Ttypename { get; set; }           //交易類別名稱(現買/現賣)
        [XmlElement("bstype")]
        [JsonPropertyName("bstype")]
        public string? Bstype { get; set; }              //買賣別(B/S)
        [XmlElement("bqty")]
        [JsonPropertyName("bqty")]
        public decimal Bqty { get; set; }               //昨日庫存股數
        [XmlElement("real_qty")]
        [JsonPropertyName("real_qty")]
        public decimal RealQTY { get; set; }            //即時庫存股數
        [XmlElement("cost")]
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }               //成本金額
        [XmlElement("avgprice")]
        [JsonPropertyName("avgprice")]
        public decimal Avgprice { get; set; }           //均價
        [XmlElement("lastprice")]
        [JsonPropertyName("lastprice")]
        //[Column("PRICE")]
        public decimal Lastprice { get; set; }          //現價
        [XmlElement("marketvalue")]
        [JsonPropertyName("marketvalue")]
        public decimal Marketvalue { get; set; }        //現值(市值)
        [XmlElement("estimateAmt")]
        [JsonPropertyName("estimateAmt")]
        public decimal EstimateAmt { get; set; }        //預估賣出價金
        [XmlElement("estimateFee")]
        [JsonPropertyName("estimateFee")]
        public decimal EstimateFee { get; set; }        //預估賣出手續費
        [XmlElement("estimateTax")]
        [JsonPropertyName("estimateTax")]
        public decimal EstimateTax { get; set; }        //預估賣出交易稅
        [XmlElement("profit")]
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }             //預估損益
        [XmlElement("pl_ratio")]
        [JsonPropertyName("pl_ratio")]
        public string? PlRatio { get; set; }            //報酬率
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }                //手續費
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                //交易稅
        [XmlElement("amt")]
        [JsonPropertyName("amt")]
        public decimal Amt { get; set; }                //成交價金

        //======================================

        [XmlElement("cramt")]
        [JsonPropertyName("cramt")]
        public decimal Cramt { get; set; }              //融資金額

        [XmlElement("bcramt")]
        [JsonPropertyName("bcramt")]
        public decimal Bcramt { get; set; }             //未償還融資金額

        [XmlElement("gtamt")]
        [JsonPropertyName("gtamt")]
        public decimal Gtamt { get; set; }              //融券保證金

        [XmlElement("bgtamt")]
        [JsonPropertyName("bgtamt")]
        public decimal Bgtamt { get; set; }             //未償還融券保證金

        [XmlElement("dnamt")]
        [JsonPropertyName("dnamt")]
        public decimal Dnamt { get; set; }             //融券擔保品

        [XmlElement("bdnamt")]
        [JsonPropertyName("bdnamt")]
        public decimal Bdnamt { get; set; }             //未償還融券擔保品

        [XmlElement("interest")]
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }           //資券利息

        [XmlElement("dbfee")]
        [JsonPropertyName("dbfee")]
        public decimal Dbfee { get; set; }              //融資手續費

        //======================================

        [XmlArray("unoffset_qtype_detail")]
        [XmlArrayItem("unoffset_qtype_detail", typeof(Detail))]
        [JsonPropertyName("unoffset_qtype_detail")]

        public List<Detail>? UnoffsetQtypeDetail { get; set; }      //未實現損益 – 個股明細

    }
    [XmlRoot("Unoffset_qtype_detail")]
    public class Detail
    {
        //[XmlIgnore]
        //[JsonIgnore]
        //public string? Stock { get; set; }               //股票代碼(Key)
        [XmlElement("tdate")]
        [JsonPropertyName("tdate")]
        //[Column("TDATE")]
        public string? Tdate { get; set; }              //交易日期
        [XmlElement("ttype")]
        [JsonPropertyName("ttype")]
        public string? Ttype { get; set; }              //交易別 0:現股 1:融資 2:融券
        [XmlElement("ttypename")]
        [JsonPropertyName("ttypename")]
        public string? Ttypename { get; set; }          //交易類別名稱
        [XmlElement("bstype")]
        [JsonPropertyName("bstype")]
        public string? Bstype { get; set; }             //買賣別(B/S)
        [XmlElement("dseq")]
        [JsonPropertyName("dseq")]
        //[Column("DSEQ")]
        public string? Dseq { get; set; }               //委託書號
        [XmlElement("dno")]
        [JsonPropertyName("dno")]
        //[Column("DNO")]
        public string? Dno { get; set; }                //分單號碼
        [XmlElement("bqty")]
        [JsonPropertyName("bqty")]
        //[Column("BQTY")]
        public decimal Bqty { get; set; }               //庫存股數
        [XmlElement("mprice")]
        [JsonPropertyName("mprice")]
        //[Column("PRICE")]
        public decimal Mprice { get; set; }             //成交價
        [XmlElement("mamt")]
        [JsonPropertyName("mamt")]
        public decimal Mamt { get; set; }               //成交價金
        [XmlElement("lastprice")]
        [JsonPropertyName("lastprice")]
        //[Column("CPRICE")]
        public decimal Lastprice { get; set; }          //現價
        [XmlElement("marketvalue")]
        [JsonPropertyName("marketvalue")]
        public decimal Marketvalue { get; set; }        //現值(市值)
        [XmlElement("fee")]
        [JsonPropertyName("fee")]
        //[Column("FEE")]
        public decimal Fee { get; set; }                //手續費
        [XmlElement("tax")]
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }                //交易稅
        
        //===================================

        [XmlElement("cramt")]
        [JsonPropertyName("cramt")]
        public decimal Cramt { get; set; }              //融資金額

        [XmlElement("bcramt")]
        [JsonPropertyName("bcramt")]
        public decimal Bcramt { get; set; }             //未償還融資金額

        [XmlElement("gtamt")]
        [JsonPropertyName("gtamt")]
        public decimal Gtamt { get; set; }              //融資保證金

        [XmlElement("bgtamt")]
        [JsonPropertyName("bgtamt")]
        public decimal Bgtamt { get; set; }             //未償還融資保證金

        [XmlElement("dnamt")]
        [JsonPropertyName("dnamt")]
        public decimal Dnamt { get; set; }              //融券擔保品

        [XmlElement("bdnamt")]
        [JsonPropertyName("bdnamt")]
        public decimal Bdnamt { get; set; }             //未償還融券擔保品

        [XmlElement("interest")]
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }           //資券利息

        [XmlElement("dbfee")]
        [JsonPropertyName("dbfee")]
        public decimal Dbfee { get; set; }              //融券手續費

        [XmlElement("dlfee")]
        [JsonPropertyName("dlfee")]
        public decimal Dlfee { get; set; }              //標借費

        [XmlElement("dlint")]
        [JsonPropertyName("dlint")]
        public decimal Dlint { get; set; }              //標借費利息

        //===================================

        [XmlElement("cost")]
        [JsonPropertyName("cost")]
        //[Column("COST")]
        public decimal Cost { get; set; }               //付出成本
        [XmlElement("estimateAmt")]
        [JsonPropertyName("estimateAmt")]
        public decimal EstimateAmt { get; set; }        //預估賣出價金
        [XmlElement("estimateFee")]
        [JsonPropertyName("estimateFee")]
        public decimal EstimateFee { get; set; }        //預估賣出手續費
        [XmlElement("estimateTax")]
        [JsonPropertyName("estimateTax")]
        public decimal EstimateTax { get; set; }        //預估賣出交易稅
        [XmlElement("profit")]
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }             //預估損益
        [XmlElement("pl_ratio")]
        [JsonPropertyName("pl_ratio")]
        public string? PlRatio { get; set; }            //報酬率

        [XmlElement("keeprate")]
        [JsonPropertyName("keeprate")]
        public string? Keeprate { get; set; }            //維持率

        [XmlElement("wtype")]
        [JsonPropertyName("wtype")]
        public string? Wtype { get; set; }              //異動別 (0-交易 A-集保匯撥)
        [XmlElement("ioflag")]                          
        [JsonPropertyName("ioflag")]                    
        public string? Ioflag { get; set; }             //匯撥集保代碼
        [XmlElement("ioname")]                          
        [JsonPropertyName("ioname")]                    
        public string? Ioname { get; set; }             //匯撥集保名稱

    }
}
