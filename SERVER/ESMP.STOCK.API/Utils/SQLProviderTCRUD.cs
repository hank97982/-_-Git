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
    public class SQLProviderTCRUD
    {
        private static string _connstr = "Server = .;Database = ESMP;Trusted_Connection=true;TrustServerCertificate=True";

        public static IEnumerable<TCRUDBean> Where(string Bhno, string Cseq, string stockSymble)
        {

            string sqlCommend;
            Dapper.SqlMapper.SetTypeMap(typeof(TCRUDBean), new ColumnAttributeTypeMapper<TCRUDBean>());
            sqlCommend = @"SELECT * FROM TCRUD
                                WHERE BHNO = @BHNO
                                AND CSEQ = @CSEQ";

            var parameters = new DynamicParameters();
            parameters.Add("BHNO", Bhno, System.Data.DbType.String);
            parameters.Add("CSEQ", Cseq, System.Data.DbType.String);
            //第三題增加股票代號查詢 StockSymbol
            if (stockSymble != "")
            {
                sqlCommend += " AND STOCK = @STOCK";
                parameters.Add("STOCK", stockSymble, System.Data.DbType.String);
            }
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<TCRUDBean>(sqlCommend, parameters);

        }
    }
}
