using Dapper;
using ESMP.STOCK.API.Bean;
using ESMP.STOCK.API.DTO;
using ESMP.STOCK.API.DTO.RealizedProfitAndLoss;
using ESMP.STOCK.API.DTO.Statement;
using ESMP.STOCK.API.DTO.UnrealizedGainsAndlLosses;
using ESMP.STOCK.API.Utils;
using Newtonsoft.Json.Linq;
using SERVER.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ESMP.STOCK.API.QUERYAPI
{
    public class BaseAPI
    {


        private static string _connstr = "";
        public BaseAPI(string connstr)
        {
            _connstr = connstr;
            TMHIO_TDATE_UPDATE();
        }
        private void TMHIO_TDATE_UPDATE()
        {
            using (var connection = new SqlConnection(_connstr))
            {
                string statement = @"UPDATE TMHIO SET TDATE = @TDATE;";
                var parameters = new DynamicParameters();
                parameters.Add("TDATE", DateTime.Now.ToString("yyyyMMdd"), System.Data.DbType.String);
                int result = connection.Execute(statement, parameters);
            }
        }


        public string Receiver(string str)
        {
            if (Util.TryParseJson(str))
            {
                JObject re = JObject.Parse(str);
                switch ((string)re["qtype"])
                {

                    case "0001":
                        UnrealizedGainsAndlLosses unrealized = new UnrealizedGainsAndlLosses(_connstr);

                        return ServerSend(unrealized.GetQuerys(JsonSerializer.Deserialize<UnrealizedGainsAndlLossesDTO>(str)), ConnectTypeItems.Json.GetHashCode());
                    case "0002":
                        RealizedProfitAndLoss realized = new RealizedProfitAndLoss(_connstr);

                        return ServerSend(realized.GetQuerys(JsonSerializer.Deserialize<RealizedProfitAndLossDTO>(str)), ConnectTypeItems.Json.GetHashCode());
                    case "0003":
                        Statement statement = new Statement(_connstr);

                        return ServerSend(statement.GetQuerys(JsonSerializer.Deserialize<StatementDTO>(str)), ConnectTypeItems.Json.GetHashCode());
                }
            }
            if (Util.TryParseXml(str))
            {
                XDocument dox = XDocument.Parse(str);
                switch (dox.Descendants().Where(n=> n.Name == "qtype").FirstOrDefault().Value)
                {
                    case "0001":
                        UnrealizedGainsAndlLosses unrealized = new UnrealizedGainsAndlLosses(_connstr);

                        return ServerSend(unrealized.GetQuerys(Util.Deserialize<UnrealizedGainsAndlLossesDTO>(str)), ConnectTypeItems.Xml.GetHashCode());
                    case "0002":
                        RealizedProfitAndLoss realized = new RealizedProfitAndLoss(_connstr);

                        return ServerSend(realized.GetQuerys(Util.Deserialize<RealizedProfitAndLossDTO>(str)), ConnectTypeItems.Xml.GetHashCode());
                    case "0003":
                        Statement statement = new Statement(_connstr);

                        return ServerSend(statement.GetQuerys(Util.Deserialize<StatementDTO>(str)), ConnectTypeItems.Xml.GetHashCode());
                }
            }
            return null;
        }
        public string ServerSend(object accsum, int TypeItems)
        {

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };

            if (TypeItems == 0)
            {

                return JsonSerializer.Serialize(accsum, options);
            }
            if (TypeItems == 1)
            {
                return Util.Serialize(accsum);
            }
            return null;
        }

        protected IEnumerable<TCNUDBean> QueryTCNUD()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(TCNUDBean), new ColumnAttributeTypeMapper<TCNUDBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<TCNUDBean>(@"SELECT * FROM TCNUD");
        }

        protected IEnumerable<TMHIOBean> QueryTMHIO()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(TMHIOBean), new ColumnAttributeTypeMapper<TMHIOBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<TMHIOBean>(@"SELECT * FROM TMHIO");
        }

        protected IEnumerable<TCSIOBean> QueryTCSIO()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(TCSIOBean), new ColumnAttributeTypeMapper<TCSIOBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<TCSIOBean>(@"SELECT * FROM TCSIO");
        }

        protected IEnumerable<HCMIOBean> QueryHCMIO()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(HCMIOBean), new ColumnAttributeTypeMapper<HCMIOBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<HCMIOBean>(@"SELECT * FROM HCMIO");
        }

        protected IEnumerable<HCNRHBean> QueryHCNRH()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(HCNRHBean), new ColumnAttributeTypeMapper<HCNRHBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<HCNRHBean>(@"SELECT * FROM HCNRH");
        }

        protected IEnumerable<HCNTDBean> QueryHCNTD()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(HCNTDBean), new ColumnAttributeTypeMapper<HCNTDBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<HCNTDBean>(@"SELECT * FROM HCNTD");
        }

        protected IEnumerable<MCUMSBean> QueryMCUMS()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(MCUMSBean), new ColumnAttributeTypeMapper<MCUMSBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<MCUMSBean>(@"SELECT * FROM MCUMS");
        }
    }
}
