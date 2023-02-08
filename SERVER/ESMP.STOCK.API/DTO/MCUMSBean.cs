using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.DTO
{
    //客戶是否有現股當沖資額
    //客戶證券交易帳戶檔
    public class MCUMSBean
    {
        public string? BHNO { get; set; }           //分公司代碼
        public string? CSEQ { get; set; }           //客戶代號
        public string? BTYPE { get; set; }          //業務別
        public string? IDNO { get; set; }           //身分證號
        public string? ACCOUNTTYPE { get; set; }    //帳戶別S:國內證券,F:國內期貨,FF:國外期貨,B:國內外期貨,FS:複委託
        public string? DCBFLAG { get; set; }        //開戶類別0:自然人1:全權委託戶
        public string? SALES { get; set; }          //營業員
        public string? OTYPE { get; set; }          //開銷戶別(0:開戶 1:銷戶)
        public string? ODATE { get; set; }          //完成開戶日期
        public string? FDATE { get; set; }          //完成銷戶日期
        public string? DSTATUS { get; set; }        //銷戶狀態
        public string? CNACNO { get; set; }         //信用帳號
        public string? SFCODE { get; set; }         //證金公司
        public string? DAYSDATE { get; set; }       //當沖申請日期
        public string? DAYRDATE { get; set; }       //當沖解約日期
        public string? CONTRA { get; set; }         //當沖註記(Y:可當沖,N:不可當沖)
        public string? CRLEVEL { get; set; }        //融資等級
        public string? DBLEVEL { get; set; }        //融券等級
        public decimal? RATIO { get; set; }         //整戶維持率
        public decimal? CRCREDIT { get; set; }      //融資額度
        public decimal? DBCREDIT { get; set; }      //融券額度
        public string? CNACOTYPE { get; set; }      //信用開銷戶別(0:開戶 1:銷戶 ''-無信用戶)
        public string? CALCN { get; set; }          //是否作日結處理
        public string? SELFQUR { get; set; }        //開放此帳號自行查詢
        public string? EMAIL { get; set; }
        public string? NOLMTMINFEE { get; set; }    //不受限最低手續費
        public string? FEECRFREE { get; set; }      //當沖時代買方免收證商手續費
        public decimal? FEECOUNT { get; set; }       //手續費折讓率
        public string? KIND { get; set; }           //依期交所身份碼規定
        public string? SBHNO { get; set; }          //證券分公司代碼 若ACCOUNT為 國內期,才會有
        public string? IBNO { get; set; }           //IB代號
        public string? CALSIT { get; set; }         //證所稅是否採設算
        public string? CALHF { get; set; }          //是否計算健保補充費
        public string? CNTDTYPE { get; set; }       //現股當充(Z:已申請現股當充, Y:可以做現股當沖, X:不可以做現股當沖 非上述值則為未申請現股當沖)
        public string? TRDATE { get; set; }         //轉檔日期
        public string? TRTIME { get; set; }         //轉檔時間
        public string? MODDATE { get; set; }        //異動日期
        public string? MODTIME { get; set; }        //異動時間
        public string? MODUSER { get; set; }        //異動人員
    }
}
