namespace Database.Models
{
    public class Model_ServerDetails
    {
        string _password = string.Empty;

        public string UID { get; set; } = string.Empty;

        public string ServerName { get; set; } = string.Empty;

        public string Server { get; set; } = string.Empty;

        public string Domain { get; set; } = string.Empty;

        public int Port { get; set; } = 0;

        public string Username { get; set; } = string.Empty;

        public string Password
        {
            get
            {
                //if (this._password != string.Empty)
                //{
                //    this._password = RijndaelSettings.Decrypt(this._password);
                //}

                return this._password;
            }
            set
            {
                string val = value;

                //if (val != string.Empty)
                //{
                //    val = RijndaelSettings.Encrypt(val);
                //}

                this._password = val;
            }
        }

        public string Description { get; set; } = string.Empty;

        public int ColorDepth { get; set; } = 0;

        public int DesktopWidth { get; set; } = 0;

        public int DesktopHeight { get; set; } = 0;

        public bool Fullscreen { get; set; } = false;

        public int GroupID { get; set; } = 0;
    }
}