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
        private int[] player;
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
`pm_1` SMALLINT UNSIGNED NOT NULL,
`pm_2` SMALLINT UNSIGNED NOT NULL,
`pm_3` SMALLINT UNSIGNED NOT NULL,
`pm_4` SMALLINT UNSIGNED NOT NULL,
`pm_5` SMALLINT UNSIGNED NOT NULL,
`pm_6` SMALLINT UNSIGNED NOT NULL,
`pm_7` SMALLINT UNSIGNED NOT NULL,
`pm_8` SMALLINT UNSIGNED NOT NULL,
`pm_9` SMALLINT UNSIGNED NOT NULL,
`pm_10` SMALLINT UNSIGNED NOT NULL,
`pm_11` SMALLINT UNSIGNED NOT NULL,
`pm_12` SMALLINT UNSIGNED NOT NULL,
`pm_13` SMALLINT UNSIGNED NOT NULL,
`pm_14` SMALLINT UNSIGNED NOT NULL,
`pm_15` SMALLINT UNSIGNED NOT NULL,
`pm_16` SMALLINT UNSIGNED NOT NULL,
`pm_17` SMALLINT UNSIGNED NOT NULL,
`pm_18` SMALLINT UNSIGNED NOT NULL,
`pm_19` SMALLINT UNSIGNED NOT NULL,
`pm_20` SMALLINT UNSIGNED NOT NULL,
`pm_21` SMALLINT UNSIGNED NOT NULL,
`pm_22` SMALLINT UNSIGNED NOT NULL,
`pm_23` SMALLINT UNSIGNED NOT NULL,
`pm_24` SMALLINT UNSIGNED NOT NULL,
`pm_25` SMALLINT UNSIGNED NOT NULL,
`pm_26` SMALLINT UNSIGNED NOT NULL,
`pm_27` SMALLINT UNSIGNED NOT NULL,
`pm_28` SMALLINT UNSIGNED NOT NULL,
`pm_29` SMALLINT UNSIGNED NOT NULL,
`pm_30` SMALLINT UNSIGNED NOT NULL,
`pm_31` SMALLINT UNSIGNED NOT NULL,
`pm_32` SMALLINT UNSIGNED NOT NULL,
`pm_33` SMALLINT UNSIGNED NOT NULL,
`pm_34` SMALLINT UNSIGNED NOT NULL,
`pm_35` SMALLINT UNSIGNED NOT NULL,
`pm_36` SMALLINT UNSIGNED NOT NULL,
`pm_37` SMALLINT UNSIGNED NOT NULL,
`pm_38` SMALLINT UNSIGNED NOT NULL,
`pm_39` SMALLINT UNSIGNED NOT NULL,
`pm_40` SMALLINT UNSIGNED NOT NULL,
`pm_41` SMALLINT UNSIGNED NOT NULL,
`pm_42` SMALLINT UNSIGNED NOT NULL,
`pm_43` SMALLINT UNSIGNED NOT NULL,
`pm_44` SMALLINT UNSIGNED NOT NULL,
`pm_45` SMALLINT UNSIGNED NOT NULL,
`pm_46` SMALLINT UNSIGNED NOT NULL,
`pm_47` SMALLINT UNSIGNED NOT NULL,
`pm_48` SMALLINT UNSIGNED NOT NULL,
`pm_49` SMALLINT UNSIGNED NOT NULL,
`pm_50` SMALLINT UNSIGNED NOT NULL,
`pm_51` SMALLINT UNSIGNED NOT NULL,
`pm_52` SMALLINT UNSIGNED NOT NULL,
`pm_53` SMALLINT UNSIGNED NOT NULL,
`pm_54` SMALLINT UNSIGNED NOT NULL,
`pm_55` SMALLINT UNSIGNED NOT NULL,
`pm_56` SMALLINT UNSIGNED NOT NULL,
`pm_57` SMALLINT UNSIGNED NOT NULL,
`pm_58` SMALLINT UNSIGNED NOT NULL,
`pm_59` SMALLINT UNSIGNED NOT NULL,
`pm_60` SMALLINT UNSIGNED NOT NULL,
`pm_61` SMALLINT UNSIGNED NOT NULL,
`pm_62` SMALLINT UNSIGNED NOT NULL,
`pm_63` SMALLINT UNSIGNED NOT NULL,
`pm_64` SMALLINT UNSIGNED NOT NULL,
`pm_65` SMALLINT UNSIGNED NOT NULL,
`pm_66` SMALLINT UNSIGNED NOT NULL,
`pm_67` SMALLINT UNSIGNED NOT NULL,
`pm_68` SMALLINT UNSIGNED NOT NULL,
`pm_69` SMALLINT UNSIGNED NOT NULL,
`pm_70` SMALLINT UNSIGNED NOT NULL,
`pm_71` SMALLINT UNSIGNED NOT NULL,
`pm_72` SMALLINT UNSIGNED NOT NULL,
`pm_73` SMALLINT UNSIGNED NOT NULL,
`pm_74` SMALLINT UNSIGNED NOT NULL,
`pm_75` SMALLINT UNSIGNED NOT NULL,
`pm_76` SMALLINT UNSIGNED NOT NULL,
`pm_77` SMALLINT UNSIGNED NOT NULL,
`pm_78` SMALLINT UNSIGNED NOT NULL,
`pm_79` SMALLINT UNSIGNED NOT NULL,
`pm_80` SMALLINT UNSIGNED NOT NULL,
`pm_81` SMALLINT UNSIGNED NOT NULL,
`pm_82` SMALLINT UNSIGNED NOT NULL,
`pm_83` SMALLINT UNSIGNED NOT NULL,
`pm_84` SMALLINT UNSIGNED NOT NULL,
`pm_85` SMALLINT UNSIGNED NOT NULL,
`pm_86` SMALLINT UNSIGNED NOT NULL,
`pm_87` SMALLINT UNSIGNED NOT NULL,
`pm_88` SMALLINT UNSIGNED NOT NULL,
`pm_89` SMALLINT UNSIGNED NOT NULL,
`pm_90` SMALLINT UNSIGNED NOT NULL,
`pm_91` SMALLINT UNSIGNED NOT NULL,
`pm_92` SMALLINT UNSIGNED NOT NULL,
`pm_93` SMALLINT UNSIGNED NOT NULL,
`pm_94` SMALLINT UNSIGNED NOT NULL,
`pm_95` SMALLINT UNSIGNED NOT NULL,
`pm_96` SMALLINT UNSIGNED NOT NULL,
`pm_97` SMALLINT UNSIGNED NOT NULL,
`pm_98` SMALLINT UNSIGNED NOT NULL,
`pm_99` SMALLINT UNSIGNED NOT NULL,
`pm_100` SMALLINT UNSIGNED NOT NULL,
PRIMARY KEY(`pm_id`),
FOREIGN KEY(`pm_mode_id`) REFERENCES `modes`(`mode_id`) ON DELETE CASCADE ON UPDATE CASCADE);


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
`pm_1`,`pm_2`,`pm_3`,`pm_4`,`pm_5`,`pm_6`,`pm_7`,`pm_8`,`pm_9`,`pm_10`,`pm_11`,`pm_12`,`pm_13`,`pm_14`,`pm_15`,`pm_16`,`pm_17`,`pm_18`,`pm_19`,`pm_20`,`pm_21`,`pm_22`,`pm_23`,`pm_24`,`pm_25`,
`pm_26`,`pm_27`,`pm_28`,`pm_29`,`pm_30`,`pm_31`,`pm_32`,`pm_33`,`pm_34`,`pm_35`,`pm_36`,`pm_37`,`pm_38`,`pm_39`,`pm_40`,`pm_41`,`pm_42`,`pm_43`,`pm_44`,`pm_45`,`pm_46`,`pm_47`,`pm_48`,`pm_49`,`pm_50`,
`pm_51`,`pm_52`,`pm_53`,`pm_54`,`pm_55`,`pm_56`,`pm_57`,`pm_58`,`pm_59`,`pm_60`,`pm_61`,`pm_62`,`pm_63`,`pm_64`,`pm_65`,`pm_66`,`pm_67`,`pm_68`,`pm_69`,`pm_70`,`pm_71`,`pm_72`,`pm_73`,`pm_74`,`pm_75`,
`pm_76`,`pm_77`,`pm_78`,`pm_79`,`pm_80`,`pm_81`,`pm_82`,`pm_83`,`pm_84`,`pm_85`,`pm_86`,`pm_87`,`pm_88`,`pm_89`,`pm_90`,`pm_91`,`pm_92`,`pm_93`,`pm_94`,`pm_95`,`pm_96`,`pm_97`,`pm_98`,`pm_99`,`pm_100`)
VALUES
("+ par["mode_id"] + @",
'" + par["match_type"] + @"',
" + par["pm_1"] + @"," + par["pm_2"] + @"," + par["pm_3"] + @"," + par["pm_4"] + @"," + par["pm_5"] + @"," + par["pm_6"] + @"," + par["pm_7"] + @"," + par["pm_8"] + @"," + par["pm_9"] + @"," + par["pm_10"] + @"," + par["pm_11"] + @"," + par["pm_12"] + @"," + par["pm_13"] + @"," + par["pm_14"] + @"," + par["pm_15"] + @"," + par["pm_16"] + @"," + par["pm_17"] + @"," + par["pm_18"] + @"," + par["pm_19"] + @"," + par["pm_20"] + @"," + par["pm_21"] + @"," + par["pm_22"] + @"," + par["pm_23"] + @"," + par["pm_24"] + @"," + par["pm_25"] + @",
" + par["pm_26"] + @"," + par["pm_27"] + @"," + par["pm_28"] + @"," + par["pm_29"] + @"," + par["pm_30"] + @"," + par["pm_31"] + @"," + par["pm_32"] + @"," + par["pm_33"] + @"," + par["pm_34"] + @"," + par["pm_35"] + @"," + par["pm_36"] + @"," + par["pm_37"] + @"," + par["pm_38"] + @"," + par["pm_39"] + @"," + par["pm_40"] + @"," + par["pm_41"] + @"," + par["pm_42"] + @"," + par["pm_43"] + @"," + par["pm_44"] + @"," + par["pm_45"] + @"," + par["pm_46"] + @"," + par["pm_47"] + @"," + par["pm_48"] + @"," + par["pm_49"] + @"," + par["pm_50"] + @",
" + par["pm_51"] + @"," + par["pm_52"] + @"," + par["pm_53"] + @"," + par["pm_54"] + @"," + par["pm_55"] + @"," + par["pm_56"] + @"," + par["pm_57"] + @"," + par["pm_58"] + @"," + par["pm_59"] + @"," + par["pm_60"] + @"," + par["pm_61"] + @"," + par["pm_62"] + @"," + par["pm_63"] + @"," + par["pm_64"] + @"," + par["pm_65"] + @"," + par["pm_66"] + @"," + par["pm_67"] + @"," + par["pm_68"] + @"," + par["pm_69"] + @"," + par["pm_70"] + @"," + par["pm_71"] + @"," + par["pm_72"] + @"," + par["pm_73"] + @"," + par["pm_74"] + @"," + par["pm_75"] + @",
" + par["pm_76"] + @"," + par["pm_77"] + @"," + par["pm_78"] + @"," + par["pm_79"] + @"," + par["pm_80"] + @"," + par["pm_81"] + @"," + par["pm_82"] + @"," + par["pm_83"] + @"," + par["pm_84"] + @"," + par["pm_85"] + @"," + par["pm_86"] + @"," + par["pm_87"] + @"," + par["pm_88"] + @"," + par["pm_89"] + @"," + par["pm_90"] + @"," + par["pm_91"] + @"," + par["pm_92"] + @"," + par["pm_93"] + @"," + par["pm_94"] + @"," + par["pm_95"] + @"," + par["pm_96"] + @"," + par["pm_97"] + @"," + par["pm_98"] + @"," + par["pm_99"] + @"," + par["pm_100"] + @");";
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
                    query = "SELECT * FROM teams where team_member = '4';";
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
        
        public int Mode_Type_ID(string id)
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

        public void AddMember(Dictionary<string, string> par)
        {
            if (this.OpenConnection() == true)
            {
                string query = "SELECT team_member from teams WHERE team_id ='" + par["cb_Player_Team_ID"] + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int member = int.Parse(cmd.ExecuteScalar() + "") + 1;
                query = "UPDATE TABLE teams SET team_member = '" + member + "' WHERE team_id = '" + par["cb_Player_Team_ID"] + "' ;";
                cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void RefreshTeamMember()
        {
            MySqlDataAdapter tableAdapter;
            DataTable dt = new DataTable();
            int[] team;
            string query = "SELECT player_team_id, count(player_id) FROM player GROUP BY player_team_id ORDER BY player_team_id asc;";
            tableAdapter = new MySqlDataAdapter(query, connection);
            tableAdapter.Fill(dt);
            team = new int[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                team[i] = int.Parse(row.ItemArray.GetValue(1).ToString());
                i++;
            }
            i = 0;
            if (this.OpenConnection() == true)
            {
                foreach (var member in team)
                {
                    query = "UPDATE TABLE teams SET team_member = '" + team[i] + "' WHERE team_id = '" + (i + 1) + "' ;";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
            tableAdapter.Dispose();
            dt.Dispose();
        }
        #region Simulator
        public void SimulateRound()
        {
            Dictionary<string, string> sim = new Dictionary<string, string>
            {
                ["Tabelle"] = "played_matches",
                ["mode_id"] = RandomMode().ToString()
            };
            string type = GetModeTypeName(int.Parse(sim["mode_id"]));
            int[] player;
            Random rand = new Random();
            if (type.Contains(","))
            {
                sim["match_type"] = type.Split(',')[rand.Next(0, type.Split(',').Length-1)];
            }
            else
            {
                sim["match_type"] = type;
            }
            if (sim["match_type"] == "solo")
            {
                player = RandomSolo();
            }
            else if (sim["match_type"] == "duo")
            {
                player = RandomDuo();
            }
            else
            {
                player = RandomSquad();
            }
            for (int i = 0; i < player.Length - 1; i++)
            {
                int j = rand.Next(i, player.Length);
                int temp = player[i];
                player[i] = player[j];
                player[j] = temp;
            }
            int place = 1;
            foreach (var index in player)
            {
                sim["pm_"+place.ToString()] = index.ToString();
                place++;
            }
            DBInsert(sim);
            SimulatePoints(sim);
            sim.Clear();
        }
        private int[] RandomSolo()
        {
            MySqlDataAdapter tableAdapter;
            DataTable dt = new DataTable();
            string query = "select player_team_id,  substring_index(group_concat(player_id order by rand()), ',' , 1) as 'player_id' from player group by player_team_id order by rand() limit 100;";
            tableAdapter = new MySqlDataAdapter(query, connection);
            tableAdapter.Fill(dt);
            player = new int[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                player[i] = int.Parse(row.ItemArray.GetValue(1).ToString());
                i++;
            }
            return player;
        }
        private int RandomMode()
        {
            string query = "select mode_id from modes order by rand() limit 1;";
            int modeID;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                modeID = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            else
            {
                modeID = -1;
            }
            return modeID;
        }
        private string GetModeTypeName(int id)
        {
            string query = "select mode_type from modes where mode_id = '" + id + "' limit 1;";
            int type_id;
            string type;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                type_id = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            else
            {
                type_id = -1;
            }
            switch (type_id)
            {
                case 1:
                    type = "solo";
                    break;
                case 2:
                    type = "duo";
                    break;
                case 3:
                    type = "solo,duo";
                    break;
                case 4:
                    type = "squad";
                    break;
                case 5:
                    type = "solo,squad";
                    break;
                case 6:
                    type = "duo,squad";
                    break;
                case 7:
                    type = "solo,duo,squad";
                    break;
                default:
                    type = "err";
                    break;
            }
            return type;
        }
        private int[] RandomDuo()
        {
            MySqlDataAdapter tableAdapter;
            DataTable dt = new DataTable();
            string query = "select player_team_id,  substring_index(group_concat(player_id order by rand()), ',' , 2) as 'player_id' from player group by player_team_id order by rand() limit 50;";
            tableAdapter = new MySqlDataAdapter(query, connection);
            tableAdapter.Fill(dt);
            player = new int[dt.Rows.Count * 2];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                player[i] = int.Parse(row.ItemArray.GetValue(1).ToString().Split(',')[0]);
                player[i+1] = int.Parse(row.ItemArray.GetValue(1).ToString().Split(',')[1]);
                i += 2;
            }
            return player;
        }
        private int[] RandomSquad()
        {
            MySqlDataAdapter tableAdapter;
            DataTable dt = new DataTable();
            string query = "select player_team_id,  group_concat(player_id order by rand()) as 'player_id' from player group by player_team_id order by rand() limit 25;";
            tableAdapter = new MySqlDataAdapter(query, connection);
            tableAdapter.Fill(dt);
            player = new int[dt.Rows.Count * 4];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                player[i] = int.Parse(row.ItemArray.GetValue(1).ToString().Split(',')[0]);
                player[i + 1] = int.Parse(row.ItemArray.GetValue(1).ToString().Split(',')[1]);
                player[i + 2] = int.Parse(row.ItemArray.GetValue(1).ToString().Split(',')[2]);
                player[i + 3] = int.Parse(row.ItemArray.GetValue(1).ToString().Split(',')[3]);
                i += 4;
            }
            return player;
        }
        private void SimulatePoints(Dictionary<string, string> par)
        {
            string queryPlayer;
            string query;
            int team = -1;
            if (this.OpenConnection() == true)
            {
                for (int i = 1; i < 101; i++)
                {
                    queryPlayer = "SELECT player_team_id FROM player WHERE player_id = '" + par["pm_" + i] + "' LIMIT 1; ";
                    MySqlCommand cmd = new MySqlCommand(queryPlayer, connection);
                    team = int.Parse(cmd.ExecuteScalar() + "");
                    query = "INSERT INTO `fortnite_wm`.`scores`(`sc_team_id`,`sc_points`) VALUES(" + team + ", " + (100 - i) + ")";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        SimulateRoundWinner(par);
                    }
                }
                this.CloseConnection();
            }

        }
        private void SimulateRoundWinner(Dictionary<string, string> par)
        {
            if (this.OpenConnection() == true)
            {
                string query = "SELECT player_team_id from player WHERE player_id ='" + par["pm_1"] + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int team = int.Parse(cmd.ExecuteScalar() + "");
                query = "SELECT team_wins FROM teams WHERE team_id = '" + team + "';";
                int wins = int.Parse(cmd.ExecuteScalar() + "") + 1;
                query = "UPDATE TABLE teams SET team_wins = '" + wins + "' WHERE team_id = '" + team + "' ;";
                cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
            
        }
        public int GetPlayedRounds()
        {
            string query = "SELECT count(*) FROM played_matches;";
            int matches;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                matches = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
            }
            else
            {
                matches = -1;
            }
            return matches;
        }
        #endregion
    }
}
