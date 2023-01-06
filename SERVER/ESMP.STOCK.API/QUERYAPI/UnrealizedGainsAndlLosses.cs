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
        private string _connstr = "";
        public UnrealizedGainsAndlLosses(string connstr) : base(connstr)
        {
            _connstr = connstr;
        }
        public object GetQuerys(UnrealizedGainsAndlLossesDTO root)
        {
            IEnumerable<TCNUDBean> tCNUDBean = new List<TCNUDBean>();
            IEnumerable<TMHIOBean> tMHIOBean = new List<TMHIOBean>();
            IEnumerable<MSTMBBean> mSTMBBean = new List<MSTMBBean>();
            IEnumerable<TCSIOBean> tCSIOBean = new List<TCSIOBean>();
            IEnumerable<MSYSBean> mSYSBean = new List<MSYSBean>();


            try
            {
                string sqlCommend;
                Dapper.SqlMapper.SetTypeMap(typeof(TCNUDBean), new ColumnAttributeTypeMapper<TCNUDBean>());
                sqlCommend = @"SELECT * FROM TCNUD
                            WHERE BHNO = @BHNO
                            AND CSEQ = @CSEQ";
                var parameters = new DynamicParameters();
                parameters.Add("BHNO", root.Bhno, System.Data.DbType.String);
                parameters.Add("CSEQ", root.Cseq, System.Data.DbType.String);
                if (root.StockSymbol != "")
                {
                    sqlCommend += " AND STOCK = @STOCK";
                    parameters.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
                }
                using (var conn = new SqlConnection(_connstr))
                    tCNUDBean = conn.Query<TCNUDBean>(sqlCommend, parameters);

                string sqlCommend2;
                Dapper.SqlMapper.SetTypeMap(typeof(TMHIOBean), new ColumnAttributeTypeMapper<TMHIOBean>());

                sqlCommend2 = @"SELECT * FROM TMHIO
                            WHERE BHNO = @BHNO
                            AND CSEQ = @CSEQ";
                var parameters2 = new DynamicParameters();
                parameters2.Add("BHNO", root.Bhno, System.Data.DbType.String);
                parameters2.Add("CSEQ", root.Cseq, System.Data.DbType.String);
                //第三題增加股票代號查詢 StockSymbol
                if (root.StockSymbol != "")
                {
                    sqlCommend2 += " AND STOCK = @STOCK";
                    parameters2.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
                }
                using (var conn = new SqlConnection(_connstr))
                    tMHIOBean = conn.Query<TMHIOBean>(sqlCommend2, parameters2);


                Dapper.SqlMapper.SetTypeMap(typeof(MSTMBBean), new ColumnAttributeTypeMapper<MSTMBBean>());
                string sqlCommend3 = @"SELECT * FROM MSTMB";
                var parameters3 = new DynamicParameters();
                using (var conn = new SqlConnection(_connstr))
                    mSTMBBean = conn.Query<MSTMBBean>(sqlCommend3, parameters3);


                string sqlCommend4;
                Dapper.SqlMapper.SetTypeMap(typeof(TCSIOBean), new ColumnAttributeTypeMapper<TCSIOBean>());

                sqlCommend4 = @"SELECT * FROM TCSIO
                            WHERE BHNO = @BHNO
                            AND CSEQ = @CSEQ";
                var parameters4 = new DynamicParameters();
                parameters4.Add("BHNO", root.Bhno, System.Data.DbType.String);
                parameters4.Add("CSEQ", root.Cseq, System.Data.DbType.String);
                //第三題增加股票代號查詢 StockSymbol
                if (root.StockSymbol != "")
                {
                    sqlCommend4 += " AND STOCK = @STOCK";
                    parameters4.Add("STOCK", root.StockSymbol, System.Data.DbType.String);
                }
                using (var conn = new SqlConnection(_connstr))
                    tCSIOBean = conn.Query<TCSIOBean>(sqlCommend4, parameters4);

            }
            catch (Exception ex)
            {
                Utils.Util.Log(ex.ToString());
                throw;
            }


            return QueryIntoFormatString(tCNUDBean, tMHIOBean, mSTMBBean, tCSIOBean);
        }

        public object QueryIntoFormatString(IEnumerable<TCNUDBean> tCNUDBean, IEnumerable<TMHIOBean> tMHIOBean, IEnumerable<MSTMBBean> mSTMBBean, IEnumerable<TCSIOBean> tCSIOBean)
        {

            try
            {


                List<TCNUDBean> tMBRam = tCNUDBean.ToList();

                //把TCSIO的資料合併進來查
                foreach (var item in tCSIOBean)
                {
                    TCNUDBean tCNUD = new TCNUDBean();
                    tCNUD.TDATE = item.TDATE;
                    tCNUD.BHNO = item.BHNO;
                    tCNUD.CSEQ = item.CSEQ;
                    tCNUD.STOCK = item.STOCK;
                    tCNUD.PRICE = 0;
                    tCNUD.QTY = item.QTY;
                    tCNUD.BQTY = item.QTY;
                    tCNUD.FEE = 0;
                    tCNUD.COST = 0;
                    tCNUD.DSEQ = item.DSEQ;
                    tCNUD.DNO = item.DNO;
                    tCNUD.WTYPE = "A";
                    tCNUD.IOFLAG = item.IOFLAG;
                    tMBRam.Add(tCNUD);
                }

                //把TMHIO的資料跟TCNUD做沖銷產出新的TCNUD餘額

                tMBRam = TMHIOWriteOff(tMBRam, tMHIOBean.ToList());

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
                              group d by new { d.STOCK } into g
                              select new Sum { Stock = g.Key.STOCK });

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

                        //這裡是第三層
                        Detail detail = new Detail();
                        detail.Tdate = item1.TDATE;
                        detail.Ttype = "0";
                        detail.Ttypename = "現買";
                        detail.Bstype = "B";
                        detail.Dseq = item1.DSEQ;
                        detail.Dno = item1.DNO;
                        detail.Bqty = item1.BQTY;
                        detail.Mprice = item1.PRICE;
                        detail.Mamt = item1.BQTY * item1.PRICE;
                        detail.Lastprice = mSTMBBean.Where(x => x.STOCK == sum.Stock).FirstOrDefault().CPRICE;
                        detail.Fee = item1.FEE < 20 ? 20m : item1.FEE;       //［手續費］計算如小於20時，以20計算
                        detail.Tax = 0;
                        detail.Cost = item1.COST;
                        detail.EstimateAmt = Math.Floor(detail.Lastprice * detail.Bqty);
                        detail.EstimateFee = Math.Floor(detail.EstimateAmt * detail.Fee * 0.001425m);
                        detail.EstimateTax = Math.Floor(detail.EstimateAmt * detail.Tax * 0.003m);
                        detail.Marketvalue = detail.EstimateAmt - detail.EstimateFee - detail.EstimateTax;
                        detail.Profit = detail.Marketvalue - item1.COST;
                        detail.PlRatio = (item1.COST == 0 ? 0 : detail.Profit / item1.COST).ToString();//資料TCSIO的cost必定為零，所以報酬率會出現除以零的錯誤，其視為零報酬率
                        detail.Wtype = item1.WTYPE;
                        detail.Ioflag = item1.IOFLAG;//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        detail.Ioname = string.IsNullOrEmpty(item1.IOFLAG) != null ? SingletonQueryProvider.queryProvider.MSYSQueryALL(item1.IOFLAG) : "";//這裡 匯撥集保代碼/匯撥集保名稱 ioflag/ioname 預設先給死資料"0"
                        details.Add(detail);
                    }
                    sum.Stocknm = mSTMBBean.Where(x => x.STOCK == sum.Stock).FirstOrDefault().CNAME;
                    sum.Ttype = "0";
                    sum.Ttypename = "現買";
                    sum.Bstype = "B";
                    sum.Bqty = details.Sum(x => x.Bqty);
                    sum.Cost = details.Sum(x => x.Cost);
                    sum.Avgprice = sum.Cost != 0 ? sum.Bqty / sum.Cost : 0;//因為TCSIO的來源資料PRICE固定為零，所以先給死資料"0"
                    sum.Lastprice = mSTMBBean.Where(x => x.STOCK == sum.Stock).FirstOrDefault().CPRICE;
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
        #region
        //［手續費］請無條件捨去至整數；如為整股，最小手續費為20元；如為零股，最小手續費為1元。
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
        //
        public List<TCNUDBean> TMHIOWriteOff(List<TCNUDBean> tCNUDs, List<TMHIOBean> tMHIOs)
        {
            List<TCNUDBean> total = new List<TCNUDBean>(tCNUDs);
            //total.CopyTo(tCNUDs);//因為.ToList()把List<>轉換成IEnuable<>，在隱含轉換成List<>
            //List<TCNUDBean> total = tCNUDs.ToList();//如果加.ToList()，(294)RemoveAll不會連tCNUD也移除掉
            //List<TCNUDBean> total = tCNUDs;
            List<HCNRHBean> hCNRHReturn = new List<HCNRHBean>();

            //分類針對匹配到的個別股票沖銷
            foreach (var tMHIOsDistinct in tMHIOs.Select(c => c.STOCK).Distinct())
            {
                total.RemoveAll(c => c.STOCK == tMHIOsDistinct);
                //取出TCNUD指定股票代碼資料並排序
                var tCNUDLQ = tCNUDs.Where(c => c.STOCK == tMHIOsDistinct).OrderBy(c => c.TDATE).ThenBy(c => c.WTYPE).ThenBy(c => c.DNO).ToList();
                List<(decimal, TCNUDBean)> tCNUDTU = tCNUDLQ.Select(x => (x.QTY, x)).ToList();

                ////取出TMHIO指定股票代碼
                var tMHIOLQ = tMHIOs.Where(c => c.STOCK == tMHIOsDistinct).ToList();
                List<(decimal, TMHIOBean)> tMHIOTU = tMHIOLQ.Select(x => (x.QTY, x)).ToList();
                var DicTCNUD = tCNUDLQ.ToDictionary(c => c.TDATE + c.BHNO + c.CSEQ + c.DSEQ + c.DNO, c => new ValueTuple<decimal, TCNUDBean>(c.QTY, c));
                var DicTMHIO = tMHIOLQ.ToDictionary(c => c.Tdate + c.BHNO + c.DSEQ + c.JRNUM, c => new ValueTuple<decimal, TMHIOBean>(c.QTY, c));


                var DictCNUDBean = tCNUDLQ.ToDictionary(c => c.TDATE + c.BHNO + c.CSEQ + c.DSEQ + c.DNO, c => c);
                var DictMHIOBean = tMHIOLQ.ToDictionary(c => c.Tdate + c.BHNO + c.DSEQ + c.JRNUM, c => c);
                var DictCNUDQty = tCNUDLQ.ToDictionary(c => c.TDATE + c.BHNO + c.CSEQ + c.DSEQ + c.DNO, c => c.QTY);
                var DictMHIOQty = tMHIOLQ.ToDictionary(c => c.Tdate + c.BHNO + c.DSEQ + c.JRNUM, c => c.QTY);

                total.AddRange(isWritOff(DicTMHIO, DicTCNUD));
                List<TCNUDBean> isWritOff(Dictionary<string, (decimal lastQTY, TMHIOBean rollData)> tMHIOs, Dictionary<string, (decimal lastQTY, TCNUDBean rollData)> tCNUDOrder)
                {
                    //decimal QTY = 0;
                    //decimal BFEE = 0;
                    //decimal FEE = 0;
                    //decimal TAX = 0;
                    //decimal INCOME = 0;
                    //decimal COST = 0;
                    //decimal PROFIT = 0;
                    HCNRHBean hCNRHBean = new HCNRHBean();
                    if (tMHIOs.Count == 0)
                    {
                        return tCNUDOrder.Values.Select(c => c.rollData).ToList();
                    }




                    if (tMHIOs.Values.FirstOrDefault().lastQTY < tCNUDOrder.Values.FirstOrDefault().lastQTY)
                    {


                        tCNUDOrder[tCNUDOrder.Keys.First()] = (Math.Abs(tMHIOs.Values.First().lastQTY - tCNUDOrder.Values.First().lastQTY), tCNUDOrder.Values.First().rollData);
                        var tC = tCNUDOrder.Values.First().rollData;
                        var tM = tMHIOs.Values.First().rollData;


                        hCNRHBean.BHNO = tC.BHNO;
                        hCNRHBean.TDATE = DateTime.Now.ToString("yyyyMMdd");
                        hCNRHBean.RDATE = tC.TDATE;      //TCNUD.TDATE
                        hCNRHBean.CSEQ = tC.CSEQ;        //TCNUD.CSEQ
                        hCNRHBean.BDSEQ = tC.DSEQ;       //TCNUD.DSEQ
                        hCNRHBean.BDNO = tC.DNO;         //TCNUD.DNO
                        hCNRHBean.SDSEQ = tM.DSEQ;
                        hCNRHBean.SDNO = tM.JRNUM;
                        hCNRHBean.STOCK = tC.STOCK;
                        hCNRHBean.CQTY = tMHIOs[tMHIOs.Keys.First()].lastQTY;               //==============
                        hCNRHBean.BPRICE = tC.PRICE;     //TCNUD.PRICE
                        hCNRHBean.BFEE = Math.Round(tCNUDOrder.Values.First().rollData.FEE * (hCNRHBean.CQTY / tC.BQTY));              //==============
                        hCNRHBean.SPRICE = tM.PRICE;
                        //零股最小手續費1元，整股最小手續費10元
                        decimal sfee = Math.Round(tM.PRICE * hCNRHBean.CQTY * 0.001425m);
                        if (hCNRHBean.CQTY >= 1000 && Math.Round(tM.PRICE * hCNRHBean.CQTY * 0.001425m) < 20)
                        {
                            sfee = 20m;
                        }
                        hCNRHBean.SFEE = sfee;               //==============
                        hCNRHBean.TAX = Math.Round(tM.PRICE * hCNRHBean.CQTY * 0.003m);                //==============
                        hCNRHBean.INCOME = Math.Floor(tM.PRICE * hCNRHBean.CQTY) - hCNRHBean.SFEE - hCNRHBean.TAX;          //==============
                        hCNRHBean.COST = Math.Floor(tC.PRICE * hCNRHBean.CQTY + hCNRHBean.BFEE);              //TCNUD.COST
                        hCNRHBean.PROFIT = hCNRHBean.INCOME - tC.COST;          //
                        hCNRHBean.ADJDATE = tC.ADJDATE;  //TCNUD.ADJDATE
                        hCNRHBean.WTYPE = tC.WTYPE;      //TCNUD.WTYPE
                        hCNRHBean.BQTY = tC.BQTY;        //TCNUD.BQTY
                        hCNRHBean.SQTY = tM.QTY;
                        hCNRHReturn.Add(hCNRHBean);


                        if (tMHIOs.Count == 1 )
                        {
                            //( 原始BFEE * ( CQTY / BQTY) ) 四捨五入至整數
                            tCNUDOrder.Values.First().rollData.FEE = tCNUDOrder.Values.First().rollData.FEE- hCNRHBean.BFEE;

                            //( BPRICE * CQTY ) 無條件捨去至整數 + 此筆BFEE
                            tCNUDOrder.Values.First().rollData.COST = tCNUDOrder.Values.First().rollData.COST - hCNRHBean.COST;

                            tCNUDOrder.Values.First().rollData.BQTY = tCNUDOrder[tCNUDOrder.Keys.First()].lastQTY;

                        }

                        tMHIOs.Remove(tMHIOs.Keys.First());
                        return isWritOff(tMHIOs, tCNUDOrder);
                    }
                    if (tMHIOs.Values.FirstOrDefault().lastQTY > tCNUDOrder.Values.FirstOrDefault().lastQTY)
                    {
                        tMHIOs[tMHIOs.Keys.First()] = (Math.Abs(tMHIOs.Values.First().lastQTY - tCNUDOrder.Values.First().lastQTY), tMHIOs.Values.First().rollData);
                        var tC = tCNUDOrder.Values.First().rollData;
                        var tM = tMHIOs.Values.First().rollData;

                        hCNRHBean.BHNO = tC.BHNO;
                        hCNRHBean.TDATE = DateTime.Now.ToString("yyyyMMdd");
                        hCNRHBean.RDATE = tC.TDATE;      //TCNUD.TDATE
                        hCNRHBean.CSEQ = tC.CSEQ;        //TCNUD.CSEQ
                        hCNRHBean.BDSEQ = tC.DSEQ;       //TCNUD.DSEQ
                        hCNRHBean.BDNO = tC.DNO;         //TCNUD.DNO
                        hCNRHBean.SDSEQ = tM.DSEQ;
                        hCNRHBean.SDNO = tM.JRNUM;
                        hCNRHBean.STOCK = tC.STOCK;
                        hCNRHBean.CQTY = tCNUDOrder[tCNUDOrder.Keys.First()].lastQTY;               //==============
                        hCNRHBean.BPRICE = tC.PRICE;     //TCNUD.PRICE
                        hCNRHBean.BFEE = Math.Round(tCNUDOrder.Values.First().rollData.FEE * (hCNRHBean.CQTY / tC.BQTY));              //==============
                        hCNRHBean.SPRICE = tM.PRICE;
                        hCNRHBean.SPRICE = tM.PRICE;
                        //零股最小手續費1元，整股最小手續費10元
                        decimal sfee = Math.Round(tM.PRICE * hCNRHBean.CQTY * 0.001425m);
                        if (hCNRHBean.CQTY >= 1000 && Math.Round((tM.PRICE * hCNRHBean.CQTY) * 0.001425m) < 20)
                        {
                            sfee = 20m;
                        }
                        hCNRHBean.SFEE = sfee;               //==============
                        hCNRHBean.TAX = Math.Round(tM.PRICE * hCNRHBean.CQTY * 0.003m);                //==============
                        hCNRHBean.INCOME = Math.Floor(tM.PRICE * hCNRHBean.CQTY) - hCNRHBean.SFEE - hCNRHBean.TAX;          //==============
                        hCNRHBean.COST = Math.Floor(tC.PRICE * hCNRHBean.CQTY + hCNRHBean.BFEE);              //TCNUD.COST
                        hCNRHBean.PROFIT = hCNRHBean.INCOME - tC.COST;          //
                        hCNRHBean.ADJDATE = tC.ADJDATE;  //TCNUD.ADJDATE
                        hCNRHBean.WTYPE = tC.WTYPE;      //TCNUD.WTYPE
                        hCNRHBean.BQTY = tC.BQTY;        //TCNUD.BQTY
                        hCNRHBean.SQTY = tM.QTY;
                        hCNRHReturn.Add(hCNRHBean);

                        if (tCNUDOrder.Count == 1)
                        {
                            //( 原始BFEE * ( CQTY / BQTY) ) 四捨五入至整數
                            tCNUDOrder.Values.First().rollData.FEE = tCNUDOrder.Values.First().rollData.FEE - hCNRHBean.BFEE;

                            //( BPRICE * CQTY ) 無條件捨去至整數 + 此筆BFEE
                            tCNUDOrder.Values.First().rollData.COST = tCNUDOrder.Values.First().rollData.COST - hCNRHBean.COST;

                            tCNUDOrder.Values.First().rollData.BQTY = tCNUDOrder[tCNUDOrder.Keys.First()].lastQTY;

                        }

                        tCNUDOrder.Remove(tCNUDOrder.Keys.First());
                        return isWritOff(tMHIOs, tCNUDOrder);
                    }
                    if (tMHIOs.Values.FirstOrDefault().lastQTY == tCNUDOrder.Values.FirstOrDefault().lastQTY)
                    {
                        var tC = tCNUDOrder.Values.First().rollData;
                        var tM = tMHIOs.Values.First().rollData;

                        hCNRHBean.BHNO = tC.BHNO;
                        hCNRHBean.TDATE = DateTime.Now.ToString("yyyyMMdd");
                        hCNRHBean.RDATE = tC.TDATE;      //TCNUD.TDATE
                        hCNRHBean.CSEQ = tC.CSEQ;        //TCNUD.CSEQ
                        hCNRHBean.BDSEQ = tC.DSEQ;       //TCNUD.DSEQ
                        hCNRHBean.BDNO = tC.DNO;         //TCNUD.DNO
                        hCNRHBean.SDSEQ = tM.DSEQ;
                        hCNRHBean.SDNO = tM.JRNUM;
                        hCNRHBean.STOCK = tC.STOCK;
                        hCNRHBean.CQTY = tC.QTY;               //==============
                        hCNRHBean.BPRICE = tC.PRICE;     //TCNUD.PRICE
                        hCNRHBean.BFEE = tC.FEE;              //==============
                        hCNRHBean.SPRICE = tM.PRICE;
                        //零股最小手續費1元，整股最小手續費10元
                        decimal sfee = Math.Round(tM.PRICE * tC.QTY * 0.001425m);
                        if (hCNRHBean.CQTY >= 1000 && Math.Round((tM.PRICE * tC.QTY) * 0.001425m) < 20)
                        {
                            sfee = 20m;
                        }
                        hCNRHBean.SFEE = sfee;               //==============
                        hCNRHBean.TAX = Math.Round(tM.PRICE * tC.QTY * 0.003m);                //==============
                        hCNRHBean.INCOME = Math.Floor(tM.PRICE * tC.QTY) - hCNRHBean.SFEE - hCNRHBean.TAX;          //==============
                        hCNRHBean.COST = Math.Floor(tC.PRICE * hCNRHBean.CQTY + hCNRHBean.BFEE); ;              //TCNUD.COST
                        hCNRHBean.PROFIT = hCNRHBean.INCOME - tC.COST;          //
                        hCNRHBean.ADJDATE = tC.ADJDATE;  //TCNUD.ADJDATE
                        hCNRHBean.WTYPE = tC.WTYPE;      //TCNUD.WTYPE
                        hCNRHBean.BQTY = tC.BQTY;        //TCNUD.BQTY
                        hCNRHBean.SQTY = tM.QTY;
                        hCNRHReturn.Add(hCNRHBean);

                        tMHIOs.Remove(tMHIOs.Keys.First());
                        tCNUDOrder.Remove(tCNUDOrder.Keys.First());
                        return isWritOff(tMHIOs, tCNUDOrder);
                    }
                    return null;
                }
            }
            return total;
        }
    }
}
