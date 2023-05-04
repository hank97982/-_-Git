using Dapper;
using ESMP.STOCK.API.Bean;
using SERVER.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.Utils
{
    public class SQLProviderTCNUD
    {
        private static string _connstr = "Server = .;Database = ESMP;Trusted_Connection=true;TrustServerCertificate=True";

        public SQLProviderTCNUD() { }

        public static IEnumerable<TCNUDBean> Where(string Bhno, string Cseq, string stockSymble)
        {

            string sqlCommend;
            Dapper.SqlMapper.SetTypeMap(typeof(TCNUDBean), new ColumnAttributeTypeMapper<TCNUDBean>());
            sqlCommend = @"SELECT * FROM TCNUD
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
                return conn.Query<TCNUDBean>(sqlCommend, parameters);

        }


        //public void Insert()
        //{

        //}
        //public void Update()
        //{

        //}
        //public void Delete()
        //{

        //}

    }
}
