using ESMP.STOCK.API.Bean;
using ESMP.STOCK.API.DTO;
using ESMP.STOCK.API.DTO.UnrealizedGainsAndlLosses;
using ESMP.STOCK.API.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ESMP.STOCK.API.QUERYAPI.Tests
{
    [TestClass()]
    public class UnrealizedGainsAndlLossesTests
    {

        /// <summary>
        /// 未實現損益
        /// </summary>
        private UnrealizedGainsAndlLosses _realizedGainsAndlls = new UnrealizedGainsAndlLosses("Server = .;Database = ESMP;Trusted_Connection=true");
        private BaseAPI baseAPI = new BaseAPI("Server = .;Database = ESMP;Trusted_Connection=true");

        private List<TCRUDBean> _tCRUDs = new List<TCRUDBean>();
        private List<TDBUDBean> _tDBUDs = new List<TDBUDBean>();
        //預估賣出價金計算結果 - 3組
        [TestMethod()]
        public void PriceCheck1()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "057047";
            tCNUD.BQTY = 200;
            tCNUDs.Add(tCNUD);


            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出價金=現價*庫存股數
                Assert.AreEqual(details.EstimateAmt, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice)));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }
        [TestMethod()]
        public void PriceCheck2()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0070"; //lastQTY =75.0000
            tCNUD.BQTY = 100;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出價金=現價*庫存股數
                Assert.AreEqual(details.EstimateAmt, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice)));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }

        }
        [TestMethod()]
        public void PriceCheck3()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0081"; //lastQTY =936.0000
            tCNUD.BQTY = 10;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出價金=現價*庫存股數
                Assert.AreEqual(details.EstimateAmt, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice)));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }

        }
        //預估賣出手續費計算結果 - 3組
        [TestMethod()]
        public void FeeCheck1()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "057047"; //lastQTY =15.0000
            tCNUD.BQTY = 200;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價


            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出手續費 = 股數*現價*手續費比率
                Assert.AreEqual(details.EstimateFee, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        [TestMethod()]
        public void FeeCheck2()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0070"; //lastQTY =75.0000
            tCNUD.BQTY = 100;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價


            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出手續費 = 股數*現價*手續費比率
                Assert.AreEqual(details.EstimateFee, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }

        }

        [TestMethod()]
        public void FeeCheck3()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0081"; //lastQTY =936.0000
            tCNUD.BQTY = 10;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價


            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出手續費 = 股數*現價*手續費比率
                Assert.AreEqual(details.EstimateFee, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }

        }


        //預估賣出交易稅計算結果 - 3組

        [TestMethod()]
        public void AmtCheck1()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "057047";
            tCNUD.BQTY = 200;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價


            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出交易稅 = 股數*現價*交易稅比率
                Assert.AreEqual(details.EstimateTax, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        [TestMethod()]
        public void AmtCheck2()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0070";
            tCNUD.BQTY = 100;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價


            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出交易稅 = 股數*現價*交易稅比率
                Assert.AreEqual(details.EstimateTax, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        [TestMethod()]
        public void AmtCheck3()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0081";
            tCNUD.BQTY = 10;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價


            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估賣出交易稅 = 股數*現價*交易稅比率
                Assert.AreEqual(details.EstimateTax, Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }

        }


        //預估市值計算結果 - 3組
        [TestMethod()]
        public void marketValueCheck1()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "057047"; //lastQTY =15.0000
            tCNUD.BQTY = 200;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);
            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //現值(市值)= 預估賣出價金 * 預估賣出手續費 *預估賣出交易稅
                Assert.AreEqual(details.Marketvalue,
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice)) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        [TestMethod()]
        public void marketValueCheck2()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0070"; //lastQTY =75.0000
            tCNUD.BQTY = 100;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);
            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //現值(市值)= 預估賣出價金 * 預估賣出手續費 *預估賣出交易稅
                Assert.AreEqual(details.Marketvalue,
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice)) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        [TestMethod()]
        public void marketValueCheck3()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0081"; //lastQTY =936.0000
            tCNUD.BQTY = 10;
            tCNUDs.Add(tCNUD);

            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);
            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //現值(市值)= 預估賣出價金 * 預估賣出手續費 *預估賣出交易稅
                Assert.AreEqual(details.Marketvalue,
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice)) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }

        }

        //預估損益計算結果 - 3組

        [TestMethod()]
        public void profitCheck1()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "057047"; //lastQTY =15.0000
            tCNUD.BQTY = 200;
            tCNUD.COST = 20;
            tCNUDs.Add(tCNUD);
            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估損益=(預估賣出價金*預估賣出手續費*預估賣出交易稅)-成本
                Assert.AreEqual(details.Profit,
                    (Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m)) -
                    tCNUDs.First().COST));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        [TestMethod()]
        public void profitCheck2()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0070"; //lastQTY =75.0000
            tCNUD.BQTY = 100;
            tCNUD.COST = 10;
            tCNUDs.Add(tCNUD);
            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估損益=(預估賣出價金*預估賣出手續費*預估賣出交易稅)-成本
                Assert.AreEqual(details.Profit,
                    (Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m)) -
                    tCNUDs.First().COST));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        [TestMethod()]
        public void profitCheck3()
        {
            List<TCNUDBean>
             tCNUDs = new List<TCNUDBean>();
            TCNUDBean tCNUD = new TCNUDBean();
            tCNUD.BHNO = "592S";
            tCNUD.CSEQ = "0000019";
            tCNUD.STOCK = "0081"; //lastQTY =936.0000
            tCNUD.BQTY = 10;
            tCNUD.COST = 50;
            tCNUDs.Add(tCNUD);
            string DealPrice = "3.7300";    //現價

            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = tCNUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估損益=(預估賣出價金*預估賣出手續費*預估賣出交易稅)-成本
                Assert.AreEqual(details.Profit,
                    (Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m)) -
                    tCNUDs.First().COST));
            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }

        }
        //當日現股買進－價金計算－整股/零股各2組
        //當日現股買進－手續費計算－整股/零股各2組
        //當日現股買進－成本計算－整股/零股各2組

        //融資未實現損益
        [TestMethod()]
        public void tCRUDCheck()
        {
            _tCRUDs = new List<TCRUDBean>();
            TCRUDBean tCRUD = new TCRUDBean();
            tCRUD.BHNO = "592S";
            tCRUD.CSEQ = "0000019";
            tCRUD.STOCK = "0081"; //lastQTY =936.0000
            tCRUD.BQTY = 10;
            tCRUD.COST = 50;
            tCRUD.PRICE = 100;
            tCRUD.BCRAMT = 50;
            _tCRUDs.Add(tCRUD);

            string DealPrice = "3.7300";    //現價
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = _tCRUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估損益=(預估賣出價金*預估賣出手續費*預估賣出交易稅)-成本
                //Assert.AreEqual(details.Profit,
                //    (Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) -
                //    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                //    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m)) -
                //    tCNUDs.First().COST));

            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }

        //融券未實現損益
        [TestMethod()]
        public void tDBUDCheck()
        {
            _tDBUDs = new List<TDBUDBean>();
            TDBUDBean tDBUD = new TDBUDBean();
            tDBUD.BHNO = "592S";
            tDBUD.CSEQ = "0000019";
            tDBUD.STOCK = "0081"; //lastQTY =936.0000
            tDBUD.BQTY = 10;
            tDBUD.COST = 50;
            tDBUD.PRICE = 100;
            _tDBUDs.Add(tDBUD);

            string DealPrice = "3.7300";    //現價
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            List<TMHIOBean> tmHIO = new List<TMHIOBean>();
            List<TCSIOBean> tCSIO = new List<TCSIOBean>();
            List<MCUMSBean> mCUMS = new List<MCUMSBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            var change = new CoverQuoteUnrealize("Server = .;Database = ESMP;Trusted_Connection=true",
                new List<Symbol>().Append(
                    new Symbol
                    {
                        Id = _tDBUDs.First().STOCK,
                        DealPrice = DealPrice
                    }).ToList(), 100, 0, 0, 0, 0);

            var obj = change.QueryIntoFormatString(tCNUDs, tmHIO, tCSIO, tCNTDBeans, t201Beans, _tCRUDs, _tDBUDs);

            if (obj.GetType() == typeof(Accsum))
            {
                Accsum ac = (Accsum)obj;
                Detail details = ac.UnoffsetQtypeSum.First().UnoffsetQtypeDetail.First();
                //預估損益=(預估賣出價金*預估賣出手續費*預估賣出交易稅)-成本
                //Assert.AreEqual(details.Profit,
                //    (Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) -
                //    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.001425m) -
                //    Math.Floor(tCNUDs.First().BQTY * Convert.ToDecimal(DealPrice) * 0.003m)) -
                //    tCNUDs.First().COST));

            }
            else if (obj.GetType() == typeof(AccsumErr))
            {
                AccsumErr acEr = (AccsumErr)obj;
                Assert.Fail(acEr.Errcode + " : " + acEr.Errmsg);
            }
            else
            {
                Assert.Fail("QueryIntoFormatString的回傳尚未匹配到任何型別" + obj.GetType());
            }
        }


        /// <summary>
        /// 已實現損益
        /// </summary>
        private RealizedProfitAndLoss _realizedProfitAndLoss = new RealizedProfitAndLoss("Server = .;Database = ESMP;Trusted_Connection=true");

        //3筆HCNRH(同TDATE、SDSEQ、SDNO)資料整理至profit_sum格式中，資料填入正確、profit_detail_out加總資料正確
        //3筆HCNTD(同TDATE、SDSEQ、SDNO)資料整理至profit_sum格式中，資料填入正確、profit_detail_out加總資料正確


        /// <summary>
        /// 對帳單查詢
        /// </summary>
        private Statement statement = new Statement("Server = .;Database = ESMP;Trusted_Connection=true");
        //價金計算結果 - 3組
        //手續費計算結果 - 3組
        //交易稅計算結果 - 3組
        //淨收付計算結果 - 3組


        /// <summary>
        /// 沖銷現股餘額
        /// </summary>
        private WriteOff wf = new WriteOff();
        //有昨日現股餘額、今日全部賣出－檢核(Ram)TCNUD與(Ram)HCNRH資料是否如預期
        [TestMethod()]
        public void TMHIOWriteOffTest()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20160504", BHNO = "592z", CSEQ = "0126687", DSEQ = "k8563", STOCK = "2330", PRICE = 147, QTY = 1000, BQTY = 1000, FEE = 78, COST = 147.078m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20160509", BHNO = "592z", CSEQ = "0126687", DSEQ = "k0254", STOCK = "2330", PRICE = 148.5m, QTY = 1000, BQTY = 1000, FEE = 79, COST = 148.579m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20160509", BHNO = "592z", CSEQ = "0126687", DSEQ = "k0266", STOCK = "2330", PRICE = 148.5m, QTY = 1000, BQTY = 1000, FEE = 79, COST = 148.579m });
            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413598", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "2330", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114543715" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413599", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "2330", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            List<TCSIOBean> tCSIOBeans = new List<TCSIOBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();


            (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = wf.StockWriteOff(tCNUDs, tMHIOs, tCSIOBeans, tCNTDBeans, t201Beans);
            List<TCNUDBean> checkTC = TC;
            List<HCNRHBean> checkHC = HC;
            Assert.AreEqual(1, checkTC.Count);
            Assert.AreEqual(2, checkHC.Count);

        }
        //有昨日現股餘額、今日部分賣出（需部分沖銷）－檢核(Ram)TCNUD與(Ram)HCNRH資料是否如預期
        [TestMethod()]
        public void TMHIOWriteOffTest1()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131028", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 0, QTY = 138, BQTY = 138, FEE = 0, COST = 0 });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131106", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 84.7m, QTY = 1000, BQTY = 1000, FEE = 120, COST = 84820m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131205", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 65.5m, QTY = 1000, BQTY = 1000, FEE = 93, COST = 65593m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131206", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 66.6m, QTY = 862, BQTY = 862, FEE = 81, COST = 57489m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140521", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 62.2m, QTY = 1000, BQTY = 1000, FEE = 88, COST = 62288m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });
            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "amPM0", JRNUM = "02844984", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 27.5m, SALES = "0085", ORGIN = "0", MTIME = "300106186" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "amPM2", JRNUM = "02844985", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 27.8m, SALES = "0085", ORGIN = "0", MTIME = "300106187" });

            List<TCSIOBean> tCSIOBeans = new List<TCSIOBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = wf.StockWriteOff(tCNUDs, tMHIOs, tCSIOBeans, tCNTDBeans, t201Beans);
            List<TCNUDBean> checkTC = TC;
            List<HCNRHBean> checkHC = HC;
            Assert.AreEqual(4, checkHC.Count);
            Assert.AreEqual(862, checkHC.Last().CQTY);
            Assert.AreEqual(80, checkHC.Last().BFEE);
            Assert.AreEqual(65.5m, checkHC.Last().BPRICE);


            Assert.AreEqual(4, checkTC.Count);
            Assert.AreEqual(138, checkTC.First().BQTY);
            Assert.AreEqual(13, checkTC.First().FEE);
            Assert.AreEqual(9052, checkTC.First().COST);
        }

        //有昨日現股餘額、有今日匯入（TCSIO）、今日部分賣出（需部分沖銷）－檢核(Ram)TCNUD與(Ram)HCNRH資料是否如預期
        [TestMethod()]
        public void TMHIOWriteOffTest2()
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131028", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 0, QTY = 138, BQTY = 138, FEE = 0, COST = 0 });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131106", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 84.7m, QTY = 1000, BQTY = 1000, FEE = 120, COST = 84820m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131205", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 65.5m, QTY = 1000, BQTY = 1000, FEE = 93, COST = 65593m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20131206", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 66.6m, QTY = 862, BQTY = 862, FEE = 81, COST = 57489m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140521", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 62.2m, QTY = 1000, BQTY = 1000, FEE = 88, COST = 62288m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });
            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "amPM0", JRNUM = "02844984", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 27.5m, SALES = "0085", ORGIN = "0", MTIME = "300106186" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "amPM2", JRNUM = "02844985", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 27.8m, SALES = "0085", ORGIN = "0", MTIME = "300106187" });

            List<TCSIOBean> tCSIOBeans = new List<TCSIOBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = wf.StockWriteOff(tCNUDs, tMHIOs, tCSIOBeans, tCNTDBeans, t201Beans);
            List<TCNUDBean> checkTC = TC;
            List<HCNRHBean> checkHC = HC;
            Assert.AreEqual(4, checkHC.Count);
            Assert.AreEqual(862, checkHC.Last().CQTY);
            Assert.AreEqual(80, checkHC.Last().BFEE);
            Assert.AreEqual(65.5m, checkHC.Last().BPRICE);



            Assert.AreEqual(4, checkTC.Count);
            Assert.AreEqual(138, checkTC.First().BQTY);
            Assert.AreEqual(13, checkTC.First().FEE);
            Assert.AreEqual(9052, checkTC.First().COST);
        }

        [TestMethod()]
        //［現股當沖資格：X］今日賣出10張、今日買進10張－檢核 (Ram)HCNTD資料是否如預期
        public void TMHIOWriteOffTest3()
        {
            WriteOff cover = new CoverStockPermissions("X");
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });


            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413598", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114543715" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413599", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413600", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413601", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413602", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413603", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413604", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413605", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413606", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01413607", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            List<TCSIOBean> tCSIOBeans = new List<TCSIOBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = cover.StockWriteOff(tCNUDs, tMHIOs, tCSIOBeans, tCNTDBeans, t201Beans);
            List<TCNUDBean> checkTC = TC;
            List<HCNTDBean> checkHCNT = HCNT;
            Assert.AreEqual(1, checkTC.Count);
            Assert.AreEqual(5, checkHCNT.Count);

        }
        //［現股當沖資格：X］今日賣出10張、今日買進12張（需部分沖銷）－檢核(Ram)HCNTD資料是否如預期
        [TestMethod()]
        public void TMHIOWriteOffTest4()
        {
            WriteOff cover = new CoverStockPermissions("X");
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });


            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114543715" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "02", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "03", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "04", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "05", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "06", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "07", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "08", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "09", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "10", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "11", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 500, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "12", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "13", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "14", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "15", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "16", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "17", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "18", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "19", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "20", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "21", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "22", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            List<TCSIOBean> tCSIOBeans = new List<TCSIOBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = cover.StockWriteOff(tCNUDs, tMHIOs, tCSIOBeans, tCNTDBeans, t201Beans);
            List<TCNUDBean> checkTC = TC;
            List<HCNTDBean> checkHCNT = HCNT;
            Assert.AreEqual(3, checkTC.Count);
            Assert.AreEqual(20, checkHCNT.Count);


        }

        //［現股當沖資格：X］有昨日現股餘額、今日賣出10張、今日買進7張（需含部分沖銷）－檢核(Ram)TCNUD、(Ram)HCNRH 與 (Ram)HCNTD資料是否如預期

        [TestMethod()]
        public void TMHIOWriteOffTest5()
        {
            WriteOff cover = new CoverStockPermissions("X");
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });


            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114543715" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "02", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "03", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "04", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "05", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "06", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "07", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "08", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "09", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "10", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "11", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 500, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "12", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "13", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "14", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "15", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "16", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "17", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            List<TCSIOBean> tCSIOBeans = new List<TCSIOBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = cover.StockWriteOff(tCNUDs, tMHIOs, tCSIOBeans, tCNTDBeans, t201Beans);
            List<TCNUDBean> checkTC = TC;
            List<HCNTDBean> checkHCNT = HCNT;
            //沖銷完後剩下4筆賣單
            Assert.AreEqual(4, checkTC.Count);
            //歷史現股當沖對應檔總共有13筆
            Assert.AreEqual(13, checkHCNT.Count);
        }

        //［現股當沖資格：Y］有昨日現股餘額、今日賣出10張（其中3張買進早）、今日買進7張（需含部分沖銷）－檢核(Ram)TCNUD、(Ram)HCNRH 與 (Ram)HCNTD資料是否如預期
        [TestMethod()]
        public void TMHIOWriteOffTest6()
        {
            //(Ram)TCNUD－昨日餘額扣掉3張
            WriteOff cover = new CoverStockPermissions("Y");
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });
            tCNUDs.Add(new TCNUDBean() { TDATE = "20140704", BHNO = "592z", CSEQ = "0105097", STOCK = "3630", PRICE = 50.8m, QTY = 1000, BQTY = 1000, FEE = 72, COST = 50872m });


            List<TMHIOBean> tMHIOs = new List<TMHIOBean>();
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "04", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114543715" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "05", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "06", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "07", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "08", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "09", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "10", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "11", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "12", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "13", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "S", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            //其中三張買進早
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "01", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 500, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "02", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "03", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "14", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "15", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "16", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });
            tMHIOs.Add(new TMHIOBean() { Tdate = "20221017", BHNO = "592z", DSEQ = "j0614", JRNUM = "17", MTYPE = "T", CSEQ = "0105097", TTYPE = "0", ETYPE = "0", BSTYPE = "B", STOCK = "3630", QTY = 1000, PRICE = 399.5m, SALES = "0071", ORGIN = "1", MTIME = "114553161" });

            List<TCSIOBean> tCSIOBeans = new List<TCSIOBean>();

            List<TCNTDBean> tCNTDBeans = new List<TCNTDBean>();
            List<T201Bean> t201Beans = new List<T201Bean>();

            (List<TCNUDBean> TC, List<HCNTDBean> HCNT, List<HCNRHBean> HC, List<HCMIOBean> HCM) = cover.StockWriteOff(tCNUDs, tMHIOs, tCSIOBeans, tCNTDBeans, t201Beans);
            List<TCNUDBean> checkTC = TC;
            List<HCNTDBean> checkHCNT = HCNT;
            //沖銷完後剩下4筆賣單
            Assert.AreEqual(9, checkTC.Count);
            //歷史現股當沖對應檔總共有13筆
            Assert.AreEqual(5, checkHCNT.Count);
        }
        [TestMethod()]
        public void testQuote()
        {
            //測試假資料
            //int testdata = 3240;
            int testdata = 100000;
            List<string> listStr = new List<string>();
            for (int i = 0; i < testdata; i++)
            {
                //listStr.Add("0050");
                listStr.Add("0056");
            }

            var list = baseAPI.QuoteAsync(listStr).Result;

            Assert.AreEqual(testdata, list.Count);
            //Assert.AreEqual("元大台灣50", list.First().ShortName);
            //Assert.AreEqual("元大高股息", list.Last().ShortName);

        }

        [TestMethod()]
        public void TCNUDProvider()
        {

            IEnumerable<TCNUDBean> tCNUDBeans0001 =
            SQLProviderTCNUD.Where("592S", "0034661","1303");



        }


    }
    //CoverStockPermissions去覆蓋WriteOff裡面StockPermissions的權限資料
    public class CoverStockPermissions : WriteOff
    {
        private string type = "";
        public CoverStockPermissions(string type)
        {
            this.type = type;
        }
        public override string StockPermissions(string costomer, string stock)
        {
            return type;
        }
    }

    //CoverQuote去覆蓋BaseAPI裡面QuoteAsync的資料
    public class CoverQuoteUnrealize : UnrealizedGainsAndlLosses
    {
        private string _connstr = "";
        private static List<Symbol> _list = null;
        private static decimal MCSRH_CNQBALE = 0;
        private static decimal MCSRH_CRAQTY = 0;
        private static decimal MCSRH_CROQTY = 0;
        private static decimal MCSRH_DBAQTY = 0;
        private static decimal MCSRH_DBOQTY = 0;


        public CoverQuoteUnrealize(string connstr, List<Symbol> list, decimal MCSRH_CNQBALE, decimal MCSRH_CRAQTY, decimal MCSRH_CROQTY, decimal MCSRH_DBAQTY, decimal MCSRH_DBOQTY) : base(connstr)
        {
            this._connstr = connstr;
            CoverQuoteUnrealize._list = list;
            CoverQuoteUnrealize.MCSRH_CNQBALE = MCSRH_CNQBALE;
            CoverQuoteUnrealize.MCSRH_CRAQTY = MCSRH_CRAQTY;
            CoverQuoteUnrealize.MCSRH_CROQTY = MCSRH_CROQTY;
            CoverQuoteUnrealize.MCSRH_DBAQTY = MCSRH_DBAQTY;
            CoverQuoteUnrealize.MCSRH_DBOQTY = MCSRH_DBOQTY;
        }

        public override Task<List<Symbol>> QuoteAsync(List<string> strings)
        {
            Task<List<Symbol>> type = Task.FromResult(_list);
            return type;
        }

        public override decimal MCSRHQueryCNQBALE(string BHNO, string CSEQ, string stock)
        {
            return MCSRH_CNQBALE;
        }
        public override decimal MCSRHQueryCRAQTY(string BHNO, string CSEQ, string stock)
        {
            return MCSRH_CRAQTY;
        }
        public override decimal MCSRHQueryCROQTY(string BHNO, string CSEQ, string stock)
        {
            return MCSRH_CROQTY;
        }
        public override decimal MCSRHQueryDBAQTY(string BHNO, string CSEQ, string stock)
        {
            return MCSRH_DBAQTY;
        }
        public override decimal MCSRHQueryDBOQTY(string BHNO, string CSEQ, string stock)
        {
            return MCSRH_DBOQTY;
        }
    }

    public class CoverQuoteRealize : RealizedProfitAndLoss
    {

        public CoverQuoteRealize(string connstr) : base(connstr)
        {

        }

    }


}