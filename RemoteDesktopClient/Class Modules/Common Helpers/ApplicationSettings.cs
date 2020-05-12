using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.IO;

namespace MultiRemoteDesktopClient
{
    public class ApplicationSettings
    {
        bool _isAppConfigExists = false;
        ExeConfigurationFileMap _exeFileMap = new ExeConfigurationFileMap();
        Configuration config = null;
        public SettingsModel Settings = new SettingsModel();

        public ApplicationSettings()
        {
            this._exeFileMap.ExeConfigFilename = Path.Combine(Application.StartupPath, "ApplicationSettings.config");
            config = ConfigurationManager.OpenMappedExeConfiguration(this._exeFileMap, ConfigurationUserLevel.None);

            if (!File.Exists(this._exeFileMap.ExeConfigFilename))
            {
                this._isAppConfigExists = false;
                this.Settings.Password = "pass";
                Save();
            }
            else
            {
                this._isAppConfigExists = true;
                Read();
            }
        }

        public bool IsAppConfigExists()
        {
            return this._isAppConfigExists;
        }

        public void Read()
        {
            this.Settings = config.GetSection("SettingsModel") as SettingsModel;
        }

        public bool Save()
        {
            bool ret = false;

            try
            {
                SettingsModel smodel = new SettingsModel();
                smodel.Password = this.Settings.Password;
                smodel.HideWhenMinimized = this.Settings.HideWhenMinimized;
                smodel.HideInformationPopupWindow = this.Settings.HideInformationPopupWindow;

                config.Sections.Remove("SettingsModel");
                config.Sections.Add("SettingsModel", smodel);

                smodel = null;

                config.Save(ConfigurationSaveMode.Modified);

                // update our configuration
                Read();

                ret = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("An error has occured while saving the configuration.\r\n\r\nMessage: {0}\r\n\r\nSource:\r\n{1}", 
                        ex.Message, ex.StackTrace
                    ), 
                    "ApplicationSettings", MessageBoxButtons.OK, MessageBoxIcon.Information
                );

                ret = false;
            }

            return ret;
        }

        public class SettingsModel : ConfigurationSection
        {
            public SettingsModel()
            {
            }

            [ConfigurationProperty("Password")]
            public string Password
            {
                get
                {
                    string ret = RijndaelSettings.Decrypt((string)this["Password"]);
                    //string ret = (string)this["Password"];
                    return ret;
                }
                set
                {
                    string val = RijndaelSettings.Encrypt(value);
                    //string val = value;
                    this["Password"] = val;
                }
            }

            [ConfigurationProperty("HideWhenMinimized")]
            public bool HideWhenMinimized
            {
                get
                {
                    return (bool)this["HideWhenMinimized"];
                }
                set
                {
                    this["HideWhenMinimized"] = value;
                }
            }

            [ConfigurationProperty("HideInformationPopupWindow")]
            public bool HideInformationPopupWindow
            {
                get
                {
                    return (bool)this["HideInformationPopupWindow"];
                }
                set
                {
                    this["HideInformationPopupWindow"] = value;
                }
            }
        }
    }
}
