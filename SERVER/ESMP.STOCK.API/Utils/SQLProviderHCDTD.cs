using Dapper;
using ESMP.STOCK.API.DTO;
using SERVER.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.Utils
{
    public class SQLProviderHCDTD
    {
        private static string _connstr = "Server = .;Database = ESMP;Trusted_Connection=true;TrustServerCertificate=True";

        public static IEnumerable<HCDTDBean> Where(string Bhno, string Cseq, string Sdate, string Edate, string stockSymble)
        {

            string sqlCommend;
            Dapper.SqlMapper.SetTypeMap(typeof(HCDTDBean), new ColumnAttributeTypeMapper<HCDTDBean>());
            sqlCommend = @"SELECT * FROM HCDTD
                                WHERE BHNO = @BHNO
                                AND CSEQ = @CSEQ
                                AND TDATE >= @Sdate
                                AND TDATE <= @Edate";

            var parameters = new DynamicParameters();
            parameters.Add("BHNO", Bhno, System.Data.DbType.String);
            parameters.Add("CSEQ", Cseq, System.Data.DbType.String);
            parameters.Add("Sdate", Sdate, System.Data.DbType.String);
            parameters.Add("Edate", Edate, System.Data.DbType.String);
            //第三題增加股票代號查詢 StockSymbol
            if (stockSymble != "")
            {
                sqlCommend += " AND STOCK = @STOCK";
                parameters.Add("STOCK", stockSymble, System.Data.DbType.String);
            }
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<HCDTDBean>(sqlCommend, parameters);

        }
    }
}
