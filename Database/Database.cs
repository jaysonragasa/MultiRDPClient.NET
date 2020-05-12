using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Database
{
    public class Database
    {
        string _connectionString = string.Empty;
        string db_name = string.Empty;

        SQLiteConnection _connection = new SQLiteConnection();
        SQLiteCommand _command = new SQLiteCommand();

        public Database()
        {
            db_name = ConfigurationManager.AppSettings["DatbaseFilepath"].ToString();
            db_name = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, db_name);

            this._connectionString = ConfigurationManager.AppSettings["connection"].ToString();
            this._connectionString = string.Format(_connectionString, db_name);

            this._connection.ConnectionString = this._connectionString;

            if (!System.IO.File.Exists(db_name))
            {
                SQLiteConnection.CreateFile(db_name);
                _connection.Open();
                _connection.Close();
            }
        }

        public void Delete(bool all)
        {
            string dbpath = System.IO.Path.GetDirectoryName(db_name);
            try
            {
                foreach (string f in System.IO.Directory.GetFiles(dbpath))
                {
                    if (!all)
                    {
                        if (f.ToLower() != this.db_name.ToLower())
                        {
                            System.IO.File.Delete(f);
                        }
                    }
                    else
                    {
                        System.IO.File.Delete(f);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void ResetDatabase()
        {
            Delete(true);

            System.Diagnostics.Debug.WriteLine("reseting database");
            ExecuteNonQuery(DefaultDataAndSchema.sql, null);
            System.Diagnostics.Debug.WriteLine("done");
        }

        public string ExecuteQuery(string sql_query, SQLiteParameter[] parameters, out SQLiteDataReader reader)
        {
            string res = string.Empty;

            if (_connection.State == ConnectionState.Open)
            {
                CloseConnection();
            }

            reader = null;

            _connection.Open();
            _command = _connection.CreateCommand();
            _command.CommandText = sql_query;

            if (parameters != null)
            {
                _command.Parameters.Clear();
                _command.Parameters.AddRange(parameters);
            }

            try
            {
                reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
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

            if (_connection.State == ConnectionState.Open)
            {
                CloseConnection();
            }

            _connection.Open();
            _command = _connection.CreateCommand();
            _command.CommandText = sql_query;

            if (parameters != null)
            {
                _command.Parameters.AddRange(parameters);
            }

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR! " + ex.Message + "\r\n" + ex.StackTrace);
                res = CreateExceptionMessage(ex);
            }
            
            _connection.Close();

            return res;
        }

        public void CloseConnection()
        {
            _connection.Close();
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
