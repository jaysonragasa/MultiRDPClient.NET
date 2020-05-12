using System;
using System.Drawing;
using System.Windows.Forms;
using TextboxRequiredWrappers;

namespace MultiRemoteDesktopClient
{
    public delegate void ApplySettings(object sender, Database.ServerDetails sd);
    public delegate Rectangle GetClientWindowSize();

    public partial class ServerSettingsWindow : Form
    {
        public event ApplySettings ApplySettings;
        public event GetClientWindowSize GetClientWindowSize;

        TextboxRequiredWrapper trw = new TextboxRequiredWrapper();
        Database.ServerDetails oldSD;

        private bool isUpdating = false;

        public ServerSettingsWindow()
        {
            InitializeComponent();
            InitializeControls();
            InitializeControlEvents();
        }

        public ServerSettingsWindow(Database.ServerDetails sd)
        {
            InitializeComponent();
            InitializeControls(sd);
            InitializeControlEvents();
        }

        public void InitializeControls()
        {
            trw.AddRange(new Control[] {
                txServername,
                txComputer,
                txUsername,
                txDescription,
                ddGroup
            });

            tbDeskSize.Value = 4;
            tbDeskSize_Scroll(tbDeskSize, null);

            tbColor.Value = 2;
            tbColor_Scroll(tbColor, null);

            this.isUpdating = false;

            btnGetClientWinS.Enabled = false;

            GlobalHelper.PopulateGroupsDropDown(ddGroup, string.Empty);
        }

        public void InitializeControls(Database.ServerDetails sd)
        {
            this.oldSD = sd;

            trw.AddRange(new Control[] {
                txServername,
                txComputer,
                txUsername,
                txDescription
            });

            txServername.Text = sd.ServerName;
            txComputer.Text = sd.Server;
            txDomain.Text = sd.Domain;
            txPort.Text = sd.Port.ToString();
            txUsername.Text = sd.Username;
            txPassword.Text = sd.Password;
            txDescription.Text = sd.Description;

            switch (sd.ColorDepth)
            {
                case 24:
                    tbColor.Value = 1;
                    break;
                case 16:
                    tbColor.Value = 2;
                    break;
                case 15:
                    tbColor.Value = 3;
                    break;
                case 8:
                    tbColor.Value = 4;
                    break;
            }
            tbColor_Scroll(tbColor, null);

            if (sd.DesktopWidth == 640 && sd.DesktopHeight == 480)
            {
                tbDeskSize.Value = 1;
            }
            else if (sd.DesktopWidth == 800 && sd.DesktopHeight == 600)
            {
                tbDeskSize.Value = 2;
            }
            else if (sd.DesktopWidth == 1024 && sd.DesktopHeight == 768)
            {
                tbDeskSize.Value = 3;
            }
            else if (sd.DesktopWidth == 1120 && sd.DesktopHeight == 700)
            {
                tbDeskSize.Value = 4;
            }
            else if (sd.DesktopWidth == 1152 && sd.DesktopHeight == 864)
            {
                tbDeskSize.Value = 5;
            }
            else if (sd.DesktopWidth == 1280 && sd.DesktopHeight == 800)
            {
                tbDeskSize.Value = 6;
            }
            else if (sd.DesktopWidth == 1280 && sd.DesktopHeight == 1024)
            {
                tbDeskSize.Value = 7;
            }
            tbColor_Scroll(tbColor, null);

            txWidth.Text = sd.DesktopWidth.ToString();
            txHeight.Text = sd.DesktopHeight.ToString();
            cbFullscreen.Checked = sd.Fullscreen;

            this.isUpdating = true;

            GlobalHelper.PopulateGroupsDropDown(ddGroup, GlobalHelper.dbGroups.GetGroupNameByID(sd.GroupID));

            btnGetClientWinS.Enabled = true;
        }

        void ddGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddGroup.SelectedIndex == ddGroup.Items.Count - 1)
            {
                GroupManagerWindow gmw = new GroupManagerWindow();
                gmw.ShowDialog();
                GlobalHelper.PopulateGroupsDropDown(ddGroup, string.Empty);
            }
        }

        public void InitializeControlEvents()
        {
            tbDeskSize.Scroll += new EventHandler(tbDeskSize_Scroll);
            tbColor.Scroll += new EventHandler(tbColor_Scroll);

            btnSave.Click += new EventHandler(btnSave_Click);

            btnGetClientWinS.Click += new EventHandler(btnGetClientWinS_Click);

            ddGroup.SelectedIndexChanged += new EventHandler(ddGroup_SelectedIndexChanged);
        }

        void btnGetClientWinS_Click(object sender, EventArgs e)
        {
            if (GetClientWindowSize != null)
            {
                Rectangle r = GetClientWindowSize();
                txWidth.Text = r.Width.ToString();
                txHeight.Text = r.Height.ToString();
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!trw.isAllFieldSet())
            {
                MessageBox.Show("One of the required field is empty", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Database.ServerDetails sd = new Database.ServerDetails();
            sd.GroupID = GlobalHelper.dbGroups.GetIDByGroupName(ddGroup.Text);
            sd.ServerName = txServername.Text;
            sd.Server = txComputer.Text;
            sd.Domain = txDomain.Text;
            sd.Port = int.Parse(txPort.Text == string.Empty ? "0" : txPort.Text);
            sd.Username = txUsername.Text;
            sd.Password = txPassword.Text;
            sd.Description = txDescription.Text;
            sd.ColorDepth = (int)lblColorDepth.Tag;
            sd.DesktopWidth = int.Parse(txWidth.Text);
            sd.DesktopHeight = int.Parse(txHeight.Text);
            sd.Fullscreen = cbFullscreen.Checked;
            
            try
            {
                if (this.isUpdating)
                {
                    // pass our old UID to new UID for saving
                    sd.UID = oldSD.UID;
                    
                    GlobalHelper.dbServers.Save(false, sd);
                    if (sd.Password != string.Empty)
                    {
                        sd.Password = RijndaelSettings.Decrypt(sd.Password);
                    }

                    // new settings changed
                    // pass new settings on our oldSD
                    this.oldSD = sd;

                    DialogResult dr = MessageBox.Show("Conection settings successfully updated.\r\nDo you want to apply your current changes.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        if (ApplySettings != null)
                        {
                            ApplySettings(sender, this.oldSD);
                        }
                    }

                    this.Close();
                }
                else
                {
                    sd.UID = DateTime.Now.Ticks.ToString();
                    GlobalHelper.dbServers.Save(true, sd);

                    MessageBox.Show("New conenction settings successfully saved", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Database.DatabaseException settingEx)
            {
                if (settingEx.ExceptionType == Database.DatabaseException.ExceptionTypes.DUPLICATE_ENTRY)
                {
                    MessageBox.Show("Can't save your connection settings due to duplicate entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        void tbColor_Scroll(object sender, EventArgs e)
        {
            switch (tbColor.Value)
            {
                case 1:
                    pictureColor.BackgroundImage = imageList1.Images[0];
                    lblColorDepth.Text = "True Color (24 bit)";
                    lblColorDepth.Tag = 24;
                    break;
                case 2:
                    pictureColor.BackgroundImage = imageList1.Images[1];
                    lblColorDepth.Text = "High Color (16 bit)";
                    lblColorDepth.Tag = 16;
                    break;
                case 3:
                    pictureColor.BackgroundImage = imageList1.Images[1];
                    lblColorDepth.Text = "High Color (15 bit)";
                    lblColorDepth.Tag = 15;
                    break;
                case 4:
                    pictureColor.BackgroundImage = imageList1.Images[2];
                    lblColorDepth.Text = "256 Colors";
                    lblColorDepth.Tag = 8;
                    break;
            }
        }

        void tbDeskSize_Scroll(object sender, EventArgs e)
        {
            switch (tbDeskSize.Value)
            {
                case 1:
                    lblResolution.Text = "640 by 480 pixels";
                    txWidth.Text = "640";
                    txHeight.Text = "480";

                    break;

                case 2:
                    lblResolution.Text = "800 by 600 pixels";
                    txWidth.Text = "800";
                    txHeight.Text = "600";

                    break;

                case 3:
                    lblResolution.Text = "1024 by 768 pixels";
                    txWidth.Text = "1024";
                    txHeight.Text = "768";

                    break;

                case 4:
                    lblResolution.Text = "1120 by 700 pixels";
                    txWidth.Text = "1120";
                    txHeight.Text = "700";

                    break;

                case 5:
                    lblResolution.Text = "1152 by 864 pixels";
                    txWidth.Text = "1152";
                    txHeight.Text = "864";

                    break;

                case 6:
                    lblResolution.Text = "1280 by 800 pixels";
                    txWidth.Text = "1280";
                    txHeight.Text = "800";

                    break;
                case 7:
                    lblResolution.Text = "1280 by 1024 pixels";
                    txWidth.Text = "1280";
                    txHeight.Text = "1024";

                    break;
            }
        }

        public Database.ServerDetails CurrentServerSettings()
        {
            return oldSD;
        }
    }
}
