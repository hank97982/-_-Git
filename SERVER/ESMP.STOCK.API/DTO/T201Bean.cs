using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.DTO
{
    //指定不現沖檔
    public class T201Bean
    {
        [Column("TDate")]
        public string? TDATE { get; set; }      //交易日
        [Column("BhNo")]
        public string? BHNO { get; set; }       //分公司
        [Column("CSeq")]
        public string? CSEQ { get; set; }       //客戶帳號
        [Column("DSEQ")]
        public string? DSEQ { get; set; }       //委託書號
        [Column("Stock")]
        public string? STOCK { get; set; }      //股票代號
        [Column("Price")]
        public decimal Price { get; set; }      //委託價
        [Column("TrDate")]
        public string? TrDate { get; set; }     //轉檔日期
        [Column("TrTime")]
        public string? TrTime { get; set; }     //轉檔時間
        [Column("ModDate")]
        public string? ModDate { get; set; }    //更新日期
        [Column("ModTime")]
        public string? ModTime { get; set; }    //更新時間
        [Column("ModUser")]
        public string? ModUser { get; set; }    //更新人員
    }
}
