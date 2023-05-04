using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.DTO
{
    public class TCNTDBean
    {
        //盤中現股當沖指定沖銷對應檔
        [Column("TDATE")]
        public string? TDATE { get; set; }      //交易日期
        [Column("BHNO")]
        public string? BHNO { get; set; }       //分公司
        [Column("BDSEQ")]
        public string? BDSEQ { get; set; }      //買進委託書號
        [Column("BDNO")]
        public string? BDNO { get; set; }       //買進分單號
        [Column("SDSEQ")]
        public string? SDSEQ { get; set; }      //賣出委託書號
        [Column("SDNO")]
        public string? SDNO { get; set; }       //賣出分單號
        [Column("CSEQ")]
        public string? CSEQ { get; set; }       //客戶帳號
        [Column("STOCK")]
        public string? STOCK { get; set; }      //股票代號
        [Column("CQTY")]
        public string? CQTY { get; set; }       //沖抵股數
        [Column("BPRICE")]
        public string? BPRICE { get; set; }     //買進單價
        [Column("BQTY")]
        public string? BQTY { get; set; }       //買進股數
        [Column("BFEE")]
        public string? BFEE { get; set; }       //買進手續費
        [Column("SPRICE")]
        public string? SPRICE { get; set; }     //賣出單價
        [Column("SQTY")]
        public string? SQTY { get; set; }       //賣出股數
        [Column("SFEE")]
        public string? SFEE { get; set; }       //賣出手續費
        [Column("TAX")]
        public string? TAX { get; set; }        //交易稅
        [Column("INCOME")]
        public string? INCOME { get; set; }     //出庫收入金額
        [Column("COST")]
        public string? COST { get; set; }       //成本
        [Column("PROFIT")]
        public string? PROFIT { get; set; }     //損益
        [Column("STINTAX")]
        public string? STINTAX { get; set; }    //證所稅
        [Column("JRNUM")]
        public string? JRNUM { get; set; }      //資料序號
        [Column("TRDATE")]
        public string? TRDATE { get; set; }     //轉檔日期
        [Column("TRTIME")]
        public string? TRTIME { get; set; }     //轉檔時間
        [Column("MODDATE")]
        public string? MODDATE { get; set; }    //異動日期
        [Column("MODETIME")]
        public string? MODETIME { get; set; }   //異動時間
        [Column("MODUSER")]
        public string? MODUSER { get; set; }    //異動人員
    }
}
