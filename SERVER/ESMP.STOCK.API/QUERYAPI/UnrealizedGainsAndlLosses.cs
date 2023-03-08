using Dapper;
using ESMP.STOCK.API.Bean;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SERVER.Utils;
using ESMP.STOCK.API.Utils;
using ESMP.STOCK.API.DTO.UnrealizedGainsAndlLosses;
using ESMP.STOCK.API.DTO;

namespace ESMP.STOCK.API.QUERYAPI
{
    //未實現損益查詢 UnrealizedGainsAndlLosses

    public class UnrealizedGainsAndlLosses : BaseAPI
    {
        //private string _connstr = "";
        public UnrealizedGainsAndlLosses(string connstr) : base(connstr)
        {
            //_connstr = connstr;
        }
        public object GetQuerys(UnrealizedGainsAndlLossesDTO root)
        {
            #region
            //IEnumerable<TCNUDBean> tCNUDBean = new List<TCNUDBean>();
            //IEnumerable<TMHIOBean> tMHIOBean = new List<TMHIOBean>();
            ////IEnumerable<MSTMBBean> mSTMBBean = new List<MSTMBBean>();
            //IEnumerable<TCSIOBean> tCSIOBean = new List<TCSIOBean>();
            //IEnumerable<MSYSBean> mSYSBean = new List<MSYSBean>();


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

            //}
            //catch (Exception ex)
            //{
            //    Utils.Util.Log(ex.ToString());
            //    throw;
            //}
            #endregion

            //return QueryIntoFormatString(tCNUDBean, tMHIOBean, tCSIOBean);
            return QueryIntoFormatString(QueryCondition(QueryTCNUD(), root), QueryCondition(QueryTMHIO(), root), QueryCondition(QueryTCSIO(), root));
        }
        /*
         * 摘要:
         *      取得該查尋容器項目，依照篩選條件取得結果
         * 
         * 參數:
         *      QueryItems:
         *          篩選條件的所有項目
         *      
         *      root:
         *          篩選條件的依據
         * 
         * 類型參數: 
         *      T:
         *          被帶進去的DTO
         * 傳回:
         *      回傳查詢結果   
         * 
         * 例外狀況:
         * 
         */
        private IEnumerable<T> QueryCondition<T>(IEnumerable<T> QueryItems, UnrealizedGainsAndlLossesDTO root)
        {
            IEnumerable<T> QueryFinal = QueryItems
                .Where(x => x.GetType().GetProperty("BHNO").GetValue(x).ToString() == root.Bhno)
                .Where(x => x.GetType().GetProperty("CSEQ").GetValue(x).ToString() == root.Cseq);
            if (root.StockSymbol != "")
            {
                QueryFinal.Where(x => x.GetType().GetProperty("STOCK").GetValue(x).ToString() == root.StockSymbol);
            }
            return QueryFinal;
        }


        public object QueryIntoFormatString(IEnumerable<TCNUDBean> tCNUDBean, IEnumerable<TMHIOBean> tMHIOBean, IEnumerable<TCSIOBean> tCSIOBean)
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
                (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = wfTM.StockWriteOff(tCNUDBean.ToList(), tMHIOBean.ToList(), tCSIOBean.ToList());
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
                if (tMBRam.Count() == 0)
                {
                    AccsumErr accsumErr = new AccsumErr();
                    accsumErr.Errcode = "0008";
                    accsumErr.Errmsg = "查無符合設定條件之資料";
                    return accsumErr;
                }


                var sumsLQ = (from d in tMBRam
                              group d by new { d.STOCK, d.BQTY, type = d.BQTY < 0 ? "S" : "B" } into g
                              select new { Stock = g.Key.STOCK, type = g.Key.type });

                List<Sum> sums = new List<Sum>();
                List<Detail> details;
                foreach (var item in sumsLQ)
                {
                    //這裡是第二層
                    Sum sum = new Sum();
                    sum.Stock = item.Stock;


                    details = new List<Detail>();
                    var detailsLQ = from t in tMBRam
                                    where t.STOCK == sum.Stock
                                    select t;
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
                        detail.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                        detail.Fee = item1.FEE < 20 ? 20m : item1.FEE;       //［手續費］計算如小於20時，以20計算

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
                        detail.Wtype = item1.WTYPE;
                        detail.Ioflag = item1.IOFLAG;//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        detail.Ioname = string.IsNullOrEmpty(item1.IOFLAG) != null ? SingletonQueryProviderMSYS.queryProvider.MSYSQueryALL(item1.IOFLAG) : "";//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        details.Add(detail);
                    }
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(sum.Stock);
                    sum.Ttype = "0";
                    sum.Bstype = item.type;
                    //if (item.type == "B")
                    //{
                    //    sum.Ttypename = "現買";
                    //    //sum.Bqty = details.Sum(x => x.Bqty);
                    //    sum.Bqty = SingletonQueryProviderMCSRH.queryProvider.MCSRHQueryCNQBALE(tMBRam.FirstOrDefault().BHNO, tMBRam.FirstOrDefault().CSEQ, sum.Stock);
                    //}
                    //else
                    //{
                    //    sum.Ttypename = "現賣";
                    //    sum.Bqty = 0m;
                    //}
                    sum.Ttypename = "現買";
                    sum.Bqty = details.Sum(x => x.Bqty);

                    sum.RealQTY = details.Sum(x => x.Bqty);
                    sum.Cost = details.Sum(x => x.Cost);
                    sum.Avgprice = sum.Cost != 0 ? sum.Bqty / sum.Cost : 0;//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    sum.Lastprice = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCPRICE(sum.Stock);
                    sum.Marketvalue = details.Sum(x => x.Marketvalue);
                    sum.EstimateAmt = details.Sum(x => x.EstimateAmt);
                    sum.EstimateFee = details.Sum(x => x.EstimateFee);
                    sum.EstimateTax = details.Sum(x => x.EstimateTax);
                    sum.Profit = details.Sum(x => x.Profit);
                    sum.PlRatio = (sum.Cost != 0 ? ((sum.Profit / sum.Cost) * 1) : 0).ToString();//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    sum.Fee = details.Sum(x => x.Fee);
                    sum.Tax = details.Sum(x => x.Tax);
                    sum.Amt = details.Sum(x => x.Mamt);
                    sum.UnoffsetQtypeDetail = details;
                    sums.Add(sum);
                }
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
    }
}
