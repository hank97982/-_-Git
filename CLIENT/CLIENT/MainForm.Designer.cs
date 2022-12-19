namespace CLIENT
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cbConnectType = new System.Windows.Forms.ComboBox();
            this.btnSearchJson = new System.Windows.Forms.Button();
            this.txtcseq = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbhno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbqtype = new System.Windows.Forms.ComboBox();
            this.dateSdate = new System.Windows.Forms.DateTimePicker();
            this.dateEdate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStockSymbol = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(4, 156);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(791, 289);
            this.listBox1.TabIndex = 0;
            // 
            // cbConnectType
            // 
            this.cbConnectType.FormattingEnabled = true;
            this.cbConnectType.Location = new System.Drawing.Point(643, 121);
            this.cbConnectType.Name = "cbConnectType";
            this.cbConnectType.Size = new System.Drawing.Size(151, 27);
            this.cbConnectType.TabIndex = 11;
            // 
            // btnSearchJson
            // 
            this.btnSearchJson.Location = new System.Drawing.Point(522, 121);
            this.btnSearchJson.Name = "btnSearchJson";
            this.btnSearchJson.Size = new System.Drawing.Size(102, 29);
            this.btnSearchJson.TabIndex = 10;
            this.btnSearchJson.Text = "我要查詢";
            this.btnSearchJson.UseVisualStyleBackColor = true;
            this.btnSearchJson.Click += new System.EventHandler(this.btnSearchJson_Click);
            // 
            // txtcseq
            // 
            this.txtcseq.Location = new System.Drawing.Point(449, 12);
            this.txtcseq.Name = "txtcseq";
            this.txtcseq.Size = new System.Drawing.Size(125, 27);
            this.txtcseq.TabIndex = 9;
            this.txtcseq.Leave += new System.EventHandler(this.txtcseq_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(404, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "帳號";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "分公司";
            // 
            // txtbhno
            // 
            this.txtbhno.Location = new System.Drawing.Point(271, 12);
            this.txtbhno.Name = "txtbhno";
            this.txtbhno.Size = new System.Drawing.Size(125, 27);
            this.txtbhno.TabIndex = 6;
            this.txtbhno.Leave += new System.EventHandler(this.txtbhno_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "查詢類別";
            // 
            // cbqtype
            // 
            this.cbqtype.FormattingEnabled = true;
            this.cbqtype.Location = new System.Drawing.Point(78, 12);
            this.cbqtype.Name = "cbqtype";
            this.cbqtype.Size = new System.Drawing.Size(125, 27);
            this.cbqtype.TabIndex = 14;
            // 
            // dateSdate
            // 
            this.dateSdate.Location = new System.Drawing.Point(78, 45);
            this.dateSdate.Name = "dateSdate";
            this.dateSdate.Size = new System.Drawing.Size(150, 27);
            this.dateSdate.TabIndex = 15;
            // 
            // dateEdate
            // 
            this.dateEdate.Location = new System.Drawing.Point(311, 45);
            this.dateEdate.Name = "dateEdate";
            this.dateEdate.Size = new System.Drawing.Size(150, 27);
            this.dateEdate.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 17;
            this.label4.Text = "查詢起日";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 18;
            this.label5.Text = "查詢迄日";
            // 
            // txtStockSymbol
            // 
            this.txtStockSymbol.Location = new System.Drawing.Point(653, 12);
            this.txtStockSymbol.Name = "txtStockSymbol";
            this.txtStockSymbol.Size = new System.Drawing.Size(125, 27);
            this.txtStockSymbol.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(582, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 19;
            this.label6.Text = "股票代號";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtStockSymbol);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateEdate);
            this.Controls.Add(this.dateSdate);
            this.Controls.Add(this.cbqtype);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbConnectType);
            this.Controls.Add(this.btnSearchJson);
            this.Controls.Add(this.txtcseq);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbhno);
            this.Controls.Add(this.listBox1);
            this.Name = "MainForm";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox listBox1;
        private ComboBox cbConnectType;
        private Button btnSearchJson;
        private TextBox txtcseq;
        private Label label2;
        private Label label1;
        private TextBox txtbhno;
        private Label label3;
        private ComboBox cbqtype;
        private DateTimePicker dateSdate;
        private DateTimePicker dateEdate;
        private Label label4;
        private Label label5;
        private TextBox txtStockSymbol;
        private Label label6;
    }
}