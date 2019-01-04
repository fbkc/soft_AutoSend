namespace AutoSend
{
    partial class frmparagraphdeal
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
            this.components = new System.ComponentModel.Container();
            this.btnonekeydeal = new System.Windows.Forms.Button();
            this.richtxtpardeal = new System.Windows.Forms.RichTextBox();
            this.btnsavetopar = new System.Windows.Forms.Button();
            this.btnclear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnchange = new System.Windows.Forms.Button();
            this.txtnewword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtoldword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnonekeydeal
            // 
            this.btnonekeydeal.Location = new System.Drawing.Point(818, 30);
            this.btnonekeydeal.Name = "btnonekeydeal";
            this.btnonekeydeal.Size = new System.Drawing.Size(121, 34);
            this.btnonekeydeal.TabIndex = 1;
            this.btnonekeydeal.Text = "一键整理";
            this.btnonekeydeal.UseVisualStyleBackColor = true;
            this.btnonekeydeal.Click += new System.EventHandler(this.btnonekeydeal_Click);
            // 
            // richtxtpardeal
            // 
            this.richtxtpardeal.Location = new System.Drawing.Point(12, 79);
            this.richtxtpardeal.Name = "richtxtpardeal";
            this.richtxtpardeal.Size = new System.Drawing.Size(936, 408);
            this.richtxtpardeal.TabIndex = 5;
            this.richtxtpardeal.Text = "";
            this.richtxtpardeal.TextChanged += new System.EventHandler(this.richtxtpardeal_TextChanged);
            this.richtxtpardeal.MouseCaptureChanged += new System.EventHandler(this.richtxtpardeal_MouseCaptureChanged);
            this.richtxtpardeal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richtxtpardeal_MouseUp);
            // 
            // btnsavetopar
            // 
            this.btnsavetopar.Location = new System.Drawing.Point(858, 498);
            this.btnsavetopar.Name = "btnsavetopar";
            this.btnsavetopar.Size = new System.Drawing.Size(90, 23);
            this.btnsavetopar.TabIndex = 6;
            this.btnsavetopar.Text = "保存到段落";
            this.btnsavetopar.UseVisualStyleBackColor = true;
            this.btnsavetopar.Click += new System.EventHandler(this.btnsavetopar_Click);
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(764, 498);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(75, 23);
            this.btnclear.TabIndex = 7;
            this.btnclear.Text = "清除";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "文本字数：0个";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnchange);
            this.groupBox1.Controls.Add(this.txtnewword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtoldword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 61);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "替换词语";
            // 
            // btnchange
            // 
            this.btnchange.Location = new System.Drawing.Point(454, 24);
            this.btnchange.Name = "btnchange";
            this.btnchange.Size = new System.Drawing.Size(75, 23);
            this.btnchange.TabIndex = 6;
            this.btnchange.Text = "替换";
            this.btnchange.UseVisualStyleBackColor = true;
            this.btnchange.Click += new System.EventHandler(this.btnchange_Click);
            // 
            // txtnewword
            // 
            this.txtnewword.Location = new System.Drawing.Point(291, 26);
            this.txtnewword.Name = "txtnewword";
            this.txtnewword.Size = new System.Drawing.Size(100, 21);
            this.txtnewword.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "替换的词语：";
            // 
            // txtoldword
            // 
            this.txtoldword.Location = new System.Drawing.Point(100, 26);
            this.txtoldword.Name = "txtoldword";
            this.txtoldword.Size = new System.Drawing.Size(100, 21);
            this.txtoldword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "被替换的词语：";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem1.Text = "全选";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem2.Text = "剪切";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem3.Text = "复制";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem4.Text = "粘贴";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // frmparagraphdeal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 543);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.btnsavetopar);
            this.Controls.Add(this.richtxtpardeal);
            this.Controls.Add(this.btnonekeydeal);
            this.Name = "frmparagraphdeal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "段落处理";
            this.Load += new System.EventHandler(this.frmparagraphdeal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnonekeydeal;
        private System.Windows.Forms.Button btnsavetopar;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtoldword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtnewword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnchange;
        private System.Windows.Forms.RichTextBox richtxtpardeal;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    }
}