namespace AutoSend
{
    partial class frmcj
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label64 = new System.Windows.Forms.Label();
            this.btnAdddeal = new System.Windows.Forms.Button();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.dgvtitlegather = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wztitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btndeal = new System.Windows.Forms.DataGridViewButtonColumn();
            this.urls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnonekeygather = new System.Windows.Forms.Button();
            this.txturlgather = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtitlegather)).BeginInit();
            this.SuspendLayout();
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(627, 403);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(161, 12);
            this.label64.TabIndex = 19;
            this.label64.Text = "注：如出现乱码，请更换网址";
            // 
            // btnAdddeal
            // 
            this.btnAdddeal.Location = new System.Drawing.Point(688, 27);
            this.btnAdddeal.Name = "btnAdddeal";
            this.btnAdddeal.Size = new System.Drawing.Size(75, 23);
            this.btnAdddeal.TabIndex = 18;
            this.btnAdddeal.Text = "一键编辑";
            this.btnAdddeal.UseVisualStyleBackColor = true;
            this.btnAdddeal.Click += new System.EventHandler(this.btnAdddeal_Click);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(66, 15);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(197, 12);
            this.label61.TabIndex = 17;
            this.label61.Text = "如：http://www.xxx.com/list.html";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(19, 403);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(83, 12);
            this.label60.TabIndex = 16;
            this.label60.Text = "文章数量：0篇";
            // 
            // dgvtitlegather
            // 
            this.dgvtitlegather.AllowUserToAddRows = false;
            this.dgvtitlegather.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvtitlegather.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvtitlegather.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtitlegather.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.wztitle,
            this.IsState,
            this.btndeal,
            this.urls});
            this.dgvtitlegather.Location = new System.Drawing.Point(12, 65);
            this.dgvtitlegather.Name = "dgvtitlegather";
            this.dgvtitlegather.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvtitlegather.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvtitlegather.RowHeadersVisible = false;
            this.dgvtitlegather.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvtitlegather.RowTemplate.Height = 23;
            this.dgvtitlegather.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvtitlegather.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvtitlegather.Size = new System.Drawing.Size(776, 326);
            this.dgvtitlegather.TabIndex = 15;
            this.dgvtitlegather.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvtitlegather_CellContentClick);
            this.dgvtitlegather.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvtitlegather_RowPostPaint);
            // 
            // Id
            // 
            this.Id.HeaderText = "序号";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // wztitle
            // 
            this.wztitle.DataPropertyName = "title";
            this.wztitle.HeaderText = "文章标题";
            this.wztitle.Name = "wztitle";
            this.wztitle.ReadOnly = true;
            this.wztitle.Width = 450;
            // 
            // IsState
            // 
            this.IsState.DataPropertyName = "isdeal";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IsState.DefaultCellStyle = dataGridViewCellStyle2;
            this.IsState.HeaderText = "处理状态";
            this.IsState.Name = "IsState";
            this.IsState.ReadOnly = true;
            // 
            // btndeal
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.NullValue = "预览";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btndeal.DefaultCellStyle = dataGridViewCellStyle3;
            this.btndeal.HeaderText = "预览";
            this.btndeal.Name = "btndeal";
            this.btndeal.ReadOnly = true;
            this.btndeal.Text = "预览段落";
            // 
            // urls
            // 
            this.urls.DataPropertyName = "url";
            this.urls.HeaderText = "urlss";
            this.urls.Name = "urls";
            this.urls.ReadOnly = true;
            this.urls.Visible = false;
            // 
            // btnonekeygather
            // 
            this.btnonekeygather.Location = new System.Drawing.Point(514, 27);
            this.btnonekeygather.Name = "btnonekeygather";
            this.btnonekeygather.Size = new System.Drawing.Size(75, 23);
            this.btnonekeygather.TabIndex = 14;
            this.btnonekeygather.Text = "一键采集";
            this.btnonekeygather.UseVisualStyleBackColor = true;
            this.btnonekeygather.Click += new System.EventHandler(this.btnonekeygather_Click);
            // 
            // txturlgather
            // 
            this.txturlgather.Location = new System.Drawing.Point(68, 30);
            this.txturlgather.Name = "txturlgather";
            this.txturlgather.Size = new System.Drawing.Size(416, 21);
            this.txturlgather.TabIndex = 13;
            this.txturlgather.Click += new System.EventHandler(this.txturlgather_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(21, 33);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(41, 12);
            this.label29.TabIndex = 12;
            this.label29.Text = "网址：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(299, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "注意：请输入与本行业相关的网址";
            // 
            // frmcj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 429);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label64);
            this.Controls.Add(this.btnAdddeal);
            this.Controls.Add(this.label61);
            this.Controls.Add(this.label60);
            this.Controls.Add(this.dgvtitlegather);
            this.Controls.Add(this.btnonekeygather);
            this.Controls.Add(this.txturlgather);
            this.Controls.Add(this.label29);
            this.Name = "frmcj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmcj";
            ((System.ComponentModel.ISupportInitialize)(this.dgvtitlegather)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Button btnAdddeal;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.DataGridView dgvtitlegather;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn wztitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsState;
        private System.Windows.Forms.DataGridViewButtonColumn btndeal;
        private System.Windows.Forms.DataGridViewTextBoxColumn urls;
        private System.Windows.Forms.Button btnonekeygather;
        private System.Windows.Forms.TextBox txturlgather;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label1;
    }
}