using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Fortnite_WM
{
    class DBcon
    {
        private MySqlConnection connection;
        private MySqlDataAdapter mda;
        private string server = "";
        private string database = "";
        private string uid;
        private string password;
        private int[] PlayerSolo;
        private int[] PlayerDuo;
        private int[] PlayerSquad;
        public string PropUid { set { uid = value; } }
        public string PropPassword { set { password = value; } }
        public string PropDatabase { set { database = value; } }
        public string PropServer { set { server = value; } }
        public DBcon()
        {
            Connector();
        }

        private void Connector()
        {
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

        }

        public bool DBConState()
        {
            Connector();
            bool state = this.OpenConnection();
            this.CloseConnection();
            return state;
        }

        public int DBexist()
        {
            string query = "SELECT COUNT(*) FROM information_schema.schemata WHERE SCHEMA_NAME='fortnite_wm';";
            int Count = -1;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                Count = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        public Dictionary<string, int> TBexist()
        {
            Dictionary<string, int> tbexist = new Dictionary<string, int>();
            string query = "";
            string key = "";

            if (this.OpenConnection() == true)
            {
                int i = 0;
                while (i < 6)
                {
                    switch (i + 1)
                    {
                        case 1:
                            key = "maps";
                            query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'fortnite_wm' AND table_name = 'maps' LIMIT 1; ";
                            break;
                        case 2:
                            key = "modes";
                            query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'fortnite_wm' AND table_name = 'modes' LIMIT 1;";
                            break;
                        case 3:
                            key = "played_matches";
                            query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'fortnite_wm' AND table_name = 'played_matches' LIMIT 1;";
                            break;
                        case 4:
                            key = "player";
                            query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'fortnite_wm' AND table_name = 'player' LIMIT 1;";
                            break;
                        case 5:
                            key = "teams";
                            query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'fortnite_wm' AND table_name = 'teams' LIMIT 1;";
                            break;
                        case 6:
                            key = "scores";
                            query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'fortnite_wm' AND table_name = 'scores' LIMIT 1;";
                            break;
                        default:
                            MessageBox.Show("Tabelle nicht bekannt.");
                            break;
                    }
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        tbexist.Add(key, int.Parse(cmd.ExecuteScalar() + ""));
                    }
                    catch (Exception)
                    {
                        tbexist.Add(key, 0);
                    }
                    i++;
                }
                this.CloseConnection();

                return tbexist;
            }
            else
            {
                return tbexist;
            }
        }

        public void DBCreate()
        {
            #region CreateDBQuery
            string createDBQuery = @"#
###############################################################
########      Erstellen der Fortnite WM Datenbank      ########
###############################################################
CREATE DATABASE IF NOT EXISTS fortnite_wm CHARACTER SET utf8;

USE fortnite_wm;

###############################################################
########          Erstellen der Tabelle Teams          ########
###############################################################
CREATE TABLE IF NOT EXISTS `teams` ( 
`team_id` SMALLINT UNSIGNED AUTO_INCREMENT,
`team_name` VARCHAR(40) UNIQUE NOT NULL, 
`team_wins` TINYINT UNSIGNED DEFAULT 0, 
`team_country` TINYTEXT NOT NULL,
`team_state` TINYTEXT NOT NULL,
`team_city` TINYTEXT NOT NULL, 
`team_zip` TINYTEXT NOT NULL, 
`team_street` TEXT NOT NULL, 
`team_streetnr` TINYTEXT NOT NULL, 
`team_mail` TINYTEXT NOT NULL, 
`team_description` TEXT DEFAULT NULL, 
`team_created` DATETIME NOT NULL, 
`team_kills` SMALLINT UNSIGNED DEFAULT 0,
`team_member` TINYINT UNSIGNED DEFAULT 0,
PRIMARY KEY (`team_id`));
 
 
 
###############################################################
########           Erstellen der Tabelle Maps          ########
###############################################################
CREATE TABLE IF NOT EXISTS `maps` ( 
`map_id` SMALLINT UNSIGNED AUTO_INCREMENT, 
`map_name` TINYTEXT NOT NULL, 
`map_type` TINYINT NOT NULL,
PRIMARY KEY (`map_id`));
 
###############################################################
########                 Zu der Map Größe              ########
###############################################################
#	map_type
#
#	1 	= small
#	2	= middle
#	4	= large
#	
#
###############################################################
 
 
###############################################################
########          Erstellen der Tabelle Player           ########
###############################################################
CREATE TABLE IF NOT EXISTS `player` ( 
`player_id` int UNSIGNED AUTO_INCREMENT, 
`player_nickname` VARCHAR(20) UNIQUE NOT NULL, 
`player_team_id` SMALLINT UNSIGNED NOT NULL, 
`player_familyname` TINYTEXT NOT NULL, 
`player_firstname` TINYTEXT NOT NULL, 
`player_age` DATE NOT NULL,
`player_country` TINYTEXT DEFAULT NULL,
`player_state` TINYTEXT DEFAULT NULL,
`player_city` TINYTEXT DEFAULT NULL, 
`player_zip` TINYTEXT DEFAULT NULL, 
`player_street` TEXT DEFAULT NULL, 
`player_streetnr` TINYTEXT DEFAULT NULL, 
`player_phonenumber` BIGINT UNSIGNED DEFAULT NULL, 
`player_mail` TINYTEXT NOT NULL, 
`player_created` DATETIME NOT NULL, 
`player_kills` SMALLINT DEFAULT 0, 
`player_played_matches` SMALLINT DEFAULT 0,
PRIMARY KEY(`player_id`),
FOREIGN KEY(`player_team_id`) REFERENCES `teams`(`team_id`) ON DELETE CASCADE ON UPDATE CASCADE);


###############################################################
########          Erstellen der Tabelle Modes          ########
###############################################################
CREATE TABLE IF NOT EXISTS `modes` (
`mode_id` TINYINT UNSIGNED AUTO_INCREMENT, 
`mode_name` TINYTEXT NOT NULL, 
`mode_type` TINYINT UNSIGNED DEFAULT 7, 
`mode_map_id` SMALLINT UNSIGNED NOT NULL, 
`mode_max_player` TINYINT UNSIGNED DEFAULT 100, 
`mode_weapon_types` TINYINT UNSIGNED DEFAULT 255,
`mode_weapon_rarity` TINYINT UNSIGNED DEFAULT 63,
PRIMARY KEY(`mode_id`),
FOREIGN KEY(`mode_map_id`) REFERENCES `maps`(`map_id`) ON DELETE CASCADE ON UPDATE CASCADE);

###############################################################
########                 Zu dem Modus                  ########
###############################################################
#	mode_type
#
#	1 	= solo
#	2	= duo
#	4	= squad
#	
###############################################################

###############################################################
########                 Zu den Waffen                 ########
###############################################################
#	mode_weapon_types
#
#	1 	= Pistolen
#	2	= Schrotflinten
#	4	= Maschinenpistolen
#	8	= Strumgeweher
#	16	= Raketenwerfer
#	32	= Granatwerfer
#	64	= Scharfschützengewehre
#	128	= Granaten und Bomben
#	
###############################################################

###############################################################
########               Zu den Seltenheiten             ########
###############################################################
#	mode_weapon_rarity
#
#	1 	= gewöhnlich
#	2	= ungewöhnlich
#	4	= selten
#	8	= episch
#	16	= legendär
#	32	= mythisch
#	
###############################################################


###############################################################
########           Zu den Gespielten Runden            ########
###############################################################
CREATE TABLE IF NOT EXISTS `played_matches` (
`pm_id` INT UNSIGNED AUTO_INCREMENT,
`pm_mode_id`  TINYINT UNSIGNED NOT NULL,
`pm_match_type` TINYTEXT NOT NULL,
`pm_winning_team_id` SMALLINT UNSIGNED NOT NULL,
`pm_second_team_id` SMALLINT UNSIGNED NOT NULL,
`pm_third_team_id` SMALLINT UNSIGNED NOT NULL,
`pm_player_round_start` TINYINT UNSIGNED DEFAULT 100,
PRIMARY KEY(`pm_id`),
FOREIGN KEY(`pm_mode_id`) REFERENCES `modes`(`mode_id`) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY(`pm_winning_team_id`) REFERENCES `teams`(`team_id`) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY(`pm_second_team_id`) REFERENCES `teams`(`team_id`) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY(`pm_third_team_id`) REFERENCES `teams`(`team_id`) ON DELETE CASCADE ON UPDATE CASCADE);


###############################################################
########                 Zu den Punkten                ########
###############################################################
CREATE TABLE IF NOT EXISTS `scores` (
`sc_id` INT UNSIGNED AUTO_INCREMENT NOT NULL,
`sc_team_id` SMALLINT UNSIGNED NOT NULL,
`sc_points` SMALLINT UNSIGNED DEFAULT 0,
PRIMARY KEY(`sc_id`),
FOREIGN KEY(`sc_team_id`) REFERENCES `teams`(`team_id`) ON DELETE CASCADE ON UPDATE CASCADE)";
            #endregion
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(createDBQuery, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void DBInsert(Dictionary<string, string> par)
        {
            string query = "";
            if (par["Tabelle"] == "maps")
            {
                #region MapsDBQuery
                query = "Insert Into maps(`map_name`,`map_type`) Values('" + par["tb_Map_Name"] + "', " + par["map_type"] + ")";
                #endregion
            }
            else if (par["Tabelle"] == "modes")
            {
                #region ModesDBQuery
                query = @"INSERT INTO `fortnite_wm`.`modes`
(`mode_name`,
`mode_type`,
`mode_map_id`,
`mode_max_player`,
`mode_weapon_types`,
`mode_weapon_rarity`)
VALUES
('" + par["tb_Mode_Name"] + @"',
" + par["modes_type"] + @",
" + par["cb_Mode_Map_Name"] + @",
" + par["nud_Max_Player"] + @",
" + par["modes_weapontype"] + @",
" + par["modes_rarity"] + @");";
                #endregion
            }
            else if (par["Tabelle"] == "played_matches")
            {
                #region Played_MatchesDBQuery
                query = @"INSERT INTO `fortnite_wm`.`played_matches`
(`pm_mode_id`,
`pm_match_type`,
`pm_winning_team_id`,
`pm_second_team_id`,
`pm_third_team_id`,
`pm_player_round_start`)
VALUES
(" + par["cb_Played_Matches_Mode_Name"] + @",
'" + par["cb_Played_Matches_Mode_Type"] + @"',
" + par["cb_Played_Matches_First_Place"] + @",
" + par["cb_Played_Matches_Second_Place"] + @",
" + par["cb_Played_Matches_Third_Place"] + @",
" + par["nud_Max_Player"] + @");";
                #endregion
            }
            else if (par["Tabelle"] == "player")
            {
                #region PlayerDBQuery
                query = @"INSERT INTO `fortnite_wm`.`player`
(`player_nickname`,
`player_team_id`,
`player_familyname`,
`player_firstname`,
`player_age`,
`player_country`,
`player_state`,
`player_city`,
`player_zip`,
`player_street`,
`player_streetnr`,
`player_phonenumber`,
`player_mail`,
`player_created`)
VALUES
('" + par["tb_Player_Nickname"] + @"',
" + par["cb_Player_Team_ID"] + @",
'" + par["tb_Player_Familyname"] + @"',
'" + par["tb_Player_Firstname"] + @"',
'" + par["tb_Player_Age"] + @"',
'" + par["tb_Player_Country"] + @"',
'" + par["tb_Player_State"] + @"',
'" + par["tb_Player_City"] + @"',
" + par["tb_Player_Postalcode"] + @",
'" + par["tb_Player_Street"] + @"',
" + par["tb_Player_Streetnr"] + @",
" + par["tb_Player_Phonenumber"] + @",
'" + par["tb_Player_Mail"] + @"',
NOW())";
                #endregion
            }
            else if (par["Tabelle"] == "scores")
            {
                #region ScoresDBQuery
                query = "INSERT INTO `fortnite_wm`.`scores`(`sc_team_id`,`sc_points`) VALUES(" + par["cb_Scores_Team_ID"] + ", " + par["tb_Scores_Points"] + ")";
                #endregion

            }
            else if (par["Tabelle"] == "teams")
            {
                #region TeamsDBQuery
                query = @"INSERT INTO `fortnite_wm`.`teams`
(`team_name`,
`team_country`,
`team_state`,
`team_city`,
`team_zip`,
`team_street`,
`team_streetnr`,
`team_mail`,
`team_description`,
`team_created`)
VALUES
('" + par["tb_Teams_Name"] + @"',
'" + par["tb_Teams_Country"] + @"',
'" + par["tb_Teams_State"] + @"',
'" + par["tb_Teams_City"] + @"',
" + par["tb_Teams_Postalcode"] + @",
'" + par["tb_Teams_Street"] + @"',
" + par["tb_Teams_Streetnr"] + @",
'" + par["tb_Teams_Mail"] + @"',
'" + par["tb_Description"] + @"',
NOW());";
                #endregion
            }
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public DataTable ComboData(int par)
        {
            string query = "";
            MySqlDataAdapter tableAdapter;
            DataTable tableDS = new DataTable();
            DataRow row;
            switch (par)
            {
                case 0:
                    query = "SELECT * FROM information_schema.tables WHERE table_schema = 'fortnite_wm'";
                    tableAdapter = new MySqlDataAdapter(query, connection);
                    tableAdapter.Fill(tableDS);
                    row = tableDS.NewRow();
                    row["TABLE_NAME"] = "-";
                    tableDS.Rows.InsertAt(row, 0);
                    return tableDS;
                case 1:
                    query = "SELECT * FROM teams;";
                    tableAdapter = new MySqlDataAdapter(query, connection);
                    tableAdapter.Fill(tableDS);
                    return tableDS;
                case 2:
                    query = "SELECT * FROM player;";
                    tableAdapter = new MySqlDataAdapter(query, connection);
                    tableAdapter.Fill(tableDS);
                    return tableDS;
                case 3:
                    query = "SELECT * FROM maps;";
                    tableAdapter = new MySqlDataAdapter(query, connection);
                    tableAdapter.Fill(tableDS);
                    return tableDS;
                case 4:
                    query = "SELECT * FROM modes;";
                    tableAdapter = new MySqlDataAdapter(query, connection);
                    tableAdapter.Fill(tableDS);
                    return tableDS;
                case 5:
                    query = "SELECT * FROM played_matches;";
                    tableAdapter = new MySqlDataAdapter(query, connection);
                    tableAdapter.Fill(tableDS);
                    return tableDS;
                case 6:
                    query = "SELECT * FROM scores;";
                    tableAdapter = new MySqlDataAdapter(query, connection);
                    tableAdapter.Fill(tableDS);
                    return tableDS;
                default:
                    row = tableDS.NewRow();
                    row["err"] = "err";
                    tableDS.Rows.InsertAt(row, 0);
                    return tableDS;
            }
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Keine Verbindung zum Server. Bitte kontaktieren sie ihren Systemadministrator");
                        break;

                    case 1045:
                        MessageBox.Show("User ID oder Passwort stimmt nicht überein, bitte versuche sie es erneut.");
                        break;
                    default:
                        MessageBox.Show(ex.ToString());
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Update(DataTable changes, string table)
        {
            OpenConnection();
            try
            {
                mda = new MySqlDataAdapter("select * from " + table, connection);
                MySqlCommandBuilder mcb = new MySqlCommandBuilder(mda);
                mda.UpdateCommand = mcb.GetUpdateCommand();
                mda.Update(changes);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                CloseConnection();
            }
        }
        
        public void Delete(DataTable changes, string table)
        {
            OpenConnection();
            try
            {
                mda = new MySqlDataAdapter("select * from " + table, connection);
                MySqlCommandBuilder mcb = new MySqlCommandBuilder(mda);
                mda.DeleteCommand = mcb.GetDeleteCommand();
                mda.Update(changes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                CloseConnection();
            }
        }
        
        public DataTable Select(string query)
        {
            MySqlDataAdapter tableAdapter;
            DataTable tableDS = new DataTable();
            tableAdapter = new MySqlDataAdapter(query, connection);
            tableAdapter.Fill(tableDS);
            return tableDS;
        }
        
        public int Mode_Type(string id)
        {
            string query = "SELECT mode_type FROM modes where mode_id = " + id + ";";
            int value = new int();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                value = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            return value;
        }

        public DataTable ColumnNames(string table)
        {
            MySqlDataAdapter tableAdapter;
            DataTable tableDS = new DataTable();
            string query = "SELECT COLUMN_NAME FROM information_schema.columns WHERE table_schema = 'fortnite_wm' AND table_name = '"+ table +"'";
            tableAdapter = new MySqlDataAdapter(query, connection);
            tableAdapter.Fill(tableDS);
            return tableDS;
        }

        public int NicknameExist(string nick)
        {
            string query = "SELECT COUNT(*) FROM player WHERE player_nickname = '" + nick + "' LIMIT 1; ";
            int state = 0;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                state = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            return state;
        }

        public int TeamnameExist(string team)
        {
            string query = "SELECT COUNT(*) FROM teams WHERE team_name = '" + team + "' LIMIT 1; ";
            int state = 0;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                state = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            return state;
        }

        public int ModeExist(string mode)
        {
            string query = "SELECT COUNT(*) FROM modes WHERE mode_name = '" + mode + "' LIMIT 1; ";
            int state = 0;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                state = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            return state;
        }

        public int MapExist(string map)
        {
            string query = "SELECT COUNT(*) FROM maps WHERE map_name = '" + map + "' LIMIT 1; ";
            int state = 0;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                state = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            return state;
        }

        public void SimulateRound()
        {

        }
        
        private void RandomSolo()
        {
            string querySolo = "select player_team_id,  substring_index(group_concat(player_id order by rand()), ',' , 1) as 'player_id' from player group by player_team_id order by rand() limit 100;";
        }
        private void RandomMode()
        {
            string queryMode = "select mode_id from modes order by rand() limit 1;";
        }
        private void RandomDuo()
        {
            string queryDuo = "select player_team_id,  substring_index(group_concat(player_id order by rand()), ',' , 2) as 'player_id' from player group by player_team_id order by rand() limit 50;";
        }
        private void RandomSquad()
        {
            string querySquad = "select player_team_id,  group_concat(player_id order by rand()) as 'player_id' from player group by player_team_id order by rand() limit 25;";
        }
    }
}
