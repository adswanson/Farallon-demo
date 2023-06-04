namespace Investment.UI.Controls
{
    partial class ProfitsAndLossesReport
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
            this.dgProfitsAndLosses = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgProfitsAndLosses)).BeginInit();
            this.SuspendLayout();
            // 
            // dgProfitsAndLosses
            // 
            this.dgProfitsAndLosses.AllowUserToAddRows = false;
            this.dgProfitsAndLosses.AllowUserToDeleteRows = false;
            this.dgProfitsAndLosses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgProfitsAndLosses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProfitsAndLosses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProfitsAndLosses.Location = new System.Drawing.Point(0, 0);
            this.dgProfitsAndLosses.Name = "dgProfitsAndLosses";
            this.dgProfitsAndLosses.ReadOnly = true;
            this.dgProfitsAndLosses.Size = new System.Drawing.Size(976, 231);
            this.dgProfitsAndLosses.TabIndex = 2;
            // 
            // ProfitsAndLossesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgProfitsAndLosses);
            this.Name = "ProfitsAndLossesReport";
            this.Size = new System.Drawing.Size(976, 231);
            this.Load += new System.EventHandler(this.ProfitsAndLossesReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgProfitsAndLosses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgProfitsAndLosses;
    }
}
