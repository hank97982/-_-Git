using ESMP.STOCK.API.DTO.RealizedProfitAndLoss;
using ESMP.STOCK.API.DTO.Statement;
using ESMP.STOCK.API.DTO.UnrealizedGainsAndlLosses;
using ESMP.STOCK.API.QUERYAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.QUERYAPI.Tests
{
    [TestClass()]
    public class SQLConnectTest
    {
        [TestMethod()]
        public void GetQuerysTest()
        {

            new UnrealizedGainsAndlLosses("Server = .;Database = ESMP;Trusted_Connection=true;TrustServerCertificate=True")
                .GetQuerys(new UnrealizedGainsAndlLossesDTO() {Ttype= "A", Bhno = "592S",Cseq = "0034661",StockSymbol= "1303" });
            new RealizedProfitAndLoss("Server = .;Database = ESMP;Trusted_Connection=true;TrustServerCertificate=True")
                .GetQuerys(new RealizedProfitAndLossDTO() { Ttype = "A", Bhno = "592S", Cseq = "0034661", StockSymbol = "1303" });
            new Statement("Server = .;Database = ESMP;Trusted_Connection=true;TrustServerCertificate=True")
                .GetQuerys(new StatementDTO() { Bhno = "592S", Cseq = "0034661", StockSymbol = "1303" });
        }
    }
}


