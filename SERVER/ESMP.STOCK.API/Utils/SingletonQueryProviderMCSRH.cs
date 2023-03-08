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
    class SingletonQueryProviderMCSRH
    {
        private Dictionary<string, MCSRHBean>? _query = null;
        private SingletonQueryProviderMCSRH()
        {
            IEnumerable<MCSRHBean> Bean = new List<MCSRHBean>();
            Dapper.SqlMapper.SetTypeMap(typeof(MCSRHBean), new ColumnAttributeTypeMapper<MCSRHBean>());
            string sqlCommend = @"SELECT * FROM MCSRH";
            using (var conn = new SqlConnection("Server = .;Database = ESMP;Trusted_Connection=true"))
                Bean = conn.Query<MCSRHBean>(sqlCommend);
            _query = new Dictionary<string, MCSRHBean>();
            Bean.ToList().ForEach(x => _query.Add(x.BHNO + x.CSEQ + x.STOCK ?? "", x));

        }
        class Inner
        {
            static Inner()
            {

            }
            internal static readonly SingletonQueryProviderMCSRH Instance = new SingletonQueryProviderMCSRH();
        }
        public static SingletonQueryProviderMCSRH queryProvider
        {
            get { return Inner.Instance; }
            //set {  }
        }

        public decimal MCSRHQueryCNQBALE(string BHNO, string CSEQ, string stock)
        {
            return BHNO == null && CSEQ == null && stock == null ? 0 : _query[BHNO + CSEQ + stock].CNQBAL;
        }
    }
}
