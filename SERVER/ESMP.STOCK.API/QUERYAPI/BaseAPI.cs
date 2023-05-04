using Dapper;
using ESMP.STOCK.API.Bean;
using ESMP.STOCK.API.DTO;
using ESMP.STOCK.API.DTO.RealizedProfitAndLoss;
using ESMP.STOCK.API.DTO.Statement;
using ESMP.STOCK.API.DTO.UnrealizedGainsAndlLosses;
using ESMP.STOCK.API.Utils;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using SERVER.Utils;
using System.Data.SqlClient;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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
                switch (dox.Descendants().Where(n => n.Name == "qtype").FirstOrDefault().Value)
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

        public async virtual Task<List<Symbol>> QuoteAsync(List<string> list)
        {
            //api一次性極限承載量
            //共 1625 筆股票代碼;
            //字串大小共 8174 ;
            int maxByteSize = 8175;
            List<QuoteBean> result = new List<QuoteBean>();
            string sendStr;
            HttpResponseMessage response;
            HttpClient client;
            string responseString;
            List<Symbol> ans = new List<Symbol>();
            string strUrl = "http://10.10.56.182:8080/Quote/Stock.jsp?stock=";


            #region
            //while (list.Count > 0)
            //{
            //    List<string> strings = new List<string>();
            //    while (Encoding.ASCII.GetBytes(string.Format("http://10.10.56.182:8080/Quote/Stock.jsp?stock={0}", string.Join(",", strings.ToArray()))).Length < maxByteSize)
            //    {
            //        strings.Add(list[count]);
            //        count++;
            //    }

            //    count--;
            //    result = new List<QuoteBean>();
            //    client = new HttpClient();
            //    sendStr = string.Format("http://10.10.56.182:8080/Quote/Stock.jsp?stock={0}", string.Join(",", list.GetRange(0, count).ToArray()));
            //    response = await client.GetAsync(sendStr);
            //    responseString = response.Content.ReadAsStringAsync().Result;
            //    result.Add(Util.Deserialize<QuoteBean>(responseString));
            //    ans.AddRange(result.Select(x => x.SymbolList).ToList().First());
            //    list.RemoveRange(0, count);
            //}
            #endregion

            #region
            //while (list.Count > 0)
            //{
            //    List<string> strings = new List<string>();
            //    int sizeCount = 0;

            //    //累加股票代碼至url裡驗證是否大於最大長度，直到否的話無視右邊判斷直接送出請求&&累加資料等於或小於實際資料，直到否的話無視左邊判斷直接送出請求
            //    while (string.Format(strUrl + "{0}", string.Join(",", strings.ToArray())).Length < maxByteSize && sizeCount < list.Count)
            //    {
            //        strings.Add(list[sizeCount]);
            //        sizeCount++;
            //    }

            //    if (string.Format(strUrl + "{0}", string.Join(",", list.ToArray())).Length > maxByteSize)
            //    {
            //        sizeCount--;    //累加資料與限制資料比對後扣除累加資料的多項
            //    }

            //    result = new List<QuoteBean>();
            //    client = new HttpClient();
            //    sendStr = string.Format(strUrl + "{0}", string.Join(",", list.GetRange(0, sizeCount).ToArray()));
            //    response = await client.GetAsync(sendStr);
            //    responseString = response.Content.ReadAsStringAsync().Result;
            //    result.Add(Util.Deserialize<QuoteBean>(responseString));
            //    ans.AddRange(result.Select(x => x.SymbolList).ToList().First());
            //    list.RemoveRange(0, sizeCount);

            //}
            #endregion

            #region
            //while (list.Count > 0)
            //{
            //    string strUrlValue = "";

            //    //累加股票代碼至url裡驗證是否大於最大長度，直到否的話無視右邊判斷直接送出請求&&累加資料等於或小於實際資料，直到否的話無視左邊判斷直接送出請求
            //    while (string.Format(strUrl + "{0}", string.Join(",", list.ToArray())).Length < maxByteSize)
            //    {

            //    }
            //    //if (string.Format(strUrl + "{0}", string.Join(",", list.ToArray())).Length > maxByteSize)
            //    //{

            //    //}

            //    result = new List<QuoteBean>();
            //    client = new HttpClient();
            //    sendStr = string.Format(strUrl + "{0}", strUrlValue);
            //    response = await client.GetAsync(sendStr);
            //    responseString = response.Content.ReadAsStringAsync().Result;
            //    result.Add(Util.Deserialize<QuoteBean>(responseString));
            //    ans.AddRange(result.Select(x => x.SymbolList).ToList().First());

            //}
            #endregion

            #region
            //List<string> strings = new List<string>();

            //string urlpath = "http://10.10.56.182:8080/Quote/Stock.jsp?stock=";
            //string urlStr = string.Format(urlpath + "{0}", string.Join(",", list.ToArray()));
            //char[] trimChars = { ',' };
            //string resultUrl = "";
            //bool flag = false;

            //while (flag == false)
            //{

            //    if (maxByteSize < urlStr.Length)
            //    {
            //        int separatorIndex = urlStr.LastIndexOf(",", maxByteSize);//會出現娶不到逗號的情況
            //        resultUrl = urlStr.Substring(0, separatorIndex);
            //        urlStr = urlpath + urlStr.Substring(resultUrl.Length).Trim(trimChars);
            //    }
            //    else if (maxByteSize > urlStr.Length)
            //    {
            //        resultUrl = urlStr;
            //        flag = true;
            //    }
            //    result = new List<QuoteBean>();
            //    client = new HttpClient();
            //    response = await client.GetAsync(resultUrl);
            //    responseString = response.Content.ReadAsStringAsync().Result;
            //    result.Add(Util.Deserialize<QuoteBean>(responseString));
            //    ans.AddRange(result.Select(x => x.SymbolList).ToList().First());
            //};
            #endregion

            List<string> strings = new List<string>();

            string urlpath = "http://10.10.56.182:8080/Quote/Stock.jsp?stock=";
            int urlPathLength = urlpath.Length;
            string urlStr = string.Join(",", list.ToArray());
            string resultUrl = "";
            List<string> urls = new List<string>();
            char[] trimChars = { ',' };
            do
            {
                result = new List<QuoteBean>();
                client = new HttpClient();
                if (maxByteSize < string.Format(urlpath + "{0}", urlStr).Length)
                {
                    int separatorIndex = string.Format(urlpath + "{0}", urlStr).LastIndexOf(",", maxByteSize);//會出現娶不到逗號的情況
                    resultUrl = string.Format(urlpath + "{0}", urlStr).Substring(0, separatorIndex);
                    urlStr = string.Format(urlpath + "{0}", urlStr).Substring(resultUrl.Length).Trim(trimChars);

                }
                else
                {
                    resultUrl = string.Format(urlpath + "{0}", urlStr);
                    urlStr = string.Format(urlpath + "{0}", urlStr).Substring(resultUrl.Length).Trim(trimChars);
                    //urlStr = "";
                }
                response = await client.GetAsync(resultUrl);
                responseString = response.Content.ReadAsStringAsync().Result;
                result.Add(Util.Deserialize<QuoteBean>(responseString));
                ans.AddRange(result.Select(x => x.SymbolList).ToList().First());
            } while (string.Format(urlpath + "{0}", urlStr).Length > urlPathLength);
            return ans;
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

        protected IEnumerable<TCNTDBean> QueryTCNTD()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(TCNTDBean), new ColumnAttributeTypeMapper<TCNTDBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<TCNTDBean>(@"SELECT * FROM TCNTD");
        }

        protected IEnumerable<T201Bean> QueryT201()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(T201Bean), new ColumnAttributeTypeMapper<T201Bean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<T201Bean>(@"SELECT * FROM t201");
        }

        protected IEnumerable<TCRUDBean> QueryTCRUD()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(TCRUDBean), new ColumnAttributeTypeMapper<TCRUDBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<TCRUDBean>(@"SELECT * FROM TCRUD");
        }

        protected virtual IEnumerable<TDBUDBean> QueryTDBUD()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(TDBUDBean), new ColumnAttributeTypeMapper<TDBUDBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<TDBUDBean>(@"SELECT * FROM TDBUD");
        }

        protected IEnumerable<HCRRHBean> QueryHCRRH()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(HCRRHBean), new ColumnAttributeTypeMapper<HCRRHBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<HCRRHBean>(@"SELECT * FROM HCRRH");
        }

        protected IEnumerable<HDBRHBean> QueryHDBRH()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(HDBRHBean), new ColumnAttributeTypeMapper<HDBRHBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<HDBRHBean>(@"SELECT * FROM HDBRH");
        }

        protected IEnumerable<HCDTDBean> QueryHCDTD()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(HCDTDBean), new ColumnAttributeTypeMapper<HCDTDBean>());
            using (var conn = new SqlConnection(_connstr))
                return conn.Query<HCDTDBean>(@"SELECT * FROM HCDTD");
        }
    }
}
