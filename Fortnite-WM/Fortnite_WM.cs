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
        /*
         * Größe des Fensters 507; 489
         * Position der GroupBoxen 6,55
         * Tabfenster Größe 467; 426
         * */
        #region Variablen Deklaration
        DBcon dbcon = new DBcon();
        private bool dbConState = false;
        Dictionary<string, string> words = new Dictionary<string, string>();
        Dictionary<int, string> insertValues = new Dictionary<int, string>();
        Dictionary<string, string> vals = new Dictionary<string, string>();
        #endregion
        public Fortnite_WM()
        {
            InitializeComponent();
            Fortnite_WMInit();
        }
        public void Fortnite_WMInit()
        {
            WordsInit();
            LabelResetter();
            insertValues.Clear();
            vals.Clear();
            gb_Players.Visible = false;
            gb_Modes.Visible = false;
            gb_Maps.Visible = false;
            gb_Teams.Visible = false;
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
                return dbcon.DBConState(); 
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
                dbcon.PropDatabase = "fortnite_wm";
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
        private void Btn_ConnectRefresh_Click(object sender, EventArgs e)
        {
            if (DBConState())
            {
                ComboFiller();
            }
        }
        private void Btn_RestoreDB_Click(object sender, EventArgs e)
        {
            dbcon.DBCreate();
            btn_RestoreDB.Visible = false;
        }
        private void Btn_SaveCred_Click(object sender, EventArgs e)
        {
            dbcon.PropUid = tb_DB_UID.Text;
            dbcon.PropPassword = tb_DB_PW.Text;
            Fortnite_WMInit();
            MessageBox.Show("Daten wurden gespeichert.");
        }
        private void Btn_Insert_Click(object sender, EventArgs e)
        {
            if (dbConState)
            {
                switch (cb_Insert_Table.SelectedIndex)
                {
                    case 0:
                        MessageBox.Show("Bitte wählen sie eine Tabelle aus.");
                        break;
                    case 1:
                        vals.Add("Tabelle", "maps");
                        if (rb_Maps_Large.Checked) insertValues.Add(0, "1");
                        if (rb_Maps_Middle.Checked) insertValues.Add(0, "2");
                        if (rb_Maps_Small.Checked) insertValues.Add(0, "4");
                        if (!words.ContainsValue(tb_Map_Name.Text))
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
                        vals.Add("Tabelle", "player");
                        if (tb_Player_Nickname.Text != words[tb_Player_Nickname.Name] &&  tb_Player_Mail.Text != words[tb_Player_Mail.Name] &&
                            tb_Player_Age.Text != words[tb_Player_Age.Name] && tb_Player_Firstname.Text != words[tb_Player_Firstname.Name] && 
                            tb_Player_Familyname.Text != words[tb_Player_Familyname.Name])
                        {
                            vals.Add(tb_Player_Nickname.Name, tb_Player_Nickname.Text);
                            vals.Add(tb_Player_Age.Name, tb_Player_Age.Text);
                            vals.Add(tb_Player_Firstname.Name, tb_Player_Firstname.Text);
                            vals.Add(tb_Player_Familyname.Name, tb_Player_Familyname.Text);
                            vals.Add(tb_Player_Mail.Name, tb_Player_Mail.Text);
                            vals.Add(cb_Player_Team_ID.Name, cb_Player_Team_ID.SelectedValue.ToString());
                            if (tb_Player_Country.Text != words[tb_Player_Country.Name]) { vals.Add(tb_Player_Country.Name, tb_Player_Country.Text); } else { vals.Add(tb_Player_Country.Name, "NULL"); }
                            if (tb_Player_Postalcode.Text != words[tb_Player_Postalcode.Name]) { vals.Add(tb_Player_Postalcode.Name, tb_Player_Postalcode.Text); } else { vals.Add(tb_Player_Postalcode.Name, "NULL"); }
                            if (tb_Player_State.Text != words[tb_Player_State.Name]) { vals.Add(tb_Player_State.Name, tb_Player_State.Text); } else { vals.Add(tb_Player_State.Name, "NULL"); }
                            if (tb_Player_City.Text != words[tb_Player_City.Name]) { vals.Add(tb_Player_City.Name, tb_Player_City.Text); } else { vals.Add(tb_Player_City.Name, "NULL"); }
                            if (tb_Player_Streetnr.Text != words[tb_Player_Streetnr.Name]) { vals.Add(tb_Player_Streetnr.Name, tb_Player_Streetnr.Text); } else { vals.Add(tb_Player_Streetnr.Name, "NULL"); }
                            if (tb_Player_Street.Text != words[tb_Player_Street.Name]) { vals.Add(tb_Player_Street.Name, tb_Player_Street.Text); } else { vals.Add(tb_Player_Street.Name, "NULL"); }
                            if (tb_Player_Phonenumber.Text != words[tb_Player_Phonenumber.Name]) { vals.Add(tb_Player_Phonenumber.Name, tb_Player_Phonenumber.Text); } else { vals.Add(tb_Player_Phonenumber.Name, "NULL"); }
                            dbcon.DBInsert(vals);
                            vals.Clear();
                            MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                        }
                        else
                        {
                            MessageBox.Show("Bitte befülle alle mit * markierten Felder.");
                        }
                        break;
                    case 5:


                        break;
                    case 6:
                        if (tb_Teams_Name.Text != words[tb_Teams_Name.Name] && tb_Teams_City.Text != words[tb_Teams_City.Name] &&
                            tb_Teams_Country.Text != words[tb_Teams_Country.Name] && tb_Teams_Mail.Text != words[tb_Teams_Mail.Name] &&
                             tb_Teams_Postalcode.Text != words[tb_Teams_Postalcode.Name] && tb_Teams_Streetnr.Text != words[tb_Teams_Streetnr.Name] &&
                            tb_Teams_State.Text != words[tb_Teams_State.Name] && tb_Teams_Street.Text != words[tb_Teams_Street.Name])
                        {
                            vals.Add(tb_Teams_Name.Name, tb_Teams_Name.Text);
                            vals.Add(tb_Teams_Country.Name, tb_Teams_Country.Text);
                            vals.Add(tb_Teams_Postalcode.Name, tb_Teams_Postalcode.Text);
                            vals.Add(tb_Teams_State.Name, tb_Teams_State.Text);
                            vals.Add(tb_Teams_City.Name, tb_Teams_City.Text);
                            vals.Add(tb_Teams_Mail.Name, tb_Teams_Mail.Text);
                            vals.Add(tb_Teams_Streetnr.Name, tb_Teams_Streetnr.Text);
                            vals.Add(tb_Teams_Street.Name, tb_Teams_Street.Text);
                            if (tb_Description.Text != words[tb_Description.Name]) { vals.Add(tb_Description.Name, tb_Description.Text); } else { vals.Add(tb_Description.Name, null); }
                            dbcon.DBInsert(vals);
                            vals.Clear();
                            MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                        }
                        else
                        {
                            MessageBox.Show("Bitte befülle alle mit * markierten Felder.");
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Bitte verbinde dich mit der Datenbank um Daten eintragen zu können.");
            }
        }
        #endregion
        #region Textbox Enter / Leave behaviour
        private void Tb_Enter(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();
            if (sender is TextBox)
            {
                tb = (TextBox)sender;
                if (words.ContainsValue(tb.Text.ToString()))
                {
                    tb.Text = String.Empty;
                    tb.ForeColor = Color.Black;
                }
            }
            if (tb.Name == "tb_Player_Age") 
            {
                mc_Age.Show();
            }
            else
            {
                mc_Age.Hide();
            }
        }
        private void Tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();
            if (sender is TextBox)
            {
                tb = (TextBox)sender;
                if (tb.Name == tb_Player_Mail.Name || tb.Name == tb_Player_City.Name || tb.Name == tb_Player_Age.Name ||
                    tb.Name == tb_Player_Street.Name || tb.Name == tb_Player_State.Name)
                {
                    tb.Text = tb.Text;
                }
                else if (tb.Name == tb_Teams_Mail.Name || tb.Name == tb_Teams_City.Name || tb.Name == tb_Teams_Street.Name ||
                     tb.Name == tb_Teams_State.Name)
                {
                    tb.Text = tb.Text;
                }
                else if (tb.Name == tb_DB_UID.Name || tb.Name == tb_DB_PW.Name || tb.Name == tb_Description.Name)
                {
                    tb.Text = tb.Text;
                }
                else
                {
                    tb.Text = string.Concat(tb.Text.Where(char.IsLetterOrDigit));
                }
                if (tb.Name == tb_Teams_Postalcode.Name || tb.Name == tb_Teams_Streetnr.Name)
                {
                    tb.Text = string.Concat(tb.Text.Where(char.IsDigit));
                }
                if (tb.Name == tb_Player_Postalcode.Name || tb.Name == tb_Player_Streetnr.Name || tb.Name == tb_Player_Phonenumber.Name)
                {
                    tb.Text = string.Concat(tb.Text.Where(char.IsDigit));
                }
                if (tb.Text.Trim().Length == 0)
                {
                    tb.Text = words[tb.Name];
                    tb.ForeColor = Color.Gray;
                }
            }
        }
        #endregion
        private void MC_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            tb_Player_Age.Text = ConvertToDateTime(e.Start.ToShortDateString()).Year.ToString() + "-" + ConvertToDateTime(e.Start.ToShortDateString()).Month.ToString() + "-" + ConvertToDateTime(e.Start.ToShortDateString()).Day.ToString();
            tb_Player_Age.ForeColor = Color.Black;
        }
        private DateTime ConvertToDateTime(string value)
        {
            try
            {
                return Convert.ToDateTime(value);
                //Console.WriteLine("'{0}' converts to {1} {2} time.", value, convertedDate, convertedDate.Kind.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine("'{0}' is not in the proper format.", value);
                return Convert.ToDateTime(value);
            }
        }
        #region Hintergrund Methoden
        private void WordsInit()
        {
            words.Clear();
            words.Add(tb_Map_Name.Name, "Name*");
            tb_Map_Name.Text = words[tb_Map_Name.Name];
            tb_Map_Name.ForeColor = Color.Gray;
            words.Add(tb_Mode_Name.Name, "Name*");
            tb_Mode_Name.Text = words[tb_Mode_Name.Name];
            tb_Mode_Name.ForeColor = Color.Gray;
            words.Add(tb_Player_Firstname.Name, "Vorname*");
            tb_Player_Firstname.Text = words[tb_Player_Firstname.Name];
            tb_Player_Firstname.ForeColor = Color.Gray;
            words.Add(tb_Player_Age.Name, "Alter*");
            tb_Player_Age.Text = words[tb_Player_Age.Name];
            tb_Player_Age.ForeColor = Color.Gray;
            words.Add(tb_Player_Street.Name, "Straße");
            tb_Player_Street.Text = words[tb_Player_Street.Name];
            tb_Player_Street.ForeColor = Color.Gray;
            words.Add(tb_Teams_Street.Name, "Straße*");
            tb_Teams_Street.Text = words[tb_Teams_Street.Name];
            tb_Teams_Street.ForeColor = Color.Gray;
            words.Add(tb_Player_State.Name, "Bundesland");
            tb_Player_State.Text = words[tb_Player_State.Name];
            tb_Player_State.ForeColor = Color.Gray;
            words.Add(tb_Teams_State.Name, "Bundesland*");
            tb_Teams_State.Text = words[tb_Teams_State.Name];
            tb_Teams_State.ForeColor = Color.Gray;
            words.Add(tb_Player_City.Name, "Stadt");
            tb_Player_City.Text = words[tb_Player_City.Name];
            tb_Player_City.ForeColor = Color.Gray;
            words.Add(tb_Teams_City.Name, "Stadt*");
            tb_Teams_City.Text = words[tb_Teams_City.Name];
            tb_Teams_City.ForeColor = Color.Gray;
            words.Add(tb_Player_Streetnr.Name, "Hausnummer");
            tb_Player_Streetnr.Text = words[tb_Player_Streetnr.Name];
            tb_Player_Streetnr.ForeColor = Color.Gray;
            words.Add(tb_Teams_Streetnr.Name, "Hausnummer*");
            tb_Teams_Streetnr.Text = words[tb_Teams_Streetnr.Name];
            tb_Teams_Streetnr.ForeColor = Color.Gray;
            words.Add(tb_Player_Phonenumber.Name, "Rufnummer");
            tb_Player_Phonenumber.Text = words[tb_Player_Phonenumber.Name];
            tb_Player_Phonenumber.ForeColor = Color.Gray;
            words.Add(tb_Player_Mail.Name, "E-Mail*");
            tb_Player_Mail.Text = words[tb_Player_Mail.Name];
            tb_Player_Mail.ForeColor = Color.Gray;
            words.Add(tb_Teams_Mail.Name, "E-Mail*");
            tb_Teams_Mail.Text = words[tb_Teams_Mail.Name];
            tb_Teams_Mail.ForeColor = Color.Gray;
            words.Add(tb_Teams_Name.Name, "Name*");
            tb_Teams_Name.Text = words[tb_Teams_Name.Name];
            tb_Teams_Name.ForeColor = Color.Gray;
            words.Add(tb_Player_Postalcode.Name, "Postleitzahl");
            tb_Player_Postalcode.Text = words[tb_Player_Postalcode.Name];
            tb_Player_Postalcode.ForeColor = Color.Gray;
            words.Add(tb_Teams_Postalcode.Name, "Postleitzahl*");
            tb_Teams_Postalcode.Text = words[tb_Teams_Postalcode.Name];
            tb_Teams_Postalcode.ForeColor = Color.Gray;
            words.Add(tb_Player_Country.Name, "Land");
            tb_Player_Country.Text = words[tb_Player_Country.Name];
            tb_Player_Country.ForeColor = Color.Gray;
            words.Add(tb_Teams_Country.Name, "Land*");
            tb_Teams_Country.Text = words[tb_Teams_Country.Name];
            tb_Teams_Country.ForeColor = Color.Gray;
            words.Add(tb_Player_Nickname.Name, "Nickname*");
            tb_Player_Nickname.Text = words[tb_Player_Nickname.Name];
            tb_Player_Nickname.ForeColor = Color.Gray;
            words.Add(tb_Player_Familyname.Name, "Nachname*");
            tb_Player_Familyname.Text = words[tb_Player_Familyname.Name];
            tb_Player_Familyname.ForeColor = Color.Gray;
            words.Add("Max.P", "Max. Spieler");
            words.Add(tb_Description.Name, "Beschreibung");
            tb_Description.Text = words[tb_Description.Name];
            tb_Description.ForeColor = Color.Gray;
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
            cb_Insert_Table.DataSource = dbcon.ComboData(0);
            cb_Insert_Table.DisplayMember = "TABLE_NAME";
            cb_Insert_Table.ValueMember = "TABLE_NAME";

            cb_Player_Team_ID.DataSource = dbcon.ComboData(1);
            cb_Player_Team_ID.DisplayMember = "team_Name";
            cb_Player_Team_ID.ValueMember = "team_id";
        }
        #endregion
        private void Cb_Insert_Table_TextChanged(object sender, EventArgs e)
        {
            switch (cb_Insert_Table.SelectedIndex)
            {
                case 0:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    WordsInit();
                    break;
                case 1:
                    WordsInit();
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = true;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 2:
                    WordsInit();
                    gb_Modes.Visible = true;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 3:
                    WordsInit();
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 4:
                    WordsInit();
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = true;
                    gb_Teams.Visible = false;
                    break;
                case 5:
                    WordsInit();
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    break;
                case 6:
                    WordsInit();
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

        private void Btn_reset_Click(object sender, EventArgs e)
        {
            WordsInit();
            insertValues.Clear();
            vals.Clear();
        }
    }
}
