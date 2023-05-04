using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.DTO
{
    //歷史信用沖銷檔
    public class HCDTDBean
    {
        [Column("TDATE")]
        public string? TDATE { get; set; }
        [Column("BHNO")]
        public string? BHNO { get; set; }
        [Column("CRDSEQ")]
        public string? CRDSEQ { get; set; }
        [Column("CRDNO")]
        public string? CRDNO { get; set; }
        [Column("DBDSEQ")]
        public string? DBDSEQ { get; set; }
        [Column("DBDNO")]
        public string? DBDNO { get; set; }
        [Column("CSEQ")]
        public string? CSEQ { get; set; }
        [Column("STOCK")]
        public string? STOCK { get; set; }
        [Column("QTY")]
        public decimal QTY { get; set; }
        [Column("BPRICE")]
        public decimal BPRICE { get; set; }
        [Column("BQTY")]
        public decimal BQTY { get; set; }
        [Column("BFEE")]
        public decimal BFEE { get; set; }
        [Column("SPRICE")]
        public decimal SPRICE { get; set; }
        [Column("SQTY")]
        public decimal SQTY { get; set; }
        [Column("SFEE")]
        public decimal SFEE { get; set; }
        [Column("TAX")]
        public decimal TAX { get; set; }
        [Column("DBFEE")]
        public decimal DBFEE { get; set; }
        [Column("INCOME")]
        public decimal INCOME { get; set; }
        [Column("COST")]
        public decimal COST { get; set; }
        [Column("PROFIT")]
        public decimal PROFIT { get; set; }
        [Column("SFCODE")]
        public string? SFCODE { get; set; }
        [Column("STINTAX")]
        public decimal STINTAX { get; set; }
        [Column("TRDATE")]
        public string? TRDATE { get; set; }
        [Column("TRTIME")]
        public string? TRTIME { get; set; }
        [Column("MODDATE")]
        public string? MODDATE { get; set; }
        [Column("MODTIME")]
        public string? MODTIME { get; set; }
        [Column("MODUSER")]
        public string? MODUSER { get; set; }
    }
}
