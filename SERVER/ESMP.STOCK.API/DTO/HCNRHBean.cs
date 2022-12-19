using System.ComponentModel.DataAnnotations.Schema;


namespace ESMP.STOCK.API.DTO
{
    //HCNRH 歷史現股沖銷對應檔
    public class HCNRHBean
    {
        [Column("BHNO")]
        public string? BHNO { get; set; }           //分公司代碼
        [Column("TDATE")]
        public string? TDATE { get; set; }          //交易日
        [Column("RDATE")]
        public string? RDATE { get; set; }          //買進成交日期
        [Column("CSEQ")]
        public string? CSEQ { get; set; }           //客戶帳號
        [Column("BDSEQ")]
        public string? BDSEQ { get; set; }          //買進委託書號
        [Column("BDNO")]
        public string? BDNO { get; set; }           //買進分單號碼
        [Column("SDSEQ")]
        public string? SDSEQ { get; set; }          //賣出委託書號
        [Column("SDNO")]
        public string? SDNO { get; set; }           //賣出分擔號碼
        [Column("STOCK")]
        public string? STOCK { get; set; }          //股票代碼
        [Column("CQTY")]
        public decimal CQTY { get; set; }           //沖銷股數
        [Column("BPRICE")]
        public decimal BPRICE { get; set; }         //買進單價
        [Column("BFEE")]
        public decimal BFEE { get; set; }           //買進手續費
        [Column("SPRICE")]
        public decimal SPRICE { get; set; }         //賣出單價
        [Column("SFEE")]
        public decimal SFEE { get; set; }           //賣出手續費
        [Column("TAX")]
        public decimal TAX { get; set; }            //交易稅
        [Column("INCOME")]
        public decimal INCOME { get; set; }         //出庫收入金額
        [Column("COST")]
        public decimal COST { get; set; }           //成本
        [Column("PROFIT")]
        public decimal PROFIT { get; set; }         //損益金額
        [Column("ADJDATE")]
        public string? ADJDATE { get; set; }        //調整成本日期
        [Column("WTYPE")]
        public string? WTYPE { get; set; }
        [Column("BQTY")]
        public decimal BQTY { get; set; }
        [Column("SQTY")]
        public decimal SQTY { get; set; }
        [Column("STINTAX")]
        public decimal STINTAX { get; set; }        //證所稅
        [Column("IOFLAG")]
        public string? IOFLAG { get; set; }
        [Column("TRDATE")]
        public string? TRDATE { get; set; }         //轉檔日期
        [Column("TRTIME")]
        public string? TRTIME { get; set; }         //轉檔時間
        [Column("MODDATE")]
        public string? MODDATE { get; set; }        //更新日期
        [Column("MODTIME")]
        public string? MODTIME { get; set; }        //更新時間
        [Column("MODUSER")]
        public string? MODUSER { get; set; }        //更新人員
    }
}
