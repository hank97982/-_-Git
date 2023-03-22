using ESMP.STOCK.API.Bean;
using ESMP.STOCK.API.DTO;

namespace ESMP.STOCK.API.Utils
{
    public class WriteOff
    {
        /*
         * 回傳:
         *  (List<TCNUDBean>)回傳剩餘庫存
         *  (List<HCNTDBean>)回傳當沖明細
         *  (List<HCNRHBean>)回傳沖銷明細
         *  (List<HCNRHBean>)不能被零股沖銷的賣單
         * 
         * 傳入:
         *  List<TCNUDBean> 現股餘額檔
         *  List<TMHIOBean> 當日交易明細
         *  List<TCSIOBean> 當入交易現股匯撥檔
         */
        public (List<TCNUDBean>, List<HCNTDBean>, List<HCNRHBean>, List<HCMIOBean>) StockWriteOff(List<TCNUDBean> tCNUDs, List<TMHIOBean> tMHIOs, List<TCSIOBean> tCSIOs)
        {
            List<HCNRHBean> hCNRHReturn = new List<HCNRHBean>();
            List<HCNTDBean> hCNTDTotalReturn = new List<HCNTDBean>();
            List<HCMIOBean> hCMIOs = new List<HCMIOBean>();
            List<HCMIOBean> hCMIOIgnoreReturn = new List<HCMIOBean>();
            //List<HCMIOBean> hCMIOnow = new List<HCMIOBean>();


            List<TCNUDBean> tCNUDtotal = new List<TCNUDBean>(tCNUDs);

            #region
            //total.CopyTo(tCNUDs);//因為.ToList()把List<>轉換成IEnuable<>，在隱含轉換成List<>
            //List<TCNUDBean> total = tCNUDs.ToList();//如果加.ToList()，(294)RemoveAll不會連tCNUD也移除掉
            //List<TCNUDBean> total = tCNUDs;
            #endregion

            #region 今日賣出TMHIO的資料轉入(Ram)HCMIO(沖銷計算)
            foreach (var tMHIO in tMHIOs)
            {
                HCMIOBean hCMIO = new HCMIOBean();
                hCMIO.Tdate = tMHIO.Tdate;              //交易日
                hCMIO.BHNO = tMHIO.BHNO;                //分公司代號
                hCMIO.CSEQ = tMHIO.CSEQ;                //客戶帳號
                hCMIO.DSEQ = tMHIO.DSEQ;                //委託書號
                hCMIO.DNO = tMHIO.JRNUM;                //分單號碼
                hCMIO.WTYPE = "0";                      //異動別 0-交易 A-集保匯撥 B-自訂資產 C-其他 S-系統
                hCMIO.STOCK = tMHIO.STOCK;              //股票代號
                hCMIO.TTYPE = tMHIO.TTYPE;              //委託別 0-普通 1-貸資 2-代券 3-融資 4-融券
                hCMIO.ETYPE = tMHIO.ETYPE;              //交易別 0-整股 1-零股 代碼 ETYPE
                hCMIO.BSTYPE = tMHIO.BSTYPE;            //買賣別 B-買/入庫 S-賣/出庫 代碼 BSTY
                hCMIO.PRICE = tMHIO.PRICE;              //單價
                hCMIO.QTY = tMHIO.QTY;                  //股數
                hCMIO.AMT = Math.Floor(hCMIO.PRICE * hCMIO.QTY);    //價金
                hCMIO.FEE = FeeCalculate(hCMIO.QTY, Math.Round(hCMIO.AMT * 0.001425m));       //手續費
                hCMIO.TAX = Math.Round(hCMIO.AMT * 0.003m);         //交易稅
                hCMIO.RVINT = 0;                        //債息
                hCMIO.NETAMT = hCMIO.AMT - hCMIO.FEE - hCMIO.TAX;   //淨收付金額
                hCMIO.DBFEE = 0;                        //融券手續費
                hCMIO.CRAMT = 0;                        //融資/保證金金額
                hCMIO.DNAMT = 0;                        //擔保品金額
                hCMIO.CRINT = 0;                        //融資/保證金利息
                hCMIO.DNINT = 0;                        //擔保品利息
                hCMIO.DLFEE = 0;                        //標借卷費
                hCMIO.BFINT = 0;                        //標借卷費利息
                hCMIO.OBAMT = 0;                        //逾期手續費
                hCMIO.INTAX = 0;                        //代扣所得稅
                hCMIO.SFCODE = "";                      //證金代號
                hCMIO.CDTDQTY = 0;                      //當沖股數
                hCMIO.ORIGN = tMHIO.ORGIN;              //委託來源 代碼 = ODOR
                hCMIO.SALES = tMHIO.SALES;              //
                hCMIO.DATAFLAG = "";                    //資料註記 空白: 一般單 1:減資出庫 2:賣出股數大於
                hCMIO.IOFLAG = "";                      //集保異動代碼 代碼=ODOR
                hCMIO.ADJCOST = 0;                      //調整成本
                hCMIO.ADJDATE = "";                     //調整成本日期
                hCMIO.STINTAX = 0;                      //證所稅
                hCMIO.HEALTHFEE = 0;                    //健保補充費
                hCMIO.TRDATE = tMHIO.TRDATE;            //轉檔日期
                hCMIO.TRTIME = tMHIO.TRTIME;            //轉檔時間
                hCMIO.MODDATE = tMHIO.MODDATE;          //異動日期
                hCMIO.MODTIME = tMHIO.MODTIME;          //異動時間
                hCMIO.MODUSER = "";      //異動人員

                hCMIOs.Add(hCMIO);
            }
            //今日匯出TCSIO的資料轉入(Ram)HCMIO中
            foreach (var tCSIO in tCSIOs.Where(c => c.BSTYPE == "S").ToList())
            {
                HCMIOBean hCMIO = new HCMIOBean();
                hCMIO.Tdate = tCSIO.TDATE;            //交易日
                hCMIO.BHNO = tCSIO.BHNO;              //分公司代號
                hCMIO.CSEQ = tCSIO.CSEQ;              //客戶帳號
                hCMIO.DSEQ = tCSIO.DSEQ;              //委託書號
                hCMIO.DNO = tCSIO.JRNUM;              //分單號碼
                hCMIO.WTYPE = "A";                    //異動別 0-交易 A-集保匯撥 B-自訂資產 C-其他 S-系統
                hCMIO.STOCK = tCSIO.STOCK;            //股票代號
                hCMIO.TTYPE = "";                     //委託別 0-普通 1-貸資 2-代券 3-融資 4-融券
                hCMIO.ETYPE = "";                     //交易別 0-整股 1-零股 代碼 ETYPE
                hCMIO.BSTYPE = tCSIO.BSTYPE;          //買賣別 B-買/入庫 S-賣/出庫 代碼 BSTY
                hCMIO.PRICE = 0;                      //單價
                hCMIO.QTY = tCSIO.QTY;                //股數
                hCMIO.AMT = 0;                        //價金
                hCMIO.FEE = 0;                        //手續費
                hCMIO.TAX = 0;                        //交易稅
                hCMIO.RVINT = 0;                      //債息
                hCMIO.NETAMT = 0;                     //淨收付金額
                hCMIO.DBFEE = 0;                      //融券手續費
                hCMIO.CRAMT = 0;                      //融資/保證金金額
                hCMIO.DNAMT = 0;                      //擔保品金額
                hCMIO.CRINT = 0;                      //融資/保證金利息
                hCMIO.DNINT = 0;                      //擔保品利息
                hCMIO.DLFEE = 0;                      //標借卷費
                hCMIO.BFINT = 0;                      //標借卷費利息
                hCMIO.OBAMT = 0;                      //逾期手續費
                hCMIO.INTAX = 0;                      //代扣所得稅
                hCMIO.SFCODE = "";                    //證金代號
                hCMIO.CDTDQTY = 0;                    //當沖股數
                hCMIO.ORIGN = "";                     //委託來源 代碼 = ODOR
                hCMIO.SALES = "";                     //
                hCMIO.DATAFLAG = "";                  //資料註記 空白: 一般單 1:減資出庫 2:賣出股數大於
                hCMIO.IOFLAG = "";                    //集保異動代碼 代碼=ODOR
                hCMIO.ADJCOST = 0;                    //調整成本
                hCMIO.ADJDATE = "";                   //調整成本日期
                hCMIO.STINTAX = 0;                    //證所稅
                hCMIO.HEALTHFEE = 0;                  //健保補充費
                hCMIO.TRDATE = tCSIO.TRDATE;          //轉檔日期
                hCMIO.TRTIME = tCSIO.TRTIME;          //轉檔時間
                hCMIO.MODDATE = tCSIO.MODDATE;        //異動日期
                hCMIO.MODTIME = tCSIO.MODTIME;        //異動時間
                hCMIO.MODUSER = "";                   //異動人員
                //hCMIO.COSTRam = Math.Floor(hCNRHBean.BPRICE * cqty) + hCNRHBean.BFEE
                hCMIOs.Add(hCMIO);
            }
            #endregion


            //把TCSIO(今日匯入)加入現股餘額
            tCNUDs.AddRange(proccessTCSIOBuy(tCSIOs));

            //分類股票代號執行沖銷
            foreach (var stockDistinct in hCMIOs.Select(c => c.STOCK).Distinct())
            {
                //將執行沖銷的股票代碼從總數移除
                tCNUDtotal.RemoveAll(c => c.STOCK == stockDistinct);
                //取出TCNUD指定股票代碼資料並排序
                var tCNUDLQ = tCNUDs.Where(c => c.STOCK == stockDistinct).OrderBy(c => c.TDATE).ThenBy(c => c.WTYPE).ThenBy(c => c.DNO).ToList();
                //HCMIO裡的Qty放置內存LastQTY準被做計算
                tCNUDLQ.ForEach(i => i.LastQtyRam = i.BQTY);

                //取出HCMIO指定股票代碼(沖銷計算:排序依據DSEQ)
                var hCMIOLQ = hCMIOs.Where(c => c.STOCK == stockDistinct).OrderBy(c => c.DSEQ).ThenBy(c => c.DNO).ToList();
                //HCMIO裡的Qty放置內存LastQTY準被做計算
                hCMIOLQ.ForEach(i => i.LastQtyRam = i.QTY);
                //HCMIO裡的COST另外計算放置內存給當沖情境準被做計算
                hCMIOLQ.ForEach(i => i.COSTRam = Math.Floor(i.PRICE * i.QTY) + i.FEE);


                //由於HCMIO資料被傳入前已經經過賭票代碼及客戶編號的過濾，所以直接取第一個資料

                //客戶現股當沖資格
                string costomerCNTDType = SingletonQueryProviderMCUMS.queryProvider.MCUMSQueryCNTDTYPE(hCMIOLQ.FirstOrDefault().BHNO, hCMIOLQ.FirstOrDefault().CSEQ);

                //股票現股當沖資格
                string stockCNTDType = SingletonQueryProviderMSTMB.queryProvider.MSTMBQueryCNTDTYPE(hCMIOLQ.FirstOrDefault().STOCK);


                //買單dno小於賣單dno
                List<HCMIOBean> hCMIONOWBuy = hCMIOLQ.Where(x => x.BSTYPE == "B").ToList();
                List<HCMIOBean> hCMIONOWSale = hCMIOLQ.Where(x => x.BSTYPE == "S").ToList();


                List<HCMIOBean> hCMIOIgnore = new List<HCMIOBean>();


                string type = StockPermissions(costomerCNTDType, stockCNTDType);
                if (type == "N")
                {
                    tCNUDtotal.AddRange(doWriteOff(type, hCMIOLQ, tCNUDLQ, hCMIOIgnore, hCNRHReturn));
                    tCNUDtotal.AddRange(proccessHCMIOBuyToTCNUD(hCMIOLQ));
                    continue;
                }



                //因為單向當沖被忽略掉的賣單(不是買單)
                List<HCMIOBean> hCMIOSalesIgnore = new List<HCMIOBean>();
                List<HCNTDBean> hCNTDReturn = new List<HCNTDBean>();

                (List<HCMIOBean> hCMIOBuysReturn, List<HCMIOBean> hCMIOSalesReturn) = doWriteOffNow(type, hCMIONOWSale, hCMIONOWBuy, hCMIOSalesIgnore, hCNTDReturn);

                //現股當沖完的剩餘賣單跟單向當沖淘汰掉的賣單，都要加起來沖掉TCNUD
                hCMIOSalesReturn.AddRange(hCMIOSalesIgnore);

                //這個就是今日沖銷剩餘的TCNUD
                tCNUDtotal.AddRange(doWriteOff(type, hCMIOSalesReturn, tCNUDLQ, hCMIOIgnore, hCNRHReturn));
                //現股當沖剩餘買單作為普通買進加入TCNUD裡
                tCNUDtotal.AddRange(proccessHCMIOBuyToTCNUD(hCMIOBuysReturn));
                //雙向現股沖還沒有買單對應的賣單，加進去TCNUD
                tCNUDtotal.AddRange(proccessHCMIOSalesToTCNUD(hCMIOIgnore));
                //將不能被零股沖銷的賣單保留起來，回傳給對帳單查詢
                //hCMIOIgnoreReturn.AddRange(hCMIOIgnore);

                //重新計算HCNTD交易稅從0.003改成0.0015
                hCNTDTotalReturn.AddRange(HCNTDCalculate(hCNTDReturn));

            }


            return (tCNUDtotal, hCNTDTotalReturn, hCNRHReturn, hCMIOIgnoreReturn);
        }

        //HCMIO賣 TCNUD買

        //股票沖銷
        public List<TCNUDBean> doWriteOff(string type, List<HCMIOBean> hCMIOs, List<TCNUDBean> tCNUDOrder, List<HCMIOBean> hCMIOIgnore, List<HCNRHBean> hCNRHReturn)
        {

            if (hCMIOs.Count == 0)
            {
                return tCNUDOrder.ToList();
            }

            if (tCNUDOrder.Count == 0)
            {
                return proccessHCMIOSalesToTCNUD(hCMIOs);
            }
            HCNRHBean hCNRHBean = new HCNRHBean();

            HCMIOBean hC = hCMIOs.First();//(賣)

            if (hC.ETYPE == "0" && tCNUDOrder.Sum(x => x.BQTY) < 1000 && type == "X")//*******************************
            {
                hCMIOs.Remove(hC);
                hCMIOIgnore.Add(hC);
                return doWriteOff(type, hCMIOs, tCNUDOrder, hCMIOIgnore, hCNRHReturn);
            }

            TCNUDBean tC = tCNUDOrder.First();//(買)

            hCNRHBean.BHNO = tC.BHNO;
            hCNRHBean.TDATE = DateTime.Now.ToString("yyyyMMdd");
            hCNRHBean.RDATE = tC.TDATE;
            hCNRHBean.CSEQ = tC.CSEQ;
            hCNRHBean.BDSEQ = tC.DSEQ;
            hCNRHBean.BDNO = tC.DNO;//現股餘額分單號
            hCNRHBean.SDSEQ = hC.DSEQ;
            hCNRHBean.SDNO = hC.DNO;//歷史交易明細分單號碼
            hCNRHBean.STOCK = tC.STOCK;
            hCNRHBean.ADJDATE = tC.ADJDATE;
            hCNRHBean.WTYPE = tC.WTYPE;

            decimal cqty = 0;

            //TCNUD沖多筆賣單
            //qty小的當沖銷股數
            if (tC.BQTY > hC.LastQtyRam)
            {
                //在(現在賣出剩餘股數>現在買進剩餘股數)情況中是賣單需要做沖銷比例，
                //因此以下所有買單(TCNUD)資料(包含:hCNRHBean.BFEE,hCNRHBean.COST)都需要做比例分配
                //其中題目要求hCNRHBean.INCOME要做(淨收付)計算，所以就拿已經比例分配好的BFEE,cqty跟總價金BPRICE去計算出INCOME

                cqty = hC.LastQtyRam;//沖銷股數
                //1:1的沖銷比例
                //decimal splitS = cqty / hC.LastQtyRam;      //沖銷股數 / 現在賣方剩餘股數 = 賣方沖銷比例
                decimal splitB = cqty / tC.BQTY;            //沖銷股數 / 現在買方剩餘股數 = 買方沖銷比例


                hCNRHBean.SQTY = hC.QTY;
                hCNRHBean.BQTY = tC.QTY;
                hCNRHBean.CQTY = cqty;
                hCNRHBean.SFEE = hC.FEE;
                hCNRHBean.TAX = hC.TAX;
                hCNRHBean.BPRICE = tC.PRICE;
                hCNRHBean.BFEE = Math.Round(tC.FEE * splitB);
                //拿沖銷分配好的 CQTY BFEE 給 BPRICE 去做計算產出COST成本
                hCNRHBean.COST = Math.Floor(hCNRHBean.BPRICE * cqty) + hCNRHBean.BFEE;
                hCNRHBean.SPRICE = hC.PRICE;
                hCNRHBean.INCOME = hC.NETAMT;
                hCNRHBean.PROFIT = hCNRHBean.INCOME - hCNRHBean.COST;   //獲利 = 收入 - 成本
                hCNRHReturn.Add(hCNRHBean);

                //沖銷HCMIO(賣)更新TCNUD(買單)
                tC.LastQtyRam -= cqty;
                tC.BQTY -= cqty;

                tC.FEE -= hCNRHBean.BFEE;   //剩餘買進手續費
                tC.COST -= hCNRHBean.COST;  //剩餘買進成本

                hCMIOs.Remove(hC);
                return doWriteOff(type, hCMIOs, tCNUDOrder, hCMIOIgnore, hCNRHReturn);
            }

            //HCMIO沖多筆餘額
            //qty小的當沖銷股數
            if (hC.LastQtyRam > tC.BQTY)
            {
                //在(現在買進剩餘股數>現在賣出剩餘股數)情況中是買單需要做沖銷比例，
                //因此以下所有賣單(HCMIO)資料(包含:hCNRHBean.SFEE,hCNRHBean.TAX,hCNRHBean.INCOME)都需要做比例分配
                //其中題目要求hCNRHBean.INCOME要做(淨收付)計算，所以就拿已經比例分配好的SFEE,TAX,cqty跟總價金SPRICE去計算出INCOME

                cqty = tC.BQTY;//沖銷股數
                decimal splitS = cqty / hC.LastQtyRam;     //沖銷股數 / 現在賣方剩餘股數 = 賣方沖銷比例
                //1:1的沖銷比例
                //decimal splitB = cqty / tC.BQTY;           //沖銷股數 / 現在買方剩餘股數 = 買方沖銷比例

                hCNRHBean.SQTY = hC.QTY;
                hCNRHBean.BQTY = tC.QTY;
                hCNRHBean.CQTY = cqty;
                hCNRHBean.SFEE = Math.Round(hC.FEE * splitS);
                hCNRHBean.TAX = Math.Round(hC.TAX * splitS);
                hCNRHBean.BPRICE = tC.PRICE;
                hCNRHBean.BFEE = tC.FEE;
                hCNRHBean.COST = tC.COST;
                hCNRHBean.SPRICE = hC.PRICE;
                //拿沖銷分配好的 CQTY SFEE TAX 給 SPRICE 去做計算產出INCOME收入
                hCNRHBean.INCOME = Math.Floor(hCNRHBean.SPRICE * cqty) - hCNRHBean.SFEE - hCNRHBean.TAX;
                hCNRHBean.PROFIT = hCNRHBean.INCOME - hCNRHBean.COST;   //獲利 = 收入 - 成本
                hCNRHReturn.Add(hCNRHBean);

                //沖銷TCNUD(買單)更新HCMIO(賣)
                hC.LastQtyRam -= tC.LastQtyRam;     //原始股數 - 沖銷股數 = 剩餘股數

                hC.FEE -= hCNRHBean.SFEE;   //剩餘賣出手續費
                hC.TAX -= hCNRHBean.TAX;    //剩餘賣出交易稅

                //有兩總方法可以得到原始INCOME:
                //1.先在TMHIO轉HCMIO的過程計算好原始INCOME
                //2.拿natamt(淨收付)當成原始INCOME
                hC.NETAMT -= hCNRHBean.INCOME;     //剩餘賣出收入

                tCNUDOrder.Remove(tC);
                return doWriteOff(type, hCMIOs, tCNUDOrder, hCMIOIgnore, hCNRHReturn);
            }


            if (hC.LastQtyRam == tC.BQTY)
            {
                hCNRHBean.SQTY = hC.QTY;
                hCNRHBean.BQTY = tC.QTY;
                hCNRHBean.CQTY = tC.LastQtyRam;//沖銷股數=剩餘股數
                hCNRHBean.SFEE = hC.FEE;
                hCNRHBean.TAX = hC.TAX;
                hCNRHBean.BFEE = tC.FEE;
                hCNRHBean.COST = tC.COST;
                hCNRHBean.INCOME = hC.NETAMT;
                hCNRHBean.PROFIT = hCNRHBean.INCOME - hCNRHBean.COST;   //獲利 = 收入 - 成本
                hCNRHReturn.Add(hCNRHBean);

                hCMIOs.Remove(hC);
                tCNUDOrder.Remove(tC);

                return doWriteOff(type, hCMIOs, tCNUDOrder, hCMIOIgnore, hCNRHReturn);
            }

            return null;
        }

        //當沖資料
        public (List<HCMIOBean> hCMIOBuysReturn, List<HCMIOBean> hCMIOSalesReturn) doWriteOffNow(string type, List<HCMIOBean> hCMIOSales, List<HCMIOBean> hCMIOBuys, List<HCMIOBean> hCMIOSalesIgnore, List<HCNTDBean> hCNTDReturn)
        {

            //選買單外拋
            //List<HCMIOBean> hCMIOBuyReturn = new List<HCMIOBean>();
            //List<HCMIOBean> hCMIOSales = hCMIOs.Where(x => x.BSTYPE == "S").ToList();
            if (hCMIOSales.Count == 0)
            {
                //return hCMIOBuys.ToList();
                return (hCMIOBuys.ToList(), hCMIOSales.ToList());
            }

            if (hCMIOBuys.Count == 0)
            {
                //return hCMIOSales.ToList();
                return (hCMIOBuys.ToList(), hCMIOSales.ToList());
            }
            //List<HCMIOBean> hCMIOBuys = hCMIOs.Where(x => x.BSTYPE == "B").ToList();



            HCNTDBean hCNTDBean = new HCNTDBean();
            HCMIOBean hCSale = hCMIOSales.FirstOrDefault();//(Sale)
            HCMIOBean hCBuy = hCMIOBuys.FirstOrDefault();//(Buy)



            if (Convert.ToInt32(hCBuy.DNO) > Convert.ToInt32(hCSale.DNO) && type == "Y")
            {
                //忽略掉單號太小的賣單繼續做下一筆沖銷
                hCMIOSalesIgnore.Add(hCSale);
                hCMIOSales.RemoveAt(0);
                return doWriteOffNow(type, hCMIOSales, hCMIOBuys, hCMIOSalesIgnore, hCNTDReturn);
            }




            hCNTDBean.BHNO = hCBuy.BHNO;
            hCNTDBean.TDATE = DateTime.Now.ToString("yyyyMMdd");
            //hCNTDBean.RDATE = hCBuy.Tdate;
            hCNTDBean.CSEQ = hCBuy.CSEQ;
            hCNTDBean.BDSEQ = hCBuy.DSEQ;
            hCNTDBean.BDNO = hCBuy.DNO;//現股餘額分單號
            hCNTDBean.SDSEQ = hCSale.DSEQ;
            hCNTDBean.SDNO = hCSale.DNO;//歷史交易明細分單號碼
            hCNTDBean.STOCK = hCBuy.STOCK;
            //hCNTDBean.ADJDATE = hCBuy.ADJDATE;
            //hCNTDBean.WTYPE = hCBuy.WTYPE;

            decimal cqty = 0;

            //TCNUD沖多筆賣單
            //qty小的當沖銷股數
            //Buy>Sale
            if (hCBuy.LastQtyRam > hCSale.LastQtyRam)
            {
                //在(現在賣出剩餘股數>現在買進剩餘股數)情況中是賣單需要做沖銷比例，
                //因此以下所有買單(TCNUD)資料(包含:hCNRHBean.BFEE,hCNRHBean.COST)都需要做比例分配
                //其中題目要求hCNRHBean.INCOME要做(淨收付)計算，所以就拿已經比例分配好的BFEE,cqty跟總價金BPRICE去計算出INCOME

                cqty = hCSale.LastQtyRam;//沖銷股數
                //1:1的沖銷比例
                //decimal splitS = cqty / hC.LastQtyRam;      //沖銷股數 / 現在賣方剩餘股數 = 賣方沖銷比例
                decimal splitB = cqty / hCBuy.LastQtyRam;            //沖銷股數 / 現在買方剩餘股數 = 買方沖銷比例

                hCNTDBean.SQTY = hCSale.QTY;
                hCNTDBean.BQTY = hCBuy.QTY;
                hCNTDBean.CQTY = cqty;
                hCNTDBean.SFEE = hCSale.FEE;
                hCNTDBean.TAX = hCSale.TAX;
                hCNTDBean.BPRICE = hCBuy.PRICE;
                hCNTDBean.BFEE = Math.Round(hCBuy.FEE * splitB);
                //拿沖銷分配好的 CQTY BFEE 給 BPRICE 去做計算產出COST成本
                hCNTDBean.COST = Math.Floor(hCNTDBean.BPRICE * cqty) + hCNTDBean.BFEE;
                hCNTDBean.SPRICE = hCSale.PRICE;
                hCNTDBean.INCOME = hCSale.NETAMT;
                hCNTDBean.PROFIT = hCNTDBean.INCOME - hCNTDBean.COST;   //獲利 = 收入 - 成本
                hCNTDReturn.Add(hCNTDBean);

                //沖銷HCMIO(賣)更新TCNUD(買單)
                hCBuy.LastQtyRam -= cqty;
                //hCBuy.BQTY -= cqty;

                hCBuy.FEE -= hCNTDBean.BFEE;   //剩餘買進手續費
                hCBuy.COSTRam -= hCNTDBean.COST;  //剩餘買進成本

                hCMIOSales.RemoveAt(0);
                //hCMIOBuyIgnore.Add(hCBuy);
                return doWriteOffNow(type, hCMIOSales, hCMIOBuys, hCMIOSalesIgnore, hCNTDReturn);
            }

            //HCMIO沖多筆餘額
            //qty小的當沖銷股數
            //Sale>Buy
            else if (hCSale.LastQtyRam > hCBuy.LastQtyRam)
            {
                //在(現在買進剩餘股數>現在賣出剩餘股數)情況中是買單需要做沖銷比例，
                //因此以下所有賣單(HCMIO)資料(包含:hCNRHBean.SFEE,hCNRHBean.TAX,hCNRHBean.INCOME)都需要做比例分配
                //其中題目要求hCNRHBean.INCOME要做(淨收付)計算，所以就拿已經比例分配好的SFEE,TAX,cqty跟總價金SPRICE去計算出INCOME

                cqty = hCBuy.LastQtyRam;//沖銷股數
                decimal splitS = cqty / hCSale.LastQtyRam;     //沖銷股數 / 現在賣方剩餘股數 = 賣方沖銷比例
                //1:1的沖銷比例
                //decimal splitB = cqty / tC.BQTY;           //沖銷股數 / 現在買方剩餘股數 = 買方沖銷比例

                hCNTDBean.SQTY = hCSale.QTY;
                hCNTDBean.BQTY = hCBuy.QTY;
                hCNTDBean.CQTY = cqty;
                hCNTDBean.SFEE = Math.Round(hCSale.FEE * splitS);
                hCNTDBean.TAX = Math.Round(hCSale.TAX * splitS);
                hCNTDBean.BPRICE = hCBuy.PRICE;
                hCNTDBean.BFEE = hCBuy.FEE;
                hCNTDBean.COST = hCBuy.COSTRam;
                hCNTDBean.SPRICE = hCSale.PRICE;
                //拿沖銷分配好的 CQTY SFEE TAX 給 SPRICE 去做計算產出INCOME收入
                hCNTDBean.INCOME = Math.Floor(hCNTDBean.SPRICE * cqty) - hCNTDBean.SFEE - hCNTDBean.TAX;
                hCNTDBean.PROFIT = hCNTDBean.INCOME - hCNTDBean.COST;   //獲利 = 收入 - 成本
                hCNTDReturn.Add(hCNTDBean);

                //沖銷TCNUD(買單)更新HCMIO(賣)
                hCSale.LastQtyRam -= hCBuy.LastQtyRam;     //原始股數 - 沖銷股數 = 剩餘股數

                hCSale.FEE -= hCNTDBean.SFEE;   //剩餘賣出手續費
                hCSale.TAX -= hCNTDBean.TAX;    //剩餘賣出交易稅

                //有兩總方法可以得到原始INCOME:
                //1.先在TMHIO轉HCMIO的過程計算好原始INCOME
                //2.拿natamt(淨收付)當成原始INCOME
                hCSale.NETAMT -= hCNTDBean.INCOME;     //剩餘賣出收入

                hCMIOBuys.RemoveAt(0);

                return doWriteOffNow(type, hCMIOSales, hCMIOBuys, hCMIOSalesIgnore, hCNTDReturn);
            }

            //Sale=Buy
            //if (hCSale.LastQtyRam == hCBuy.LastQtyRam)
            else
            {
                hCNTDBean.SQTY = hCSale.QTY;
                hCNTDBean.BQTY = hCBuy.QTY;
                hCNTDBean.CQTY = hCBuy.LastQtyRam;//沖銷股數=剩餘股數
                hCNTDBean.SFEE = hCSale.FEE;
                hCNTDBean.TAX = hCSale.TAX;
                hCNTDBean.BFEE = hCBuy.FEE;
                hCNTDBean.COST = hCBuy.COSTRam;
                hCNTDBean.INCOME = hCSale.NETAMT;
                hCNTDBean.PROFIT = hCNTDBean.INCOME - hCNTDBean.COST;   //獲利 = 收入 - 成本
                hCNTDReturn.Add(hCNTDBean);

                hCMIOSales.RemoveAt(0);
                hCMIOBuys.RemoveAt(0);

                return doWriteOffNow(type, hCMIOSales, hCMIOBuys, hCMIOSalesIgnore, hCNTDReturn);
            }

        }

        public virtual string StockPermissions(string costomer, string stock)
        {
            if (costomer == "X" && stock == "X")
            {
                return "X";
            }
            if (costomer == "B" && stock == "X")
            {
                return "X";
            }
            if (costomer == "Y" && stock == "Y")
            {
                return "Y";
            }
            if (costomer == "Y" && stock == "X")
            {
                return "Y";
            }
            if (costomer == "X" && stock == "Y")
            {
                return "Y";
            }
            if (costomer == "B" && stock == "Y")
            {
                return "Y";
            }
            if (costomer == "N" && stock == "N")
            {
                return "N";
            }
            return "N";
        }

        #region
        //［手續費］請無條件捨去至整數；如為整股，最小手續費為20元；如為零股，最小手續費為1元。
        #endregion
        private decimal FeeCalculate(decimal QTY, decimal FEE)
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

        //把TCSIO(今日匯入)加入現股餘額
        private List<TCNUDBean> proccessTCSIOBuy(List<TCSIOBean> tCSIOs)
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            foreach (var item in tCSIOs.Where(c => c.BSTYPE == "B").ToList())
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
                tCNUDs.Add(tCNUD);
            }
            return tCNUDs;
        }


        //把TMHIO(今日買進)加入現股餘額
        //private List<TCNUDBean> proccessTMHIOBuy(List<TMHIOBean> tMHIOs)
        //{
        //    List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
        //    foreach (var item in tMHIOs.Where(c => c.BSTYPE == "B"))
        //    {
        //        TCNUDBean tCNUD = new TCNUDBean();
        //        tCNUD.TDATE = item.Tdate;
        //        tCNUD.BHNO = item.BHNO;
        //        tCNUD.CSEQ = item.CSEQ;
        //        tCNUD.STOCK = item.STOCK;
        //        tCNUD.PRICE = item.PRICE;
        //        tCNUD.QTY = item.QTY;
        //        tCNUD.BQTY = item.QTY;
        //        tCNUD.AMT = Math.Floor(item.PRICE * item.QTY);
        //        tCNUD.FEE = FeeCalculate(item.QTY, Math.Floor(tCNUD.AMT * 0.001425m));
        //        tCNUD.COST = tCNUD.AMT + tCNUD.FEE;
        //        tCNUD.DSEQ = item.DSEQ;
        //        tCNUD.DNO = item.JRNUM;
        //        tCNUD.WTYPE = "0";
        //        tCNUDs.Add(tCNUD);
        //    }
        //    return tCNUDs;
        //}

        //把HCMIO(今日買進)加入現股餘額

        private List<TCNUDBean> proccessHCMIOBuyToTCNUD(List<HCMIOBean> hCMIOs)
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            foreach (var item in hCMIOs)
            {
                TCNUDBean tCNUD = new TCNUDBean();
                tCNUD.TDATE = item.Tdate;
                tCNUD.BHNO = item.BHNO;
                tCNUD.CSEQ = item.CSEQ;
                tCNUD.STOCK = item.STOCK;
                tCNUD.PRICE = item.PRICE;
                tCNUD.QTY = item.QTY;
                tCNUD.BQTY = item.QTY;
                tCNUD.AMT = Math.Floor(item.PRICE * item.QTY);
                tCNUD.FEE = FeeCalculate(item.QTY, Math.Floor(tCNUD.AMT * 0.001425m));
                tCNUD.COST = tCNUD.AMT + tCNUD.FEE;
                tCNUD.DSEQ = item.DSEQ;
                tCNUD.DNO = item.DNO;
                tCNUD.WTYPE = "0";
                tCNUDs.Add(tCNUD);
            }
            return tCNUDs;
        }

        private List<TCNUDBean> proccessHCMIOSalesToTCNUD(List<HCMIOBean> hCMIOs)
        {
            List<TCNUDBean> tCNUDs = new List<TCNUDBean>();
            foreach (var item in hCMIOs)
            {
                TCNUDBean tCNUD = new TCNUDBean();
                tCNUD.TDATE = item.Tdate;
                tCNUD.BHNO = item.BHNO;
                tCNUD.CSEQ = item.CSEQ;
                tCNUD.STOCK = item.STOCK;
                tCNUD.PRICE = item.PRICE;
                tCNUD.QTY = item.QTY;
                tCNUD.BQTY = -item.QTY;
                tCNUD.AMT = Math.Floor(item.PRICE * item.QTY);
                tCNUD.TAXRam = item.TAX;
                //tCNUD.FEE = FeeCalculate(item.QTY, Math.Floor(tCNUD.AMT * 0.001425m));
                tCNUD.FEE = item.FEE;
                //tCNUD.COST = tCNUD.AMT + tCNUD.FEE;
                tCNUD.COST = item.NETAMT;
                tCNUD.DSEQ = item.DSEQ;
                tCNUD.DNO = item.DNO;
                tCNUD.WTYPE = "0";
                tCNUDs.Add(tCNUD);
            }
            return tCNUDs;
        }


        private List<HCNTDBean> HCNTDCalculate(List<HCNTDBean> hCNTDReturn)
        {

            hCNTDReturn.ForEach(x => x.TAX = Math.Round(x.SPRICE * x.CQTY * 0.0015m));
            foreach (var hcntdItem in hCNTDReturn)
            {
                hcntdItem.INCOME = (hcntdItem.SPRICE * (hcntdItem.BQTY / hcntdItem.CQTY)) - hcntdItem.SFEE - hcntdItem.TAX;
                hcntdItem.PROFIT = hcntdItem.INCOME - hcntdItem.COST;
            }

            return hCNTDReturn;
        }
    }
}
