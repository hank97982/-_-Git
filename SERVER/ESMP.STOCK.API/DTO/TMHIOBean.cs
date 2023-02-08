using System.ComponentModel.DataAnnotations.Schema;


namespace ESMP.STOCK.API.DTO
{
    //TMHIO 當日交易明細
    public class TMHIOBean
    {
        [Column("TDATE")]
        public string? Tdate { get; set; }          //成交日期  //測試一下有沒有抓到attrbute
        [Column("BHNO")]
        public string? BHNO { get; set; }           //分公司代號
        [Column("DSEQ")]
        public string? DSEQ { get; set; }           //書櫃號
        [Column("JRNUM")]
        public string? JRNUM { get; set; }          //成交流水號
        [Column("MTYPE")]
        public string? MTYPE { get; set; }          //市場別
        [Column("CSEQ")]
        public string? CSEQ { get; set; }           //客戶帳號
        [Column("TTYPE")]
        public string? TTYPE { get; set; }          //委託別 0-普通 1-代資 2-代券 3-自資 4-自券
        [Column("ETYPE")]
        public string? ETYPE { get; set; }          //交易別 0-整股 1-鉅額 2-零股 3-定價
        [Column("BSTYPE")]
        public string? BSTYPE { get; set; }         //買賣別
        [Column("STOCK")]
        public string? STOCK { get; set; }          //股票代號
        [Column("QTY")]
        public decimal QTY { get; set; }            //成交數量
        [Column("PRICE")]
        public decimal PRICE { get; set; }          //成交價格
        [Column("SALES")]
        public string? SALES { get; set; }          //營業員代號
        [Column("ORGIN")]
        public string? ORGIN { get; set; }          //委託來源 1-網路 2-語音 3-代理 4-營業員 9-現場
        [Column("MTIME")]
        public string? MTIME { get; set; }          //成交時間
        [Column("TRDATE")]
        public string? TRDATE { get; set; }         //轉檔日期
        [Column("TRTIME")]
        public string? TRTIME { get; set; }         //轉檔時間
        [Column("MODDATE")]
        public string? MODDATE { get; set; }        //異動日期
        [Column("MODTIME")]
        public string? MODTIME { get; set; }        //異動時間
        public decimal LastQtyRam { get; set; }
    }
}
