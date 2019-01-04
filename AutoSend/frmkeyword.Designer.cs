namespace AutoSend
{
    partial class frmkeyword
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtgjc1 = new System.Windows.Forms.TextBox();
            this.txtgjc2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtgjc3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txthz = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtjg = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtscgs = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtnum = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键词1，共有0行";
            // 
            // txtgjc1
            // 
            this.txtgjc1.Location = new System.Drawing.Point(24, 29);
            this.txtgjc1.Multiline = true;
            this.txtgjc1.Name = "txtgjc1";
            this.txtgjc1.Size = new System.Drawing.Size(122, 313);
            this.txtgjc1.TabIndex = 1;
            this.txtgjc1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtgjc2
            // 
            this.txtgjc2.Location = new System.Drawing.Point(171, 29);
            this.txtgjc2.Multiline = true;
            this.txtgjc2.Name = "txtgjc2";
            this.txtgjc2.Size = new System.Drawing.Size(122, 313);
            this.txtgjc2.TabIndex = 3;
            this.txtgjc2.TextChanged += new System.EventHandler(this.txtgjc2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "关键词2，共有0行";
            // 
            // txtgjc3
            // 
            this.txtgjc3.Location = new System.Drawing.Point(313, 29);
            this.txtgjc3.Multiline = true;
            this.txtgjc3.Name = "txtgjc3";
            this.txtgjc3.Size = new System.Drawing.Size(122, 313);
            this.txtgjc3.TabIndex = 5;
            this.txtgjc3.TextChanged += new System.EventHandler(this.txtgjc3_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "关键词3，共有0行";
            // 
            // txthz
            // 
            this.txthz.Location = new System.Drawing.Point(460, 29);
            this.txthz.Multiline = true;
            this.txthz.Name = "txthz";
            this.txthz.Size = new System.Drawing.Size(122, 313);
            this.txthz.TabIndex = 7;
            this.txthz.TextChanged += new System.EventHandler(this.txthz_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(458, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "后缀，共有0行";
            // 
            // txtjg
            // 
            this.txtjg.Location = new System.Drawing.Point(607, 29);
            this.txtjg.Multiline = true;
            this.txtjg.Name = "txtjg";
            this.txtjg.Size = new System.Drawing.Size(122, 313);
            this.txtjg.TabIndex = 9;
            this.txtjg.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(605, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "生成结果，共有0行";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(735, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 68);
            this.button1.TabIndex = 10;
            this.button1.Text = "加入到主变量";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(40, 364);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "关键词1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(146, 364);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "关键词2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(252, 364);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "关键词3";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(358, 364);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "后缀";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtscgs
            // 
            this.txtscgs.Location = new System.Drawing.Point(84, 403);
            this.txtscgs.Name = "txtscgs";
            this.txtscgs.Size = new System.Drawing.Size(487, 21);
            this.txtscgs.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 406);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "生成格式：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(460, 374);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "生成数量：";
            // 
            // txtnum
            // 
            this.txtnum.Location = new System.Drawing.Point(520, 366);
            this.txtnum.Name = "txtnum";
            this.txtnum.Size = new System.Drawing.Size(51, 21);
            this.txtnum.TabIndex = 18;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(607, 366);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 58);
            this.button6.TabIndex = 19;
            this.button6.Text = "开始生成";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // frmkeyword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 446);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.txtnum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtscgs);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtjg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txthz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtgjc3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtgjc2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtgjc1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmkeyword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关键词组合器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtgjc1;
        private System.Windows.Forms.TextBox txtgjc2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtgjc3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txthz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtjg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtscgs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtnum;
        private System.Windows.Forms.Button button6;
    }
}