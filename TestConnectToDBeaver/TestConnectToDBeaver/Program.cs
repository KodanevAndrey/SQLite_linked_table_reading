using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Reflection.Emit;
using System.Data;

namespace TestConnectToDBeaver
{
    public class DBHelper
    {
        private SQLiteConnection m_dbConn = new SQLiteConnection();
        private SQLiteCommand m_sqlCmd = new SQLiteCommand();
        private string FileLocation = "DBeaverTest.sqlite";

        public void ReadDB()
        {
            try
            {
                string connectionString = "Data Source=" + FileLocation + ";Version=3;";
                Console.WriteLine("Connected!");
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для получения данных из связанных таблиц

                    string query = @"
                        SELECT a.id, a.userName, b.city, c.Type
                        AS BasicTable, b.city, c.Type
                        FROM BasicTable a
                        JOIN CityTable b ON a.city = b.id
                        JOIN TypeTable c ON a.Type = c.id
                        ;";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string userID = reader["id"].ToString();
                                string userName = reader["userName"].ToString();
                                string city = reader["city"].ToString();
                                string type = reader["Type"].ToString();
                                Console.WriteLine($"UserID: {userID}, userName: {userName}, City: {city}, Type: {type}");
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error ConnectDB: " + ex.Message);
            }
        }
        public void ConnectDB()
        {
            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + FileLocation + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;
                Console.WriteLine("Connected!");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error ConnectDB: " + ex.Message);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DBHelper db = new DBHelper();
            //db.ConnectDB();
            db.ReadDB();
            Console.ReadKey();
        }
    }
}
