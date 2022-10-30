namespace DocearReminder
{
    partial class Tools
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
            this.reducejson = new System.Windows.Forms.Button();
            this.deletetemp = new System.Windows.Forms.Button();
            this.pwd = new System.Windows.Forms.TextBox();
            this.setPWD = new System.Windows.Forms.Button();
            this.Decrypt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.createsuggest = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.encry_txt = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // reducejson
            // 
            this.reducejson.Location = new System.Drawing.Point(122, 200);
            this.reducejson.Name = "reducejson";
            this.reducejson.Size = new System.Drawing.Size(97, 30);
            this.reducejson.TabIndex = 0;
            this.reducejson.Text = "简备JSON";
            this.reducejson.UseVisualStyleBackColor = true;
            this.reducejson.Click += new System.EventHandler(this.reducejson_Click);
            // 
            // deletetemp
            // 
            this.deletetemp.Location = new System.Drawing.Point(12, 200);
            this.deletetemp.Name = "deletetemp";
            this.deletetemp.Size = new System.Drawing.Size(104, 30);
            this.deletetemp.TabIndex = 11;
            this.deletetemp.Text = "删除临时文件";
            this.deletetemp.UseVisualStyleBackColor = true;
            this.deletetemp.Click += new System.EventHandler(this.deletetemp_Click);
            // 
            // pwd
            // 
            this.pwd.Location = new System.Drawing.Point(12, 11);
            this.pwd.Name = "pwd";
            this.pwd.Size = new System.Drawing.Size(525, 21);
            this.pwd.TabIndex = 12;
            // 
            // setPWD
            // 
            this.setPWD.Location = new System.Drawing.Point(543, 12);
            this.setPWD.Name = "setPWD";
            this.setPWD.Size = new System.Drawing.Size(73, 21);
            this.setPWD.TabIndex = 13;
            this.setPWD.Text = "设置密码";
            this.setPWD.UseVisualStyleBackColor = true;
            this.setPWD.Click += new System.EventHandler(this.setPWD_Click);
            // 
            // Decrypt
            // 
            this.Decrypt.Location = new System.Drawing.Point(631, 12);
            this.Decrypt.Name = "Decrypt";
            this.Decrypt.Size = new System.Drawing.Size(75, 21);
            this.Decrypt.TabIndex = 14;
            this.Decrypt.Text = "解密";
            this.Decrypt.UseVisualStyleBackColor = true;
            this.Decrypt.Click += new System.EventHandler(this.Decrypt_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(229, 200);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 30);
            this.button1.TabIndex = 15;
            this.button1.Text = " 格式化文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // createsuggest
            // 
            this.createsuggest.Location = new System.Drawing.Point(341, 200);
            this.createsuggest.Name = "createsuggest";
            this.createsuggest.Size = new System.Drawing.Size(138, 30);
            this.createsuggest.TabIndex = 16;
            this.createsuggest.Text = "生成建议文件";
            this.createsuggest.UseVisualStyleBackColor = true;
            this.createsuggest.Click += new System.EventHandler(this.createsuggest_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(501, 208);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "任务名脱敏";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(172, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "给所有Node节点添加ID";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // encry_txt
            // 
            this.encry_txt.Location = new System.Drawing.Point(203, 89);
            this.encry_txt.Name = "encry_txt";
            this.encry_txt.Size = new System.Drawing.Size(75, 23);
            this.encry_txt.TabIndex = 19;
            this.encry_txt.Text = "加密Txt文件";
            this.encry_txt.UseVisualStyleBackColor = true;
            this.encry_txt.Click += new System.EventHandler(this.encry_txt_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(298, 89);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "处理Link";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(393, 89);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(118, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "处理所有Link";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(529, 89);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 23);
            this.button5.TabIndex = 22;
            this.button5.Text = "所有Links，建议文件";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 133);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(118, 23);
            this.button6.TabIndex = 23;
            this.button6.Text = "设置窗口颜色风格";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(721, 11);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 24;
            this.button7.Text = "加密";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(674, 89);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 25;
            this.button8.Text = "重复文件检查";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(611, 250);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 26;
            this.button9.Text = "拆分任务";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(591, 204);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 27;
            this.button10.Text = "番茄添加8小时";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(180, 133);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(147, 23);
            this.button11.TabIndex = 28;
            this.button11.Text = "删除时长等于0的时间块";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 252);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(104, 21);
            this.textBox1.TabIndex = 29;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(180, 251);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 30;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(393, 251);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker2.TabIndex = 31;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(37, 290);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 32;
            this.checkBox2.Text = "工作日";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(122, 278);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 33;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(813, 318);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.encry_txt);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.createsuggest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Decrypt);
            this.Controls.Add(this.setPWD);
            this.Controls.Add(this.pwd);
            this.Controls.Add(this.deletetemp);
            this.Controls.Add(this.reducejson);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tools";
            this.Text = "Tools";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.Load += new System.EventHandler(this.Tools_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button reducejson;
        private System.Windows.Forms.Button deletetemp;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.Button setPWD;
        private System.Windows.Forms.Button Decrypt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button createsuggest;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button encry_txt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}