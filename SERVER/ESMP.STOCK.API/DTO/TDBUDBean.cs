namespace ESMP.STOCK.API.DTO
{
    public class TDBUDBean
    {
        //融券餘額檔
        public string TDATE { get; set; }           //成交日期
        public string BHNO { get; set; }            //分公司代號
        public string DSEQ { get; set; }            //委託書櫃號
        public string DNO { get; set; }             //委託書分單號
        public string CSEQ { get; set; }            //客戶帳號
        public string STOCK { get; set; }           //股票代號
        public decimal PRICE { get; set; }          //單價
        public decimal QTY { get; set; }            //股數
        public decimal DBAMT { get; set; }          //融券賣出金額
        public decimal GTAMT { get; set; }          //保證金金額
        public decimal DNAMT { get; set; }          //擔保品金額
        public decimal BQTY { get; set; }           //未償還股數
        public decimal BDBAMT { get; set; }         //未償還融券金額
        public decimal BGTAMT { get; set; }         //未償還融券保證金
        public decimal BDNAMT { get; set; }         //未償還融券擔保品
        public decimal GTINT { get; set; }          //保證金利息
        public decimal DNINT { get; set; }          //擔保品利息
        public decimal DLFEE { get; set; }          //標界費用
        public decimal DLINT { get; set; }          //標借費利息
        public decimal DBRATIO { get; set; }        //融券維持率
        public string SFCODE { get; set; }          //證金公司
        public decimal AGTAMT { get; set; }         //融券追繳保證金
        public decimal FEE { get; set; }            //手續費
        public decimal TAX { get; set; }            //交易稅
        public decimal DBFEE { get; set; }          //融券手續費
        public decimal COST { get; set; }           //成本
        public decimal STINTAX { get; set; }        //證所稅
        public decimal HEALTHFEE { get; set; }      //健保補充費
        public string TRDATE { get; set; }          //轉檔日期
        public string TRTIME { get; set; }          //轉檔時間
        public string MODDATE { get; set; }         //異動日期
        public string MODTIME { get; set; }         //異動時間
        public string MODUSER { get; set; }         //異動人員

    }
}
