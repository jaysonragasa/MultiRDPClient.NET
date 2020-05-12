using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextboxRequiredWrappers;

namespace MultiRemoteDesktopClient
{
    public partial class GroupManagerWindow : Form
    {
        TextboxRequiredWrapper trw = new TextboxRequiredWrapper();

        public GroupManagerWindow()
        {
            InitializeComponent();
            InitializeControls();
            InitializeControlEvents();
        }

        public void InitializeControls()
        {
            PopulateListView(this.lvGroups);

            lvGroups.AddControlForEmptyListItem(new object[] {
                btnDelete,
                btnUpdate
            });

            lvGroups.AddControlForItemSelection(new object[] {
                btnDelete,
                btnUpdate
            });
        }

        public void InitializeControlEvents()
        {
            this.lvGroups.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(lvGroups_ItemSelectionChanged);
            this.btnCreate.Click += new EventHandler(btnCreate_Click);
            this.btnUpdate.Click += new EventHandler(btnUpdate_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
        }

        void btnUpdate_Click(object sender, EventArgs e)
        {
            string group_name = CreateForm(string.Empty);
            if (group_name == null) { return; }

            int groupid = int.Parse(lvGroups.SelectedItems[0].Tag.ToString());
            Database.GroupDetails gd = new Database.GroupDetails();
            gd.GroupID = groupid;
            gd.GroupName = group_name;
            GlobalHelper.dbGroups.Save(false, gd);

            // let's just repopulate for a while
            PopulateListView(lvGroups);
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            //if (!trw.isAllFieldSet())
            //{
            //    MessageBox.Show("One of the required field is empty", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            ListViewItem item = lvGroups.SelectedItems[0];

            int server_count = int.Parse(item.SubItems[1].Text);
            int groupid = int.Parse(item.Tag.ToString());

            DialogResult dr = DialogResult.None;

            if (server_count != 0)
            {
                dr = MessageBox.Show("This group contains " + server_count + " servers and it's not advisable to delete.\r\nHowever, the servers in this group will be automatically moved to Unorganized group.\r\n\r\nAre you sure you want to continue", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    GlobalHelper.dbServers.Read();
                    foreach (Database.ServerDetails sd in GlobalHelper.dbServers.ArrayListServers)
                    {
                        if (groupid == sd.GroupID)
                        {
                            // move to Unorganized group;
                            GlobalHelper.dbServers.UpdateGroupIdByID(sd.UID, 0);
                        }
                    }
                }
                else
                {
                    return;
                }
            }

            if (dr == DialogResult.None)
            {
                dr = MessageBox.Show("Are you sure you want to delete this group '" + item.Text + "'", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (dr == DialogResult.Yes)
            {
                groupid = int.Parse(lvGroups.SelectedItems[0].Tag.ToString());
                GlobalHelper.dbGroups.DeleteByID(groupid);

                // let's just repopulate for a while
                PopulateListView(lvGroups);
            }
        }

        void btnCreate_Click(object sender, EventArgs e)
        {
            string group_name = CreateForm(string.Empty);
            if (group_name == null) { return; }

            Database.GroupDetails gd = new Database.GroupDetails();
            gd.GroupName = group_name;

            try
            {
                GlobalHelper.dbGroups.Save(true, gd);
            }
            catch (Database.DatabaseException ex)
            {
                if (ex.ExceptionType == Database.DatabaseException.ExceptionTypes.DUPLICATE_ENTRY)
                {
                    MessageBox.Show("A group by that name is already exist. Please give a different name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save the group due to error.\r\n\r\nError Message: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // let's just repopulate for a while
            PopulateListView(lvGroups);
        }

        void lvGroups_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //txGroup.Text = e.Item.Text;
        }

        string CreateForm(string TextboxValue)
        {
            string ret = string.Empty;

            #region create form
            using (Form f = new Form())
            {
                Label lbl = new Label();
                lbl.Text = "Group Name";
                lbl.Location = new Point(3, 3);
                TextBox txGroup = new TextBox();
                txGroup.Text = TextboxValue;
                txGroup.SelectAll();
                txGroup.Location = new Point(lbl.Location.X, lbl.Location.Y + lbl.Size.Height + 3);
                txGroup.Width = 200;
                Button btnCreate = new Button();
                btnCreate.Text = "Create";
                btnCreate.Location = new Point(lbl.Location.X, txGroup.Location.Y + txGroup.Size.Height + 3);
                btnCreate.Click += new EventHandler(delegate
                {
                    if (txGroup.Text == string.Empty)
                    {
                        MessageBox.Show("Please enter a group name", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ret = txGroup.Text;
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                });
                Button btnCancel = new Button();
                btnCancel.Text = "Cancel";
                btnCancel.Location = new Point(btnCreate.Location.X + btnCreate.Size.Width + 3, btnCreate.Location.Y);
                btnCancel.Click += new EventHandler(delegate
                {
                    ret = null;
                    f.DialogResult = DialogResult.Cancel;
                    f.Close();
                });

                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MaximizeBox = false;
                f.MinimizeBox = false;
                f.ControlBox = false;
                f.StartPosition = FormStartPosition.CenterParent;
                f.AcceptButton = btnCreate;
                f.CancelButton = btnCancel;
                f.ClientSize = new Size(
                    txGroup.Location.X + txGroup.Size.Width + 3,
                    btnCreate.Location.Y + btnCreate.Size.Height + 3
                );

                f.Controls.AddRange(new Control[] {
                    lbl, txGroup, btnCreate, btnCancel
                });

                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    f.Dispose();
                    return ret;
                }
            }
            #endregion

            return ret;
        }

        private void PopulateListView(ListView lv)
        {
            lv.Items.Clear();

            GlobalHelper.dbGroups.GetGroupsWithServerCount();
            foreach (Database.GroupDetails gd in GlobalHelper.dbGroups.ArrayListGroups)
            {
                ListViewItem item = new ListViewItem(gd.GroupName);
                item.SubItems.Add(gd.ServerCount.ToString());
                item.Tag = gd.GroupID;
                item.ImageIndex = 0;

                lv.Items.Add(item);
            }
        }
    }
}
