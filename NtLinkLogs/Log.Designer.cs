namespace NtLinkLogs
{
    partial class Log
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
            this.gbQueryConfiguration = new System.Windows.Forms.GroupBox();
            this.txtQueryHoraFinal = new System.Windows.Forms.MaskedTextBox();
            this.txtQueryHoraInicial = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpQueryDate = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.gbQueryResults = new System.Windows.Forms.GroupBox();
            this.dgvQueryResults = new System.Windows.Forms.DataGridView();
            this.tcLogViews = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStopMonitor = new System.Windows.Forms.Button();
            this.btnStartMonitor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tmrMonitorTicker = new System.Windows.Forms.Timer(this.components);
            this.gbQueryConfiguration.SuspendLayout();
            this.gbQueryResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResults)).BeginInit();
            this.tcLogViews.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbQueryConfiguration
            // 
            this.gbQueryConfiguration.Controls.Add(this.txtQueryHoraFinal);
            this.gbQueryConfiguration.Controls.Add(this.txtQueryHoraInicial);
            this.gbQueryConfiguration.Controls.Add(this.label3);
            this.gbQueryConfiguration.Controls.Add(this.label1);
            this.gbQueryConfiguration.Controls.Add(this.label2);
            this.gbQueryConfiguration.Controls.Add(this.dtpQueryDate);
            this.gbQueryConfiguration.Controls.Add(this.btnQuery);
            this.gbQueryConfiguration.Location = new System.Drawing.Point(6, 6);
            this.gbQueryConfiguration.Name = "gbQueryConfiguration";
            this.gbQueryConfiguration.Size = new System.Drawing.Size(905, 79);
            this.gbQueryConfiguration.TabIndex = 2;
            this.gbQueryConfiguration.TabStop = false;
            this.gbQueryConfiguration.Text = "Configuración";
            // 
            // txtQueryHoraFinal
            // 
            this.txtQueryHoraFinal.Location = new System.Drawing.Point(408, 30);
            this.txtQueryHoraFinal.Mask = "00:00";
            this.txtQueryHoraFinal.Name = "txtQueryHoraFinal";
            this.txtQueryHoraFinal.Size = new System.Drawing.Size(50, 20);
            this.txtQueryHoraFinal.TabIndex = 12;
            this.txtQueryHoraFinal.ValidatingType = typeof(System.DateTime);
            // 
            // txtQueryHoraInicial
            // 
            this.txtQueryHoraInicial.Location = new System.Drawing.Point(287, 31);
            this.txtQueryHoraInicial.Mask = "00:00";
            this.txtQueryHoraInicial.Name = "txtQueryHoraInicial";
            this.txtQueryHoraInicial.Size = new System.Drawing.Size(54, 20);
            this.txtQueryHoraInicial.TabIndex = 11;
            this.txtQueryHoraInicial.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(347, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Hora Final";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hora Inicial";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Fecha";
            // 
            // dtpQueryDate
            // 
            this.dtpQueryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpQueryDate.Location = new System.Drawing.Point(51, 31);
            this.dtpQueryDate.Name = "dtpQueryDate";
            this.dtpQueryDate.Size = new System.Drawing.Size(164, 20);
            this.dtpQueryDate.TabIndex = 5;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnQuery.Location = new System.Drawing.Point(800, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(99, 44);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "Iniciar";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // gbQueryResults
            // 
            this.gbQueryResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbQueryResults.Controls.Add(this.dgvQueryResults);
            this.gbQueryResults.Location = new System.Drawing.Point(6, 85);
            this.gbQueryResults.Name = "gbQueryResults";
            this.gbQueryResults.Size = new System.Drawing.Size(905, 594);
            this.gbQueryResults.TabIndex = 3;
            this.gbQueryResults.TabStop = false;
            this.gbQueryResults.Text = "Entradas de Log";
            // 
            // dgvQueryResults
            // 
            this.dgvQueryResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQueryResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueryResults.Location = new System.Drawing.Point(12, 19);
            this.dgvQueryResults.Name = "dgvQueryResults";
            this.dgvQueryResults.Size = new System.Drawing.Size(887, 565);
            this.dgvQueryResults.TabIndex = 0;
            this.dgvQueryResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueryResults_CellContentClick);
            this.dgvQueryResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvQueryResults_CellFormatting);
            // 
            // tcLogViews
            // 
            this.tcLogViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcLogViews.Controls.Add(this.tabPage1);
            this.tcLogViews.Controls.Add(this.tabPage2);
            this.tcLogViews.Location = new System.Drawing.Point(12, 12);
            this.tcLogViews.Name = "tcLogViews";
            this.tcLogViews.SelectedIndex = 0;
            this.tcLogViews.Size = new System.Drawing.Size(924, 707);
            this.tcLogViews.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbQueryConfiguration);
            this.tabPage1.Controls.Add(this.gbQueryResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(916, 681);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Búsqueda";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(916, 681);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Monitor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStopMonitor);
            this.groupBox2.Controls.Add(this.btnStartMonitor);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(905, 79);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuración";
            // 
            // btnStopMonitor
            // 
            this.btnStopMonitor.Enabled = false;
            this.btnStopMonitor.Location = new System.Drawing.Point(800, 21);
            this.btnStopMonitor.Name = "btnStopMonitor";
            this.btnStopMonitor.Size = new System.Drawing.Size(99, 44);
            this.btnStopMonitor.TabIndex = 5;
            this.btnStopMonitor.Text = "Detener";
            this.btnStopMonitor.UseVisualStyleBackColor = true;
            this.btnStopMonitor.Click += new System.EventHandler(this.btnStopMonitor_Click);
            // 
            // btnStartMonitor
            // 
            this.btnStartMonitor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnStartMonitor.Location = new System.Drawing.Point(695, 21);
            this.btnStartMonitor.Name = "btnStartMonitor";
            this.btnStartMonitor.Size = new System.Drawing.Size(99, 44);
            this.btnStartMonitor.TabIndex = 4;
            this.btnStartMonitor.Text = "Iniciar";
            this.btnStartMonitor.UseVisualStyleBackColor = true;
            this.btnStartMonitor.Click += new System.EventHandler(this.btnStartMonitor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOutput);
            this.groupBox1.Location = new System.Drawing.Point(6, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(905, 594);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entradas de Log";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(6, 19);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(893, 569);
            this.txtOutput.TabIndex = 0;
            // 
            // tmrMonitorTicker
            // 
            this.tmrMonitorTicker.Interval = 5000;
            this.tmrMonitorTicker.Tick += new System.EventHandler(this.tmrMonitorTicker_Tick);
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 731);
            this.Controls.Add(this.tcLogViews);
            this.Name = "Log";
            this.Text = "Log NtLink";
            this.Load += new System.EventHandler(this.Log_Load);
            this.gbQueryConfiguration.ResumeLayout(false);
            this.gbQueryConfiguration.PerformLayout();
            this.gbQueryResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResults)).EndInit();
            this.tcLogViews.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbQueryConfiguration;
        private System.Windows.Forms.GroupBox gbQueryResults;
        private System.Windows.Forms.DataGridView dgvQueryResults;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DateTimePicker dtpQueryDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtQueryHoraFinal;
        private System.Windows.Forms.MaskedTextBox txtQueryHoraInicial;
        private System.Windows.Forms.TabControl tcLogViews;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStartMonitor;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnStopMonitor;
        private System.Windows.Forms.Timer tmrMonitorTicker;

    }
}

