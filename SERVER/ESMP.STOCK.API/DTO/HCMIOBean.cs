using System.ComponentModel.DataAnnotations.Schema;


namespace ESMP.STOCK.API.DTO
{
    //HCMIO 歷史交易明細
    public class HCMIOBean
    {
        [Column("TDATE")]
        public string? Tdate { get; set; }          //交易日       //測試一下有沒有抓到attrbute
        [Column("BHNO")]
        public string? BHNO { get; set; }           //分公司代號
        [Column("CSEQ")]
        public string? CSEQ { get; set; }           //客戶帳號
        [Column("DSEQ")]
        public string? DSEQ { get; set; }           //委託書號
        [Column("DNO")]
        public string? DNO { get; set; }            //分單號碼
        [Column("WTYPE")]
        public string? WTYPE { get; set; }          //異動別 0-交易 A-集保匯撥 B-自訂資產 C-其他 S-系統
        [Column("STOCK")]
        public string? STOCK { get; set; }          //股票代號
        [Column("TTYPE")]
        public string? TTYPE { get; set; }          //委託別 0-普通 1-貸資 2-代券 3-融資 4-融券
        [Column("ETYPE")]
        public string? ETYPE { get; set; }          //交易別 0-整股 1-零股 代碼 ETYPE
        [Column("BSTYPE")]
        public string? BSTYPE { get; set; }         //買賣別 B-買/入庫 S-賣/出庫 代碼 BSTY
        [Column("PRICE")]
        public decimal PRICE { get; set; }          //單價
        [Column("QTY")]
        public decimal QTY { get; set; }            //股數
        [Column("AMT")]
        public decimal AMT { get; set; }            //價金
        [Column("FEE")]
        public decimal FEE { get; set; }            //手續費
        [Column("TAX")]                             
        public decimal TAX { get; set; }            //交易稅
        [Column("RVINT")]                           
        public decimal RVINT { get; set; }          //債息
        [Column("NETAMT")]                          
        public decimal NETAMT { get; set; }         //淨收付金額
        [Column("DBFEE")]                           
        public decimal DBFEE { get; set; }          //融券手續費
        [Column("CRAMT")]                           
        public decimal CRAMT { get; set; }          //融資/保證金金額
        [Column( "DNAMT")]                          
        public decimal DNAMT { get; set; }          //擔保品金額
        [Column("CRINT")]                           
        public decimal CRINT { get; set; }          //融資/保證金利息
        [Column("DNINT")]                           
        public decimal DNINT { get; set; }          //擔保品利息
        [Column("DLFEE")]                           
        public decimal DLFEE { get; set; }          //標借卷費
        [Column("BFINT")]                           
        public decimal BFINT { get; set; }          //標借卷費利息
        [Column("OBAMT")]                           
        public decimal OBAMT { get; set; }          //逾期手續費
        [Column("INTAX")]                           
        public decimal INTAX { get; set; }          //代扣所得稅
        [Column("SFCODE")]
        public string? SFCODE { get; set; }         //證金代號
        [Column("CDTDQTY")]
        public decimal CDTDQTY { get; set; }        //當沖股數
        [Column("ORIGN")]
        public string? ORIGN { get; set; }          //委託來源 代碼 = ODOR
        [Column("SALES")]
        public string? SALES { get; set; }          //
        [Column("DATAFLAG")]
        public string? DATAFLAG { get; set; }       //資料註記 空白: 一般單 1:減資出庫 2:賣出股數大於
        [Column("IOFLAG")]
        public string? IOFLAG { get; set; }         //集保異動代碼 代碼=ODOR
        [Column("ADJCOST")]
        public decimal ADJCOST { get; set; }        //調整成本
        [Column("ADJDATE")]
        public string? ADJDATE { get; set; }        //調整成本日期
        [Column("STINTAX")]
        public decimal STINTAX { get; set; }        //證所稅
        [Column("HEALTHFEE")]
        public decimal HEALTHFEE { get; set; }      //健保補充費
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
