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

        private void btn_TestCon_Click(object sender, EventArgs e)
        {
            dbcon.propUid = tb_DB_UID.Text;
            dbcon.propPassword = tb_DB_PW.Text;
            DBConState();
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
                Dictionary<string, string> tbs = dbcon.TBexist();
                if (tbs["maps"] != "") lb_TB_MapsValue.Text = "existiert";
                if (tbs["modes"] != "") lb_TB_ModesValue.Text = "existiert";
                if (tbs["players"] != "") lb_TB_PlayersValue.Text = "existiert";
                if (tbs["played_matches"] != "") lb_TB_PMValue.Text = "existiert";
                if (tbs["teams"] != "") lb_TB_TeamsValue.Text = "existiert";
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
    }
}
