using Dapper;
using ESMP.STOCK.API.Bean;
using ESMP.STOCK.API.DTO;
using ESMP.STOCK.API.DTO.RealizedProfitAndLoss;

using ESMP.STOCK.API.Utils;
using SERVER.Utils;

using System.Data.SqlClient;


namespace ESMP.STOCK.API.QUERYAPI
{
    //已實現損益查詢 RealizedProfitAndLoss

    public class RealizedProfitAndLoss : BaseAPI
    {
        private string _connstr = "";
        public RealizedProfitAndLoss(string connstr) : base(connstr)
        {
            _connstr = connstr;
        }
        public object GetQuerys(RealizedProfitAndLossDTO root)
        {

            #region
            //IEnumerable<HCNRHBean> hCNRHBean = new List<HCNRHBean>();
            //IEnumerable<HCNTDBean> hCNTDBean = new List<HCNTDBean>();
            //IEnumerable<MSTMBBean> mSTMBBean = new List<MSTMBBean>();
            try
            {
                //Dapper.SqlMapper.SetTypeMap(typeof(HCNRHBean), new ColumnAttributeTypeMapper<HCNRHBean>());

                //string sqlCommend = @"SELECT * FROM HCNRH
                //            WHERE BHNO = @BHNO
                //            AND CSEQ = @CSEQ
                //            AND TDATE >= @Sdate
                //            AND TDATE <= @Edate";
                //var parameters = new DynamicParameters();
                //parameters.Add("BHNO", root.Bhno, System.Data.DbType.String);
                //parameters.Add("CSEQ", root.Cseq, System.Data.DbType.String);
                //parameters.Add("Sdate", root.Sdate, System.Data.DbType.String);
                //parameters.Add("Edate", root.Edate, System.Data.DbType.String);
                ////第三題增加股票代號查詢 StockSymbol
                //if (root.StockSymbol != "")
                //{
                //    sqlCommend += " AND STOCK = @STOCK";
                //    parameters.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
                //}

                //using (var conn = new SqlConnection(_connstr))
                //    hCNRHBean = conn.Query<HCNRHBean>(sqlCommend, parameters);

                //Dapper.SqlMapper.SetTypeMap(typeof(HCNTDBean), new ColumnAttributeTypeMapper<HCNTDBean>());

                //string sqlCommend2 = @"SELECT * FROM HCNTD
                //            WHERE BHNO = @BHNO
                //            AND CSEQ = @CSEQ
                //            AND TDATE >= @Sdate
                //            AND TDATE <= @Edate";
                //var parameters2 = new DynamicParameters();
                //parameters2.Add("BHNO", root.Bhno, System.Data.DbType.String);
                //parameters2.Add("CSEQ", root.Cseq, System.Data.DbType.String);
                //parameters2.Add("Sdate", root.Sdate, System.Data.DbType.String);
                //parameters2.Add("Edate", root.Edate, System.Data.DbType.String);
                ////第三題增加股票代號查詢 StockSymbol
                //if (root.StockSymbol != "")
                //{
                //    sqlCommend2 += " AND STOCK = @STOCK";
                //    parameters2.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
                //}


                //using (var conn = new SqlConnection(_connstr))
                //    hCNTDBean = conn.Query<HCNTDBean>(sqlCommend2, parameters2);

                //Dapper.SqlMapper.SetTypeMap(typeof(HCNTDBean), new ColumnAttributeTypeMapper<HCNTDBean>());

                //string sqlCommend3 = @"SELECT * FROM MSTMB";
                //var parameters3 = new DynamicParameters();

                //using (var conn = new SqlConnection(_connstr))
                //    mSTMBBean = conn.Query<MSTMBBean>(sqlCommend3, parameters3);

            }
            catch (Exception ex)
            {
                Utils.Util.Log(ex.ToString());
                throw;
            }
            #endregion

            IEnumerable<HCNRHBean> hCNRHBean = new List<HCNRHBean>();
            IEnumerable<HCNTDBean> hCNTDBean = new List<HCNTDBean>();

            IEnumerable<TCNUDBean> tCNUDBean = new List<TCNUDBean>();
            IEnumerable<TMHIOBean> tMHIOBean = new List<TMHIOBean>();
            IEnumerable<TCSIOBean> tCSIOBean = new List<TCSIOBean>();
            IEnumerable<TCNTDBean> tCNTDBean = new List<TCNTDBean>();
            IEnumerable<T201Bean> t201Bean = new List<T201Bean>();
            IEnumerable<HCRRHBean> hCRRHBean = new List<HCRRHBean>();           //歷史融資沖銷檔V
            IEnumerable<HDBRHBean> hDBRHBean = new List<HDBRHBean>();           //歷史融券沖銷檔V
            IEnumerable<HCDTDBean> hCDTDBean = new List<HCDTDBean>();           //歷史信用沖銷檔V


            if (root.Ttype == "0")
            {
                tCNUDBean = SQLProviderTCNUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tMHIOBean = SQLProviderTMHIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCSIOBean = SQLProviderTCSIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCNTDBean = SQLProviderTCNTD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                t201Bean = SQLProviderT201.Where(root.Bhno, root.Cseq, root.StockSymbol);
                hCNRHBean = SQLProviderHCNRH.Where(root.Bhno, root.Cseq, root.Sdate, root.Edate, root.StockSymbol);
            }
            else if (root.Ttype == "1")
            {
                hCRRHBean = SQLProviderHCRRH.Where(root.Bhno, root.Cseq, root.Sdate, root.Edate, root.StockSymbol);
            }
            else if (root.Ttype == "2")
            {
                hDBRHBean = SQLProviderHDBRH.Where(root.Bhno, root.Cseq, root.Sdate, root.Edate, root.StockSymbol);
            }
            else if (root.Ttype == "3")
            {
                hCDTDBean = SQLProviderHCDTD.Where(root.Bhno, root.Cseq, root.Sdate, root.Edate, root.StockSymbol);
            }
            else if (root.Ttype == "4")
            {
                tCNUDBean = SQLProviderTCNUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tMHIOBean = SQLProviderTMHIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCSIOBean = SQLProviderTCSIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCNTDBean = SQLProviderTCNTD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                t201Bean = SQLProviderT201.Where(root.Bhno, root.Cseq, root.StockSymbol);
                hCNTDBean = SQLProviderHCNTD.Where(root.Bhno, root.Cseq, root.Sdate, root.Edate, root.StockSymbol);
            }
            else if (root.Ttype == "A")
            {
                tCNUDBean = SQLProviderTCNUD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tMHIOBean = SQLProviderTMHIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCSIOBean = SQLProviderTCSIO.Where(root.Bhno, root.Cseq, root.StockSymbol);
                tCNTDBean = SQLProviderTCNTD.Where(root.Bhno, root.Cseq, root.StockSymbol);
                t201Bean = SQLProviderT201.Where(root.Bhno, root.Cseq, root.StockSymbol);
            }

            //return QueryIntoFormatString(hCNRHBean, hCNTDBean);
            return QueryIntoFormatString(
                hCNRHBean,
                hCNTDBean,
                tCNUDBean,
                tMHIOBean,
                tCSIOBean,
                tCNTDBean,
                t201Bean,
                hCRRHBean,
                hDBRHBean,
                hCDTDBean);
        }
        private object QueryIntoFormatString(IEnumerable<HCNRHBean> hcnrhBean, IEnumerable<HCNTDBean> hcntdBean, IEnumerable<TCNUDBean> tcnudBean, IEnumerable<TMHIOBean> tmhioBean, IEnumerable<TCSIOBean> tcsioBean, IEnumerable<TCNTDBean> tcntdBean, IEnumerable<T201Bean> t201Bean, IEnumerable<HCRRHBean> hCRRHs, IEnumerable<HDBRHBean> hDBRHs, IEnumerable<HCDTDBean> hCDTDs)
        {
            try
            {
                List<HCNRHBean> hcnRam = hcnrhBean.ToList();
                List<HCNTDBean> hcntdRam = hcntdBean.ToList();

                //沖銷賣出
                WriteOff wfTM = new WriteOff();
                (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = wfTM.StockWriteOff(tcnudBean.ToList(), tmhioBean.ToList(), tcsioBean.ToList(), tcntdBean.ToList(), t201Bean.ToList());
                hcnRam.AddRange(HC);
                hcntdRam.AddRange(HCNT);

                Accsum accsum = new Accsum();
                List<Sum> sums = new List<Sum>();

                #region HCNRH
                var hcnrhDetailOutLQ = from db_hcnrh in hcnRam
                                       group db_hcnrh by new { db_hcnrh.TDATE, db_hcnrh.SDSEQ, db_hcnrh.SDNO, db_hcnrh.WTYPE, db_hcnrh.STOCK, db_hcnrh.BHNO, db_hcnrh.CSEQ } into g
                                       select new
                                       {
                                           TDATE = g.Key.TDATE,
                                           SDSEQ = g.Key.SDSEQ,
                                           SDNO = g.Key.SDNO,
                                           SQTY = g.Sum(p => p.SQTY),
                                           CQTY = g.Sum(p => p.CQTY),
                                           SPRICE = g.Sum(p => p.SPRICE),
                                           COST = g.Sum(p => p.COST),
                                           INCOME = g.Sum(p => p.INCOME),
                                           SFEE = g.Sum(p => p.SFEE),
                                           TAX = g.Sum(p => p.TAX),
                                           WTYPE = g.Key.WTYPE,
                                           PROFIT = g.Sum(p => p.PROFIT),
                                           STOCK = g.Key.STOCK,
                                           BHNO = g.Key.BHNO,
                                           CSEQ = g.Key.CSEQ
                                       };

                //List<Detail> details = new List<Detail>();//===
                foreach (var item in hcnrhDetailOutLQ)
                {

                    var hcnrhLQ = from db_hcnrh in hcnrhBean
                                  where db_hcnrh.TDATE == item.TDATE &&
                                  db_hcnrh.SDSEQ == item.SDSEQ &&
                                  db_hcnrh.SDNO == item.SDNO
                                  select db_hcnrh;

                    List<Detail> details = new List<Detail>(); //===
                    foreach (var item1 in hcnrhLQ)
                    {
                        Detail detail = new Detail();
                        detail.Tdate = item1.RDATE;
                        detail.Dseq = item1.BDSEQ;
                        detail.Dno = item1.BDNO;
                        detail.Mqty = item1.BQTY;
                        detail.Cqty = item1.CQTY;
                        detail.Mprice = item1.BPRICE.ToString();
                        detail.Mamt = (item1.CQTY * item1.BPRICE).ToString();
                        detail.Cost = item1.COST;
                        detail.Income = item1.INCOME;
                        detail.Netamt = -item1.COST;
                        detail.Fee = item1.BFEE < 20 ? 20m : item1.BFEE;              //［手續費］計算如小於20時，以20計算。
                        detail.Tax = 0;
                        detail.Adjdate = item1.ADJDATE;
                        detail.Ttype = "0";
                        detail.Ttypename = "現買";
                        detail.Bstype = "B";
                        detail.Wtype = item1.WTYPE;
                        detail.Profit = item1.PROFIT;
                        detail.PlRatio = (item1.PROFIT / item1.COST).ToString();
                        detail.Ctype = "0";
                        detail.Ioflag = item1.IOFLAG;
                        detail.Ioname = item1.INCOME.ToString();
                        detail.Ttypename2 = "現買";
                        details.Add(detail);
                    }

                    DetailOut detailOut = new DetailOut();
                    detailOut.Tdate = item.TDATE;
                    detailOut.Dseq = item.SDSEQ;
                    detailOut.Dno = item.SDNO;
                    detailOut.Mqty = item.SQTY;
                    detailOut.Cqty = item.CQTY;
                    detailOut.Mprice = item.SPRICE.ToString();
                    detailOut.Mamt = (item.CQTY * item.SPRICE).ToString();
                    detailOut.Cost = item.COST;
                    detailOut.Income = item.INCOME;
                    detailOut.Netamt = detailOut.Income;
                    detailOut.Fee = item.SFEE;
                    detailOut.Tax = item.TAX;
                    detailOut.Ttype = "0";
                    detailOut.Ttypename = "現股";
                    detailOut.Bstype = "S";
                    detailOut.Wtype = item.WTYPE;
                    detailOut.Profit = item.PROFIT;
                    detailOut.PlRatio = (item.PROFIT / item.COST).ToString();
                    detailOut.Ctype = "0";
                    detailOut.Ttypename2 = "現賣";


                    Sum sum = new Sum();
                    sum.Bhno = item.BHNO;
                    sum.Cseq = item.CSEQ;
                    sum.Tdate = detailOut.Tdate;//==
                    sum.Dseq = detailOut.Dseq;
                    sum.Dno = detailOut.Dno;
                    sum.Ttype = "0";
                    sum.Ttypename = "現股";
                    sum.Bstype = "S";
                    sum.Stock = item.STOCK;
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(item.STOCK);
                    sum.Cqty = detailOut.Cqty;
                    sum.Mprice = detailOut.Mprice;
                    sum.Fee = detailOut.Fee;
                    sum.Tax = detailOut.Tax;
                    sum.Cost = detailOut.Cost;
                    sum.Income = detailOut.Income;
                    sum.Profit = detailOut.Profit;
                    sum.PlRatio = detailOut.PlRatio;
                    sum.Ctype = "0";
                    sum.Ttypename2 = "現賣";
                    sum.ProfitDetail = details;
                    sum.profitDetailOut = detailOut;
                    sums.Add(sum);
                }
                #endregion

                #region HCNTD
                var hcntdDetailOutLQ = from db_hcntd in hcntdRam
                                       group db_hcntd by new { db_hcntd.TDATE, db_hcntd.SDSEQ, db_hcntd.SDNO, db_hcntd.STOCK, db_hcntd.BHNO, db_hcntd.CSEQ } into g
                                       select new
                                       {
                                           TDATE = g.Key.TDATE,
                                           SDSEQ = g.Key.SDSEQ,
                                           SDNO = g.Key.SDNO,
                                           SQTY = g.Sum(p => p.SQTY),
                                           CQTY = g.Sum(p => p.CQTY),
                                           SPRICE = g.Sum(p => p.SPRICE),
                                           COST = g.Sum(p => p.COST),
                                           INCOME = g.Sum(p => p.INCOME),
                                           SFEE = g.Sum(p => p.SFEE),
                                           TAX = g.Sum(p => p.TAX),
                                           PROFIT = g.Sum(p => p.PROFIT),
                                           STOCK = g.Key.STOCK,
                                           BHNO = g.Key.BHNO,
                                           CSEQ = g.Key.CSEQ
                                       };
                foreach (var item in hcntdDetailOutLQ)
                {
                    var hcntdLQ = from db_hcntd in hcntdBean
                                  where db_hcntd.TDATE == item.TDATE &&
                                  db_hcntd.SDSEQ == item.SDSEQ &&
                                  db_hcntd.SDNO == item.SDNO
                                  select db_hcntd;
                    List<Detail> details = new List<Detail>();      //===
                    foreach (var item1 in hcntdLQ)
                    {
                        Detail detail = new Detail();
                        detail.Tdate = item1.TDATE;
                        detail.Dseq = item1.BDSEQ;
                        detail.Dno = item1.BDNO;
                        detail.Mqty = item1.BQTY;
                        detail.Cqty = item1.CQTY;
                        detail.Mprice = item1.BPRICE.ToString();
                        detail.Mamt = (item1.CQTY * item1.BPRICE).ToString();
                        detail.Cost = item1.COST;
                        detail.Income = item1.INCOME;
                        detail.Netamt = -item1.COST;
                        detail.Fee = item1.BFEE < 20 ? 20m : item1.BFEE;              //［手續費］計算如小於20時，以20計算。
                        detail.Tax = 0;
                        detail.Adjdate = "";
                        detail.Ttype = "0";
                        detail.Ttypename = "現買";
                        detail.Bstype = "B";
                        detail.Wtype = "0";//?
                        detail.Profit = item1.PROFIT;
                        detail.PlRatio = (item1.PROFIT / item1.COST).ToString();
                        detail.Ctype = "0";
                        detail.Ioflag = "0";
                        detail.Ioname = item1.INCOME.ToString();
                        detail.Ttypename2 = "現買";
                        details.Add(detail);
                    }

                    DetailOut detailOut = new DetailOut();
                    detailOut.Tdate = item.TDATE;
                    detailOut.Dseq = item.SDSEQ;
                    detailOut.Dno = item.SDNO;
                    detailOut.Mqty = item.SQTY;
                    detailOut.Cqty = item.CQTY;
                    detailOut.Mprice = item.SPRICE.ToString();
                    detailOut.Mamt = (item.CQTY * item.SPRICE).ToString();
                    detailOut.Cost = item.COST;
                    detailOut.Income = item.INCOME;
                    detailOut.Netamt = detailOut.Income;
                    detailOut.Fee = item.SFEE;
                    detailOut.Tax = item.TAX;
                    detailOut.Ttype = "0";
                    detailOut.Ttypename = "現股";
                    detailOut.Bstype = "S";
                    detailOut.Wtype = "0";
                    detailOut.Profit = item.PROFIT;
                    detailOut.PlRatio = (item.PROFIT / item.COST).ToString();
                    detailOut.Ctype = "0";
                    detailOut.Ttypename2 = "賣沖";

                    Sum sum = new Sum();
                    sum.Bhno = item.BHNO;
                    sum.Cseq = item.CSEQ;
                    sum.Tdate = detailOut.Tdate;//==
                    sum.Dseq = detailOut.Dseq;
                    sum.Dno = detailOut.Dno;
                    sum.Ttype = "0";
                    sum.Ttypename = "現股";
                    sum.Bstype = "S";
                    sum.Stock = item.STOCK;
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(item.STOCK);
                    sum.Cqty = detailOut.Cqty;
                    sum.Mprice = detailOut.Mprice;
                    sum.Fee = detailOut.Fee;
                    sum.Tax = detailOut.Tax;
                    sum.Cost = detailOut.Cost;
                    sum.Income = detailOut.Income;
                    sum.Profit = detailOut.Profit;
                    sum.PlRatio = detailOut.PlRatio;
                    sum.Ctype = "0";
                    sum.Ttypename2 = "賣沖";
                    sum.ProfitDetail = details;
                    sum.profitDetailOut = detailOut;
                    sums.Add(sum);
                }
                #endregion

                #region HCRRH
                var hcrrhDetailOutLQ = from db_hcrrh in hCRRHs
                                       group db_hcrrh by new { db_hcrrh.TDATE, db_hcrrh.DSEQ, db_hcrrh.DNO, db_hcrrh.RDSEQ, db_hcrrh.RDNO, db_hcrrh.STOCK, db_hcrrh.BHNO, db_hcrrh.CSEQ, db_hcrrh.CCRAMT } into g
                                       select new
                                       {
                                           TDATE = g.Key.TDATE,//
                                           DSEQ = g.Key.DSEQ,//
                                           DNO = g.Key.DNO,//
                                           RDSEQ = g.Key.RDSEQ,//
                                           RDNO = g.Key.RDNO,//
                                           SQTY = g.Sum(p => p.SQTY),
                                           CQTY = g.Sum(p => p.CQTY),
                                           SPRICE = g.Sum(p => p.SPRICE),
                                           COST = g.Sum(p => p.COST),
                                           INCOME = g.Sum(p => p.INCOME),
                                           SFEE = g.Sum(p => p.SFEE),
                                           TAX = g.Sum(p => p.TAX),
                                           PROFIT = g.Sum(p => p.PROFIT),
                                           CCRAMT = g.Sum(p => p.CCRAMT),
                                           STOCK = g.Key.STOCK,//
                                           BHNO = g.Key.BHNO,//
                                           CSEQ = g.Key.CSEQ//
                                       };

                //List<Detail> details = new List<Detail>();//===
                foreach (var item in hcrrhDetailOutLQ)
                {

                    var hcrrhLQ = from db_hcnrh in hCRRHs
                                  where db_hcnrh.TDATE == item.TDATE &&
                                  db_hcnrh.DSEQ == item.DSEQ &&
                                  db_hcnrh.DNO == item.DNO
                                  select db_hcnrh;

                    List<Detail> details = new List<Detail>(); //===
                    foreach (var item1 in hcrrhLQ)
                    {
                        Detail detail = new Detail();
                        detail.Tdate = item1.RDATE;
                        detail.Dseq = item1.RDSEQ;
                        detail.Dno = item1.RDNO;
                        detail.Mqty = item1.BQTY;
                        detail.Cqty = item1.CQTY;
                        detail.Mprice = item1.BPRICE.ToString();
                        detail.Mamt = (item1.CQTY * item1.BPRICE).ToString();
                        detail.Cost = item1.COST;
                        detail.Income = item1.INCOME;
                        detail.Netamt = -item1.COST;
                        detail.Ccramt = item1.CCRAMT;
                        detail.Fee = item1.BFEE < 20 ? 20m : item1.BFEE;              //［手續費］計算如小於20時，以20計算。
                        detail.Tax = 0;
                        detail.Adjdate = "";
                        detail.Ttype = "1";
                        detail.Ttypename = "融資";
                        detail.Bstype = "B";
                        detail.Wtype = "0";
                        detail.Profit = item1.PROFIT;
                        detail.PlRatio = (item1.PROFIT / item1.COST).ToString();
                        detail.Ctype = "1";
                        detail.Ioflag = "0";
                        detail.Ioname = item1.INCOME.ToString();
                        detail.Ttypename2 = "資買";
                        details.Add(detail);
                    }

                    DetailOut detailOut = new DetailOut();
                    detailOut.Tdate = item.TDATE;
                    detailOut.Dseq = item.DSEQ;
                    detailOut.Dno = item.DNO;
                    detailOut.Mqty = item.SQTY;
                    detailOut.Cqty = item.CQTY;
                    detailOut.Mprice = item.SPRICE.ToString();
                    detailOut.Mamt = (item.CQTY * item.SPRICE).ToString();
                    detailOut.Cost = item.COST;
                    detailOut.Income = item.INCOME;
                    detailOut.Netamt = detailOut.Income;
                    detailOut.Ccramt = item.CCRAMT;

                    detailOut.Fee = item.SFEE;
                    detailOut.Tax = item.TAX;
                    detailOut.Ttype = "1";
                    detailOut.Ttypename = "融資";
                    detailOut.Bstype = "S";
                    detailOut.Profit = item.PROFIT;
                    detailOut.PlRatio = (item.PROFIT / item.COST).ToString();
                    detailOut.Ctype = "1";
                    detailOut.Ttypename2 = "資賣";


                    Sum sum = new Sum();
                    sum.Bhno = item.BHNO;
                    sum.Cseq = item.CSEQ;
                    sum.Tdate = detailOut.Tdate;//==
                    sum.Dseq = detailOut.Dseq;
                    sum.Dno = detailOut.Dno;
                    sum.Ttype = "1";
                    sum.Ttypename = "融資";
                    sum.Bstype = "S";
                    sum.Stock = item.STOCK;
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(item.STOCK);
                    sum.Cqty = detailOut.Cqty;
                    sum.Mprice = detailOut.Mprice;
                    sum.Fee = detailOut.Fee;
                    sum.Tax = detailOut.Tax;
                    sum.Cost = detailOut.Cost;
                    sum.Income = detailOut.Income;
                    sum.Profit = detailOut.Profit;
                    sum.PlRatio = detailOut.PlRatio;
                    sum.Ctype = "1";
                    sum.Ttypename2 = "資賣";
                    sum.ProfitDetail = details;
                    sum.profitDetailOut = detailOut;
                    sums.Add(sum);
                }
                #endregion

                #region HDBRH
                var hdbrhDetailOutLQ = from db_hdbrh in hDBRHs
                                       group db_hdbrh by new { db_hdbrh.TDATE, db_hdbrh.DSEQ, db_hdbrh.DNO, db_hdbrh.RDSEQ, db_hdbrh.RDNO, db_hdbrh.STOCK, db_hdbrh.BHNO, db_hdbrh.CSEQ } into g
                                       select new
                                       {
                                           TDATE = g.Key.TDATE,//
                                           DSEQ = g.Key.DSEQ,//
                                           DNO = g.Key.DNO,//
                                           RDSEQ = g.Key.RDSEQ,//
                                           RDNO = g.Key.RDNO,//
                                           SQTY = g.Sum(p => p.SQTY),
                                           CQTY = g.Sum(p => p.CQTY),
                                           SPRICE = g.Sum(p => p.SPRICE),
                                           COST = g.Sum(p => p.COST),
                                           INCOME = g.Sum(p => p.INCOME),
                                           SFEE = g.Sum(p => p.SFEE),
                                           TAX = g.Sum(p => p.TAX),
                                           PROFIT = g.Sum(p => p.PROFIT),
                                           STOCK = g.Key.STOCK,//
                                           BHNO = g.Key.BHNO,//
                                           CSEQ = g.Key.CSEQ//
                                       };

                //List<Detail> details = new List<Detail>();//===
                foreach (var item in hdbrhDetailOutLQ)
                {

                    var hdbrhLQ = from db_hcnrh in hCRRHs
                                  where db_hcnrh.TDATE == item.TDATE &&
                                  db_hcnrh.RDSEQ == item.RDSEQ &&
                                  db_hcnrh.RDNO == item.RDNO
                                  select db_hcnrh;

                    List<Detail> details = new List<Detail>(); //===
                    foreach (var item1 in hdbrhLQ)
                    {
                        Detail detail = new Detail();
                        detail.Tdate = item1.RDATE;
                        detail.Dseq = item1.RDSEQ;
                        detail.Dno = item1.RDNO;
                        detail.Mqty = item1.BQTY;
                        detail.Cqty = item1.CQTY;
                        detail.Mprice = item1.BPRICE.ToString();
                        detail.Mamt = (item1.CQTY * item1.BPRICE).ToString();
                        detail.Cost = item1.COST;
                        detail.Income = item1.INCOME;
                        detail.Netamt = -item1.COST;
                        detail.Ccramt = item1.CCRAMT;
                        detail.Fee = item1.SFEE < 20 ? 20m : item1.SFEE;              //［手續費］計算如小於20時，以20計算。
                        detail.Tax = 0;
                        detail.Adjdate = "";
                        detail.Ttype = "2";
                        detail.Ttypename = "融券";
                        detail.Bstype = "S";
                        detail.Wtype = "0";
                        detail.Profit = item1.PROFIT;
                        detail.PlRatio = (item1.PROFIT / item1.COST).ToString();
                        detail.Ctype = "2";
                        detail.Ioflag = "0";
                        detail.Ioname = item1.INCOME.ToString();
                        detail.Ttypename2 = "券賣";
                        details.Add(detail);
                    }

                    DetailOut detailOut = new DetailOut();
                    detailOut.Tdate = item.TDATE;
                    detailOut.Dseq = item.DSEQ;
                    detailOut.Dno = item.DNO;
                    detailOut.Mqty = item.SQTY;
                    detailOut.Cqty = item.CQTY;
                    detailOut.Mprice = item.SPRICE.ToString();
                    detailOut.Mamt = (item.CQTY * item.SPRICE).ToString();
                    detailOut.Cost = item.COST;
                    detailOut.Income = item.INCOME;
                    detailOut.Netamt = detailOut.Income;

                    detailOut.Fee = item.SFEE;
                    detailOut.Tax = item.TAX;
                    detailOut.Ttype = "2";
                    detailOut.Ttypename = "融券";
                    detailOut.Bstype = "S";//??
                    detailOut.Profit = item.PROFIT;
                    detailOut.PlRatio = (item.PROFIT / item.COST).ToString();
                    detailOut.Ctype = "2";
                    detailOut.Ttypename2 = "券買";


                    Sum sum = new Sum();
                    sum.Bhno = item.BHNO;
                    sum.Cseq = item.CSEQ;
                    sum.Tdate = detailOut.Tdate;//==
                    sum.Dseq = detailOut.Dseq;
                    sum.Dno = detailOut.Dno;
                    sum.Ttype = "2";
                    sum.Ttypename = "融券";
                    sum.Bstype = "B";
                    sum.Stock = item.STOCK;
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(item.STOCK);
                    sum.Cqty = detailOut.Cqty;
                    sum.Mprice = detailOut.Mprice;
                    sum.Fee = detailOut.Fee;
                    sum.Tax = detailOut.Tax;
                    sum.Cost = detailOut.Cost;
                    sum.Income = detailOut.Income;
                    sum.Profit = detailOut.Profit;
                    sum.PlRatio = detailOut.PlRatio;
                    sum.Ctype = "2";
                    sum.Ttypename2 = "券買";
                    sum.ProfitDetail = details;
                    sum.profitDetailOut = detailOut;
                    sums.Add(sum);
                }
                #endregion

                #region HCDTD
                var hcdtdDetailOutLQ = from db_hcdtd in hCDTDs
                                       group db_hcdtd by new { db_hcdtd.TDATE, db_hcdtd.DBDNO, db_hcdtd.DBDSEQ, db_hcdtd.STOCK, db_hcdtd.BHNO, db_hcdtd.CSEQ } into g
                                       select new
                                       {
                                           TDATE = g.Key.TDATE,//
                                           DBDNO = g.Key.DBDNO,
                                           DBDSEQ = g.Key.DBDSEQ,
                                           SQTY = g.Sum(p => p.SQTY),
                                           QTY = g.Sum(p => p.QTY),
                                           SPRICE = g.Sum(p => p.SPRICE),
                                           COST = g.Sum(p => p.COST),
                                           INCOME = g.Sum(p => p.INCOME),
                                           SFEE = g.Sum(p => p.SFEE),
                                           TAX = g.Sum(p => p.TAX),
                                           PROFIT = g.Sum(p => p.PROFIT),
                                           STOCK = g.Key.STOCK,//
                                           BHNO = g.Key.BHNO,//
                                           CSEQ = g.Key.CSEQ//
                                       };

                //List<Detail> details = new List<Detail>();//===
                foreach (var item in hcdtdDetailOutLQ)
                {

                    var hcdtdLQ = from db_hcdtd in hCDTDs
                                  where db_hcdtd.TDATE == item.TDATE &&
                                  db_hcdtd.DBDSEQ == item.DBDSEQ &&
                                  db_hcdtd.DBDNO == item.DBDNO
                                  select db_hcdtd;

                    List<Detail> details = new List<Detail>(); //===
                    foreach (var item1 in hcdtdLQ)
                    {
                        Detail detail = new Detail();
                        detail.Tdate = item1.TDATE;
                        detail.Dseq = item1.CRDNO;
                        detail.Dno = item1.CRDNO;
                        detail.Mqty = item1.BQTY;
                        detail.Cqty = item1.QTY;
                        detail.Mprice = item1.BPRICE.ToString();
                        detail.Mamt = (detail.Cqty * item1.BPRICE).ToString();
                        detail.Cost = item1.COST;
                        detail.Income = item1.INCOME;
                        detail.Netamt = -item1.COST;
                        detail.Fee = item1.BFEE < 20 ? 20m : item1.BFEE;              //［手續費］計算如小於20時，以20計算。
                        detail.Tax = 0;
                        detail.Adjdate = "";
                        detail.Ttype = "2";
                        detail.Ttypename = "融券";
                        detail.Bstype = "B";
                        detail.Wtype = "0";
                        detail.Profit = item1.PROFIT;
                        detail.PlRatio = (item1.PROFIT / item1.COST).ToString();
                        detail.Ctype = "3";
                        detail.Ioflag = "0";
                        detail.Ioname = item1.INCOME.ToString();
                        detail.Ttypename2 = "資買";
                        details.Add(detail);
                    }

                    DetailOut detailOut = new DetailOut();
                    detailOut.Tdate = item.TDATE;
                    detailOut.Dseq = item.DBDSEQ;
                    detailOut.Dno = item.DBDNO;
                    detailOut.Mqty = item.SQTY;
                    detailOut.Cqty = item.QTY;//
                    detailOut.Mprice = item.SPRICE.ToString();
                    detailOut.Mamt = (detailOut.Cqty * item.SPRICE).ToString();
                    detailOut.Cost = item.COST;
                    detailOut.Income = item.INCOME;
                    detailOut.Netamt = detailOut.Income;

                    detailOut.Fee = item.SFEE;
                    detailOut.Tax = item.TAX;
                    detailOut.Ttype = "2";
                    detailOut.Ttypename = "融券";
                    detailOut.Bstype = "S";
                    detailOut.Profit = item.PROFIT;
                    detailOut.PlRatio = (item.PROFIT / item.COST).ToString();
                    detailOut.Ctype = "3";
                    detailOut.Ttypename2 = "券賣";


                    Sum sum = new Sum();
                    sum.Bhno = item.BHNO;
                    sum.Cseq = item.CSEQ;
                    sum.Tdate = detailOut.Tdate;//==
                    sum.Dseq = detailOut.Dseq;
                    sum.Dno = detailOut.Dno;
                    sum.Ttype = "2";
                    sum.Ttypename = "融券";
                    sum.Bstype = "S";
                    sum.Stock = item.STOCK;
                    sum.Stocknm = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNAME(item.STOCK);
                    sum.Cqty = detailOut.Cqty;
                    sum.Mprice = detailOut.Mprice;
                    sum.Fee = detailOut.Fee;
                    sum.Tax = detailOut.Tax;
                    sum.Cost = detailOut.Cost;
                    sum.Income = detailOut.Income;
                    sum.Profit = detailOut.Profit;
                    sum.PlRatio = detailOut.PlRatio;
                    sum.Ctype = "3";
                    sum.Ttypename2 = "券買";
                    sum.ProfitDetail = details;
                    sum.profitDetailOut = detailOut;
                    sums.Add(sum);
                }
                #endregion


                //==========================================================================
                //第一層  這個寫法參考一下為實現損益的作法
                var sumsLQ = from c in sums
                             group c by new { c.Cqty, c.Cost, c.Income, c.Profit, c.PlRatio, c.Fee, c.Tax } into g
                             select new
                             {
                                 cqty = g.Sum(p => p.Cqty),
                                 cost = g.Sum(p => p.Cost),
                                 income = g.Sum(p => p.Income),
                                 profit = g.Sum(p => p.Profit),
                                 PlRatio = g.Sum(p => Convert.ToDecimal(p.PlRatio)).ToString(),
                                 Fee = g.Sum(p => p.Fee),
                                 Tax = g.Sum(p => p.Tax),
                             };
                //if (sumsLQ.FirstOrDefault() == null)
                //{
                //    AccsumErr accsumErr = new AccsumErr();
                //    accsumErr.Errcode = "0008";
                //    accsumErr.Errmsg = "查無符合設定條件之資料";
                //    return accsumErr;
                //}

                foreach (var item in sumsLQ)
                {
                    accsum.Errcode = "0000";
                    accsum.Errmsg = "成功";
                    accsum.Cqty = item.cqty;
                    accsum.Cost = item.cost;
                    accsum.Income = item.income;
                    accsum.Profit = item.profit;
                    accsum.PlRatio = item.PlRatio;
                    accsum.Fee = item.Fee;
                    accsum.Tax = item.Tax;
                    accsum.ProfitSum = sums;
                }

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
