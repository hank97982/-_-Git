using System.ComponentModel.DataAnnotations.Schema;

namespace ESMP.STOCK.API.DTO
{
    //股票是否有現股當沖資格
    //MSTMB 商品主檔
    public class MSTMBBean
    {
        [Column("STOCK")]
        public string? STOCK { get; set; }          //證卷代號
        [Column("CNAME")]
        public string? CNAME { get; set; }          //中文名稱
        [Column("ENAME")]
        public string? ENAME { get; set; }          //英文名稱
        [Column("MTYPE")]
        public string? MTYPE { get; set; }          //市場別 T-上市 O-上櫃 R-興櫃
        [Column("STYPE")]
        public string? STYPE { get; set; }          //股票類別
        [Column("SCLASS")]
        public string? SCLASS { get; set; }         //股票分類
        [Column("TSDATE")]
        public string? TSDATE { get; set; }         //交易起始日期
        [Column("TEDATE")]
        public string? TEDATE { get; set; }         //交易截止日期
        [Column("CLDATE")]
        public string? CLDATE { get; set; }         //收盤日期
        [Column("CPRICE")]
        public decimal CPRICE { get; set; }         //收盤價
        [Column("TPRICE")]
        public decimal TPRICE { get; set; }         //漲停價
        [Column("BPRICE")]
        public decimal BPRICE { get; set; }         //跌停價
        [Column("TSTATUS")]
        public string? TSTATUS { get; set; }        //交易狀況 0-停止交易 1-交易所警示 2-證券商警示 3-注意股票
        [Column("BRKNO")]
        public string? BRKNO { get; set; }          //承銷/委任商代號
        [Column("IDATE")]
        public string? IDATE { get; set; }          //債息基準日期
        [Column("IRATE")]
        public decimal IRATE { get; set; }          //債息利率
        [Column("IDAY")]
        public decimal IDAY { get; set; }           //年息天數
        [Column("CURRENCY")]
        public string? CURRENCY { get; set; }       //幣別
        [Column("COUNTRY")]
        public string? COUNTRY { get; set; }        //國家
        [Column("SHARE")]
        public decimal SHARE { get; set; }          //單位股數
        [Column("WARNING")]
        public string? WARNING { get; set; }        //警示股
        [Column("TMARK")]
        public string? TMARK { get; set; }          //交易註記(0.一般股票 1.全額股票 2.全額股票&分盤撮合 3.管理)
        [Column("MFLAG")]
        public string? MFLAG { get; set; }
        [Column("WMARK")]
        public string? WMARK { get; set; }          //處置註記(1.處置股票 2.在處置股票 3.彈性處置股票)
        [Column("TAXTYPE")]
        public string? TAXTYPE { get; set; }        //稅率類別
        [Column("PTYPE")]
        public string? PTYPE { get; set; }
        [Column("DRDATE")]
        public string? DRDATE { get; set; }         //除權日期
        [Column("PDRDATE")]
        public string? PDRDATE { get; set; }        //假除權日
        [Column("CDIV")]
        public decimal CDIV { get; set; }           //現金股利
        [Column("SDIV")]
        public decimal SDIV { get; set; }           //股票股利
        [Column("CNTDTYPE")]
        public string? CNTDTYPE { get; set; }       //現股當沖別(Y:可當沖,N:不可當沖)
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
