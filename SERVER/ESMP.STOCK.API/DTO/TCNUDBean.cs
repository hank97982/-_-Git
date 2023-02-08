using System.ComponentModel.DataAnnotations.Schema;


namespace ESMP.STOCK.API.Bean
{
    //TCNUD 現股餘額檔
    public class TCNUDBean
    {
        [Column("TDATE")]
        public string? TDATE { get; set; }          //成交日期
        [Column("BHNO")]
        public string? BHNO { get; set; }           //分公司代號
        [Column("CSEQ")]
        public string? CSEQ { get; set; }           //客戶帳號
        [Column("STOCK")]
        public string? STOCK { get; set; }          //股票代號
        [Column("PRICE")]
        public decimal PRICE { get; set; }          //單價
        [Column("QTY")]
        public decimal QTY { get; set; }            //股數
        [Column("BQTY")]
        public decimal BQTY { get; set; }           //未償還股數
        [Column("FEE")]
        public decimal FEE { get; set; }            //手續費
        [Column("COST")]
        public decimal COST { get; set; }           //成本
        [Column("DSEQ")]
        public string? DSEQ { get; set; }           //委託書號
        [Column("DNO")]
        public string? DNO { get; set; }            //分單號
        [Column("ADJDATE")]
        public string? ADJDATE { get; set; }        //調整日期
        [Column("WTYPE")]
        public string? WTYPE { get; set; }
        [Column("TRDATE")]
        public string? TRDATE { get; set; }         //轉檔日期
        [Column("TRTIME")]
        public string? TRTIME { get; set; }         //轉檔時間
        [Column("MODDATE")]
        public string? MODDATE { get; set; }        //異動日期
        [Column("MODTIME")]
        public string? MODTIME { get; set; }        //異動時間
        [Column("MODEUSER")]
        public string? MODEUSER { get; set; }       //異動人員
        [Column("IOFLAG")]
        public string? IOFLAG { get; set; }
        public decimal AMT { get; set; }
        public decimal LastQtyRam { get; set; }
    }
}
