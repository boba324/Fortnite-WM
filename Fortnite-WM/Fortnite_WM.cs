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
        Dictionary<string, string> vals = new Dictionary<string, string>();
        private string player_age = "";
        private int modes_weapontype = 0;//255
        private int modes_type = 0;//7
        private int modes_rarity = 0;//63
        private bool resett = true;
        private string table = "";
        DataTable ranking = new DataTable();
        DataTable ranking_second = new DataTable();
        DataTable ranking_third = new DataTable();
        #endregion
        #region Init
        public Fortnite_WM()
        {
            InitializeComponent();
            Fortnite_WMInit();
        }
        public void Fortnite_WMInit()
        {
            WordsInit();
            LabelResetter();
            vals.Clear();
            mc_Age.SetDate(mc_Age.TodayDate.AddYears(-18));
            gb_Modes.Visible = false;
            gb_Maps.Visible = false;
            gb_Players.Visible = false;
            gb_Teams.Visible = false;
            gb_Played_Matches.Visible = false;
            gb_Scores.Visible = false;
        }
        #endregion
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
                        #region Maps
                        vals.Add("Tabelle", "maps");
                        if (rb_Maps_Large.Checked) vals.Add("map_type", "4");
                        if (rb_Maps_Middle.Checked) vals.Add("map_type", "2");
                        if (rb_Maps_Small.Checked) vals.Add("map_type", "1");
                        if (!words.ContainsValue(tb_Map_Name.Text) && rb_Maps_Small.Checked || rb_Maps_Middle.Checked || rb_Maps_Large.Checked)
                        {
                            vals.Add(tb_Map_Name.Name, tb_Map_Name.Text);
                            dbcon.DBInsert(vals);
                            MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                            WordsInit();
                            LabelResetter();
                            ComboFiller();
                        }
                        else
                        {
                            MessageBox.Show("Bitte befülle alle mit * markierten Felder.");
                        }
                        vals.Clear();
                        #endregion
                        break;
                    case 2:
                        #region Modes
                        vals.Add("Tabelle", "modes");
                        if (cb_Modes_Weapontype_Pistol.Checked) modes_weapontype += 1;
                        if (cb_Modes_Weapontype_Schotgun.Checked) modes_weapontype += 2;
                        if (cb_Modes_Weapontype_Submaschinegun.Checked) modes_weapontype += 4;
                        if (cb_Modes_Weapontype_Assultrifle.Checked) modes_weapontype += 8;
                        if (cb_Modes_Weapontype_Rocketlauncher.Checked) modes_weapontype += 16;
                        if (cb_Modes_Weapontype_Granadelauncher.Checked) modes_weapontype += 32;
                        if (cb_Modes_Weapontype_Sniperrifel.Checked) modes_weapontype += 64;
                        if (cb_Modes_Weapontype_Bombs_and_Granates.Checked) modes_weapontype += 128;
                        if (cb_Modes_Type_Solo.Checked) modes_type += 1;
                        if (cb_Modes_Type_Duo.Checked) modes_type += 2;
                        if (cb_Modes_Type_Squad.Checked) modes_type += 4;
                        if (cb_Modes_Rarity_Common.Checked) modes_rarity += 1;
                        if (cb_Modes_Rarity_Uncommon.Checked) modes_rarity += 2;
                        if (cb_Modes_Rarity_Rare.Checked) modes_rarity += 4;
                        if (cb_Modes_Rarity_Epic.Checked) modes_rarity += 8;
                        if (cb_Modes_Rarity_Legendary.Checked) modes_rarity += 16;
                        if (cb_Modes_Rarity_Mythical.Checked) modes_rarity += 32;
                        if (!words.ContainsValue(tb_Mode_Name.Text))
                        {
                            if (modes_rarity == 0) modes_rarity = 63;
                            if (modes_type == 0) modes_type = 7;
                            if (modes_weapontype == 0) modes_weapontype = 255;
                            vals.Add(tb_Mode_Name.Name, tb_Mode_Name.Text);
                            vals.Add(cb_Mode_Map_Name.Name, cb_Mode_Map_Name.SelectedValue.ToString());
                            vals.Add("modes_weapontype", modes_weapontype.ToString());
                            vals.Add("modes_type", modes_type.ToString());
                            vals.Add("modes_rarity", modes_rarity.ToString());
                            vals.Add(nud_Max_Player.Name, nud_Max_Player.Value.ToString());
                            dbcon.DBInsert(vals);
                            MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                            WordsInit();
                            LabelResetter();
                            ComboFiller();
                        }
                        modes_weapontype = 0;
                        modes_type = 0;
                        modes_rarity = 0;
                        vals.Clear();
                        #endregion
                        break;
                    case 3:
                        #region Played Matches
                        vals.Add("Tabelle", "played_matches");
                        if (cb_Played_Matches_Mode_Name.SelectedIndex > -1) vals.Add(cb_Played_Matches_Mode_Name.Name, cb_Played_Matches_Mode_Name.SelectedValue.ToString());
                        if (cb_Played_Matches_Mode_Type.SelectedIndex > -1) vals.Add(cb_Played_Matches_Mode_Type.Name, cb_Played_Matches_Mode_Type.SelectedItem.ToString());
                        if (cb_Played_Matches_First_Place.SelectedIndex > -1) vals.Add(cb_Played_Matches_First_Place.Name, cb_Played_Matches_First_Place.SelectedValue.ToString());
                        if (cb_Played_Matches_Second_Place.SelectedIndex > -1) vals.Add(cb_Played_Matches_Second_Place.Name, cb_Played_Matches_Second_Place.SelectedValue.ToString());
                        if (cb_Played_Matches_Third_Place.SelectedIndex > -1) vals.Add(cb_Played_Matches_Third_Place.Name, cb_Played_Matches_Third_Place.SelectedValue.ToString());
                        vals.Add(nud_Max_Player.Name, nud_Max_Player.Value.ToString());
                        if (cb_Played_Matches_First_Place.SelectedIndex > -1 && cb_Played_Matches_Second_Place.SelectedIndex > -1 && cb_Played_Matches_Third_Place.SelectedIndex > -1)
                        {
                            if (cb_Played_Matches_First_Place.SelectedValue.ToString() != cb_Played_Matches_Second_Place.SelectedValue.ToString() &&
                                cb_Played_Matches_First_Place.SelectedValue.ToString() != cb_Played_Matches_Third_Place.SelectedValue.ToString())
                            {
                                dbcon.DBInsert(vals);
                                MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                                WordsInit();
                                LabelResetter();
                                ComboFiller();
                                cb_Played_Matches_Mode_Type.SelectedIndex = 0;
                                cb_Played_Matches_First_Place.SelectedIndex = 0;
                                cb_Played_Matches_Second_Place.SelectedIndex = 0;
                                cb_Played_Matches_Third_Place.SelectedIndex = 0;
                            }
                            else
                            {
                                MessageBox.Show("Bitte darauf achten das kein Team auf 2 Platzierungen eingetragen ist.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bitte zuerst den Modus und den Modus Typ wählen.");
                        }
                        vals.Clear();
                        #endregion
                        break;
                    case 4:
                        #region Player
                        vals.Add("Tabelle", "player");
                        if (tb_Player_Nickname.Text != words[tb_Player_Nickname.Name] &&  tb_Player_Mail.Text != words[tb_Player_Mail.Name] &&
                            tb_Player_Age.Text != words[tb_Player_Age.Name] && tb_Player_Firstname.Text != words[tb_Player_Firstname.Name] && 
                            tb_Player_Familyname.Text != words[tb_Player_Familyname.Name])
                        {
                            vals.Add(tb_Player_Nickname.Name, tb_Player_Nickname.Text);
                            vals.Add(tb_Player_Age.Name, player_age);
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
                            MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                            WordsInit();
                            LabelResetter();
                            ComboFiller();
                        }
                        else
                        {
                            MessageBox.Show("Bitte befülle alle mit * markierten Felder.");
                        }
                        vals.Clear();
                        #endregion
                        break;
                    case 5:
                        #region Scores
                        vals.Add("Tabelle", "scores");
                        if (cb_Scores_Team_ID.SelectedIndex > -1 && words[tb_Scores_Points.Name] != tb_Scores_Points.Text.ToString())
                        {
                            vals.Add(cb_Scores_Team_ID.Name ,cb_Scores_Team_ID.SelectedValue.ToString());
                            vals.Add(tb_Scores_Points.Name, tb_Scores_Points.Text.ToString());
                            dbcon.DBInsert(vals);
                            MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                            WordsInit();
                            LabelResetter();
                            ComboFiller();
                        }
                        else
                        {
                            MessageBox.Show("Bitte befülle alle mit * markierten Felder und wähle ein Team aus.");
                        }
                        vals.Clear();
                        #endregion
                        break;
                    case 6:
                        #region Teams
                        vals.Add("Tabelle", "teams");
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
                            MessageBox.Show("Die Daten wurden Erfolgreich in die Datenbank eingetragen.");
                            WordsInit();
                            LabelResetter();
                            ComboFiller();
                        }
                        else
                        {
                            MessageBox.Show("Bitte befülle alle mit * markierten Felder.");
                        }
                        vals.Clear();
                        #endregion
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
        private void Btn_reset_Click(object sender, EventArgs e)
        {
            cb_Played_Matches_Mode_Type.SelectedIndex = 0;
            cb_Played_Matches_First_Place.SelectedIndex = 0;
            cb_Played_Matches_Second_Place.SelectedIndex = 0;
            cb_Played_Matches_Third_Place.SelectedIndex = 0;
            WordsInit();
            vals.Clear();
        }
        #endregion
        #region TextBox Events
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
                if (tb.Name == tb_Teams_Postalcode.Name || tb.Name == tb_Teams_Streetnr.Name || tb.Name == tb_Scores_Points.Name)
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
        #region MonthCalender Events
        private void MC_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            tb_Player_Age.Text = e.Start.ToShortDateString();
            player_age = ConvertToDateTime(e.Start.ToShortDateString()).Year.ToString() + "-" + ConvertToDateTime(e.Start.ToShortDateString()).Month.ToString() + "-" + ConvertToDateTime(e.Start.ToShortDateString()).Day.ToString();
            tb_Player_Age.ForeColor = Color.Black;
        }
        #endregion
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
            words.Add(tb_Scores_Points.Name, "Punkte*");
            tb_Scores_Points.Text = words[tb_Scores_Points.Name];
            tb_Scores_Points.ForeColor = Color.Gray;
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
            if (resett)
            {
                cb_Insert_Table.DataSource = dbcon.ComboData(0);
                cb_Insert_Table.DisplayMember = "TABLE_NAME";
                cb_Insert_Table.ValueMember = "TABLE_NAME";
            }
            resett = false;

            cb_Player_Team_ID.DataSource = dbcon.ComboData(1);
            cb_Player_Team_ID.DisplayMember = "team_name";
            cb_Player_Team_ID.ValueMember = "team_id";

            cb_Mode_Map_Name.DataSource = dbcon.ComboData(3);
            cb_Mode_Map_Name.DisplayMember = "map_name";
            cb_Mode_Map_Name.ValueMember = "map_id";

            cb_Played_Matches_Mode_Name.DataSource = dbcon.ComboData(4);
            cb_Played_Matches_Mode_Name.DisplayMember = "mode_name";
            cb_Played_Matches_Mode_Name.ValueMember = "mode_id";

            cb_Scores_Team_ID.DataSource = dbcon.ComboData(1);
            cb_Scores_Team_ID.DisplayMember = "team_name";
            cb_Scores_Team_ID.ValueMember = "team_id";
        }
        private DateTime ConvertToDateTime(string value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch (FormatException)
            {
                Console.WriteLine("'{0}' is not in the proper format.", value);
                return Convert.ToDateTime(value);
            }
        }
        #endregion
        #region ComboBox Events
        private void Cb_Insert_Table_TextChanged(object sender, EventArgs e)
        {
            switch (cb_Insert_Table.SelectedIndex)
            {
                case 0:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    gb_Played_Matches.Visible = false;
                    gb_Scores.Visible = false;
                    break;
                case 1:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = true;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    gb_Played_Matches.Visible = false;
                    gb_Scores.Visible = false;
                    break;
                case 2:
                    gb_Modes.Visible = true;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    gb_Played_Matches.Visible = false;
                    gb_Scores.Visible = false;
                    break;
                case 3:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    gb_Played_Matches.Visible = true;
                    gb_Scores.Visible = false;
                    break;
                case 4:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = true;
                    gb_Teams.Visible = false;
                    gb_Played_Matches.Visible = false;
                    gb_Scores.Visible = false;
                    break;
                case 5:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = false;
                    gb_Played_Matches.Visible = false;
                    gb_Scores.Visible = true;
                    break;
                case 6:
                    gb_Modes.Visible = false;
                    gb_Maps.Visible = false;
                    gb_Players.Visible = false;
                    gb_Teams.Visible = true;
                    gb_Played_Matches.Visible = false;
                    gb_Scores.Visible = false;
                    break;
                default:
                    MessageBox.Show("Option mit der Indexnummer " + cb_Insert_Table.SelectedIndex + " nicht bekannt.");
                    break;
            }
            ComboFiller();
            WordsInit();
        }
        private void CB_Played_Matches_Mode_Name_TextChanged(object sender, EventArgs e)
        {
            if (dbConState && cb_Insert_Table.SelectedIndex == 3) {
            
                switch (dbcon.Mode_Type(cb_Played_Matches_Mode_Name.SelectedValue.ToString()))
                {
                    case 1:
                        cb_Played_Matches_Mode_Type.Items.Clear();
                        cb_Played_Matches_Mode_Type.Items.Insert(0, "Solo");
                        break;
                    case 2:
                        cb_Played_Matches_Mode_Type.Items.Clear();
                        cb_Played_Matches_Mode_Type.Items.Insert(0, "Duo");
                        break;
                    case 3:
                        cb_Played_Matches_Mode_Type.Items.Clear();
                        cb_Played_Matches_Mode_Type.Items.Insert(0, "Solo");
                        cb_Played_Matches_Mode_Type.Items.Insert(1, "Duo");
                        break;
                    case 4:
                        cb_Played_Matches_Mode_Type.Items.Clear();
                        cb_Played_Matches_Mode_Type.Items.Insert(0, "Squad");
                        break;
                    case 5:
                        cb_Played_Matches_Mode_Type.Items.Clear();
                        cb_Played_Matches_Mode_Type.Items.Insert(0, "Solo");
                        cb_Played_Matches_Mode_Type.Items.Insert(1, "Squad");
                        break;
                    case 6:
                        cb_Played_Matches_Mode_Type.Items.Clear();
                        cb_Played_Matches_Mode_Type.Items.Insert(0, "Duo");
                        cb_Played_Matches_Mode_Type.Items.Insert(1, "Squad");
                        break;
                    case 7:
                        cb_Played_Matches_Mode_Type.Items.Clear();
                        cb_Played_Matches_Mode_Type.Items.Insert(0, "Solo");
                        cb_Played_Matches_Mode_Type.Items.Insert(1, "Duo");
                        cb_Played_Matches_Mode_Type.Items.Insert(2, "Squad");
                        break;
                    default: MessageBox.Show("Unbekannter Modus Typ. " + dbcon.Mode_Type(cb_Played_Matches_Mode_Name.SelectedValue.ToString()));
                        break;
                }
            }
        }
        private void CB_Played_Matches_Mode_Type_TextChanged(object sender, EventArgs e)
        {
            
            if (cb_Played_Matches_Mode_Type.SelectedItem.ToString() == "Solo")
            {
                ranking = dbcon.ComboData(2);
                table = "player";
                cb_Played_Matches_First_Place.DataSource = ranking;
                cb_Played_Matches_First_Place.DisplayMember = "player_nickname";
                cb_Played_Matches_First_Place.ValueMember = "player_team_id";
                cb_Played_Matches_First_Place.SelectedIndex = 0;
            }
            else if (cb_Played_Matches_Mode_Type.SelectedItem.ToString() == "Duo" || cb_Played_Matches_Mode_Type.SelectedItem.ToString() == "Squad")
            {
                ranking = dbcon.ComboData(1);
                table = "teams";
                cb_Played_Matches_First_Place.DataSource = ranking;
                cb_Played_Matches_First_Place.DisplayMember = "team_name";
                cb_Played_Matches_First_Place.ValueMember = "team_id";
                cb_Played_Matches_First_Place.SelectedIndex = 0;
            }
        }
        private void CB_Played_Matches_First_Place_TextChanged(object sender, EventArgs e)
        {
            if (table != "teams")
            {
                ranking_second = dbcon.ComboData(2);
                for (int i = ranking_second.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = ranking_second.Rows[i];
                    if (dr["player_team_id"].ToString() == cb_Played_Matches_First_Place.SelectedValue.ToString())
                        dr.Delete();
                }
                ranking_second.AcceptChanges();
                cb_Played_Matches_Second_Place.DataSource = ranking_second;
                cb_Played_Matches_Second_Place.DisplayMember = "player_nickname";
                cb_Played_Matches_Second_Place.ValueMember = "player_team_id";
                cb_Played_Matches_Second_Place.SelectedIndex = 0;
            }
            else
            {
                ranking_second = dbcon.ComboData(1);
                for (int i = ranking_second.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = ranking_second.Rows[i];
                    if (dr["team_id"].ToString() == cb_Played_Matches_First_Place.SelectedValue.ToString())
                        dr.Delete();
                }
                ranking_second.AcceptChanges();
                cb_Played_Matches_Second_Place.DataSource = ranking_second;
                cb_Played_Matches_Second_Place.DisplayMember = "team_name";
                cb_Played_Matches_Second_Place.ValueMember = "team_id";
                cb_Played_Matches_Second_Place.SelectedIndex = 0;
            }
            
        }
        private void CB_Played_Matches_Second_Place_TextChanged(object sender, EventArgs e)
        {
            if (table != "teams")
            {
                ranking_third = dbcon.ComboData(2);
                for (int i = ranking_third.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = ranking_third.Rows[i];
                    if (dr["player_team_id"].ToString() == cb_Played_Matches_Second_Place.SelectedValue.ToString() || dr["player_team_id"].ToString() == cb_Played_Matches_First_Place.SelectedValue.ToString())
                        dr.Delete();
                }
                ranking_third.AcceptChanges();
                cb_Played_Matches_Third_Place.DataSource = ranking_third;
                cb_Played_Matches_Third_Place.DisplayMember = "player_nickname";
                cb_Played_Matches_Third_Place.ValueMember = "player_team_id";
                cb_Played_Matches_Third_Place.SelectedIndex = 0;
            }
            else
            {
                ranking_third = dbcon.ComboData(1);
                for (int i = ranking_third.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = ranking_third.Rows[i];
                    if (dr["team_id"].ToString() == cb_Played_Matches_Second_Place.SelectedValue.ToString() || dr["team_id"].ToString() == cb_Played_Matches_First_Place.SelectedValue.ToString())
                        dr.Delete();
                }
                ranking_third.AcceptChanges();
                cb_Played_Matches_Third_Place.DataSource = ranking_third;
                cb_Played_Matches_Third_Place.DisplayMember = "team_name";
                cb_Played_Matches_Third_Place.ValueMember = "team_id";
                cb_Played_Matches_Third_Place.SelectedIndex = 0;
            }
            
        }
        #endregion
    }
}
