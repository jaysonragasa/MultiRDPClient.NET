using Database.Models;
using System;
using System.Collections;
using System.Data.SQLite;

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
                    Model_ServerDetails sd = new Model_ServerDetails();
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

        public void Save(bool isNew, Model_ServerDetails server_details)
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

        private void Save(Model_ServerDetails server_details)
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

        private void Update(Model_ServerDetails server_details)
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
}
