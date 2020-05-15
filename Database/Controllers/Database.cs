using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Database
{
    public class SQLiteDataSource
    {
        string _connectionString = string.Empty;
        string _db_name = string.Empty;

        public SQLiteConnection Connection { get; set; } = new SQLiteConnection();
        public SQLiteCommand Command { get; set; } = new SQLiteCommand();
        public SQLiteDataReader Reader { get; set; } = null;

        static Lazy<SQLiteDataSource> _database = new Lazy<SQLiteDataSource>(() =>
        {
            var db = new SQLiteDataSource();
            db.Initialize();

            return db;
        });

        public static SQLiteDataSource I => _database.Value;

        public void Initialize()
        {
            _db_name = ConfigurationManager.AppSettings["DatbaseFilepath"].ToString();
            _db_name = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, _db_name);

            _connectionString = ConfigurationManager.AppSettings["connection"].ToString();
            _connectionString = string.Format(_connectionString, _db_name);

            this.Connection.ConnectionString = _connectionString;
        }

        public void CloseConnection()
        {
            this.Reader.Close();
            this.Command.Dispose();
            this.Connection.Close();
        }
    }

    public class SQLiteDataController
    {
        public SQLiteDataSource Database => SQLiteDataSource.I;

        //public void Delete(bool all)
        //{
        //    string dbpath = System.IO.Path.GetDirectoryName(db_name);
        //    try
        //    {
        //        foreach (string f in System.IO.Directory.GetFiles(dbpath))
        //        {
        //            if (!all)
        //            {
        //                if (f.ToLower() != this.db_name.ToLower())
        //                {
        //                    System.IO.File.Delete(f);
        //                }
        //            }
        //            else
        //            {
        //                System.IO.File.Delete(f);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //}

        //public void ResetDatabase()
        //{
        //    Delete(true);

        //    System.Diagnostics.Debug.WriteLine("reseting database");
        //    ExecuteNonQuery(DefaultDataAndSchema.sql, null);
        //    System.Diagnostics.Debug.WriteLine("done");
        //}

        public string ExecuteQuery(string sql_query, SQLiteParameter[] parameters)
        {
            string res = string.Empty;

            this.Database.Connection.Open();

            this.Database.Command = this.Database.Connection.CreateCommand();
            this.Database.Command.CommandText = sql_query;
            this.Database.Command.Parameters.Clear();

            if (parameters != null)
                this.Database.Command.Parameters.AddRange(parameters);

            try
            {
                this.Database.Reader = this.Database.Command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                res = CreateExceptionMessage(ex);
            }

            return res;
        }

        public string ExecuteNonQuery(string sql_query, SQLiteParameter[] parameters)
        {
            string res = string.Empty;

            this.Database.Connection.Open();

            this.Database.Command = this.Database.Connection.CreateCommand();
            this.Database.Command.CommandText = sql_query;
            this.Database.Command.Parameters.Clear();

            if (parameters != null)
                this.Database.Command.Parameters.AddRange(parameters);

            try
            {
                this.Database.Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR! " + ex.Message + "\r\n" + ex.StackTrace);
                res = CreateExceptionMessage(ex);
            }

            //this.Database.CloseConnection();

            return res;
        }

        private string CreateExceptionMessage(Exception ex)
        {
            string err = string.Empty;

            err = @"An error occured while executing a query.
Exception Message: {0}
Stack Trace:
{1}";
            err = string.Format(err, ex.Message, ex.StackTrace);

            return err;
        }
    }

    internal class DefaultDataAndSchema
    {
        public static string sql = @"
DROP TABLE IF EXISTS Groups;

CREATE TABLE [Groups] (
[groupid] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[group_name] varchar(50)  UNIQUE NOT NULL
);

DROP TABLE IF EXISTS Servers;

CREATE TABLE [Servers] (
[uid] varchar(20)  UNIQUE NOT NULL,
[groupid] integer  NOT NULL,
[servername] varchar(50)  UNIQUE NOT NULL,
[server] varchar(15)  NOT NULL,
[domain] varchar(255),
[port] integer  NOT NULL,
[username] varchar(50)  NOT NULL,
[password] varchar(1000)  NOT NULL,
[description] varchar(255)  NOT NULL,
[colordepth] integer  NOT NULL,
[desktopwidth] integer  NOT NULL,
[desktopheight] integer  NOT NULL,
[fullscreen] integer  NOT NULL
);

DROP VIEW IF EXISTS viewGroupsWithServerCount;

CREATE VIEW [viewGroupsWithServerCount] AS 
select
   Groups.groupid,
   Groups.group_name,
   (select count(Servers.uid) from Servers where Servers.groupid = Groups.groupid) as ServerCount
from
   Groups;
   
/*
  insert default datas
*/

INSERT INTO Groups(group_name) VALUES('Uncategorized');
INSERT INTO Groups(group_name) VALUES('Application Servers');
INSERT INTO Groups(group_name) VALUES('Web Servers');";
    }
}
