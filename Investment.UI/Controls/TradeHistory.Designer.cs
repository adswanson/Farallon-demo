namespace Investment.UI.Controls
{
    partial class TradeHistory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgPortfolioTradeHistory = new System.Windows.Forms.DataGridView();
            this.pnlTradeHistory = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolioTradeHistory)).BeginInit();
            this.pnlTradeHistory.SuspendLayout();
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
            this.dgPortfolioTradeHistory.Size = new System.Drawing.Size(363, 150);
            this.dgPortfolioTradeHistory.TabIndex = 1;
            // 
            // pnlTradeHistory
            // 
            this.pnlTradeHistory.Controls.Add(this.dgPortfolioTradeHistory);
            this.pnlTradeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTradeHistory.Location = new System.Drawing.Point(0, 0);
            this.pnlTradeHistory.Name = "pnlTradeHistory";
            this.pnlTradeHistory.Size = new System.Drawing.Size(363, 150);
            this.pnlTradeHistory.TabIndex = 2;
            // 
            // TradeHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlTradeHistory);
            this.Name = "TradeHistory";
            this.Size = new System.Drawing.Size(363, 150);
            this.Load += new System.EventHandler(this.TradeHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolioTradeHistory)).EndInit();
            this.pnlTradeHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPortfolioTradeHistory;
        private System.Windows.Forms.Panel pnlTradeHistory;
    }
}
