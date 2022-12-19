using System.ComponentModel.DataAnnotations.Schema;


namespace ESMP.STOCK.API.DTO
{
    //MSYS 系統設定檔
    public class MSYSBean
    {
        [Column("VARNAME")]
        public string? VARNAME { get; set; }        //系統變數名稱
        [Column("NUMBER")]
        public decimal NUMBER { get; set; }         //序號
        [Column("VALUE")]
        public string? VALUE { get; set; }          //設定值
        [Column("VARDESC")]
        public string? VARDESC { get; set; }        //說明
        [Column("MODDATE")]
        public string? MODDATE { get; set; }        //異動日期
        [Column("MODTIME")]
        public string? MODTIME { get; set; }        //異動時間
        [Column("MODUSER")]
        public string? MODUSER { get; set; }        //異動人員

    }
}
