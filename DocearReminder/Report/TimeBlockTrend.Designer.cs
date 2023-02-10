using System;

namespace DocearReminder
{
    partial class TimeBlockTrend
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeBlockTrend));
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.startDt = new System.Windows.Forms.DateTimePicker();
            this.endDT = new System.Windows.Forms.DateTimePicker();
            this.textBox_searchwork = new System.Windows.Forms.TextBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.mostcount = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.mosthour = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.RemarkDetailCount = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.RemarkDetailPercent = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.remarksDetailCount = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.PerMaxDays = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.MaxDays = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.totalTimeEventDay = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.totalTimeEventDays = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.PerCountEveryDay = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.PerCountEveryDays = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.totalcount1 = new System.Windows.Forms.Label();
            this.DaysPercent = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.totalDaysWithContent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.totalDays = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.totalCount = new System.Windows.Forms.Label();
            this.totalTime = new System.Windows.Forms.Label();
            this.RemarkCount = new System.Windows.Forms.Label();
            this.RemarkPercentEvery = new System.Windows.Forms.Label();
            this.remarksCount1 = new System.Windows.Forms.Label();
            this.RemarkPercent = new System.Windows.Forms.Label();
            this.备注次数 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Type = new System.Windows.Forms.ComboBox();
            this.exclude = new System.Windows.Forms.TextBox();
            this.SubClass = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(44, 185);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(994, 441);
            this.formsPlot1.TabIndex = 0;
            // 
            // startDt
            // 
            this.startDt.Location = new System.Drawing.Point(188, 13);
            this.startDt.Name = "startDt";
            this.startDt.Size = new System.Drawing.Size(172, 21);
            this.startDt.TabIndex = 1;
            this.startDt.Value = new System.DateTime(2022, 9, 23, 0, 0, 0, 0);
            this.startDt.ValueChanged += new System.EventHandler(this.startDt_ValueChanged);
            // 
            // endDT
            // 
            this.endDT.Location = new System.Drawing.Point(366, 12);
            this.endDT.Name = "endDT";
            this.endDT.Size = new System.Drawing.Size(156, 21);
            this.endDT.TabIndex = 2;
            this.endDT.Value = new System.DateTime(2022, 10, 1, 0, 0, 0, 0);
            this.endDT.ValueChanged += new System.EventHandler(this.endDT_ValueChanged);
            // 
            // textBox_searchwork
            // 
            this.textBox_searchwork.Location = new System.Drawing.Point(680, 12);
            this.textBox_searchwork.Name = "textBox_searchwork";
            this.textBox_searchwork.Size = new System.Drawing.Size(138, 21);
            this.textBox_searchwork.TabIndex = 3;
            this.textBox_searchwork.TextChanged += new System.EventHandler(this.searchword_TextChanged);
            this.textBox_searchwork.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(974, 13);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(64, 23);
            this.SearchBtn.TabIndex = 4;
            this.SearchBtn.Text = "统计";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "所有类别",
            "工作",
            "事务",
            "形象改造工程",
            "编程",
            "学习",
            "英语",
            "阅读",
            "精彩",
            "运动",
            "维持",
            "电脑",
            "手机",
            "平板",
            "解构魔幻",
            "休息",
            "浪费",
            "未分类",
            "音乐"});
            this.comboBox1.Location = new System.Drawing.Point(528, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 20);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.Text = "所有类别";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.8919F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.89189F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.21622F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.89189F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.89189F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.21622F));
            this.tableLayoutPanel1.Controls.Add(this.label43, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.label42, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.mostcount, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.label40, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.mosthour, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label38, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.RemarkDetailCount, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.label36, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.RemarkDetailPercent, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label34, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.remarksDetailCount, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label32, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label25, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.label24, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.PerMaxDays, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label22, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.MaxDays, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label20, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.totalTimeEventDay, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label18, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.totalTimeEventDays, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label16, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.PerCountEveryDay, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label14, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.PerCountEveryDays, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.totalcount1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.DaysPercent, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.totalDaysWithContent, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.totalDays, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.totalCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.totalTime, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.RemarkCount, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.RemarkPercentEvery, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.remarksCount1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.RemarkPercent, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.备注次数, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label26, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(44, 52);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(994, 139);
            this.tableLayoutPanel1.TabIndex = 16;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label43
            // 
            this.label43.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(833, 120);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(125, 12);
            this.label43.TabIndex = 41;
            this.label43.Text = "                    ";
            // 
            // label42
            // 
            this.label42.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(701, 120);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(125, 12);
            this.label42.TabIndex = 40;
            this.label42.Text = "                    ";
            // 
            // mostcount
            // 
            this.mostcount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.mostcount.AutoSize = true;
            this.mostcount.Location = new System.Drawing.Point(499, 120);
            this.mostcount.Name = "mostcount";
            this.mostcount.Size = new System.Drawing.Size(125, 12);
            this.mostcount.TabIndex = 39;
            this.mostcount.Text = "                    ";
            // 
            // label40
            // 
            this.label40.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(427, 120);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(65, 12);
            this.label40.TabIndex = 38;
            this.label40.Text = "最多次数：";
            // 
            // mosthour
            // 
            this.mosthour.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.mosthour.AutoSize = true;
            this.mosthour.Location = new System.Drawing.Point(171, 120);
            this.mosthour.Name = "mosthour";
            this.mosthour.Size = new System.Drawing.Size(125, 12);
            this.mosthour.TabIndex = 37;
            this.mosthour.Text = "                    ";
            // 
            // label38
            // 
            this.label38.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(105, 120);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(59, 12);
            this.label38.TabIndex = 36;
            this.label38.Text = "最多时长:";
            // 
            // RemarkDetailCount
            // 
            this.RemarkDetailCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemarkDetailCount.AutoSize = true;
            this.RemarkDetailCount.Location = new System.Drawing.Point(833, 99);
            this.RemarkDetailCount.Name = "RemarkDetailCount";
            this.RemarkDetailCount.Size = new System.Drawing.Size(125, 12);
            this.RemarkDetailCount.TabIndex = 35;
            this.RemarkDetailCount.Text = "                    ";
            // 
            // label36
            // 
            this.label36.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(713, 99);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(113, 12);
            this.label36.TabIndex = 34;
            this.label36.Text = "详细备注平均字数：";
            // 
            // RemarkDetailPercent
            // 
            this.RemarkDetailPercent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemarkDetailPercent.AutoSize = true;
            this.RemarkDetailPercent.Location = new System.Drawing.Point(499, 99);
            this.RemarkDetailPercent.Name = "RemarkDetailPercent";
            this.RemarkDetailPercent.Size = new System.Drawing.Size(125, 12);
            this.RemarkDetailPercent.TabIndex = 33;
            this.RemarkDetailPercent.Text = "                    ";
            // 
            // label34
            // 
            this.label34.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(403, 99);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(89, 12);
            this.label34.TabIndex = 32;
            this.label34.Text = "详细备注比率：";
            // 
            // remarksDetailCount
            // 
            this.remarksDetailCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.remarksDetailCount.AutoSize = true;
            this.remarksDetailCount.Location = new System.Drawing.Point(171, 99);
            this.remarksDetailCount.Name = "remarksDetailCount";
            this.remarksDetailCount.Size = new System.Drawing.Size(125, 12);
            this.remarksDetailCount.TabIndex = 31;
            this.remarksDetailCount.Text = "                    ";
            // 
            // label32
            // 
            this.label32.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(75, 99);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(89, 12);
            this.label32.TabIndex = 30;
            this.label32.Text = "详细备注次数：";
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(833, 61);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(125, 12);
            this.label25.TabIndex = 23;
            this.label25.Text = "                    ";
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(701, 61);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(125, 12);
            this.label24.TabIndex = 22;
            this.label24.Text = "                    ";
            // 
            // PerMaxDays
            // 
            this.PerMaxDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PerMaxDays.AutoSize = true;
            this.PerMaxDays.Location = new System.Drawing.Point(499, 61);
            this.PerMaxDays.Name = "PerMaxDays";
            this.PerMaxDays.Size = new System.Drawing.Size(113, 12);
            this.PerMaxDays.TabIndex = 21;
            this.PerMaxDays.Text = "                  ";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(415, 61);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 12);
            this.label22.TabIndex = 20;
            this.label22.Text = "平均连续天数";
            // 
            // MaxDays
            // 
            this.MaxDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MaxDays.AutoSize = true;
            this.MaxDays.Location = new System.Drawing.Point(171, 61);
            this.MaxDays.Name = "MaxDays";
            this.MaxDays.Size = new System.Drawing.Size(125, 12);
            this.MaxDays.TabIndex = 19;
            this.MaxDays.Text = "                    ";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(75, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 12);
            this.label20.TabIndex = 18;
            this.label20.Text = "最多连续天数：";
            // 
            // totalTimeEventDay
            // 
            this.totalTimeEventDay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.totalTimeEventDay.AutoSize = true;
            this.totalTimeEventDay.Location = new System.Drawing.Point(833, 42);
            this.totalTimeEventDay.Name = "totalTimeEventDay";
            this.totalTimeEventDay.Size = new System.Drawing.Size(125, 12);
            this.totalTimeEventDay.TabIndex = 17;
            this.totalTimeEventDay.Text = "                    ";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(677, 39);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(149, 18);
            this.label18.TabIndex = 16;
            this.label18.Text = "平均每天时长（记录天数）：";
            // 
            // totalTimeEventDays
            // 
            this.totalTimeEventDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.totalTimeEventDays.AutoSize = true;
            this.totalTimeEventDays.Location = new System.Drawing.Point(499, 42);
            this.totalTimeEventDays.Name = "totalTimeEventDays";
            this.totalTimeEventDays.Size = new System.Drawing.Size(113, 12);
            this.totalTimeEventDays.TabIndex = 15;
            this.totalTimeEventDays.Text = "                  ";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(343, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 12);
            this.label16.TabIndex = 14;
            this.label16.Text = "平均每天时长（总天数）：";
            // 
            // PerCountEveryDay
            // 
            this.PerCountEveryDay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PerCountEveryDay.AutoSize = true;
            this.PerCountEveryDay.Location = new System.Drawing.Point(833, 23);
            this.PerCountEveryDay.Name = "PerCountEveryDay";
            this.PerCountEveryDay.Size = new System.Drawing.Size(125, 12);
            this.PerCountEveryDay.TabIndex = 13;
            this.PerCountEveryDay.Text = "                    ";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(689, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 12);
            this.label14.TabIndex = 12;
            this.label14.Text = "每天几次（记录天数）：";
            // 
            // PerCountEveryDays
            // 
            this.PerCountEveryDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PerCountEveryDays.AutoSize = true;
            this.PerCountEveryDays.Location = new System.Drawing.Point(499, 23);
            this.PerCountEveryDays.Name = "PerCountEveryDays";
            this.PerCountEveryDays.Size = new System.Drawing.Size(113, 12);
            this.PerCountEveryDays.TabIndex = 11;
            this.PerCountEveryDays.Text = "                  ";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(367, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "每天几次（总天数）：";
            // 
            // totalcount1
            // 
            this.totalcount1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.totalcount1.AutoSize = true;
            this.totalcount1.Location = new System.Drawing.Point(171, 23);
            this.totalcount1.Name = "totalcount1";
            this.totalcount1.Size = new System.Drawing.Size(125, 12);
            this.totalcount1.TabIndex = 9;
            this.totalcount1.Text = "                    ";
            // 
            // DaysPercent
            // 
            this.DaysPercent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DaysPercent.AutoSize = true;
            this.DaysPercent.Location = new System.Drawing.Point(833, 4);
            this.DaysPercent.Name = "DaysPercent";
            this.DaysPercent.Size = new System.Drawing.Size(125, 12);
            this.DaysPercent.TabIndex = 8;
            this.DaysPercent.Text = "                    ";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(761, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "天数比率：";
            // 
            // totalDaysWithContent
            // 
            this.totalDaysWithContent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.totalDaysWithContent.AutoSize = true;
            this.totalDaysWithContent.Location = new System.Drawing.Point(499, 4);
            this.totalDaysWithContent.Name = "totalDaysWithContent";
            this.totalDaysWithContent.Size = new System.Drawing.Size(35, 12);
            this.totalDaysWithContent.TabIndex = 6;
            this.totalDaysWithContent.Text = "     ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "总天数：";
            // 
            // totalDays
            // 
            this.totalDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.totalDays.AutoSize = true;
            this.totalDays.Location = new System.Drawing.Point(171, 4);
            this.totalDays.Name = "totalDays";
            this.totalDays.Size = new System.Drawing.Size(35, 12);
            this.totalDays.TabIndex = 1;
            this.totalDays.Text = "     ";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "记录天数：";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "总时长：";
            // 
            // totalCount
            // 
            this.totalCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.totalCount.AutoSize = true;
            this.totalCount.Location = new System.Drawing.Point(111, 23);
            this.totalCount.Name = "totalCount";
            this.totalCount.Size = new System.Drawing.Size(53, 12);
            this.totalCount.TabIndex = 4;
            this.totalCount.Text = "总次数：";
            // 
            // totalTime
            // 
            this.totalTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.totalTime.AutoSize = true;
            this.totalTime.Location = new System.Drawing.Point(171, 42);
            this.totalTime.Name = "totalTime";
            this.totalTime.Size = new System.Drawing.Size(125, 12);
            this.totalTime.TabIndex = 5;
            this.totalTime.Text = "                    ";
            // 
            // RemarkCount
            // 
            this.RemarkCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemarkCount.AutoSize = true;
            this.RemarkCount.Location = new System.Drawing.Point(833, 80);
            this.RemarkCount.Name = "RemarkCount";
            this.RemarkCount.Size = new System.Drawing.Size(125, 12);
            this.RemarkCount.TabIndex = 27;
            this.RemarkCount.Text = "                    ";
            // 
            // RemarkPercentEvery
            // 
            this.RemarkPercentEvery.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RemarkPercentEvery.AutoSize = true;
            this.RemarkPercentEvery.Location = new System.Drawing.Point(737, 80);
            this.RemarkPercentEvery.Name = "RemarkPercentEvery";
            this.RemarkPercentEvery.Size = new System.Drawing.Size(89, 12);
            this.RemarkPercentEvery.TabIndex = 26;
            this.RemarkPercentEvery.Text = "备注平均字数：";
            // 
            // remarksCount1
            // 
            this.remarksCount1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.remarksCount1.AutoSize = true;
            this.remarksCount1.Location = new System.Drawing.Point(171, 80);
            this.remarksCount1.Name = "remarksCount1";
            this.remarksCount1.Size = new System.Drawing.Size(125, 12);
            this.remarksCount1.TabIndex = 29;
            this.remarksCount1.Text = "                    ";
            // 
            // RemarkPercent
            // 
            this.RemarkPercent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemarkPercent.AutoSize = true;
            this.RemarkPercent.Location = new System.Drawing.Point(499, 80);
            this.RemarkPercent.Name = "RemarkPercent";
            this.RemarkPercent.Size = new System.Drawing.Size(125, 12);
            this.RemarkPercent.TabIndex = 25;
            this.RemarkPercent.Text = "                    ";
            // 
            // 备注次数
            // 
            this.备注次数.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.备注次数.AutoSize = true;
            this.备注次数.Location = new System.Drawing.Point(99, 80);
            this.备注次数.Name = "备注次数";
            this.备注次数.Size = new System.Drawing.Size(65, 12);
            this.备注次数.TabIndex = 28;
            this.备注次数.Text = "备注次数：";
            // 
            // label26
            // 
            this.label26.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(415, 80);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(77, 12);
            this.label26.TabIndex = 24;
            this.label26.Text = "有备注比率：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(1044, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(610, 601);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // Type
            // 
            this.Type.FormattingEnabled = true;
            this.Type.Items.AddRange(new object[] {
            "时间块",
            "金钱",
            "卡路里",
            "进步",
            "错误"});
            this.Type.Location = new System.Drawing.Point(44, 12);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(129, 20);
            this.Type.TabIndex = 18;
            this.Type.Text = "时间块";
            this.Type.SelectedIndexChanged += new System.EventHandler(this.Type_SelectedIndexChanged);
            // 
            // exclude
            // 
            this.exclude.Location = new System.Drawing.Point(824, 12);
            this.exclude.Name = "exclude";
            this.exclude.Size = new System.Drawing.Size(89, 21);
            this.exclude.TabIndex = 19;
            this.exclude.TextChanged += new System.EventHandler(this.searchword_TextChanged);
            this.exclude.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
            // 
            // SubClass
            // 
            this.SubClass.AutoSize = true;
            this.SubClass.Location = new System.Drawing.Point(920, 16);
            this.SubClass.Name = "SubClass";
            this.SubClass.Size = new System.Drawing.Size(48, 16);
            this.SubClass.TabIndex = 20;
            this.SubClass.Text = "子类";
            this.SubClass.UseVisualStyleBackColor = true;
            this.SubClass.CheckedChanged += new System.EventHandler(this.SubClass_CheckedChanged);
            // 
            // TimeBlockTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1666, 621);
            this.Controls.Add(this.SubClass);
            this.Controls.Add(this.exclude);
            this.Controls.Add(this.Type);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.textBox_searchwork);
            this.Controls.Add(this.endDT);
            this.Controls.Add(this.startDt);
            this.Controls.Add(this.formsPlot1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TimeBlockTrend";
            this.Text = "TimeBlockTrend";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.DateTimePicker startDt;
        private System.Windows.Forms.DateTimePicker endDT;
        private System.Windows.Forms.TextBox textBox_searchwork;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label mostcount;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label mosthour;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label RemarkDetailCount;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label RemarkDetailPercent;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label remarksDetailCount;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label remarksCount1;
        private System.Windows.Forms.Label 备注次数;
        private System.Windows.Forms.Label RemarkCount;
        private System.Windows.Forms.Label RemarkPercentEvery;
        private System.Windows.Forms.Label RemarkPercent;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label PerMaxDays;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label MaxDays;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label totalTimeEventDay;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label totalTimeEventDays;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label PerCountEveryDay;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label PerCountEveryDays;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label totalcount1;
        private System.Windows.Forms.Label DaysPercent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label totalDaysWithContent;
        private System.Windows.Forms.Label totalDays;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label totalCount;
        private System.Windows.Forms.Label totalTime;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox Type;
        private System.Windows.Forms.TextBox exclude;
        private System.Windows.Forms.CheckBox SubClass;
    }
}