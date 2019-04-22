using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fortnite_WM
{
    public partial class Form1 : Form
    {
        DBcon dbcon = new DBcon();
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_SaveCred_Click(object sender, EventArgs e)
        {
            dbcon.propUid = tb_DB_UID.Text;
            dbcon.propPassword = tb_DB_PW.Text;
            MessageBox.Show("Daten wurden gespeichert.");
        }
        private void DBConState()
        {
            if (dbcon.DBConState())
            {
                lb_ConnectionValue.Text = "verbunden";
                lb_ConnectionValue.ForeColor = Color.Green;
                DBexist();//select * from maps limit 0;   
            }
            else
            {
                lb_ConnectionValue.Text = "nicht verbunden";
                lb_ConnectionValue.ForeColor = Color.Red;
                return;
            }

        }
        private void DBexist()
        {
            if (dbcon.DBexist() > 0)
            {
                lb_DatabaseValue.Text = "existiert";
                lb_DatabaseValue.ForeColor = Color.Green;
                dbcon.propDatabase = "fortnite_wm";
                this.TBexist(true);
            }
            else
            {
                lb_DatabaseValue.Text = "existiert nicht";
                lb_DatabaseValue.ForeColor = Color.Red;
                this.DBCreate();
                this.TBexist(false);
            }
        }
        private void TBexist(bool dbex)
        {
            if (dbex)
            {
                Dictionary<string, int> tbs = dbcon.TBexist();
                if (tbs["maps"] != 0) { lb_TB_MapsValue.Text = "existiert"; } else { lb_TB_MapsValue.Text = "existiert nicht"; }
                if (tbs["modes"] != 0) { lb_TB_ModesValue.Text = "existiert"; } else { lb_TB_ModesValue.Text = "existiert nicht"; }
                if (tbs["player"] != 0) { lb_TB_PlayersValue.Text = "existiert"; } else { lb_TB_PlayersValue.Text = "existiert nicht"; }
                if (tbs["played_matches"] != 0) { lb_TB_PMValue.Text = "existiert"; } else { lb_TB_PMValue.Text = "existiert nicht"; }
                if (tbs["teams"] != 0) { lb_TB_TeamsValue.Text = "existiert"; }else { lb_TB_TeamsValue.Text = "existiert nicht"; }
                if (tbs.ContainsValue(0))
                {
                    btn_RestoreDB.Visible = true;
                }
                else
                {
                    MessageBox.Show("Die Datenbankstruktur ist auf dem aktuellsten Stand.U");
                }
            }
            else
            {
                lb_TB_MapsValue.Text = "existiert nicht";
                lb_TB_ModesValue.Text = "existiert nicht";
                lb_TB_PlayersValue.Text = "existiert nicht";
                lb_TB_PMValue.Text = "existiert nicht";
                lb_TB_TeamsValue.Text = "existiert nicht";
            }
        }
        private void DBCreate()
        {
            DialogResult dialogResult = MessageBox.Show("Soll die Datenbank und alle dazu gehörigen Tabellen erstellt werden.", "Datenbank Erstellung", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dbcon.DBCreate();
                this.DBexist();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Es wurde keine Datenbank erstellt.");
            }
        }
        private void btn_ConnectRefresh_Click(object sender, EventArgs e)
        {
            DBConState();
        }
        private void btn_RestoreDB_Click(object sender, EventArgs e)
        {
            dbcon.DBCreate();
            btn_RestoreDB.Visible = false;
        }
    }
}
