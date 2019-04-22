using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Fortnite_WM
{
    class DBcon
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public string propUid { set { uid = value; } }
        public string propPassword { set { password = value; } }
        public string propDatabase { set { database = value; } }
        public DBcon()
        {
            Connector();
        }

        //Initialize values
        private void Connector()
        {
            server = "localhost";
            database = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";"; // + "DATABASE=" + database + ";"

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

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
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

            //Open Connection
            if (this.OpenConnection() == true)
            {
                int i = 0;
                while (i<5)
                {
                    switch (i+1)
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
`team_name` TINYTEXT NOT NULL, 
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
`team_membercount` TINYINT UNSIGNED DEFAULT NULL,
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
########                 Zu der Map größe                 ########
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
`player_nickname`TINYTEXT NOT NULL, 
`player_team_id` SMALLINT UNSIGNED NOT NULL, 
`player_familyname` TINYTEXT NOT NULL, 
`player_firstname` TINYTEXT NOT NULL, 
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
`mode_max_player` TINYINT UNSIGNED NOT NULL, 
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
#	mode_type
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
#	mode_type
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
########             Zu den Seltenheiten               ########
###############################################################
CREATE TABLE IF NOT EXISTS `played_matches` (
`pm_id` INT UNSIGNED AUTO_INCREMENT, 
`pm_mode_id`  TINYINT UNSIGNED NOT NULL, 
`pm_map_id`  SMALLINT UNSIGNED NOT NULL, 
`pm_match_type` TINYTEXT NOT NULL,
`pm_player_round_start` TINYINT UNSIGNED DEFAULT 100,
PRIMARY KEY(`pm_id`),
FOREIGN KEY(`pm_mode_id`) REFERENCES `modes`(`mode_id`) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY(`pm_map_id`) REFERENCES `maps`(`map_id`) ON DELETE CASCADE ON UPDATE CASCADE);";
#endregion
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(createDBQuery, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void DBInsert()
        {
            #region InsertDBQuery
            string insertDBQuery = @"";
            #endregion
            //open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(insertDBQuery, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Open connection to database
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
                    default :
                        MessageBox.Show(ex.ToString());
                        break;
                }
                return false;
            }
        }

        //Close connection
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

        //Insert statements
        public void Insert()
        {
            this.OpenConnection();
        }

        //Update statements
        public void Update()
        {
        }

        //Delete statements
        public void Delete()
        {
        }

        //Select statements
        public List<string>[] Select(Array[] par)
        {
            string query = "";
            // 1 maps
            // 2 modes
            // 3 played_matches
            // 4 player
            // 5 teams
            switch (int.Parse(par[0].ToString()))
            {
                case 1: query = ";"; break;
                case 2: query = ";"; break;
                case 3: query = ";"; break;
                case 4: query = ";"; break;
                case 5: query = ";"; break;
                default:
                    break;
            }

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["bla"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statements
        public int Count()
        {
            return 1;
        }
    }
}
