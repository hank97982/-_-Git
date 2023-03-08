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
    class SingletonQueryProviderMCUMS
    {
        private Dictionary<string, MCUMSBean>? _query = null;
        private SingletonQueryProviderMCUMS()
        {
            IEnumerable<MCUMSBean> Bean = new List<MCUMSBean>();
            Dapper.SqlMapper.SetTypeMap(typeof(MCUMSBean), new ColumnAttributeTypeMapper<MCUMSBean>());
            string sqlCommend = @"SELECT * FROM MCUMS";
            using (var conn = new SqlConnection("Server = .;Database = ESMP;Trusted_Connection=true"))
                Bean = conn.Query<MCUMSBean>(sqlCommend);
            _query = new Dictionary<string, MCUMSBean>();
            Bean.ToList().ForEach(x => _query.Add(x.BHNO + x.CSEQ ?? "", x));


        }
        class Inner
        {
            static Inner()
            {

            }
            internal static readonly SingletonQueryProviderMCUMS Instance = new SingletonQueryProviderMCUMS();
        }
        public static SingletonQueryProviderMCUMS queryProvider
        {
            get { return Inner.Instance; }
            //set {  }
        }

        public string MCUMSQueryCNTDTYPE(string BHNO, string CSEQ)
        {
            return BHNO == null && CSEQ == null ? "" : _query[BHNO + CSEQ].CNTDTYPE;
        }
    }
}
