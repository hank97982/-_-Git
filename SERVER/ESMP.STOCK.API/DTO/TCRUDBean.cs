using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.DTO
{
    public class TCRUDBean
    {
        //融資餘額檔
        public string TDATE { get; set; }               //成交日期
        public string BHNO { get; set; }                //分公司代碼
        public string DSEQ { get; set; }                //委託書號
        public decimal PRICE { get; set; }               //單價
        public string DNO { get; set; }                 //分單號
        public string CSEQ { get; set; }                //客戶帳號
        public string STOCK { get; set; }               //股票代號
        public decimal QTY { get; set; }                //股數
        public decimal CRAMT { get; set; }              //融資金額
        public decimal PAMT { get; set; }               //自備款
        public decimal BQTY { get; set; }               //未償還股數
        public decimal BCRAMT { get; set; }             //未償還融資金額
        public decimal CRINT { get; set; }              //融資金額利息
        public string SFCODE { get; set; }              //證金公司
        public decimal CRRATIO { get; set; }            //融資維持率
        public decimal ASFAMT { get; set; }             //融資追繳金額
        public decimal FEE { get; set; }                //手續費
        public decimal COST { get; set; }               //成本
        public string TRDATE { get; set; }              //轉檔日期
        public string TRTIME { get; set; }              //轉檔時間
        public string MODDATE { get; set; }             //異動日期
        public string MODTIME { get; set; }             //異動時間
        public string MODUSER { get; set; }             //異動人員

    }
}
