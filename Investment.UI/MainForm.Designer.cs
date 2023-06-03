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
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolioTradeHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPortfolioTradeHistory
            // 
            this.dgPortfolioTradeHistory.AllowUserToAddRows = false;
            this.dgPortfolioTradeHistory.AllowUserToDeleteRows = false;
            this.dgPortfolioTradeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPortfolioTradeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPortfolioTradeHistory.Location = new System.Drawing.Point(0, 0);
            this.dgPortfolioTradeHistory.Name = "dgPortfolioTradeHistory";
            this.dgPortfolioTradeHistory.ReadOnly = true;
            this.dgPortfolioTradeHistory.Size = new System.Drawing.Size(952, 450);
            this.dgPortfolioTradeHistory.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 450);
            this.Controls.Add(this.dgPortfolioTradeHistory);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolioTradeHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPortfolioTradeHistory;
    }
}

