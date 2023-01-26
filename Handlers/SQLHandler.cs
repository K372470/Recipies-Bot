using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeleSharpPlus;

namespace ReceipeBot.Handlers
{
    public class SQLHandler
    {
        #region connection String
        const string TABLE_CREATE = "CREATE TABLE \"Sending\" (\"Id\" INTEGER NOT NULL,\"FirstName\" VARCHAR(20),PRIMARY KEY(\"Id\")) WITHOUT ROWID;";
        static readonly string connectionString = $@"Data Source={Environment.CurrentDirectory + "/Receipe.db"}";
        #endregion
        static SqliteConnection connection;

        public static async void Start()
        {
            try
            {
                connection = new SqliteConnection(connectionString);
                connection.Open();
                try
                {
                    SqliteCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = TABLE_CREATE;
                    command.ExecuteNonQuery();
                }
                catch { }
                Debug.Warn("DataBase Ready");
            }
            catch (Exception ex) { Debug.Error(ex.Message); }
        }
        public static void Close()
        {
            connection.Close();
        }
        public static void AddUser(long UserID, string FirstName)
        {
            try
            {
                string cmdString = string.Format("INSERT INTO Sending(Id,FirstName) VALUES({0}, \'{1}\')", UserID.ToString(), FirstName);
                SqliteCommand command = new SqliteCommand(cmdString, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.Warn(ex.Message);
            }
        }
        public static async void SpamToAll(string Message)
        {
            string cmdString = ("SELECT * FROM Sending");
            SqliteCommand command = new SqliteCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    var ID = reader.GetFieldValue<long>(0);
                    var Name = reader.GetFieldValue<string>(1);
                    new DBUser(ID, Name).SendMessage(string.Format(Message, Name));
                    await Task.Delay(3000);
                }
        }
    }
    public class DBUser
    {
        public void SendMessage(string Message)
        {
            BotClient.SendMessage(Id, Message);
        }
        public DBUser(long ID, string FName)
        {
            this.Id = ID;
            this.FirstName = FName;
        }
        public DBUser(long ID)
        {
            this.Id = ID;
            this.FirstName = null;
        }
        long Id;
        public string FirstName;
    }
}
