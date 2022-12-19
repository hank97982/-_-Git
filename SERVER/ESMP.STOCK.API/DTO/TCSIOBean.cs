using System.ComponentModel.DataAnnotations.Schema;


namespace ESMP.STOCK.API.DTO
{
    //TCSIO 當日現股匯撥檔
    public class TCSIOBean
    {
        [Column("TDATE")]
        public string? TDATE { get; set; }          //交易日期
        [Column("BHNO")]
        public string? BHNO { get; set; }           //分公司代號
        [Column("DSEQ")]
        public string? DSEQ { get; set; }           //書櫃號(系統產生)
        [Column("DNO")]
        public string? DNO { get; set; }            //分單號(系統產生)
        [Column("CSEQ")]
        public string? CSEQ { get; set; }           //客戶帳號
        [Column("STOCK")]
        public string? STOCK { get; set; }          //股票代號
        [Column("BSTYPE")]
        public string? BSTYPE { get; set; }         //買賣別 B-買/入庫 S-賣/出庫
        [Column("QTY")]
        public decimal QTY { get; set; }            //股數
        [Column("IOFLAG")]
        public string? IOFLAG { get; set; }         //集保異動代碼 1-減資
        [Column("REMARK")]
        public string? REMARK { get; set; }
        [Column("JRNUM")]
        public string? JRNUM { get; set; }          //資料流水號
        [Column("TRDATE")]
        public string? TRDATE { get; set; }         //轉檔日期
        [Column("TRTIME")]
        public string? TRTIME { get; set; }         //轉檔時間
        [Column("MODDATE")]
        public string? MODDATE { get; set; }        //異動日期
        [Column("MODTIME")]
        public string? MODTIME { get; set; }        //異動時間
        [Column("MODUSER")]
        public string? MODUSER { get; set; }        //異動人員
    }
}
