using Dapper;
using ESMP.STOCK.API.Bean;
using ESMP.STOCK.API.DTO;
using ESMP.STOCK.API.DTO.UnrealizedGainsAndlLosses;
using ESMP.STOCK.API.Utils;
using SERVER.Utils;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace ESMP.STOCK.API.QUERYAPI
{
    //未實現損益查詢 UnrealizedGainsAndlLosses

    public class UnrealizedGainsAndlLosses : BaseAPI
    {
        private string _connstr = "";
        public UnrealizedGainsAndlLosses(string connstr) : base(connstr)
        {
            _connstr = connstr;
        }
        public object GetQuerys(UnrealizedGainsAndlLossesDTO root)
        {
            IEnumerable<TCNUDBean> tCNUDBean = new List<TCNUDBean>();
            IEnumerable<TMHIOBean> tMHIOBean = new List<TMHIOBean>();
            IEnumerable<TCSIOBean> tCSIOBean = new List<TCSIOBean>();
            IEnumerable<TCNTDBean> tcntdBean = new List<TCNTDBean>();
            IEnumerable<T201Bean> t201Bean = new List<T201Bean>();
            IEnumerable<TCRUDBean> tCRUDBean = new List<TCRUDBean>();
            IEnumerable<TDBUDBean> tDBUDBean = new List<TDBUDBean>();
            #region



            //try
            //{
            //    string sqlCommend;
            //    Dapper.SqlMapper.SetTypeMap(typeof(TCNUDBean), new ColumnAttributeTypeMapper<TCNUDBean>());
            //    sqlCommend = @"SELECT * FROM TCNUD
            //                WHERE BHNO = @BHNO
            //                AND CSEQ = @CSEQ";
            //    var parameters = new DynamicParameters();
            //    parameters.Add("BHNO", root.Bhno, System.Data.DbType.String);
            //    parameters.Add("CSEQ", root.Cseq, System.Data.DbType.String);
            //    if (root.StockSymbol != "")
            //    {
            //        sqlCommend += " AND STOCK = @STOCK";
            //        parameters.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
            //    }
            //    using (var conn = new SqlConnection(_connstr))
            //        tCNUDBean = conn.Query<TCNUDBean>(sqlCommend, parameters);

            //    string sqlCommend2;
            //    Dapper.SqlMapper.SetTypeMap(typeof(TMHIOBean), new ColumnAttributeTypeMapper<TMHIOBean>());

            //    sqlCommend2 = @"SELECT * FROM TMHIO
            //                WHERE BHNO = @BHNO
            //                AND CSEQ = @CSEQ";
            //    var parameters2 = new DynamicParameters();
            //    parameters2.Add("BHNO", root.Bhno, System.Data.DbType.String);
            //    parameters2.Add("CSEQ", root.Cseq, System.Data.DbType.String);
            //    //第三題增加股票代號查詢 StockSymbol
            //    if (root.StockSymbol != "")
            //    {
            //        sqlCommend2 += " AND STOCK = @STOCK";
            //        parameters2.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
            //    }
            //    using (var conn = new SqlConnection(_connstr))
            //        tMHIOBean = conn.Query<TMHIOBean>(sqlCommend2, parameters2);


            //    //Dapper.SqlMapper.SetTypeMap(typeof(MSTMBBean), new ColumnAttributeTypeMapper<MSTMBBean>());
            //    //string sqlCommend3 = @"SELECT * FROM MSTMB";
            //    //var parameters3 = new DynamicParameters();
            //    //using (var conn = new SqlConnection(_connstr))
            //    //    mSTMBBean = conn.Query<MSTMBBean>(sqlCommend3, parameters3);


            //    string sqlCommend4;
            //    Dapper.SqlMapper.SetTypeMap(typeof(TCSIOBean), new ColumnAttributeTypeMapper<TCSIOBean>());

            //    sqlCommend4 = @"SELECT * FROM TCSIO
            //                WHERE BHNO = @BHNO
            //                AND CSEQ = @CSEQ";
            //    var parameters4 = new DynamicParameters();
            //    parameters4.Add("BHNO", root.Bhno, System.Data.DbType.String);
            //    parameters4.Add("CSEQ", root.Cseq, System.Data.DbType.String);
            //    //第三題增加股票代號查詢 StockSymbol
            //    if (root.StockSymbol != "")
            //    {
            //        sqlCommend4 += " AND STOCK = @STOCK";
            //        parameters4.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
            //    }
            //    using (var conn = new SqlConnection(_connstr))
            //        tCSIOBean = conn.Query<TCSIOBean>(sqlCommend4, parameters4);

            //    string sqlCommend5;
            //    Dapper.SqlMapper.SetTypeMap(typeof(TCSIOBean), new ColumnAttributeTypeMapper<TCSIOBean>());

            //    sqlCommend5 = @"SELECT * FROM TCNTD
            //                WHERE BHNO = @BHNO
            //                AND CSEQ = @CSEQ";
            //    var parameters5 = new DynamicParameters();
            //    parameters5.Add("BHNO", root.Bhno, System.Data.DbType.String);
            //    parameters5.Add("CSEQ", root.Cseq, System.Data.DbType.String);
            //    //第三題增加股票代號查詢 StockSymbol
            //    if (root.StockSymbol != "")
            //    {
            //        sqlCommend5 += " AND STOCK = @STOCK";
            //        parameters5.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
            //    }
            //    using (var conn = new SqlConnection(_connstr))
            //        tCSIOBean = conn.Query<TCSIOBean>(sqlCommend5, parameters5);

            //    string sqlCommend6;
            //    Dapper.SqlMapper.SetTypeMap(typeof(TCSIOBean), new ColumnAttributeTypeMapper<TCSIOBean>());

            //    sqlCommend6 = @"SELECT * FROM TCNTD
            //                WHERE BHNO = @BHNO
            //                AND CSEQ = @CSEQ";
            //    var parameters6 = new DynamicParameters();
            //    parameters6.Add("BHNO", root.Bhno, System.Data.DbType.String);
            //    parameters6.Add("CSEQ", root.Cseq, System.Data.DbType.String);
            //    //第三題增加股票代號查詢 StockSymbol
            //    if (root.StockSymbol != "")
            //    {
            //        sqlCommend6 += " AND STOCK = @STOCK";
            //        parameters5.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
            //    }
            //    using (var conn = new SqlConnection(_connstr))
            //        tCSIOBean = conn.Query<TCSIOBean>(sqlCommend6, parameters6);


            //}
            //catch (Exception ex)
            //{
            //    Utils.Util.Log(ex.ToString());
            //    throw;
            //}
            #endregion


            if (root.Ttype == "0")
            {
                tCNUDBean = SQLProviderTCNUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tMHIOBean = SQLProviderTMHIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCSIOBean = SQLProviderTCSIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tcntdBean = SQLProviderTCNTD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                t201Bean = SQLProviderT201.Where(root.Bhno, root.Cseq, root.StockSymbol);

            }
            else if (root.Ttype == "1")
            {
                tCRUDBean = SQLProviderTCRUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
            }
            else if (root.Ttype == "2")
            {
                tDBUDBean = SQLProviderTDBUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
            }
            else if (root.Ttype == "A")
            {
                tCNUDBean = SQLProviderTCNUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tMHIOBean = SQLProviderTMHIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCSIOBean = SQLProviderTCSIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tcntdBean = SQLProviderTCNTD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                t201Bean = SQLProviderT201.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCRUDBean = SQLProviderTCRUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tDBUDBean = SQLProviderTDBUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
            }

            return QueryIntoFormatString(tCNUDBean, tMHIOBean, tCSIOBean, tcntdBean, t201Bean, tCRUDBean, tDBUDBean);
        }


        public object QueryIntoFormatString(IEnumerable<TCNUDBean> tCNUDBean, IEnumerable<TMHIOBean> tMHIOBean, IEnumerable<TCSIOBean> tCSIOBean, IEnumerable<TCNTDBean> tcntdBean, IEnumerable<T201Bean> t201Bean, IEnumerable<TCRUDBean> tCRUDBean, IEnumerable<TDBUDBean> tDBUDBean)
        {

            try
            {

                //List<TCNUDBean> tMBRam = tCNUDBean.ToList();



                //把TCSIO2(今日匯入)的資料合併進來查
                //foreach (var item in tCSIOBean.Where(c => c.BSTYPE == "B").ToList())
                //{
                //    TCNUDBean tCNUD = new TCNUDBean();
                //    tCNUD.TDATE = item.TDATE;
                //    tCNUD.BHNO = item.BHNO;
                //    tCNUD.CSEQ = item.CSEQ;
                //    tCNUD.STOCK = item.STOCK;
                //    tCNUD.PRICE = 0;
                //    tCNUD.QTY = item.QTY;
                //    tCNUD.BQTY = item.QTY;
                //    tCNUD.FEE = 0;
                //    tCNUD.COST = 0;
                //    tCNUD.DSEQ = item.DSEQ;
                //    tCNUD.DNO = item.DNO;
                //    tCNUD.WTYPE = "A";
                //    tCNUD.IOFLAG = item.IOFLAG;
                //    tMBRam.Add(tCNUD);
                //}


                List<TCNUDBean> tMBRam = new List<TCNUDBean>();

                //把TMHIO的資料跟TCNUD做沖銷產出新的TCNUD餘額


                WriteOff wfTM = new WriteOff();
                (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = wfTM.StockWriteOff(tCNUDBean.ToList(), tMHIOBean.ToList(), tCSIOBean.ToList(), tcntdBean.ToList(), t201Bean.ToList());
                tMBRam = TC;




                //foreach (var item in tMHIOBean)
                //{
                //    TCNUDBean tCNUD = new TCNUDBean();
                //    tCNUD.TDATE = item.Tdate;
                //    tCNUD.BHNO = item.BHNO;
                //    tCNUD.CSEQ = item.CSEQ;
                //    tCNUD.STOCK = item.STOCK;
                //    tCNUD.PRICE = item.PRICE;
                //    tCNUD.QTY = item.QTY;
                //    tCNUD.BQTY = item.QTY;
                //    tCNUD.AMT = Math.Floor(item.PRICE * item.QTY);
                //    tCNUD.FEE = FeeCalculate(item.QTY, Math.Floor(tCNUD.AMT * 0.001425m));
                //    tCNUD.COST = tCNUD.AMT + tCNUD.FEE;
                //    tCNUD.DSEQ = item.DSEQ;
                //    tCNUD.DNO = item.JRNUM;
                //    tCNUD.WTYPE = "0";
                //    tMBRam.Add(tCNUD);
                //}


                //=================================================
                //if (tMBRam.Count() == 0)
                //{
                //    AccsumErr accsumErr = new AccsumErr();
                //    accsumErr.Errcode = "0008";
                //    accsumErr.Errmsg = "查無符合設定條件之資料";
                //    return accsumErr;
                //}

                List<Sum> sums = new List<Sum>();
                List<Detail> details;
                Dictionary<string, Symbol> keyValuePairs;
                List<Symbol> s;

                #region 現股
                var sumsLQ = (from d in tMBRam
                              group d by new { d.STOCK, type = d.BQTY < 0 ? "S" : "B" } into g
                              select new { Stock = g.Key.STOCK, type = g.Key.type });

                s = QuoteAsync(sumsLQ.Select(x => x.Stock).ToList()).Result;
                keyValuePairs = new Dictionary<string, Symbol>();
                s.ForEach(x => keyValuePairs.Add(x.Id, x));

                foreach (var item in sumsLQ)
                {
                    //這裡是第二層
                    Sum sum = new Sum();
                    sum.Stock = item.Stock;


                    details = new List<Detail>();
                    var detailsLQ = from t in tMBRam
                                    where t.STOCK == sum.Stock
                                    select t;
                    decimal Lastprice = Convert.ToDecimal(keyValuePairs[sum.Stock].DealPrice);

                    foreach (var item1 in detailsLQ)
                    {
                        decimal tempMarketValue = 0m;
                        //這裡是第三層
                        Detail detail = new Detail();
                        detail.Tdate = item1.TDATE;
                        detail.Ttype = "0";
                        if (item1.BQTY < 0)
                        {
                            detail.Ttypename = "現賣";
                            detail.Bstype = "S";
                        }
                        else
                        {
                            detail.Ttypename = "現買";
                            detail.Bstype = "B";
                        }

                        detail.Dseq = item1.DSEQ;
                        detail.Dno = item1.DNO;
                        detail.Bqty = item1.BQTY;
                        detail.Mprice = item1.PRICE;
                        detail.Mamt = item1.BQTY * item1.PRICE;
                        //detail.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                        detail.Lastprice = Lastprice;
                        detail.Fee = item1.FEE < 20 ? 20m : item1.FEE;       //［手續費］計算如小於20時，以20計算

                        detail.Cramt = 0m;
                        detail.Bcramt = 0m;
                        detail.Gtamt = 0m;
                        detail.Bgtamt = 0m;
                        detail.Dnamt = 0m;
                        detail.Bdnamt = 0m;
                        detail.Interest = 0m;
                        detail.Dbfee = 0m;
                        detail.Dlfee = 0m;
                        detail.Dlint = 0m;

                        detail.Cost = item1.COST;
                        detail.EstimateAmt = Math.Floor(detail.Lastprice * detail.Bqty);
                        detail.EstimateFee = Math.Floor(detail.EstimateAmt * 0.001425m);

                        if (item1.BQTY < 0)
                        {
                            detail.Tax = item1.TAXRam;
                            detail.EstimateTax = 0;
                            tempMarketValue = detail.EstimateAmt + detail.EstimateFee;
                            detail.Marketvalue = -1 * tempMarketValue;//前端顯示邏輯現賣的市值為負的
                            detail.Profit = item1.COST - tempMarketValue;
                        }
                        else
                        {
                            detail.Tax = 0;
                            detail.EstimateTax = Math.Floor(detail.EstimateAmt * 0.003m);
                            detail.Marketvalue = detail.EstimateAmt - detail.EstimateFee - detail.EstimateTax;
                            detail.Profit = detail.Marketvalue - item1.COST;
                        }

                        detail.PlRatio = (item1.COST == 0 ? 0 : detail.Profit / item1.COST).ToString();//資料TCSIO的cost必定為零，所以報酬率會出現除以零的錯誤，其視為零報酬率
                        detail.Keeprate = "";
                        detail.Wtype = item1.WTYPE;
                        detail.Ioflag = item1.IOFLAG;//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        detail.Ioname = string.IsNullOrEmpty(item1.IOFLAG) != null ? SingletonQueryProviderMSYS.queryProvider.MSYSQueryALL(item1.IOFLAG) : "";//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        details.Add(detail);
                    }
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(sum.Stock);
                    sum.Ttype = "0";
                    sum.Bstype = item.type;


                    if (item.type == "B")
                    {
                        sum.Ttypename = "現買";
                        sum.Bqty = MCSRHQueryCNQBALE(tMBRam.FirstOrDefault().BHNO, tMBRam.FirstOrDefault().CSEQ, sum.Stock);
                    }
                    else
                    {
                        sum.Ttypename = "現賣";
                        sum.Bqty = 0m;
                    }

                    sum.RealQTY = details.Sum(x => x.Bqty);
                    sum.Cost = details.Sum(x => x.Cost);
                    sum.Avgprice = sum.Cost != 0 ? sum.Bqty / sum.Cost : 0;//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    //sum.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                    sum.Lastprice = Lastprice;
                    sum.Marketvalue = details.Sum(x => x.Marketvalue);
                    sum.EstimateAmt = details.Sum(x => x.EstimateAmt);
                    sum.EstimateFee = details.Sum(x => x.EstimateFee);
                    sum.EstimateTax = details.Sum(x => x.EstimateTax);
                    sum.Profit = details.Sum(x => x.Profit);
                    sum.PlRatio = (sum.Cost != 0 ? ((sum.Profit / sum.Cost) * 1) : 0).ToString();//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    sum.Fee = details.Sum(x => x.Fee);
                    sum.Tax = details.Sum(x => x.Tax);
                    sum.Amt = details.Sum(x => x.Mamt);
                    sum.Cramt = details.Sum(x => x.Cramt);
                    sum.Gtamt = details.Sum(x => x.Gtamt);
                    sum.Bgtamt = details.Sum(x => x.Bgtamt);
                    sum.Dnamt = details.Sum(x => x.Dnamt);
                    sum.Bdnamt = details.Sum(x => x.Bdnamt);
                    sum.Interest = details.Sum(x => x.Interest);
                    sum.Dbfee = details.Sum(x => x.Dbfee);
                    sum.UnoffsetQtypeDetail = details;
                    sums.Add(sum);
                }
                #endregion

                #region 融資
                var sumstCRUDLQ = (from d in tCRUDBean
                                   group d by new { d.STOCK, type = "B" } into g
                                   select new { Stock = g.Key.STOCK, type = g.Key.type });

                s = QuoteAsync(sumstCRUDLQ.Select(x => x.Stock).ToList()).Result;
                keyValuePairs = new Dictionary<string, Symbol>();
                s.ForEach(x => keyValuePairs.Add(x.Id, x));

                foreach (var item in sumstCRUDLQ)
                {
                    //這裡是第二層
                    Sum sum = new Sum();
                    sum.Stock = item.Stock;


                    details = new List<Detail>();
                    var detailsLQ = from t in tCRUDBean
                                    where t.STOCK == sum.Stock
                                    select t;
                    decimal Lastprice = Convert.ToDecimal(keyValuePairs[sum.Stock].DealPrice);

                    foreach (var item1 in detailsLQ)
                    {
                        decimal tempMarketValue = 0m;
                        //這裡是第三層
                        Detail detail = new Detail();
                        detail.Tdate = item1.TDATE;
                        detail.Ttype = "1";

                        detail.Ttypename = "融資";
                        detail.Bstype = "B";

                        detail.Dseq = item1.DSEQ;
                        detail.Dno = item1.DNO;
                        detail.Bqty = item1.BQTY;
                        detail.Mprice = item1.PRICE;
                        detail.Mamt = item1.BQTY * item1.PRICE;
                        //detail.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                        detail.Lastprice = Lastprice;
                        detail.Fee = item1.FEE < 20 ? 20m : item1.FEE;       //［手續費］計算如小於20時，以20計算

                        detail.Cramt = item1.CRAMT;
                        detail.Bcramt = item1.BCRAMT;
                        detail.Gtamt = 0m;
                        detail.Bgtamt = 0m;
                        detail.Dnamt = 0m;
                        detail.Bdnamt = 0m;
                        detail.Interest = item1.CRINT;
                        detail.Dbfee = 0m;
                        detail.Dlfee = 0m;
                        detail.Dlint = 0m;

                        detail.Cost = item1.COST;
                        detail.EstimateAmt = Math.Floor(detail.Lastprice * detail.Bqty);
                        detail.EstimateFee = Math.Floor(detail.EstimateAmt * 0.001425m);



                        detail.Tax = 0;
                        detail.EstimateTax = Math.Floor(detail.EstimateAmt * 0.003m);
                        detail.Marketvalue = detail.EstimateAmt - detail.EstimateFee - detail.Bcramt;
                        detail.Profit = detail.Marketvalue - item1.COST;


                        detail.PlRatio = (item1.COST == 0 ? 0 : detail.Profit / item1.COST).ToString();//資料TCSIO的cost必定為零，所以報酬率會出現除以零的錯誤，其視為零報酬率
                        detail.Keeprate = Math.Floor(item1.BQTY * item1.PRICE / item1.BCRAMT).ToString();
                        detail.Wtype = "0";
                        detail.Ioflag = "0";//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        detail.Ioname = "0";//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        details.Add(detail);
                    }
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(sum.Stock);
                    sum.Ttype = "1";
                    sum.Bstype = "B";


                    sum.Ttypename = "融資";
                    //sum.Bqty = MCSRHQueryCNQBALE(tMBRam.FirstOrDefault().BHNO, tMBRam.FirstOrDefault().CSEQ, sum.Stock);
                    sum.Bqty = MCSRHQueryCRAQTY(tCRUDBean.FirstOrDefault().BHNO, tCRUDBean.FirstOrDefault().CSEQ, sum.Stock) + MCSRHQueryCROQTY(tCRUDBean.FirstOrDefault().BHNO, tCRUDBean.FirstOrDefault().CSEQ, sum.Stock);


                    sum.RealQTY = details.Sum(x => x.Bqty);
                    sum.Cost = details.Sum(x => x.Cost);
                    sum.Avgprice = sum.Cost != 0 ? sum.Bqty / sum.Cost : 0;//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    //sum.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                    sum.Lastprice = Lastprice;
                    sum.Marketvalue = details.Sum(x => x.Marketvalue);
                    sum.EstimateAmt = details.Sum(x => x.EstimateAmt);
                    sum.EstimateFee = details.Sum(x => x.EstimateFee);
                    sum.EstimateTax = details.Sum(x => x.EstimateTax);
                    sum.Profit = details.Sum(x => x.Profit);
                    sum.PlRatio = (sum.Cost != 0 ? ((sum.Profit / sum.Cost) * 1) : 0).ToString();//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    sum.Fee = details.Sum(x => x.Fee);
                    sum.Tax = details.Sum(x => x.Tax);
                    sum.Amt = details.Sum(x => x.Mamt);
                    sum.Cramt = details.Sum(x => x.Cramt);
                    sum.Gtamt = details.Sum(x => x.Gtamt);
                    sum.Bgtamt = details.Sum(x => x.Bgtamt);
                    sum.Dnamt = details.Sum(x => x.Dnamt);
                    sum.Bdnamt = details.Sum(x => x.Bdnamt);
                    sum.Interest = details.Sum(x => x.Interest);
                    sum.Dbfee = details.Sum(x => x.Dbfee);
                    sum.UnoffsetQtypeDetail = details;
                    sums.Add(sum);
                }
                #endregion

                #region 融券
                var sumszTDBUDLQ = (from d in tDBUDBean
                                    group d by new { d.STOCK, type = "S" } into g
                                    select new { Stock = g.Key.STOCK, type = g.Key.type });

                s = QuoteAsync(sumszTDBUDLQ.Select(x => x.Stock).ToList()).Result;
                keyValuePairs = new Dictionary<string, Symbol>();
                s.ForEach(x => keyValuePairs.Add(x.Id, x));

                foreach (var item in sumszTDBUDLQ)
                {
                    //這裡是第二層
                    Sum sum = new Sum();
                    sum.Stock = item.Stock;


                    details = new List<Detail>();
                    var detailsLQ = from t in tDBUDBean
                                    where t.STOCK == sum.Stock
                                    select t;
                    decimal Lastprice = Convert.ToDecimal(keyValuePairs[sum.Stock].DealPrice);

                    foreach (var item1 in detailsLQ)
                    {
                        decimal tempMarketValue = 0m;
                        //這裡是第三層
                        Detail detail = new Detail();
                        detail.Tdate = item1.TDATE;
                        detail.Ttype = "2";

                        detail.Ttypename = "融券";
                        detail.Bstype = "S";

                        detail.Dseq = item1.DSEQ;
                        detail.Dno = item1.DNO;
                        detail.Bqty = item1.BQTY;
                        detail.Mprice = item1.PRICE;
                        detail.Mamt = item1.BQTY * item1.PRICE;
                        //detail.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                        detail.Lastprice = Lastprice;
                        detail.Fee = item1.FEE < 20 ? 20m : item1.FEE;       //［手續費］計算如小於20時，以20計算

                        detail.Cramt = 0m;
                        detail.Bcramt = 0m;
                        detail.Gtamt = item1.GTAMT;
                        detail.Bgtamt = item1.BGTAMT;
                        detail.Dnamt = item1.DNAMT;
                        detail.Bdnamt = item1.BDNAMT;
                        detail.Interest = item1.GTINT + item1.DNINT;
                        detail.Dbfee = item1.DBFEE;
                        detail.Dlfee = item1.DLFEE;
                        detail.Dlint = item1.DLINT;

                        detail.Cost = item1.COST;
                        detail.EstimateAmt = -1 * Math.Floor(detail.Lastprice * detail.Bqty);
                        detail.EstimateFee = Math.Floor(detail.EstimateAmt * 0.001425m);

                        detail.Tax = item1.TAX;
                        detail.EstimateTax = 0;
                        tempMarketValue = detail.EstimateAmt + detail.EstimateFee;
                        detail.Marketvalue = item1.BGTAMT + item1.BDNAMT + item1.GTINT + item1.DNINT + item1.DLFEE + item1.DLINT - detail.EstimateAmt - detail.EstimateFee;
                        detail.Profit = tempMarketValue - item1.COST;

                        detail.PlRatio = (item1.COST == 0 ? 0 : detail.Profit / item1.COST).ToString();//資料TCSIO的cost必定為零，所以報酬率會出現除以零的錯誤，其視為零報酬率
                        detail.Keeprate = Math.Floor((item1.BGTAMT + item1.BDNAMT) / (item1.BQTY * item1.PRICE)).ToString();
                        detail.Wtype = "0";
                        detail.Ioflag = "0";//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        detail.Ioname = "0";//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        details.Add(detail);
                    }
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(sum.Stock);
                    sum.Ttype = "2";
                    sum.Bstype = item.type;

                    sum.Ttypename = "融券";
                    sum.Bqty = MCSRHQueryDBAQTY(tDBUDBean.FirstOrDefault().BHNO, tDBUDBean.FirstOrDefault().CSEQ, sum.Stock) + MCSRHQueryDBOQTY(tDBUDBean.FirstOrDefault().BHNO, tDBUDBean.FirstOrDefault().CSEQ, sum.Stock);

                    sum.RealQTY = details.Sum(x => x.Bqty);
                    sum.Cost = details.Sum(x => x.Cost);
                    sum.Avgprice = sum.Cost != 0 ? sum.Bqty / sum.Cost : 0;//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    //sum.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                    sum.Lastprice = Lastprice;
                    sum.Marketvalue = details.Sum(x => x.Marketvalue);
                    sum.EstimateAmt = details.Sum(x => x.EstimateAmt);
                    sum.EstimateFee = details.Sum(x => x.EstimateFee);
                    sum.EstimateTax = details.Sum(x => x.EstimateTax);
                    sum.Profit = details.Sum(x => x.Profit);
                    sum.PlRatio = (sum.Cost != 0 ? ((sum.Profit / sum.Cost) * 1) : 0).ToString();//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    sum.Fee = details.Sum(x => x.Fee);
                    sum.Tax = details.Sum(x => x.Tax);
                    sum.Amt = details.Sum(x => x.Mamt);
                    sum.Cramt = details.Sum(x => x.Cramt);
                    sum.Gtamt = details.Sum(x => x.Gtamt);
                    sum.Bgtamt = details.Sum(x => x.Bgtamt);
                    sum.Dnamt = details.Sum(x => x.Dnamt);
                    sum.Bdnamt = details.Sum(x => x.Bdnamt);
                    sum.Interest = details.Sum(x => x.Interest);
                    sum.Dbfee = details.Sum(x => x.Dbfee);
                    sum.UnoffsetQtypeDetail = details;
                    sums.Add(sum);
                }
                #endregion


                //這裡是第一層
                Accsum accsum = new Accsum();
                accsum.Errcode = "0000";
                accsum.Errmsg = "成功";
                accsum.Bqty = sums.Sum(x => x.Bqty);
                accsum.Cost = sums.Sum(x => x.Cost);
                accsum.Marketvalue = sums.Sum(x => x.Marketvalue);
                accsum.Profit = sums.Sum(x => x.Profit);
                accsum.PlRatio = (accsum.Cost != 0 ? ((accsum.Profit / accsum.Cost) * 1) : 0).ToString();//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                accsum.Fee = sums.Sum(x => x.Fee);
                accsum.Tax = sums.Sum(x => x.Tax);
                accsum.EstimateAmt = sums.Sum(x => x.EstimateAmt);
                accsum.EstimateFee = sums.Sum(x => x.EstimateFee);
                accsum.EstimateTax = sums.Sum(x => x.EstimateTax);
                accsum.Cramt = sums.Sum(x => x.Cramt);
                accsum.Gtamt = sums.Sum(x => x.Gtamt);
                accsum.Bgtamt = sums.Sum(x => x.Bgtamt);
                accsum.Dnamt = sums.Sum(x => x.Dnamt);
                accsum.Bdnamt = sums.Sum(x => x.Bdnamt);
                accsum.Interest = sums.Sum(x => x.Interest);
                accsum.Dbfee = sums.Sum(x => x.Dbfee);
                accsum.UnoffsetQtypeSum = sums;
                return accsum;

            }
            catch (Exception ex)
            {
                Utils.Util.Log("交易異常: " + ex.ToString());
                AccsumErr accsumErr = new AccsumErr();
                accsumErr.Errcode = "9999";
                accsumErr.Errmsg = "交易異常: " + ex.Message;
                return accsumErr;
            }
        }
        public virtual decimal MCSRHQueryCNQBALE(string BHNO, string CSEQ, string stock)
        {
            return SingletonQueryProviderMCSRH.queryProvider.MCSRHQueryCNQBALE(BHNO, CSEQ, stock);
        }

        public virtual decimal MCSRHQueryCRAQTY(string BHNO, string CSEQ, string stock)
        {
            return SingletonQueryProviderMCSRH.queryProvider.MCSRHQueryCRAQTY(BHNO, CSEQ, stock);
        }

        public virtual decimal MCSRHQueryCROQTY(string BHNO, string CSEQ, string stock)
        {
            return SingletonQueryProviderMCSRH.queryProvider.MCSRHQueryCROQTY(BHNO, CSEQ, stock);
        }

        public virtual decimal MCSRHQueryDBAQTY(string BHNO, string CSEQ, string stock)
        {
            return SingletonQueryProviderMCSRH.queryProvider.MCSRHQueryDBAQTY(BHNO, CSEQ, stock);
        }
        public virtual decimal MCSRHQueryDBOQTY(string BHNO, string CSEQ, string stock)
        {
            return SingletonQueryProviderMCSRH.queryProvider.MCSRHQueryDBOQTY(BHNO, CSEQ, stock);
        }
    }
}
