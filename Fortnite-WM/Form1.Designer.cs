namespace Fortnite_WM
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_DB_PW = new System.Windows.Forms.TextBox();
            this.lb_DB_PW = new System.Windows.Forms.Label();
            this.tb_DB_UID = new System.Windows.Forms.TextBox();
            this.lb_DB_UID = new System.Windows.Forms.Label();
            this.lb_DatabaseValue = new System.Windows.Forms.Label();
            this.lb_Database = new System.Windows.Forms.Label();
            this.lb_ConnectionValue = new System.Windows.Forms.Label();
            this.lb_Connection = new System.Windows.Forms.Label();
            this.tc_fortnitewm = new System.Windows.Forms.TabControl();
            this.tp_Database_Infos = new System.Windows.Forms.TabPage();
            this.tb_insert = new System.Windows.Forms.TabPage();
            this.tb_Select = new System.Windows.Forms.TabPage();
            this.tb_update = new System.Windows.Forms.TabPage();
            this.btn_TestCon = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tc_fortnitewm.SuspendLayout();
            this.tp_Database_Infos.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_TestCon);
            this.groupBox1.Controls.Add(this.tb_DB_PW);
            this.groupBox1.Controls.Add(this.lb_DB_PW);
            this.groupBox1.Controls.Add(this.tb_DB_UID);
            this.groupBox1.Controls.Add(this.lb_DB_UID);
            this.groupBox1.Controls.Add(this.lb_DatabaseValue);
            this.groupBox1.Controls.Add(this.lb_Database);
            this.groupBox1.Controls.Add(this.lb_ConnectionValue);
            this.groupBox1.Controls.Add(this.lb_Connection);
            this.groupBox1.Location = new System.Drawing.Point(562, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datenbank Status";
            // 
            // tb_DB_PW
            // 
            this.tb_DB_PW.Location = new System.Drawing.Point(94, 85);
            this.tb_DB_PW.Name = "tb_DB_PW";
            this.tb_DB_PW.PasswordChar = '*';
            this.tb_DB_PW.Size = new System.Drawing.Size(100, 20);
            this.tb_DB_PW.TabIndex = 9;
            // 
            // lb_DB_PW
            // 
            this.lb_DB_PW.AutoSize = true;
            this.lb_DB_PW.Location = new System.Drawing.Point(6, 88);
            this.lb_DB_PW.Name = "lb_DB_PW";
            this.lb_DB_PW.Size = new System.Drawing.Size(53, 13);
            this.lb_DB_PW.TabIndex = 8;
            this.lb_DB_PW.Text = "Passwort:";
            // 
            // tb_DB_UID
            // 
            this.tb_DB_UID.Location = new System.Drawing.Point(94, 59);
            this.tb_DB_UID.Name = "tb_DB_UID";
            this.tb_DB_UID.Size = new System.Drawing.Size(100, 20);
            this.tb_DB_UID.TabIndex = 7;
            this.tb_DB_UID.Text = "root";
            // 
            // lb_DB_UID
            // 
            this.lb_DB_UID.AutoSize = true;
            this.lb_DB_UID.Location = new System.Drawing.Point(6, 62);
            this.lb_DB_UID.Name = "lb_DB_UID";
            this.lb_DB_UID.Size = new System.Drawing.Size(46, 13);
            this.lb_DB_UID.TabIndex = 6;
            this.lb_DB_UID.Text = "User ID:";
            // 
            // lb_DatabaseValue
            // 
            this.lb_DatabaseValue.AutoSize = true;
            this.lb_DatabaseValue.Location = new System.Drawing.Point(109, 33);
            this.lb_DatabaseValue.Name = "lb_DatabaseValue";
            this.lb_DatabaseValue.Size = new System.Drawing.Size(58, 13);
            this.lb_DatabaseValue.TabIndex = 3;
            this.lb_DatabaseValue.Text = "unbekannt";
            // 
            // lb_Database
            // 
            this.lb_Database.AutoSize = true;
            this.lb_Database.Location = new System.Drawing.Point(6, 33);
            this.lb_Database.Name = "lb_Database";
            this.lb_Database.Size = new System.Drawing.Size(94, 13);
            this.lb_Database.TabIndex = 2;
            this.lb_Database.Text = "Datenbank status:";
            // 
            // lb_ConnectionValue
            // 
            this.lb_ConnectionValue.AutoSize = true;
            this.lb_ConnectionValue.Location = new System.Drawing.Point(109, 20);
            this.lb_ConnectionValue.Name = "lb_ConnectionValue";
            this.lb_ConnectionValue.Size = new System.Drawing.Size(58, 13);
            this.lb_ConnectionValue.TabIndex = 1;
            this.lb_ConnectionValue.Text = "unbekannt";
            // 
            // lb_Connection
            // 
            this.lb_Connection.AutoSize = true;
            this.lb_Connection.Location = new System.Drawing.Point(6, 20);
            this.lb_Connection.Name = "lb_Connection";
            this.lb_Connection.Size = new System.Drawing.Size(97, 13);
            this.lb_Connection.TabIndex = 0;
            this.lb_Connection.Text = "Verbindungsstatus:";
            // 
            // tc_fortnitewm
            // 
            this.tc_fortnitewm.Controls.Add(this.tp_Database_Infos);
            this.tc_fortnitewm.Controls.Add(this.tb_insert);
            this.tc_fortnitewm.Controls.Add(this.tb_Select);
            this.tc_fortnitewm.Controls.Add(this.tb_update);
            this.tc_fortnitewm.Location = new System.Drawing.Point(12, 12);
            this.tc_fortnitewm.Name = "tc_fortnitewm";
            this.tc_fortnitewm.SelectedIndex = 0;
            this.tc_fortnitewm.Size = new System.Drawing.Size(776, 426);
            this.tc_fortnitewm.TabIndex = 2;
            // 
            // tp_Database_Infos
            // 
            this.tp_Database_Infos.Controls.Add(this.groupBox1);
            this.tp_Database_Infos.Location = new System.Drawing.Point(4, 22);
            this.tp_Database_Infos.Name = "tp_Database_Infos";
            this.tp_Database_Infos.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Database_Infos.Size = new System.Drawing.Size(768, 400);
            this.tp_Database_Infos.TabIndex = 0;
            this.tp_Database_Infos.Text = "DB Infos";
            this.tp_Database_Infos.UseVisualStyleBackColor = true;
            // 
            // tb_insert
            // 
            this.tb_insert.Location = new System.Drawing.Point(4, 22);
            this.tb_insert.Name = "tb_insert";
            this.tb_insert.Padding = new System.Windows.Forms.Padding(3);
            this.tb_insert.Size = new System.Drawing.Size(768, 400);
            this.tb_insert.TabIndex = 1;
            this.tb_insert.Text = "Dateneingaben";
            this.tb_insert.UseVisualStyleBackColor = true;
            // 
            // tb_Select
            // 
            this.tb_Select.Location = new System.Drawing.Point(4, 22);
            this.tb_Select.Name = "tb_Select";
            this.tb_Select.Padding = new System.Windows.Forms.Padding(3);
            this.tb_Select.Size = new System.Drawing.Size(768, 400);
            this.tb_Select.TabIndex = 2;
            this.tb_Select.Text = "Datenabfragen";
            this.tb_Select.UseVisualStyleBackColor = true;
            // 
            // tb_update
            // 
            this.tb_update.Location = new System.Drawing.Point(4, 22);
            this.tb_update.Name = "tb_update";
            this.tb_update.Padding = new System.Windows.Forms.Padding(3);
            this.tb_update.Size = new System.Drawing.Size(768, 400);
            this.tb_update.TabIndex = 3;
            this.tb_update.Text = "Datenänderung";
            this.tb_update.UseVisualStyleBackColor = true;
            // 
            // btn_TestCon
            // 
            this.btn_TestCon.Location = new System.Drawing.Point(94, 112);
            this.btn_TestCon.Name = "btn_TestCon";
            this.btn_TestCon.Size = new System.Drawing.Size(100, 23);
            this.btn_TestCon.TabIndex = 10;
            this.btn_TestCon.Text = "Verbinden";
            this.btn_TestCon.UseVisualStyleBackColor = true;
            this.btn_TestCon.Click += new System.EventHandler(this.btn_TestCon_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tc_fortnitewm);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tc_fortnitewm.ResumeLayout(false);
            this.tp_Database_Infos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_DB_PW;
        private System.Windows.Forms.Label lb_DB_PW;
        private System.Windows.Forms.TextBox tb_DB_UID;
        private System.Windows.Forms.Label lb_DB_UID;
        private System.Windows.Forms.Label lb_DatabaseValue;
        private System.Windows.Forms.Label lb_Database;
        private System.Windows.Forms.Label lb_ConnectionValue;
        private System.Windows.Forms.Label lb_Connection;
        private System.Windows.Forms.TabControl tc_fortnitewm;
        private System.Windows.Forms.TabPage tp_Database_Infos;
        private System.Windows.Forms.TabPage tb_insert;
        private System.Windows.Forms.Button btn_TestCon;
        private System.Windows.Forms.TabPage tb_Select;
        private System.Windows.Forms.TabPage tb_update;
    }
}

