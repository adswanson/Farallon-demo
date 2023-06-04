namespace Investment.UI
{
    partial class MainForm
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
            this.dgPortfolioTradeHistory = new System.Windows.Forms.DataGridView();
            this.pnlTradeLog = new System.Windows.Forms.Panel();
            this.tabTradeHistory = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlPortfolioPicker = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolioTradeHistory)).BeginInit();
            this.pnlTradeLog.SuspendLayout();
            this.tabTradeHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgPortfolioTradeHistory
            // 
            this.dgPortfolioTradeHistory.AllowUserToAddRows = false;
            this.dgPortfolioTradeHistory.AllowUserToDeleteRows = false;
            this.dgPortfolioTradeHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPortfolioTradeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPortfolioTradeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPortfolioTradeHistory.Location = new System.Drawing.Point(0, 0);
            this.dgPortfolioTradeHistory.Name = "dgPortfolioTradeHistory";
            this.dgPortfolioTradeHistory.ReadOnly = true;
            this.dgPortfolioTradeHistory.Size = new System.Drawing.Size(952, 199);
            this.dgPortfolioTradeHistory.TabIndex = 0;
            // 
            // pnlTradeLog
            // 
            this.pnlTradeLog.AutoScroll = true;
            this.pnlTradeLog.Controls.Add(this.dgPortfolioTradeHistory);
            this.pnlTradeLog.Location = new System.Drawing.Point(0, 91);
            this.pnlTradeLog.Name = "pnlTradeLog";
            this.pnlTradeLog.Size = new System.Drawing.Size(952, 199);
            this.pnlTradeLog.TabIndex = 1;
            // 
            // tabTradeHistory
            // 
            this.tabTradeHistory.Controls.Add(this.tabPage1);
            this.tabTradeHistory.Controls.Add(this.tabPage2);
            this.tabTradeHistory.Location = new System.Drawing.Point(37, 314);
            this.tabTradeHistory.Name = "tabTradeHistory";
            this.tabTradeHistory.SelectedIndex = 0;
            this.tabTradeHistory.Size = new System.Drawing.Size(200, 100);
            this.tabTradeHistory.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnlPortfolioPicker
            // 
            this.pnlPortfolioPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPortfolioPicker.Location = new System.Drawing.Point(0, 0);
            this.pnlPortfolioPicker.Name = "pnlPortfolioPicker";
            this.pnlPortfolioPicker.Size = new System.Drawing.Size(952, 93);
            this.pnlPortfolioPicker.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 450);
            this.Controls.Add(this.pnlPortfolioPicker);
            this.Controls.Add(this.tabTradeHistory);
            this.Controls.Add(this.pnlTradeLog);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolioTradeHistory)).EndInit();
            this.pnlTradeLog.ResumeLayout(false);
            this.tabTradeHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPortfolioTradeHistory;
        private System.Windows.Forms.Panel pnlTradeLog;
        private System.Windows.Forms.TabControl tabTradeHistory;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlPortfolioPicker;
    }
}

