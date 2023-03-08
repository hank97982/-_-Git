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
    class SingletonQueryProviderMSTMB
    {
        private Dictionary<string, MSTMBBean>? _query = null;
        private SingletonQueryProviderMSTMB()
        {
            IEnumerable<MSTMBBean> Bean = new List<MSTMBBean>();
            Dapper.SqlMapper.SetTypeMap(typeof(MSTMBBean), new ColumnAttributeTypeMapper<MSTMBBean>());
            string sqlCommend = @"SELECT * FROM MSTMB";
            using (var conn = new SqlConnection("Server = .;Database = ESMP;Trusted_Connection=true"))
                Bean = conn.Query<MSTMBBean>(sqlCommend);
            _query = new Dictionary<string, MSTMBBean>();
            Bean.ToList().ForEach(x => _query.Add(x.STOCK ?? "", x));
            
            //foreach (MSTMBBean item in Bean)
            //{
            //    _query.Add(item.STOCK ?? "", item);
            //}
        }
        class Inner
        {
            static Inner()
            {

            }
            internal static readonly SingletonQueryProviderMSTMB Instance = new SingletonQueryProviderMSTMB();
        }
        public static SingletonQueryProviderMSTMB queryProvider
        {
            get { return Inner.Instance; }
            //set {  }
        }

        //針對stetic的key值進行value的查詢
        public string MSTMBQueryCNAME(string STOCK)
        {
            return STOCK == null ? "" : _query[STOCK].CNAME;
        }

        public decimal MSTMBQueryCPRICE(string STOCK)
        {
            return STOCK == null ? 0 : _query[STOCK].CPRICE;
        }

        public string MSTMBQueryCNTDTYPE(string STOCK)
        {
            return STOCK == null ? "" : _query[STOCK].CNTDTYPE;
        }
    }
}
