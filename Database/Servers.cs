using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Database
{
    public class Servers : Database
    {
        ArrayList _alServes = new ArrayList();

        public Servers()
        {
        }

        public ArrayList ArrayListServers
        {
            get
            {
                return this._alServes;
            }
        }

        public void Read()
        {
            string sql = "SELECT * FROM Servers";

            //using (SQLiteConnection conn = new SQLiteConnection())
            //{

            //}

            SQLiteDataReader reader;
            string result = ExecuteQuery(sql, null, out reader);

            this._alServes.Clear();

            if (result == string.Empty)
            {
                while (reader.Read())
                {
                    ServerDetails sd = new ServerDetails();
                    sd.UID = reader["uid"].ToString();
                    sd.GroupID = int.Parse(reader["groupid"].ToString());
                    sd.ServerName = reader["servername"].ToString();
                    sd.Server = reader["server"].ToString();
                    sd.Domain = reader["domain"].ToString();
                    sd.Port = int.Parse(reader["port"].ToString());
                    sd.Username = reader["username"].ToString();

                    string pword = reader["password"].ToString();
                    if (pword != string.Empty) { pword = RijndaelSettings.Decrypt(pword); }

                    sd.Password = pword;

                    sd.Description = reader["description"].ToString();
                    sd.ColorDepth = int.Parse(reader["colordepth"].ToString());
                    sd.DesktopWidth = int.Parse(reader["desktopwidth"].ToString());
                    sd.DesktopHeight = int.Parse(reader["desktopheight"].ToString());
                    sd.Fullscreen = int.Parse(reader["fullscreen"].ToString()) == 1 ? true : false;

                    this._alServes.Add(sd);
                }
            }
            else
            {
                CloseConnection();
                System.Diagnostics.Debug.WriteLine(result);
                throw new Exception(result);
            }

            CloseConnection();
        }

        public void Save(bool isNew, ServerDetails server_details)
        {
            if (isNew)
            {
                Save(server_details);
            }
            else
            {
                Update(server_details);
            }
        }

        private void Save(ServerDetails server_details)
        {
            #region sql
            string sql = "INSERT INTO Servers(uid, groupid, servername, server, domain, port, username, password, description, colordepth, desktopwidth, desktopheight, fullscreen) ";
            sql += "VALUES(@uid, @gid, @sname, @server, @domain, @port, @uname, @pword, @desc, @cdepth, @dwidth, @dheight, @fscreen)";
            #endregion

            #region params
            if (server_details.Password != string.Empty) { server_details.Password = RijndaelSettings.Encrypt(server_details.Password); }

            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@uid", server_details.UID),
                                               new SQLiteParameter("@gid", server_details.GroupID),
                                               new SQLiteParameter("@sname", server_details.ServerName),
                                               new SQLiteParameter("@server", server_details.Server),
                                               new SQLiteParameter("@domain", server_details.Domain),
                                               new SQLiteParameter("@port", server_details.Port),
                                               new SQLiteParameter("@uname", server_details.Username),
                                               new SQLiteParameter("@pword", server_details.Password),
                                               new SQLiteParameter("@desc", server_details.Description),
                                               new SQLiteParameter("@cdepth", server_details.ColorDepth),
                                               new SQLiteParameter("@dwidth", server_details.DesktopWidth),
                                               new SQLiteParameter("@dheight", server_details.DesktopHeight),
                                               new SQLiteParameter("@fscreen", server_details.Fullscreen)
                                           };
            #endregion

            string result = ExecuteNonQuery(sql, parameters);

            if (result == string.Empty)
            {
            }
            else
            {
                CloseConnection();
                System.Diagnostics.Debug.WriteLine(result);

                if (result.Contains("Abort due to constraint violation"))
                {
                    throw new DatabaseException(DatabaseException.ExceptionTypes.DUPLICATE_ENTRY);
                }
                else
                {
                    throw new Exception(result);
                }
            }

            CloseConnection();
        }

        private void Update(ServerDetails server_details)
        {
            #region sql
            string sql = @"
UPDATE 
    Servers 
SET 
    uid=@uid, 
    groupid=@gid,
    servername=@sname, 
    server=@server,
    domain=@domain,
    port=@port,
    username=@uname,
    password=@pword,
    description=@desc,
    colordepth=@cdepth, 
    desktopwidth=@dwidth,
    desktopheight=@dheight,
    fullscreen=@fscreen
WHERE
    uid=@uid";
            #endregion

            #region params
            if (server_details.Password != string.Empty) { server_details.Password = RijndaelSettings.Encrypt(server_details.Password); }

            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@uid", server_details.UID),
                                               new SQLiteParameter("@gid", server_details.GroupID),
                                               new SQLiteParameter("@sname", server_details.ServerName),
                                               new SQLiteParameter("@server", server_details.Server),
                                               new SQLiteParameter("@domain", server_details.Domain),
                                               new SQLiteParameter("@port", server_details.Port),
                                               new SQLiteParameter("@uname", server_details.Username),
                                               new SQLiteParameter("@pword", server_details.Password),
                                               new SQLiteParameter("@desc", server_details.Description),
                                               new SQLiteParameter("@cdepth", server_details.ColorDepth),
                                               new SQLiteParameter("@dwidth", server_details.DesktopWidth),
                                               new SQLiteParameter("@dheight", server_details.DesktopHeight),
                                               new SQLiteParameter("@fscreen", server_details.Fullscreen)
                                           };
            #endregion

            string result = ExecuteNonQuery(sql, parameters);

            if (result == string.Empty)
            {
            }
            else
            {
                CloseConnection();
                System.Diagnostics.Debug.WriteLine(result);

                if (result.Contains("Abort due to constraint violation"))
                {
                    throw new DatabaseException(DatabaseException.ExceptionTypes.DUPLICATE_ENTRY);
                }
                else
                {
                    throw new Exception(result);
                }
            }

            CloseConnection();
        }

        public void UpdateGroupIdByID(string id, int newGroupID)
        {
            string sql = "UPDATE Servers SET groupid = @gid WHERE Servers.uid = @uid";
            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@gid", newGroupID),
                                               new SQLiteParameter("@uid", id)
                                           };

            string result = ExecuteNonQuery(sql, parameters);

            if (result == string.Empty)
            {

            }
            else
            {
                CloseConnection();
                throw new Exception(result);
            }

            CloseConnection();
        }

        public void DeleteByID(string id)
        {
            string sql = "DELETE FROM Servers WHERE Servers.uid=@uid";

            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@uid", id)
                                           };

            string result = ExecuteNonQuery(sql, parameters);

            if (result == string.Empty)
            {

            }
            else
            {
                CloseConnection();
                System.Diagnostics.Debug.WriteLine(result);
                throw new Exception(result);
            }
        }
    }

    public class ServerDetails
    {
        string _uid = string.Empty;
        string _serverName = string.Empty;
        string _server = string.Empty;
        string _domain = string.Empty;
        int _port = 0;
        string _username = string.Empty;
        string _password = string.Empty;
        string _description = string.Empty;

        int _colorDepth = 0;
        int _desktopWidth = 0;
        int _desktopHeight = 0;
        bool _fullScreen = false;

        int _groupID = 0;

        public ServerDetails()
        {
        }

        public string UID
        {
            set
            {
                this._uid = value;
            }
            get
            {
                return this._uid;
            }
        }

        public string ServerName
        {
            set
            {
                this._serverName = value;
            }
            get
            {
                return this._serverName;
            }
        }

        public string Server
        {
            set
            {
                this._server = value;
            }
            get
            {
                return this._server;
            }
        }

        public string Domain
        {
            set
            {
                this._domain = value;
            }
            get
            {
                return this._domain;
            }
        }

        public int Port
        {
            set
            {
                this._port = value;
            }
            get
            {
                return this._port;
            }
        }

        public string Username
        {
            set
            {
                this._username = value;
            }
            get
            {
                return this._username;
            }
        }

        public string Password
        {
            set
            {
                string val = value;

                if (val != string.Empty)
                {
                    //val = RijndaelSettings.Encrypt(val);
                }

                this._password = val;
            }
            get
            {
                if (this._password != string.Empty)
                {
                    //this._password = RijndaelSettings.Decrypt(this._password);
                }

                return this._password;
            }
        }

        public string Description
        {
            set
            {
                this._description = value;
            }
            get
            {
                return this._description;
            }
        }

        public int ColorDepth
        {
            set
            {
                this._colorDepth = value;
            }
            get
            {
                return this._colorDepth;
            }
        }

        public int DesktopWidth
        {
            set
            {
                this._desktopWidth = value;
            }
            get
            {
                return this._desktopWidth;
            }
        }

        public int DesktopHeight
        {
            set
            {
                this._desktopHeight = value;
            }
            get
            {
                return this._desktopHeight;
            }
        }

        public bool Fullscreen
        {
            set
            {
                this._fullScreen = value;
            }
            get
            {
                return this._fullScreen;
            }
        }

        public int GroupID
        {
            set
            {
                this._groupID = value;
            }
            get
            {
                return this._groupID;
            }
        }
    }
}
