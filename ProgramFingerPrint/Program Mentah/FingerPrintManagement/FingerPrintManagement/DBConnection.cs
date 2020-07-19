using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FingerPrintManagement
{
    public class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnection()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "fingerprint";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
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
        #region Identitas
        //Insert Identitas
        public void InsertIdentitas(String idRule,String Nama, String Nomor)
        {
            string query = "INSERT INTO identitas (IDRULE, NAMAIDENTITAS, NOMORIDENTITAS) VALUES('"+idRule+"', '"+Nama+"','"+Nomor+"')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Update identitas
        public void UpdateIdentitas(int id,String idRule, String Nama, String Nomor)
        {
            string query = "UPDATE identitas SET IDRULE = "+idRule+", NAMAIDENTITAS = "+Nama+", NOMORIDENTITAS = "+Nomor+" WHERE IDIDENTITAS = "+id+"; ";
            //Open connection
            if (this.OpenConnection() == true) {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Delete identitas
        public void DeleteIdentitas(int id)
        {
            string query = "DELETE FROM identitas WHERE IDIDENTITAS = "+id+";";
            if (this.OpenConnection() == true){
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select identitas
        public List<string>[] SelectIdentitas(String Name)
        {
            string query = "SELECT identitas.*, rule.NAMERULE FROM identitas where NAMEIDENTITAS = "+Name+" JOIN rule on identitas.IDRULE = rule.IDRULE;";
            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            //Open connection
            if (this.OpenConnection() == true){
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["IDIDENTITAS"] + "");
                    list[1].Add(dataReader["NAMERULE"] + "");
                    list[2].Add(dataReader["NAMAIDENTITAS"] + "");
                    list[2].Add(dataReader["NOMORIDENTITAS"] + "");
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
        #endregion

        #region Rule
        //Insert Rule
        public void InsertRule(String Nama)
        {
            string query = "INSERT INTO rule (NAMERULE) VALUES('" + Nama + "')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Update identitas
        public void UpdateRule(int id, String Nama)
        {
            string query = "UPDATE rule SET NAMERULE = " + Nama + " WHERE IDRULE = " + id + "; ";
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Delete identitas
        public void DeleteRule(int id)
        {
            string query = "DELETE FROM rule WHERE IDRULE = " + id + ";";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select identitas
        public List<string>[] SelectRule()
        {
            string query = "SELECT * FROM rule ";
            //Create a list to store the result
            List<string>[] list = new List<string>[2];
            list[0] = new List<string>();
            list[1] = new List<string>();
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
                    list[0].Add(dataReader["IDRULE"] + "");
                    list[1].Add(dataReader["NAMERULE"] + "");
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
        #endregion

        #region Mata Kuliah
        //Insert Mata Kuliah 
        public void InsertMataKuliah(String Nama)
        {
            string query = "INSERT INTO matakuliah (NAMAMATAKULIAH) VALUES('" + Nama + "')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Update matakuliah
        public void UpdateMataKuliah(int id, String Name)
        {
            string query = "UPDATE matakuliah SET NAMAMATAKULIAH = " + Name + " WHERE IDMATAKULIAH = " + id + "; ";
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Delete Matakuliah
        public void DeleteMataKuliah(int id)
        {
            string query = "DELETE FROM matakuliah WHERE IDMATAKULIAH = " + id + ";";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select Matakuliah
        public List<string>[] SelectMataKuliah()
        {
            string query = "SELECT * FROM matakuliah";
            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
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
                    list[0].Add(dataReader["IDMATAKULIAH"] + "");
                    list[1].Add(dataReader["NAMAMATAKULIAH"] + "");
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
        #endregion

        #region JADWAL KULIAH
        //Insert Identitas
        public void InsertIdentitas(String idRule, String Nama, String Nomor)
        {
            string query = "INSERT INTO identitas (IDRULE, NAMAIDENTITAS, NOMORIDENTITAS) VALUES('" + idRule + "', '" + Nama + "','" + Nomor + "')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Update identitas
        public void UpdateIdentitas(int id, String idRule, String Nama, String Nomor)
        {
            string query = "UPDATE identitas SET IDRULE = " + idRule + ", NAMAIDENTITAS = " + Nama + ", NOMORIDENTITAS = " + Nomor + " WHERE IDIDENTITAS = " + id + "; ";
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Delete identitas
        public void DeleteIdentitas(int id)
        {
            string query = "DELETE FROM identitas WHERE IDIDENTITAS = " + id + ";";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select identitas
        public List<string>[] SelectIdentitas(String Name)
        {
            string query = "SELECT identitas.*, rule.NAMERULE FROM identitas where NAMEIDENTITAS = " + Name + " JOIN rule on identitas.IDRULE = rule.IDRULE;";
            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
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
                    list[0].Add(dataReader["IDIDENTITAS"] + "");
                    list[1].Add(dataReader["NAMERULE"] + "");
                    list[2].Add(dataReader["NAMAIDENTITAS"] + "");
                    list[2].Add(dataReader["NOMORIDENTITAS"] + "");
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
        #endregion

        #region JADWAL
        //Insert Identitas
        public void InsertIdentitas(String idRule, String Nama, String Nomor)
        {
            string query = "INSERT INTO identitas (IDRULE, NAMAIDENTITAS, NOMORIDENTITAS) VALUES('" + idRule + "', '" + Nama + "','" + Nomor + "')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Update identitas
        public void UpdateIdentitas(int id, String idRule, String Nama, String Nomor)
        {
            string query = "UPDATE identitas SET IDRULE = " + idRule + ", NAMAIDENTITAS = " + Nama + ", NOMORIDENTITAS = " + Nomor + " WHERE IDIDENTITAS = " + id + "; ";
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Delete identitas
        public void DeleteIdentitas(int id)
        {
            string query = "DELETE FROM identitas WHERE IDIDENTITAS = " + id + ";";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select identitas
        public List<string>[] SelectIdentitas(String Name)
        {
            string query = "SELECT identitas.*, rule.NAMERULE FROM identitas where NAMEIDENTITAS = " + Name + " JOIN rule on identitas.IDRULE = rule.IDRULE;";
            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
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
                    list[0].Add(dataReader["IDIDENTITAS"] + "");
                    list[1].Add(dataReader["NAMERULE"] + "");
                    list[2].Add(dataReader["NAMAIDENTITAS"] + "");
                    list[2].Add(dataReader["NOMORIDENTITAS"] + "");
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
        #endregion

        #region Finger Print
        //Insert Identitas
        public void InsertIdentitas(String idRule, String Nama, String Nomor)
        {
            string query = "INSERT INTO identitas (IDRULE, NAMAIDENTITAS, NOMORIDENTITAS) VALUES('" + idRule + "', '" + Nama + "','" + Nomor + "')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Update identitas
        public void UpdateIdentitas(int id, String idRule, String Nama, String Nomor)
        {
            string query = "UPDATE identitas SET IDRULE = " + idRule + ", NAMAIDENTITAS = " + Nama + ", NOMORIDENTITAS = " + Nomor + " WHERE IDIDENTITAS = " + id + "; ";
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Delete identitas
        public void DeleteIdentitas(int id)
        {
            string query = "DELETE FROM identitas WHERE IDIDENTITAS = " + id + ";";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select identitas
        public List<string>[] SelectIdentitas(String Name)
        {
            string query = "SELECT identitas.*, rule.NAMERULE FROM identitas where NAMEIDENTITAS = " + Name + " JOIN rule on identitas.IDRULE = rule.IDRULE;";
            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
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
                    list[0].Add(dataReader["IDIDENTITAS"] + "");
                    list[1].Add(dataReader["NAMERULE"] + "");
                    list[2].Add(dataReader["NAMAIDENTITAS"] + "");
                    list[2].Add(dataReader["NOMORIDENTITAS"] + "");
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
        #endregion

        #region History
        //Insert Identitas
        public void InsertIdentitas(String idRule, String Nama, String Nomor)
        {
            string query = "INSERT INTO identitas (IDRULE, NAMAIDENTITAS, NOMORIDENTITAS) VALUES('" + idRule + "', '" + Nama + "','" + Nomor + "')";
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Update identitas
        public void UpdateIdentitas(int id, String idRule, String Nama, String Nomor)
        {
            string query = "UPDATE identitas SET IDRULE = " + idRule + ", NAMAIDENTITAS = " + Nama + ", NOMORIDENTITAS = " + Nomor + " WHERE IDIDENTITAS = " + id + "; ";
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                //Execute query
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }

        //Delete identitas
        public void DeleteIdentitas(int id)
        {
            string query = "DELETE FROM identitas WHERE IDIDENTITAS = " + id + ";";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select identitas
        public List<string>[] SelectIdentitas(String Name)
        {
            string query = "SELECT identitas.*, rule.NAMERULE FROM identitas where NAMEIDENTITAS = " + Name + " JOIN rule on identitas.IDRULE = rule.IDRULE;";
            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
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
                    list[0].Add(dataReader["IDIDENTITAS"] + "");
                    list[1].Add(dataReader["NAMERULE"] + "");
                    list[2].Add(dataReader["NAMAIDENTITAS"] + "");
                    list[2].Add(dataReader["NOMORIDENTITAS"] + "");
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
        #endregion
    }
}
