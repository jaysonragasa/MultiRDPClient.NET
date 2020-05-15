using Database.Models;
using System;
using System.Collections;
using System.Data.SQLite;

namespace Database
{
    public class Groups : SQLiteDataController
    {
        public Groups()
        {
        }

        public ArrayList ArrayListGroups { get; set; } = new ArrayList();

        public void Read()
        {
            string sql = "SELECT groupid, group_name FROM Groups";

            string result = base.ExecuteQuery(sql, null);

            this.ArrayListGroups.Clear();

            if (result == string.Empty)
            {
                while (base.Database.Reader.Read())
                {
                    Model_GroupDetails gd = new Model_GroupDetails();
                    gd.GroupID = int.Parse(base.Database.Reader["groupid"].ToString());
                    gd.GroupName = base.Database.Reader["group_name"].ToString();

                    this.ArrayListGroups.Add(gd);
                }
            }
            else
            {
                base.Database.CloseConnection();
                System.Diagnostics.Debug.WriteLine(result);
                throw new Exception(result);
            }

            base.Database.CloseConnection();
        }

        public void Save(bool isNew, Model_GroupDetails group_details)
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

        private void Save(Model_GroupDetails group_details)
        {
            string sql = "INSERT INTO Groups(groupid, group_name) VALUES((select count(Groups.groupid) from groups) + 1, @gname);";
            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@gname", group_details.GroupName)
                                           };

            string result = base.ExecuteNonQuery(sql, parameters);

            if (result == string.Empty)
            {
            }
            else
            {
                base.Database.CloseConnection();
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

            base.Database.CloseConnection();
        }

        private void Update(Model_GroupDetails group_details)
        {
            string sql = "UPDATE Groups SET groupid = @gid, group_name = @gname WHERE groupid = @gid";
            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@gid", group_details.GroupID),
                                               new SQLiteParameter("@gname", group_details.GroupName)
                                           };

            string result = base.ExecuteNonQuery(sql, parameters);

            if (result == string.Empty)
            {
            }
            else
            {
                base.Database.CloseConnection();
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

            base.Database.CloseConnection();
        }

        public void DeleteByID(int id)
        {
            string sql = "DELETE FROM Groups WHERE Groups.groupid = @gid";
            SQLiteParameter[] parameters = {
                                               new SQLiteParameter("@gid", id)
                                           };

            string result = base.ExecuteNonQuery(sql, parameters);

            if (result == string.Empty)
            {
            }
            else
            {
                base.Database.CloseConnection();
                System.Diagnostics.Debug.WriteLine(result);
                throw new Exception(result);
            }

            base.Database.CloseConnection();
        }

        public string GetGroupNameByID(int id)
        {
            string ret = string.Empty;

            string SQL = "SELECT group_name FROM Groups WHERE groupid = @gid";
            SQLiteParameter[] paramters = {
                                              new SQLiteParameter("@gid", id)
                                          };

            string result = base.ExecuteQuery(SQL, paramters);

            if (result == string.Empty)
            {
                if (base.Database.Reader.HasRows)
                {
                    base.Database.Reader.Read();
                    ret = base.Database.Reader["group_name"].ToString();
                }
            }
            else
            {
                base.Database.CloseConnection();
                throw new Exception(result);
            }

            base.Database.CloseConnection();

            return ret;
        }

        public int GetIDByGroupName(string name)
        {
            int ret = 0;

            string SQL = "SELECT groupid FROM Groups WHERE group_name = @gname";
            SQLiteParameter[] paramters = {
                                              new SQLiteParameter("@gname", name)
                                          };

            string result = base.ExecuteQuery(SQL, paramters);

            if (result == string.Empty)
            {
                if (base.Database.Reader.HasRows)
                {
                    base.Database.Reader.Read();
                    ret = int.Parse(base.Database.Reader["groupid"].ToString());
                }
            }
            else
            {
                base.Database.CloseConnection();
                throw new Exception(result);
            }

            base.Database.CloseConnection();

            return ret;
        }

        public void GetGroupsWithServerCount()
        {
            string sql = "select * from viewGroupsWithServerCount";

            string result = base.ExecuteQuery(sql, null);

            if (result == string.Empty)
            {
                if (base.Database.Reader.HasRows)
                {
                    this.ArrayListGroups.Clear();

                    while (base.Database.Reader.Read())
                    {
                        Model_GroupDetails gd = new Model_GroupDetails();
                        gd.GroupID = int.Parse(base.Database.Reader["groupid"].ToString());
                        gd.GroupName = base.Database.Reader["group_name"].ToString();
                        gd.ServerCount = int.Parse(base.Database.Reader["ServerCount"].ToString());

                        this.ArrayListGroups.Add(gd);
                    }
                }
            }
            else
            {
                base.Database.CloseConnection();
                throw new Exception(result);
            }

            base.Database.CloseConnection();
        }
    }
}