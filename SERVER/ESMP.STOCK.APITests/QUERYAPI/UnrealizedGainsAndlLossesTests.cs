using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESMP.STOCK.API.QUERYAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESMP.STOCK.API.Bean;
using ESMP.STOCK.API.DTO;

namespace ESMP.STOCK.API.QUERYAPI.Tests
{
    [TestClass()]
    public class UnrealizedGainsAndlLossesTests
    {
        private UnrealizedGainsAndlLosses _unrealizedGainsAndlLosses = new UnrealizedGainsAndlLosses("Server = .;Database = ESMP;Trusted_Connection=true");
        //1.	有昨日現股餘額、今日全部賣出－檢核(Ram)TCNUD與(Ram)HCNRH資料是否如預期
        [TestMethod()]
        public void TMHIOWriteOffTest()
        {
            
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20160504", BHNO = "5920", CSEQ = "0126687", DSEQ = "k8563", STOCK = "2330", PRICE = 147, QTY = 1000, BQTY = 1000, FEE = 78, COST = 147.078m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20160509", BHNO = "5920", CSEQ = "0126687", DSEQ = "k0254", STOCK = "2330", PRICE = 148.5m, QTY = 1000, BQTY = 1000, FEE = 79, COST = 148.579m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20160509", BHNO = "5920", CSEQ = "0126687", DSEQ = "k0266", STOCK = "2330", PRICE = 148.5m, QTY = 1000, BQTY = 1000, FEE = 79, COST = 148.579m });
            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592a" , DSEQ = "j0614" , JRNUM = "01413598", MTYPE = "T" , CSEQ = "0424032" , TTYPE = "0", ETYPE = "0" , BSTYPE = "S" , STOCK = "2330" ,QTY = 1000,PRICE = 399.5m,SALES = "0071",ORGIN = "1",MTIME = "114543715"});
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592a" , DSEQ = "j0614" , JRNUM = "01413599", MTYPE = "T" , CSEQ = "0424032" , TTYPE = "0", ETYPE = "0" , BSTYPE = "S" , STOCK = "2330" ,QTY = 1000,PRICE = 399.5m,SALES = "0071",ORGIN = "1",MTIME = "114553161"});

            List<TCNUDBean> check = new List<TCNUDBean>();
            check=_unrealizedGainsAndlLosses.TMHIOWriteOff(tCNUDs, tMHIOs);

            Assert.AreEqual(1, check.Count);

        }
        //2.	有昨日現股餘額、今日部分賣出（需部分沖銷）－檢核(Ram)TCNUD與(Ram)HCNRH資料是否如預期
        [TestMethod()]
        public void TMHIOWriteOffTest1()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131028", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 0, QTY = 138, BQTY = 138, FEE = 0, COST = 0 });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131106", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 84.7m, QTY = 1000, BQTY = 1000, FEE = 120, COST = 84.820m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131205", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 65.5m, QTY = 1000, BQTY = 1000, FEE = 93, COST = 65.593m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131206", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 66.6m, QTY = 862, BQTY = 862, FEE = 81, COST = 57.489m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140521", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 62.2m, QTY = 1000, BQTY = 1000, FEE = 88, COST = 62.288m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50.872m });
            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "amPM0", JRNUM = "02844984", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 27.5m, SALES = "0085", ORGIN = "0", MTIME = "300106186" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "amPM2", JRNUM = "02844985", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 27.8m, SALES = "0085", ORGIN = "0", MTIME = "300106187" });

            List<TCNUDBean> check = new List<TCNUDBean>();
            check = _unrealizedGainsAndlLosses.TMHIOWriteOff(tCNUDs, tMHIOs);

            Assert.AreEqual(4, check.Count);
            
            Assert.AreEqual(138, check.First().BQTY);
            Assert.AreEqual(13m, check.First().FEE);
            Assert.AreEqual(9052, check.First().COST);
        }
    }
}