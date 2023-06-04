namespace Investment.UI.Controls
{
    partial class PortfolioPicker
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
            this.lblPortfoliosHeading = new System.Windows.Forms.Label();
            this.ddlPortfolios = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblPortfoliosHeading
            // 
            this.lblPortfoliosHeading.AutoSize = true;
            this.lblPortfoliosHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortfoliosHeading.Location = new System.Drawing.Point(3, 19);
            this.lblPortfoliosHeading.Name = "lblPortfoliosHeading";
            this.lblPortfoliosHeading.Size = new System.Drawing.Size(82, 24);
            this.lblPortfoliosHeading.TabIndex = 0;
            this.lblPortfoliosHeading.Text = "Portfolio:";
            // 
            // ddlPortfolios
            // 
            this.ddlPortfolios.DisplayMember = "Name";
            this.ddlPortfolios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPortfolios.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPortfolios.FormattingEnabled = true;
            this.ddlPortfolios.Location = new System.Drawing.Point(103, 16);
            this.ddlPortfolios.Name = "ddlPortfolios";
            this.ddlPortfolios.Size = new System.Drawing.Size(262, 32);
            this.ddlPortfolios.TabIndex = 1;
            this.ddlPortfolios.SelectedIndexChanged += new System.EventHandler(this.ddlPortfolios_SelectedIndexChanged);
            // 
            // PortfolioPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.ddlPortfolios);
            this.Controls.Add(this.lblPortfoliosHeading);
            this.Name = "PortfolioPicker";
            this.Size = new System.Drawing.Size(377, 62);
            this.Load += new System.EventHandler(this.PortfolioPicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPortfoliosHeading;
        private System.Windows.Forms.ComboBox ddlPortfolios;
    }
}
