using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.DTO
{
    public class MCSRHBean
    {
        public string BHNO { get; set; }                //分公司別
        public string CSEQ { get; set; }                //客戶帳號
        public string STOCK { get; set; }               //股票代碼
        public decimal CNQBAL { get; set; }             //昨日集保庫存
        public decimal CRAQTY { get; set; }             //昨日代辦融資庫存
        public decimal CROQTY { get; set; }             //昨日自辦融資庫存
        public decimal DBAQTY { get; set; }             //昨日代辦融券庫存
        public decimal DBOQTY { get; set; }             //昨日自辦融券庫存
        public string CLDATE { get; set; }              //結帳日期
        public string TRDATE { get; set; }              //轉檔日期
        public string TRTIME { get; set; }              //轉檔時間
        public string MODDATE { get; set; }             //異動日期
        public string MODTIME { get; set; }             //異動時間
        public string MODUSER { get; set; }             //異動人員
        public decimal CNQCNQBAL_DEBAL { get; set; }    //現股借貸股數
    }
}
