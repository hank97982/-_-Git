using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.DTO
{
    //歷史融券沖銷檔
    public class HDBRHBean
    {
        [Column("BHNO")]
        public string? BHNO { get; set; }
        [Column("TDATE")]
        public string? TDATE { get; set; }
        [Column("DSEQ")]
        public string? DSEQ { get; set; }
        [Column("DNO")]
        public string? DNO { get; set; }
        [Column("RDATE")]
        public string? RDATE { get; set; }
        [Column("RDSEQ")]
        public string? RDSEQ { get; set; }
        [Column("RDNO")]
        public string? RDNO { get; set; }
        [Column("CSEQ")]
        public string? CSEQ { get; set; }
        [Column("RCODE")]
        public string? RCODE { get; set; }
        [Column("STOCK")]
        public string? STOCK { get; set; }
        [Column("SPRICE")]
        public decimal SPRICE { get; set; }
        [Column("SQTY")]
        public decimal SQTY { get; set; }
        [Column("CQTY")]
        public decimal CQTY { get; set; }
        [Column("CDBAMT")]
        public decimal CDBAMT { get; set; }
        [Column("CGTAMT")]
        public decimal CGTAMT { get; set; }
        [Column("CGTINT")]
        public decimal CGTINT { get; set; }
        [Column("CDNAMT")]
        public decimal CDNAMT { get; set; }
        [Column("CDNINT")]
        public decimal CDNINT { get; set; }
        [Column("CDLFEE")]
        public decimal CDLFEE { get; set; }
        [Column("LBFINT")]
        public decimal LBFINT { get; set; }
        [Column("SFEE")]
        public decimal SFEE { get; set; }
        [Column("TAX")]
        public decimal TAX { get; set; }
        [Column("DBFEE")]
        public decimal DBFEE { get; set; }
        [Column("BPRICE")]
        public decimal BPRICE { get; set; }
        [Column("BQTY")]
        public decimal BQTY { get; set; }
        [Column("BFEE")]
        public decimal BFEE { get; set; }
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
        [Column("HEALTHFEE")]
        public decimal HEALTHFEE { get; set; }
        [Column("TRDATE")]
        public string? TRDATE { get; set; }
        [Column("TRTIME")]
        public string? TRTIME { get; set; }
        [Column("MODDATE")]
        public string? MODDATE { get; set; }
        [Column("MODTIME")]
        public string? MODTIME { get; set; }

    }
}
