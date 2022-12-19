using Dapper;
using ESMP.STOCK.API.DTO;
using ESMP.STOCK.API.DTO.Statement;
using SERVER.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.QUERYAPI
{
    //對帳單查詢
    public class Statement : BaseAPI
    {
        private string _connstr = "";
        public Statement(string connstr) : base(connstr)
        {
            _connstr = connstr;
        }
        public object GetQuerys(StatementDTO root)
        {
            IEnumerable<HCMIOBean> hCMIOBean = new List<HCMIOBean>();
            IEnumerable<TMHIOBean> tMHIOBean = new List<TMHIOBean>();
            IEnumerable<MSTMBBean> mSTMBBean = new List<MSTMBBean>();
            try
            {

                Dapper.SqlMapper.SetTypeMap(typeof(HCMIOBean), new ColumnAttributeTypeMapper<HCMIOBean>());

                string sqlCommend = @"SELECT * FROM HCMIO
                            WHERE BHNO = @BHNO
                            AND CSEQ = @CSEQ
                            AND TDATE >= @Sdate
                            AND TDATE <= @Edate";
                var parameters = new DynamicParameters();
                parameters.Add("BHNO", root.Bhno, System.Data.DbType.String);
                parameters.Add("CSEQ", root.Cseq, System.Data.DbType.String);
                parameters.Add("Sdate", root.Sdate, System.Data.DbType.String);
                parameters.Add("Edate", root.Edate, System.Data.DbType.String);
                //第三題增加股票代號查詢 StockSymbol
                if (root.StockSymbol != "")
                {
                    sqlCommend += " AND STOCK = @STOCK";
                    parameters.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
                }
                using (var conn = new SqlConnection(_connstr))
                    hCMIOBean = conn.Query<HCMIOBean>(sqlCommend, parameters);




                string sqlCommend2;
                Dapper.SqlMapper.SetTypeMap(typeof(TMHIOBean), new ColumnAttributeTypeMapper<TMHIOBean>());

                sqlCommend2 = @"SELECT * FROM TMHIO
                            WHERE BHNO = @BHNO
                            AND CSEQ = @CSEQ
                            AND TDATE >= @Sdate
                            AND TDATE <= @Edate";



                var parameters2 = new DynamicParameters();
                parameters2.Add("BHNO", root.Bhno, System.Data.DbType.String);
                parameters2.Add("CSEQ", root.Cseq, System.Data.DbType.String);
                parameters2.Add("Sdate", root.Sdate, System.Data.DbType.String);
                parameters2.Add("Edate", root.Edate, System.Data.DbType.String);
                //第三題增加股票代號查詢 StockSymbol
                if (root.StockSymbol != "")
                {
                    sqlCommend2 += " AND STOCK = @STOCK";
                    parameters2.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
                }
                using (var conn = new SqlConnection(_connstr))
                    tMHIOBean = conn.Query<TMHIOBean>(sqlCommend2, parameters2);



                string sqlCommend3 = @"SELECT * FROM MSTMB";
                var parameters3 = new DynamicParameters();
                using (var conn = new SqlConnection(_connstr))
                    mSTMBBean = conn.Query<MSTMBBean>(sqlCommend3, parameters3);

            }
            catch (Exception ex)
            {
                Utils.Util.Log(ex.ToString());
                throw;
            }
            //Debug.WriteLine("HCMIO Count: "+ hCMIOBean.Count()+ "TMHIO Count: "+ tMHIOBean.Count());
            return QueryIntoFormatString(root, hCMIOBean, tMHIOBean, mSTMBBean);
        }
        public object QueryIntoFormatString(StatementDTO root, IEnumerable<HCMIOBean> hcmioBean, IEnumerable<TMHIOBean> tmhioBean, IEnumerable<MSTMBBean> mstmbBean)
        {
            try
            {
                List<Profile> profiles = new List<Profile>();
                foreach (HCMIOBean hCMIOBeanItem in hcmioBean)
                {
                    Profile profile = new Profile();
                    profile.Bhno = hCMIOBeanItem.BHNO;
                    profile.Cseq = hCMIOBeanItem.CSEQ;
                    profile.Name = "";
                    profile.Stock = hCMIOBeanItem.STOCK;
                    profile.Stocknm = (from a in mstmbBean where a.STOCK == hCMIOBeanItem.STOCK select a.CNAME).ToArray()[0];
                    profile.Mdate = hCMIOBeanItem.Tdate;
                    profile.Dseq = hCMIOBeanItem.DSEQ;
                    profile.Dno = hCMIOBeanItem.DNO;
                    profile.Ttype = hCMIOBeanItem.TTYPE;
                    profile.Ttypename = TtypenameCalculate("HCMIO", hCMIOBeanItem.TTYPE, hCMIOBeanItem.BSTYPE, hCMIOBeanItem.ETYPE);
                    profile.Bstype = hCMIOBeanItem.BSTYPE;
                    profile.Bstypename = BstypenameCalculate(hCMIOBeanItem.BSTYPE);
                    profile.Etype = hCMIOBeanItem.ETYPE;
                    profile.Mprice = hCMIOBeanItem.PRICE;
                    profile.Mqty = hCMIOBeanItem.QTY;
                    profile.Mamt = hCMIOBeanItem.AMT;
                    profile.Fee = Math.Floor(FeeCalculate(hCMIOBeanItem.QTY,hCMIOBeanItem.FEE));
                    profile.Tax = hCMIOBeanItem.TAX;
                    profile.Netamt = hCMIOBeanItem.NETAMT;
                    profiles.Add(profile);
                }
                foreach (TMHIOBean tMHIOBeanitem in tmhioBean)
                {

                    if (TDATECalculate(DateTime.Now.ToString("yyyyMMdd"), root.Sdate, root.Edate))
                    {
                        Profile profile = new Profile();
                        profile.Bhno = tMHIOBeanitem.BHNO;
                        profile.Cseq = tMHIOBeanitem.CSEQ;
                        profile.Name = "";
                        profile.Stock = tMHIOBeanitem.STOCK;
                        profile.Stocknm = (from a in mstmbBean where a.STOCK == tMHIOBeanitem.STOCK select a.CNAME).ToArray()[0];
                        profile.Mdate = tMHIOBeanitem.Tdate;
                        profile.Dseq = tMHIOBeanitem.DSEQ;
                        profile.Dno = tMHIOBeanitem.JRNUM;
                        profile.Ttype = tMHIOBeanitem.TTYPE;
                        profile.Ttypename = TtypenameCalculate("HCMIO", tMHIOBeanitem.TTYPE, tMHIOBeanitem.BSTYPE, tMHIOBeanitem.ETYPE);
                        profile.Bstype = tMHIOBeanitem.BSTYPE;
                        profile.Bstypename = BstypenameCalculate(tMHIOBeanitem.BSTYPE);
                        profile.Etype = tMHIOBeanitem.ETYPE == "2" ? "1" : "0";
                        profile.Mprice = tMHIOBeanitem.PRICE;
                        profile.Mqty = tMHIOBeanitem.QTY;
                        profile.Mamt = Math.Floor(tMHIOBeanitem.PRICE * tMHIOBeanitem.QTY);
                        profile.Fee = Math.Floor(FeeCalculate(tMHIOBeanitem.QTY, tMHIOBeanitem.PRICE * 0.003m));
                        profile.Tax = Math.Floor(TaxCalculate(tMHIOBeanitem.BSTYPE, tMHIOBeanitem.PRICE, tMHIOBeanitem.QTY));
                        profile.Netamt = NetamtCalculate(tMHIOBeanitem.BSTYPE, profile.Mamt, profile.Fee, profile.Tax);
                        profiles.Add(profile);
                    }

                }

                var profilesLQ = from p in profiles
                                 group p by 0 into g
                                 select new
                                 {
                                     Mamt = g.Sum(g => g.Mamt),
                                     Fee = g.Sum(g => g.Fee),
                                     Tax = g.Sum(g => g.Tax),
                                     Netamt = g.Sum(g => g.Netamt),
                                     Mqty = g.Sum(g => g.Mqty)
                                 };
                //https://dotblogs.com.tw/noncoder/2019/03/25/Lambda-GroupBy-Sum
                var profilesMqtySumLQ = from p in profiles
                                        group p by new { p.Bstype } into g
                                        select new
                                        {
                                            Bstype = g.Key.Bstype,
                                            Mqty = g.Sum(g => g.Mqty),
                                            Mant = g.Sum(g => g.Mamt)
                                        };


                BillSum bill = new BillSum();


                Accsum accsum = new Accsum();

                if (profilesLQ.FirstOrDefault() == null)
                {
                    AccsumErr accsumErr = new AccsumErr();
                    accsumErr.Errcode = "0008";
                    accsumErr.Errmsg = "查無符合設定條件之資料";
                    return accsumErr;
                }
                foreach (var profilesItem in profilesLQ)
                {

                    bill.Cnbamt = (from a in profilesMqtySumLQ where a.Bstype == "B" select a.Mant).ToArray()[0];
                    bill.Cnsamt = (from a in profilesMqtySumLQ where a.Bstype == "S" select a.Mant).ToArray()[0];
                    bill.Cnfee = profilesItem.Fee;
                    bill.Cntax = profilesItem.Tax;
                    bill.Cnnetamt = profilesItem.Netamt;
                    bill.Bqty = (from a in profilesMqtySumLQ where a.Bstype == "B" select a.Mqty).ToArray()[0];
                    bill.Sqty = (from a in profilesMqtySumLQ where a.Bstype == "S" select a.Mqty).ToArray()[0];

                    accsum.Errcode = "0000";
                    accsum.Errmsg = "成功";
                    accsum.Netamt = profilesItem.Netamt;
                    accsum.Fee = profilesItem.Fee;
                    accsum.Tax = profilesItem.Tax;
                    accsum.Mqty = profilesItem.Mqty;
                    accsum.Mamt = profilesItem.Mamt;
                }
                accsum.Sum = bill;
                accsum.Profile = profiles;

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
        #region
        //(5)	［手續費］請無條件捨去至整數；如為整股，最小手續費為20元；如為零股，最小手續費為1元。
        #endregion
        public decimal FeeCalculate(decimal QTY, decimal FEE)
        {
            if (QTY >= 1000)
            {
                if (FEE < 20)
                {
                    return 20;
                }
                return FEE;
            }
            else
            {
                if (FEE < 1)
                {
                    return 1;
                }
                return FEE;
            }
        }
        #region
        /*
         * (8)	［交易類別名稱］：
        如為[HCMIO]來源：
        	[TTYPE]為0且[BSTYPE]為B時，提供「現買」；
        	[TTYPE]為0且[BSTYPE]為B時，提供「現賣」；
        如為[TMHIO]來源：
        	[TTYPE]為0、[BSTYPE]為B且[ETYPE]為0時，提供「現買」；
        	[TTYPE]為0、[BSTYPE]為B且[ETYPE]為2時，提供「盤後零買」；
        	[TTYPE]為0、[BSTYPE]為B且[ETYPE]為5時，提供「盤中零買」；
        	[TTYPE]為0、[BSTYPE]為S且[ETYPE]為0時，提供「現賣」；
        	[TTYPE]為0、[BSTYPE]為S且[ETYPE]為2時，提供「盤後零賣」；
        	[TTYPE]為0、[BSTYPE]為S且[ETYPE]為5時，提供「盤中零賣」；
        */
        #endregion
        public string TtypenameCalculate(string table, string? TTYPE, string? BSTYPE, string? ETYPE)
        {
            if (table == "HCMIO")
            {
                if (TTYPE == "0" && BSTYPE == "B")
                    return "現買";

                if (TTYPE == "0" && BSTYPE == "S")
                    return "現賣";
            }
            if (table == "TMHIO")
            {
                if (TTYPE == "0" && BSTYPE == "B" && ETYPE == "0")
                    return "現買";

                if (TTYPE == "0" && BSTYPE == "B" && ETYPE == "2")
                    return "盤後零買";

                if (TTYPE == "0" && BSTYPE == "B" && ETYPE == "5")
                    return "盤中零買";

                if (TTYPE == "0" && BSTYPE == "S" && ETYPE == "0")
                    return "現賣";

                if (TTYPE == "0" && BSTYPE == "S" && ETYPE == "2")
                    return "盤後零賣";

                if (TTYPE == "0" && BSTYPE == "S" && ETYPE == "5")
                    return "盤中零賣";
            }
            Utils.Util.Log("錯誤的交易類別名稱指標! \n 資料表:" + table + " TTYPE:" + TTYPE + " BSTYPE:" + BSTYPE + " ETYPE:" + ETYPE);
            return "";
        }
        public string BstypenameCalculate(string? BSTYPE)
        {
            if (BSTYPE == "B")
                return "買";
            if (BSTYPE == "S")
                return "賣";
            Utils.Util.Log("錯誤的買賣別名稱指標! \n BSTYPE:" + BSTYPE);
            return "";
        }
        public decimal TaxCalculate(string BSTYPE, decimal PRICE, decimal QTY)
        {
            try
            {
                if (BSTYPE == "B")
                    return 0;
                if (BSTYPE == "S")
                    return PRICE * QTY * 0.003m;
            }
            catch (Exception ex)
            {
                Utils.Util.Log(ex.ToString());
            }

            Utils.Util.Log("錯誤的買賣別名稱指標導致錯誤的交易稅計算指標! \n BSTYPE:" + BSTYPE + " PRICE:" + PRICE + "  QTY:" + QTY);
            return 0;
        }

        public decimal NetamtCalculate(string BSTYPE, decimal Mamt, decimal Fee, decimal Tax)
        {
            try
            {
                if (BSTYPE == "B")
                    return -(Mamt + Fee);
                if (BSTYPE == "S")
                    return (Mamt - Fee - Tax);
            }
            catch (Exception ex)
            {
                Utils.Util.Log(ex.ToString());
            }

            Utils.Util.Log("錯誤的買賣別名稱指標導致錯誤的淨收付計算指標! \n BSTYPE:" + BSTYPE + " Mamt:" + Mamt + "  Fee:" + Fee + "  Tax:" + Tax);
            return 0;
        }

        #region
        //(9)	如查詢起訖日期包含［當天日期］，則需查詢TMHIO
        #endregion

        public bool TDATECalculate(string DateNow, string Sdate, string Edate)
        {
            int DateNow1 = Convert.ToInt32(DateNow);
            int Sdate1 = Convert.ToInt32(Sdate);
            int Edate1 = Convert.ToInt32(Edate);

            if (Sdate1 <= DateNow1 && DateNow1 <= Edate1)
            {
                return true;
            }
            return false;
        }

    }
}
