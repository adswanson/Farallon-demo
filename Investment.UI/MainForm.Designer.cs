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
            this.tcSubPages = new System.Windows.Forms.TabControl();
            this.tpTradeHistory = new System.Windows.Forms.TabPage();
            this.tpProfitsAndLossesReport = new System.Windows.Forms.TabPage();
            this.pnlPortfolioPicker = new System.Windows.Forms.Panel();
            this.tcSubPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSubPages
            // 
            this.tcSubPages.Controls.Add(this.tpTradeHistory);
            this.tcSubPages.Controls.Add(this.tpProfitsAndLossesReport);
            this.tcSubPages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tcSubPages.Location = new System.Drawing.Point(0, 111);
            this.tcSubPages.Name = "tcSubPages";
            this.tcSubPages.SelectedIndex = 0;
            this.tcSubPages.Size = new System.Drawing.Size(952, 339);
            this.tcSubPages.TabIndex = 2;
            // 
            // tpTradeHistory
            // 
            this.tpTradeHistory.Location = new System.Drawing.Point(4, 22);
            this.tpTradeHistory.Name = "tpTradeHistory";
            this.tpTradeHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpTradeHistory.Size = new System.Drawing.Size(944, 313);
            this.tpTradeHistory.TabIndex = 0;
            this.tpTradeHistory.Text = "Trade History";
            this.tpTradeHistory.UseVisualStyleBackColor = true;
            // 
            // tpProfitsAndLossesReport
            // 
            this.tpProfitsAndLossesReport.Location = new System.Drawing.Point(4, 22);
            this.tpProfitsAndLossesReport.Name = "tpProfitsAndLossesReport";
            this.tpProfitsAndLossesReport.Padding = new System.Windows.Forms.Padding(3);
            this.tpProfitsAndLossesReport.Size = new System.Drawing.Size(944, 313);
            this.tpProfitsAndLossesReport.TabIndex = 1;
            this.tpProfitsAndLossesReport.Text = "P & L Report";
            this.tpProfitsAndLossesReport.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.tcSubPages);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcSubPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tcSubPages;
        private System.Windows.Forms.TabPage tpTradeHistory;
        private System.Windows.Forms.TabPage tpProfitsAndLossesReport;
        private System.Windows.Forms.Panel pnlPortfolioPicker;
    }
}

