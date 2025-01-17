﻿using ESMP.STOCK.API.Utils;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ESMP.STOCK.API.DTO.RealizedProfitAndLoss
{
    [Serializable()]
    [XmlRoot("root")]
    //已實現損益查詢
    public class RealizedProfitAndLossDTO
    {
        [XmlElement("qtype")]
        [JsonPropertyName("qtype")]
        public string? Qtype { get; set; }              //查詢類別
        [XmlElement("bhno")]
        [JsonPropertyName("bhno")]
        [MappingRequest("BHNO")]
        public string? Bhno { get; set; }               //分公司
        [XmlElement("cseq")]
        [JsonPropertyName("cseq")]
        [MappingRequest("CSEQ")]
        public string? Cseq { get; set; }               //帳號
        [XmlElement("sdate")]
        [JsonPropertyName("sdate")]
        public string? Sdate { get; set; }              //查詢起日
        [XmlElement("edate")]
        [JsonPropertyName("edate")]
        public string? Edate { get; set; }              //查詢迄日
        [XmlElement("stockSymbol")]
        [JsonPropertyName("stockSymbol")]
        [MappingRequest("STOCK")]
        public string? StockSymbol { get; set; }        //股票代號,若查詢全部帶空白
        [XmlElement("ttype")]
        [JsonPropertyName("ttype")]
        public string? Ttype { get; set; }              //交易類別
    }
}
