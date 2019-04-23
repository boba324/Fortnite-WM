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
    public partial class Fortnite_WM : Form
    {
        #region Variablen Deklaration
        DBcon dbcon = new DBcon();
        private bool dbConState = false;
        Dictionary<int, string> backgroundWords = new Dictionary<int, string>();
        Dictionary<int, string> insertValues = new Dictionary<int, string>();
        #endregion
        public Fortnite_WM()
        {
            InitializeComponent();
            Fortnite_WMInit();
        }
        public void Fortnite_WMInit()
        {
            BackgroundWordsInit();
            LabelResetter();
            insertValues.Clear();
            gb_Players.Visible = false;
        }
        #region DB Status Abfrage / Erstellung
        private bool DBConState()
        {
            if (dbcon.DBConState())
            {
                lb_ConnectionValue.Text = "verbunden";
                lb_ConnectionValue.ForeColor = Color.Lime;
                DBexist();
                dbConState = true;
                return true; 
            }
            else
            {
                lb_ConnectionValue.Text = "nicht verbunden";
                lb_ConnectionValue.ForeColor = Color.Red;
                dbConState = false;
                return false;
            }

        }
        private void DBexist()
        {
            if (dbcon.DBexist() > 0)
            {
                lb_DatabaseValue.Text = "existiert";
                lb_DatabaseValue.ForeColor = Color.Lime;
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
                if (tbs["maps"] != 0) { lb_TB_MapsValue.Text = "existiert"; lb_TB_MapsValue.ForeColor = Color.Lime; } else { lb_TB_MapsValue.Text = "existiert nicht"; }
                if (tbs["modes"] != 0) { lb_TB_ModesValue.Text = "existiert"; lb_TB_ModesValue.ForeColor = Color.Lime; } else { lb_TB_ModesValue.Text = "existiert nicht"; }
                if (tbs["player"] != 0) { lb_TB_PlayersValue.Text = "existiert"; lb_TB_PlayersValue.ForeColor = Color.Lime; } else { lb_TB_PlayersValue.Text = "existiert nicht"; }
                if (tbs["played_matches"] != 0) { lb_TB_PMValue.Text = "existiert"; lb_TB_PMValue.ForeColor = Color.Lime; } else { lb_TB_PMValue.Text = "existiert nicht"; }
                if (tbs["teams"] != 0) { lb_TB_TeamsValue.Text = "existiert"; lb_TB_TeamsValue.ForeColor = Color.Lime; } else { lb_TB_TeamsValue.Text = "existiert nicht"; }
                if (tbs.ContainsValue(0))
                {
                    btn_RestoreDB.Visible = true;
                }
                else
                {
                    MessageBox.Show("Die Datenbankstruktur ist auf dem aktuellsten Stand.");
                }
            }
            else
            {
                lb_TB_MapsValue.Text = "unbekannt";
                lb_TB_ModesValue.Text = "unbekannt";
                lb_TB_PlayersValue.Text = "unbekannt";
                lb_TB_PMValue.Text = "unbekannt";
                lb_TB_TeamsValue.Text = "unbekannt";
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
        #endregion
        #region Button Events
        private void btn_ConnectRefresh_Click(object sender, EventArgs e)
        {
            if (DBConState())
            {
                ComboFiller();
            }
        }
        private void btn_RestoreDB_Click(object sender, EventArgs e)
        {
            dbcon.DBCreate();
            btn_RestoreDB.Visible = false;
        }
        private void btn_SaveCred_Click(object sender, EventArgs e)
        {
            dbcon.propUid = tb_DB_UID.Text;
            dbcon.propPassword = tb_DB_PW.Text;
            Fortnite_WMInit();
            MessageBox.Show("Daten wurden gespeichert.");
        }
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            switch (cb_Insert_Table.SelectedIndex)
            {
                case 0:
                    MessageBox.Show("Bitte wählen sie eine Tabelle aus.");
                    break;
                case 1:
                    if (rb_Maps_Large.Checked) insertValues.Add(0, "1");
                    if (rb_Maps_Middle.Checked) insertValues.Add(0, "2");
                    if (rb_Maps_Small.Checked) insertValues.Add(0, "4");
                    if (!backgroundWords.ContainsValue(tb_Map_Name.Text))
                    {
                        insertValues.Add(1, tb_Map_Name.Text);
                        dbcon.Insert(insertValues);
                    }
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                    

                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Textbox Enter / Leave behaviour
        private void tb_Map_Name_Enter(object sender, EventArgs e)
        {
            if (backgroundWords.ContainsValue(tb_Map_Name.Text.ToString()))
            {
                tb_Map_Name.Text = String.Empty;
                tb_Map_Name.ForeColor = Color.Black;
            }
        }
        private void tb_Map_Name_Leave(object sender, EventArgs e)
        {
            if (tb_Map_Name.Text.Trim().Length == 0) { 
                tb_Map_Name.Text = backgroundWords[1];
                tb_Map_Name.ForeColor = Color.Gray;
            }
        }
        #endregion
        #region Hintergrund Methoden
        private void BackgroundWordsInit()
        {
            backgroundWords.Clear();
            backgroundWords.Add(1, "Name");
            backgroundWords.Add(2, "Vorname");
            backgroundWords.Add(3, "Alter");
            backgroundWords.Add(4, "Straße");
            backgroundWords.Add(5, "Bundesland");
            backgroundWords.Add(6, "Stadt");
            backgroundWords.Add(7, "Hausnummer");
            backgroundWords.Add(8, "Rufnummer");
            backgroundWords.Add(9, "E-Mail");
            backgroundWords.Add(10, "Team Name");
            backgroundWords.Add(11, "Postleitzahl");
            backgroundWords.Add(12, "Land");
            backgroundWords.Add(13, "Nickname");
            backgroundWords.Add(14, "Nachname");
            backgroundWords.Add(15, "Max. Spieler");
            backgroundWords.Add(16, "Beschreibung");
        }
        private void LabelResetter()
        {
            lb_ConnectionValue.Text = "unbekannt";
            lb_ConnectionValue.ForeColor = Color.Black;
            lb_DatabaseValue.Text = "unbekannt";
            lb_DatabaseValue.ForeColor = Color.Black;
            lb_TB_MapsValue.Text = "unbekannt";
            lb_TB_MapsValue.ForeColor = Color.Black;
            lb_TB_ModesValue.Text = "unbekannt";
            lb_TB_ModesValue.ForeColor = Color.Black;
            lb_TB_PlayersValue.Text = "unbekannt";
            lb_TB_PlayersValue.ForeColor = Color.Black;
            lb_TB_PMValue.Text = "unbekannt";
            lb_TB_PMValue.ForeColor = Color.Black;
            lb_TB_TeamsValue.Text = "unbekannt";
            lb_TB_TeamsValue.ForeColor = Color.Black;
        }
        private void ComboFiller()
        {
            cb_Insert_Table.DataSource = dbcon.ComboData();
            cb_Insert_Table.DisplayMember = "TABLE_NAME";
            cb_Insert_Table.ValueMember = "TABLE_NAME";
        }
        #endregion
        private void cb_Insert_Table_TextChanged(object sender, EventArgs e)
        {
            switch (cb_Insert_Table.SelectedIndex)
            {
                case 0:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 1:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = true;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 2:
                    gb_Modes.Visible = true;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 3:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 4:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = true;
                    gb_Teams.Visible = false;
                    break;
                case 5:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = true;

                    break;
                default:
                    MessageBox.Show("Option mit der Indexnummer " + cb_Insert_Table.SelectedIndex + " nicht bekannt.");
                    break;
            }
        }
    }
}
