using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Database
{
    public class GroupDetails
    {
        int _groupID = 0;
        string _group_name = string.Empty;
        int _serverCount = 0;

        public GroupDetails()
        {
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

        public string GroupName
        {
            set
            {
                this._group_name = value;
            }
            get
            {
                return this._group_name;
            }
        }

        public int ServerCount
        {
            set
            {
                this._serverCount = value;
            }
            get
            {
                return this._serverCount;
            }
        }
    }

    public class Groups : Database
    {
        ArrayList _alGroups = new ArrayList();

        public Groups()
        {
        }

        public ArrayList ArrayListGroups
        {
            get
            {
                return this._alGroups;
            }
        }

        public void Read()
        {
            string sql = "SELECT groupid, group_name FROM Groups";

            SQLiteDataReader reader;
            string result = ExecuteQuery(sql, null, out reader);

            this._alGroups.Clear();

            if (result == string.Empty)
            {
                while (reader.Read())
                {
                    GroupDetails gd = new GroupDetails();
                    gd.GroupID = int.Parse(reader["groupid"].ToString());
                    gd.GroupName = reader["group_name"].ToString();

                    this._alGroups.Add(gd);
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

        public void Save(bool isNew, GroupDetails group_details)
        {
            if (isNew)
            {
                Save(group_details);
            }
            else
            {
                Update(group_details);
            }
        }

        private void Save(GroupDetails group_details)
        {
            string sql = "INSERT INTO Groups(groupid, group_name) VALUES((select count(Groups.groupid) from groups) + 1, @gname);";
            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@gname", group_details.GroupName)
                                           };

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

        private void Update(GroupDetails group_details)
        {
            string sql = "UPDATE Groups SET groupid = @gid, group_name = @gname WHERE groupid = @gid";
            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@gid", group_details.GroupID),
                                               new SQLiteParameter("@gname", group_details.GroupName)
                                           };

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

        public void DeleteByID(int id)
        {
            string sql = "DELETE FROM Groups WHERE Groups.groupid = @gid";
            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@gid", id)
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

            CloseConnection();
        }

        public string GetGroupNameByID(int id)
        {
            string ret = string.Empty;

            string SQL = "SELECT group_name FROM Groups WHERE groupid = @gid";
            SQLiteParameter[] paramters = {
                                              new SQLiteParameter("@gid", id)
                                          };

            SQLiteDataReader reader;
            string result = ExecuteQuery(SQL, paramters, out reader);

            if (result == string.Empty)
            {
                reader.Read();
                ret = reader["group_name"].ToString();
            }
            else
            {
                CloseConnection();
                throw new Exception(result);
            }

            CloseConnection();

            return ret;
        }

        public int GetIDByGroupName(string name)
        {
            int ret = 0;

            string SQL = "SELECT groupid FROM Groups WHERE group_name = @gname";
            SQLiteParameter[] paramters = {
                                              new SQLiteParameter("@gname", name)
                                          };

            SQLiteDataReader reader;
            string result = ExecuteQuery(SQL, paramters, out reader);

            if (result == string.Empty)
            {
                reader.Read();
                ret = int.Parse(reader["groupid"].ToString());
            }
            else
            {
                CloseConnection();
                throw new Exception(result);
            }

            CloseConnection();

            return ret;
        }

        public void GetGroupsWithServerCount()
        {
            string sql = "select * from viewGroupsWithServerCount";

            SQLiteDataReader reader;
            string result = ExecuteQuery(sql, null, out reader);

            if (result == string.Empty)
            {
                this._alGroups.Clear();

                while (reader.Read())
                {
                    GroupDetails gd = new GroupDetails();
                    gd.GroupID = int.Parse(reader["groupid"].ToString());
                    gd.GroupName = reader["group_name"].ToString();
                    gd.ServerCount = int.Parse(reader["ServerCount"].ToString());

                    this._alGroups.Add(gd);
                }
            }
            else
            {
                CloseConnection();
                throw new Exception(result);
            }

            CloseConnection();
        }
    }
}
