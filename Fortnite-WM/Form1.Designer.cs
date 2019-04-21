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
            this.gb_DB_State = new System.Windows.Forms.GroupBox();
            this.btn_TestCon = new System.Windows.Forms.Button();
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
            this.gb_TB_State = new System.Windows.Forms.GroupBox();
            this.lb_TB_Maps = new System.Windows.Forms.Label();
            this.lb_TB_Modes = new System.Windows.Forms.Label();
            this.lb_TB_Players = new System.Windows.Forms.Label();
            this.lb_TB_Teams = new System.Windows.Forms.Label();
            this.lb_TB_PM = new System.Windows.Forms.Label();
            this.lb_TB_PMValue = new System.Windows.Forms.Label();
            this.lb_TB_TeamsValue = new System.Windows.Forms.Label();
            this.lb_TB_PlayersValue = new System.Windows.Forms.Label();
            this.lb_TB_ModesValue = new System.Windows.Forms.Label();
            this.lb_TB_MapsValue = new System.Windows.Forms.Label();
            this.gb_DB_State.SuspendLayout();
            this.tc_fortnitewm.SuspendLayout();
            this.tp_Database_Infos.SuspendLayout();
            this.gb_TB_State.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_DB_State
            // 
            this.gb_DB_State.Controls.Add(this.btn_TestCon);
            this.gb_DB_State.Controls.Add(this.tb_DB_PW);
            this.gb_DB_State.Controls.Add(this.lb_DB_PW);
            this.gb_DB_State.Controls.Add(this.tb_DB_UID);
            this.gb_DB_State.Controls.Add(this.lb_DB_UID);
            this.gb_DB_State.Controls.Add(this.lb_DatabaseValue);
            this.gb_DB_State.Controls.Add(this.lb_Database);
            this.gb_DB_State.Controls.Add(this.lb_ConnectionValue);
            this.gb_DB_State.Controls.Add(this.lb_Connection);
            this.gb_DB_State.Location = new System.Drawing.Point(6, 6);
            this.gb_DB_State.Name = "gb_DB_State";
            this.gb_DB_State.Size = new System.Drawing.Size(200, 142);
            this.gb_DB_State.TabIndex = 0;
            this.gb_DB_State.TabStop = false;
            this.gb_DB_State.Text = "Datenbank Status";
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
            // tb_DB_PW
            // 
            this.tb_DB_PW.Location = new System.Drawing.Point(94, 85);
            this.tb_DB_PW.Name = "tb_DB_PW";
            this.tb_DB_PW.PasswordChar = '*';
            this.tb_DB_PW.Size = new System.Drawing.Size(100, 20);
            this.tb_DB_PW.TabIndex = 9;
            this.tb_DB_PW.Text = "123";
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
            this.tp_Database_Infos.Controls.Add(this.gb_TB_State);
            this.tp_Database_Infos.Controls.Add(this.gb_DB_State);
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
            this.tb_insert.Size = new System.Drawing.Size(245, 307);
            this.tb_insert.TabIndex = 1;
            this.tb_insert.Text = "Dateneingaben";
            this.tb_insert.UseVisualStyleBackColor = true;
            // 
            // tb_Select
            // 
            this.tb_Select.Location = new System.Drawing.Point(4, 22);
            this.tb_Select.Name = "tb_Select";
            this.tb_Select.Padding = new System.Windows.Forms.Padding(3);
            this.tb_Select.Size = new System.Drawing.Size(245, 307);
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
            // gb_TB_State
            // 
            this.gb_TB_State.Controls.Add(this.lb_TB_MapsValue);
            this.gb_TB_State.Controls.Add(this.lb_TB_ModesValue);
            this.gb_TB_State.Controls.Add(this.lb_TB_PlayersValue);
            this.gb_TB_State.Controls.Add(this.lb_TB_TeamsValue);
            this.gb_TB_State.Controls.Add(this.lb_TB_PMValue);
            this.gb_TB_State.Controls.Add(this.lb_TB_PM);
            this.gb_TB_State.Controls.Add(this.lb_TB_Teams);
            this.gb_TB_State.Controls.Add(this.lb_TB_Players);
            this.gb_TB_State.Controls.Add(this.lb_TB_Modes);
            this.gb_TB_State.Controls.Add(this.lb_TB_Maps);
            this.gb_TB_State.Location = new System.Drawing.Point(6, 154);
            this.gb_TB_State.Name = "gb_TB_State";
            this.gb_TB_State.Size = new System.Drawing.Size(200, 91);
            this.gb_TB_State.TabIndex = 1;
            this.gb_TB_State.TabStop = false;
            this.gb_TB_State.Text = "Tabellen Status";
            // 
            // lb_TB_Maps
            // 
            this.lb_TB_Maps.AutoSize = true;
            this.lb_TB_Maps.Location = new System.Drawing.Point(15, 20);
            this.lb_TB_Maps.Name = "lb_TB_Maps";
            this.lb_TB_Maps.Size = new System.Drawing.Size(33, 13);
            this.lb_TB_Maps.TabIndex = 0;
            this.lb_TB_Maps.Text = "Maps";
            // 
            // lb_TB_Modes
            // 
            this.lb_TB_Modes.AutoSize = true;
            this.lb_TB_Modes.Location = new System.Drawing.Point(15, 33);
            this.lb_TB_Modes.Name = "lb_TB_Modes";
            this.lb_TB_Modes.Size = new System.Drawing.Size(39, 13);
            this.lb_TB_Modes.TabIndex = 1;
            this.lb_TB_Modes.Text = "Modes";
            // 
            // lb_TB_Players
            // 
            this.lb_TB_Players.AutoSize = true;
            this.lb_TB_Players.Location = new System.Drawing.Point(15, 46);
            this.lb_TB_Players.Name = "lb_TB_Players";
            this.lb_TB_Players.Size = new System.Drawing.Size(41, 13);
            this.lb_TB_Players.TabIndex = 2;
            this.lb_TB_Players.Text = "Players";
            // 
            // lb_TB_Teams
            // 
            this.lb_TB_Teams.AutoSize = true;
            this.lb_TB_Teams.Location = new System.Drawing.Point(15, 59);
            this.lb_TB_Teams.Name = "lb_TB_Teams";
            this.lb_TB_Teams.Size = new System.Drawing.Size(39, 13);
            this.lb_TB_Teams.TabIndex = 3;
            this.lb_TB_Teams.Text = "Teams";
            // 
            // lb_TB_PM
            // 
            this.lb_TB_PM.AutoSize = true;
            this.lb_TB_PM.Location = new System.Drawing.Point(15, 72);
            this.lb_TB_PM.Name = "lb_TB_PM";
            this.lb_TB_PM.Size = new System.Drawing.Size(83, 13);
            this.lb_TB_PM.TabIndex = 4;
            this.lb_TB_PM.Text = "Played Matches";
            // 
            // lb_TB_PMValue
            // 
            this.lb_TB_PMValue.AutoSize = true;
            this.lb_TB_PMValue.Location = new System.Drawing.Point(104, 72);
            this.lb_TB_PMValue.Name = "lb_TB_PMValue";
            this.lb_TB_PMValue.Size = new System.Drawing.Size(60, 13);
            this.lb_TB_PMValue.TabIndex = 5;
            this.lb_TB_PMValue.Text = "Unbekannt";
            // 
            // lb_TB_TeamsValue
            // 
            this.lb_TB_TeamsValue.AutoSize = true;
            this.lb_TB_TeamsValue.Location = new System.Drawing.Point(104, 59);
            this.lb_TB_TeamsValue.Name = "lb_TB_TeamsValue";
            this.lb_TB_TeamsValue.Size = new System.Drawing.Size(60, 13);
            this.lb_TB_TeamsValue.TabIndex = 6;
            this.lb_TB_TeamsValue.Text = "Unbekannt";
            // 
            // lb_TB_PlayersValue
            // 
            this.lb_TB_PlayersValue.AutoSize = true;
            this.lb_TB_PlayersValue.Location = new System.Drawing.Point(104, 46);
            this.lb_TB_PlayersValue.Name = "lb_TB_PlayersValue";
            this.lb_TB_PlayersValue.Size = new System.Drawing.Size(60, 13);
            this.lb_TB_PlayersValue.TabIndex = 7;
            this.lb_TB_PlayersValue.Text = "Unbekannt";
            // 
            // lb_TB_ModesValue
            // 
            this.lb_TB_ModesValue.AutoSize = true;
            this.lb_TB_ModesValue.Location = new System.Drawing.Point(104, 33);
            this.lb_TB_ModesValue.Name = "lb_TB_ModesValue";
            this.lb_TB_ModesValue.Size = new System.Drawing.Size(60, 13);
            this.lb_TB_ModesValue.TabIndex = 8;
            this.lb_TB_ModesValue.Text = "Unbekannt";
            // 
            // lb_TB_MapsValue
            // 
            this.lb_TB_MapsValue.AutoSize = true;
            this.lb_TB_MapsValue.Location = new System.Drawing.Point(104, 20);
            this.lb_TB_MapsValue.Name = "lb_TB_MapsValue";
            this.lb_TB_MapsValue.Size = new System.Drawing.Size(60, 13);
            this.lb_TB_MapsValue.TabIndex = 9;
            this.lb_TB_MapsValue.Text = "Unbekannt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tc_fortnitewm);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gb_DB_State.ResumeLayout(false);
            this.gb_DB_State.PerformLayout();
            this.tc_fortnitewm.ResumeLayout(false);
            this.tp_Database_Infos.ResumeLayout(false);
            this.gb_TB_State.ResumeLayout(false);
            this.gb_TB_State.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_DB_State;
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
        private System.Windows.Forms.GroupBox gb_TB_State;
        private System.Windows.Forms.Label lb_TB_MapsValue;
        private System.Windows.Forms.Label lb_TB_ModesValue;
        private System.Windows.Forms.Label lb_TB_PlayersValue;
        private System.Windows.Forms.Label lb_TB_TeamsValue;
        private System.Windows.Forms.Label lb_TB_PMValue;
        private System.Windows.Forms.Label lb_TB_PM;
        private System.Windows.Forms.Label lb_TB_Teams;
        private System.Windows.Forms.Label lb_TB_Players;
        private System.Windows.Forms.Label lb_TB_Modes;
        private System.Windows.Forms.Label lb_TB_Maps;
    }
}

