namespace Fortnite_WM
{
    partial class Ausgabe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ausgabe));
            this.dgv_Ausgabe = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Ausgabe)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Ausgabe
            // 
            this.dgv_Ausgabe.AllowUserToAddRows = false;
            this.dgv_Ausgabe.AllowUserToDeleteRows = false;
            this.dgv_Ausgabe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Ausgabe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Ausgabe.Location = new System.Drawing.Point(0, 0);
            this.dgv_Ausgabe.Name = "dgv_Ausgabe";
            this.dgv_Ausgabe.ReadOnly = true;
            this.dgv_Ausgabe.Size = new System.Drawing.Size(800, 450);
            this.dgv_Ausgabe.TabIndex = 0;
            // 
            // Ausgabe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_Ausgabe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ausgabe";
            this.Text = "Ausgabe";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Ausgabe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Ausgabe;
    }
}