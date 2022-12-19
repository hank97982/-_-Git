using HankLibrary;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using WinFormsAppClient.Bean;
using WinFormsAppClient.Controller;

namespace CLIENT
{
    public partial class MainForm : Form
    {
        //private SockClientCallBack _sockClientCallBack = null;
        private MainController _controller;

        public MainForm()
        {
            InitializeComponent();
            _controller = new MainController(this);
            Init();
        }
        private void Init()
        {
            cbConnectType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbConnectType.Items.Add(ConnectTypeItems.Json);
            cbConnectType.Items.Add(ConnectTypeItems.Xml);
            cbConnectType.SelectedIndex = 0;

            cbqtype.DropDownStyle = ComboBoxStyle.DropDownList;
            cbqtype.Items.Add("0001");
            cbqtype.Items.Add("0002");
            cbqtype.Items.Add("0003");
            cbqtype.SelectedIndex = 0;
            ChangeEvent(null,null);
            cbqtype.SelectedIndexChanged += ChangeEvent;
        }

        private void ChangeEvent(object? sender, EventArgs e)
        {
            switch (cbqtype.GetItemText(cbqtype.SelectedItem)) {
                case "0001":
                    dateSdate.Enabled = false;
                    dateEdate.Enabled = false;
                    break;
                case "0002":
                    dateSdate.Enabled = true;
                    dateEdate.Enabled = true;
                    break;
                case "0003":
                    dateSdate.Enabled = true;
                    dateEdate.Enabled = true;
                    break;
            }
        }

        private void btnSearchJson_Click(object sender, EventArgs e)
        {

            List<string> strings = new List<string>();
            if (txtbhno.Text == "")
            {
                strings.Add("分公司");
            }
            if (txtcseq.Text == "")
            {
                strings.Add("帳號");
            }
            if (txtbhno.Text == "" || txtcseq.Text == "")
            {
                MessageAlert(String.Join(",", strings) + "欄位不得為空");
                return;
            }


            if (cbqtype.GetItemText(cbqtype.SelectedItem) == "0001")
            {
                _controller.AskForSearch(cbConnectType.SelectedIndex,new UnrealizedGainsAndlLossesDTO
                {
                    Qtype = cbqtype.GetItemText(cbqtype.SelectedItem),
                    Bhno = txtbhno.Text,
                    Cseq = txtcseq.Text,
                    StockSymbol = txtStockSymbol.Text
                });
                return;
            }
            if (cbqtype.GetItemText(cbqtype.SelectedItem) == "0002")
            {
                if ((dateSdate.Value) > (dateEdate.Value))
                {
                    MessageAlert("查詢起日必須小於等於查詢迄日");
                    return;
                }
                _controller.AskForSearch(cbConnectType.SelectedIndex,new RealizedProfitAndLossDTO
                {
                    Qtype = cbqtype.GetItemText(cbqtype.SelectedItem),
                    Bhno = txtbhno.Text,
                    Cseq = txtcseq.Text,
                    Sdate = dateSdate.Value.ToString("yyyyMMdd"),
                    Edate = dateEdate.Value.ToString("yyyyMMdd"),
                    StockSymbol = txtStockSymbol.Text
                });
                return;
            }
            if (cbqtype.GetItemText(cbqtype.SelectedItem) == "0003")
            {
                if ((dateSdate.Value) > (dateEdate.Value))
                {
                    MessageAlert("查詢起日必須小於等於查詢迄日");
                    return;
                }
                _controller.AskForSearch(cbConnectType.SelectedIndex, new StatementDTO
                {
                    Qtype = cbqtype.GetItemText(cbqtype.SelectedItem),
                    Bhno = txtbhno.Text,
                    Cseq = txtcseq.Text,
                    Sdate = dateSdate.Value.ToString("yyyyMMdd"),
                    Edate = dateEdate.Value.ToString("yyyyMMdd"),
                    StockSymbol = txtStockSymbol.Text
                });
                return;
            }

        }

        public void AppendToListBox(string stringData)
        {
            MethodInvoker methodInvoker = new MethodInvoker(delegate
            {
                listBox1.Items.Add(stringData);
            });
            this.Invoke(methodInvoker);
        }

        public void MessageAlert(string messageStr)
        {
            MessageBox.Show(messageStr, "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtbhno_Leave(object sender, EventArgs e)
        {
            if (txtbhno.Text == "")
            {
                MessageAlert("分公司欄位不得為空");
            }
        }

        private void txtcseq_Leave(object sender, EventArgs e)
        {
            if (txtcseq.Text == "")
            {
                MessageAlert("帳號欄位不得為空");
            }
        }
    }
}