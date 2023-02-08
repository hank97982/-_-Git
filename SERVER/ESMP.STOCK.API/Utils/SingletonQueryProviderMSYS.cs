using Dapper;
using ESMP.STOCK.API.DTO;
using ESMP.STOCK.API.QUERYAPI;
using SERVER.Utils;
using System.Configuration;
using System.Data.SqlClient;

namespace ESMP.STOCK.API.Utils
{
    //Singleton 單例模式
    //http://charlesbc.blogspot.com/2009/04/design-pattern-singleton.html
    class SingletonQueryProviderMSYS
    {
        private Dictionary<string, MSYSBean>? _query = null;
        private SingletonQueryProviderMSYS()
        {
            IEnumerable<MSYSBean> Bean = new List<MSYSBean>();
            Dapper.SqlMapper.SetTypeMap(typeof(MSYSBean), new ColumnAttributeTypeMapper<MSYSBean>());
            string sqlCommend = @"SELECT * FROM MSYS";
            var parameters = new DynamicParameters();
            using (var conn = new SqlConnection("Server = .;Database = ESMP;Trusted_Connection=true"))
                Bean = conn.Query<MSYSBean>(sqlCommend, parameters);
            _query = new Dictionary<string, MSYSBean>();
            foreach (MSYSBean item in Bean)
            {
                _query.Add(Convert.ToInt32(item.VARNAME.Replace("IOFLAG", "")).ToString("0000"), item);
            }
        }
        class Inner
        {
            static Inner()
            {

            }
            internal static readonly SingletonQueryProviderMSYS Instance = new SingletonQueryProviderMSYS();
        }
        public static SingletonQueryProviderMSYS queryProvider
        {
            get { return Inner.Instance; }
            //set {  }
        }

        //針對IOFLAG的key值進行value的查詢
        public string MSYSQueryALL(string IOFLAG)
        {
            return IOFLAG == null ? "" : _query[IOFLAG].VARDESC;
        }
    }
}
