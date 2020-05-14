namespace Database.Models
{
    public class Model_ServerDetails
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

        public Model_ServerDetails()
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