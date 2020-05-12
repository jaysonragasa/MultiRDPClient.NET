/*
    Author: Jayson Ragasa | aka: Nullstring
    Application Developer - Anomalist Designs LLC
 *  ---
 *  RDPFileReader 1.0
 *  
 *  RDP File Settings - http://dev.remotenetworktechnology.com/ts/rdpfile.htm
 *  Terminal Services Team Blog - http://blogs.msdn.com/ts/archive/2008/09/02/specifying-the-ts-client-start-location-on-the-virtual-desktop.aspx
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace RDPFileReader
{
    public class RDPFile
    {
        #region enum

        public enum KeyboardHooks
        {
            ON_THE_LOCAL_COMPUTER = 0,
            ON_THE_REMOTE_COMPUTER = 1,
            IN_FULL_SCREEN_MODE_ONLY = 2
        };

        public enum AudioModes
        {
            BRING_TO_THIS_COMPUTER = 0,
            DO_NOT_PLAY = 1,
            LeAVE_AT_REMOTE_COMOPUTER = 2
        };

        public enum WindowState : int
        {
            NORMAL = 1,
            MAXMIZE = 3
        }

        public enum SessionBPPs
        {
            BPP_8 = 8,
            BPP_15 = 15,
            BPP_16 = 16,
            BPP_24 = 24
        }

        #endregion

        #region structs

        public struct RECT
        {
            public int Top;
            public int Left;
            public int Width;
            public int Height;
        }

        public struct WindowsPosition
        {
            public WindowState WinState;
            public RECT Rect;
        }

        #endregion

        #region variables

        private string _filename = string.Empty;

        #region RDP template
        string[] _rdpTemplate = {
                                    "screen mode id:i:{0}",
                                    "desktopwidth:i:{1}",
                                    "desktopheight:i:{2}",
                                    "session bpp:i:{3}",
                                    "winposstr:s:0,{4},{5},{6},{7},{8}",
                                    "full address:s:{9}",
                                    "compression:i:{10}",
                                    "keyboardhook:i:{11}",
                                    "audiomode:i:{12}",
                                    "redirectdrives:i:{13}",
                                    "redirectprinters:i:{14}",
                                    "redirectcomports:i:{15}",
                                    "redirectsmartcards:i:{16}",
                                    "displayconnectionbar:i:{17}",
                                    "autoreconnection enabled:i:{18}",
                                    "username:s:{19}",
                                    "domain:s:{20}",
                                    "alternate shell:s:{21}",
                                    "shell working directory:s:{22}",
                                    "password 51:b:{23}",
                                    "disable wallpaper:i:{24}",
                                    "disable full window drag:i:{25}",
                                    "disable menu anims:i:{26}",
                                    "disable themes:i:{27}",
                                    "disable cursor setting:i:{28}",
                                    "bitmapcachepersistenable:i:{29}"
                                };
        #endregion

        #region member fields

        int _screenMode = 0;
        int _desktopWidth = 0;
        int _desktopHeight = 0;
        SessionBPPs _sessionBPP = 0;
        WindowsPosition _winPosStr;
        string _fullAddress = string.Empty;
        int _compression = 0;
        KeyboardHooks _keyboardHook = 0;
        AudioModes _audiomode = 0;
        int _redirectDrives = 0;
        int _redirectPrinters = 0;
        int _redirectComPorts = 0;
        int _redirectSmartCards = 0;
        int _displayConnectionBar = 0;
        int _autoReconnectionEnabled = 0;
        string _username = string.Empty;
        string _domain = string.Empty;
        string _alternateShell = string.Empty;
        string _shellWorkingDirectory = string.Empty;
        string _password = string.Empty;
        int _disableWallpaper = 0;
        int _disableFullWindowDrag = 0;
        int _disableMenuAnims = 0;
        int _disableThemes = 0;
        int _disableCursorSettings = 0;
        int _bitmapCachePersistEnable = 0;

        #endregion

        #endregion

        #region properties

        public int ScreenMode
        {
            get
            {
                return this._screenMode;
            }
            set
            {
                this._screenMode = value;
            }
        }

        public int DesktopWidth
        {
            get
            {
                return this._desktopWidth;
            }
            set
            {
                this._desktopWidth = value;
            }
        }

        public int DesktopHeight
        {
            get
            {
                return this._desktopHeight;
            }
            set
            {
                this._desktopHeight = value;
            }
        }

        public SessionBPPs SessionBPP
        {
            get
            {
                return this._sessionBPP;
            }
            set
            {
                this._sessionBPP = value;
            }
        }

        public WindowsPosition WinPosStr
        {
            get
            {
                return this._winPosStr;
            }
            set
            {
                this._winPosStr = value;
            }
        }

        public string FullAddress
        {
            get
            {
                return this._fullAddress;
            }
            set
            {
                this._fullAddress = value;
            }
        }

        public int Compression
        {
            get
            {
                return this._compression;
            }
            set
            {
                this._compression = value;
            }
        }

        public KeyboardHooks KeyboardHook
        {
            get
            {
                return this._keyboardHook;
            }
            set
            {
                this._keyboardHook = value;
            }
        }

        public AudioModes AudioMode
        {
            get
            {
                return this._audiomode;
            }
            set
            {
                this._audiomode = value;
            }
        }

        public int RedirectDrives
        {
            get
            {
                return this._redirectDrives;
            }
            set
            {
                this._redirectDrives = value;
            }
        }

        public int RedirectPrinters
        {
            get
            {
                return this._redirectPrinters;
            }
            set
            {
                this._redirectPrinters = value;
            }
        }

        public int RedirectComPorts
        {
            get
            {
                return this._redirectComPorts;
            }
            set
            {
                this._redirectComPorts = value;
            }
        }

        public int RedirectSmartCards
        {
            get
            {
                return this._redirectSmartCards;
            }
            set
            {
                this._redirectSmartCards = value;
            }
        }

        public int DisplayConnectionBar
        {
            get
            {
                return this._displayConnectionBar;
            }
            set
            {
                this._displayConnectionBar = value;
            }
        }

        public int AutoReconnectionEnabled
        {
            get
            {
                return this._autoReconnectionEnabled;
            }
            set
            {
                this._autoReconnectionEnabled = value;
            }
        }

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public string Domain
        {
            get
            {
                return this._domain;
            }
            set
            {
                this._domain = value;
            }
        }

        public string AlternateShell
        {
            get
            {
                return this._alternateShell;
            }
            set
            {
                this._alternateShell = value;
            }
        }

        public string ShellWorkingDirectory
        {
            get
            {
                return this._shellWorkingDirectory;
            }
            set
            {
                this._shellWorkingDirectory = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public int DisableWallpaper
        {
            get
            {
                return this._disableWallpaper;
            }
            set
            {
                this._disableWallpaper = value;
            }
        }

        public int DisableFullWindowDrag
        {
            get
            {
                return this._disableFullWindowDrag;
            }
            set
            {
                this._disableFullWindowDrag = value;
            }
        }

        public int DisableMenuAnims
        {
            get
            {
                return this._disableMenuAnims;
            }
            set
            {
                this._disableMenuAnims = value;
            }
        }

        public int DisableThemes
        {
            get
            {
                return this._disableThemes;
            }
            set
            {
                this._disableThemes = value;
            }
        }

        public int DisableCursorSettings
        {
            get
            {
                return this._disableCursorSettings;
            }
            set
            {
                this._displayConnectionBar = value;
            }
        }

        public int BitmapCachePersistEnable
        {
            get
            {
                return this._bitmapCachePersistEnable;
            }
            set
            {
                this._bitmapCachePersistEnable = value;
            }
        }

        #endregion

        #region methods

        public void Read(string filepath)
        {
            this._filename = filepath;

            string data = string.Empty;

            using (StreamReader reader = new StreamReader(filepath))
            {
                data = reader.ReadToEnd();
            }

            string[] settings = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string thisSetting in settings)
            {
                string regex = "(?<type>.*)\\:(?<dtype>\\w)\\:(?<value>.*)";
                
                RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
                Regex reg = new Regex(regex, options);

                if (reg.IsMatch(thisSetting))
                {
                    Match m = reg.Match(thisSetting);

                    string v = m.Groups["value"].Value;

                    switch (m.Groups["type"].Value)
                    {
                        case "screen mode id":
                            this._screenMode = int.Parse(v);
                            break;

                        case "desktopwidth":
                            this._desktopWidth = int.Parse(v);
                            break;

                        case "desktopheight":
                            this._desktopHeight = int.Parse(v);
                            break;

                        case "session bpp":
                            this._sessionBPP = (SessionBPPs)int.Parse(v);
                            break;

                        case "winposstr":
                            string[] vals = v.Split(',');

                            this._winPosStr.WinState = (WindowState)int.Parse(vals[1]);

                            this._winPosStr.Rect.Top = int.Parse(vals[2]);
                            this._winPosStr.Rect.Left = int.Parse(vals[3]);
                            this._winPosStr.Rect.Width = int.Parse(vals[4]);
                            this._winPosStr.Rect.Height = int.Parse(vals[5]);

                            break;

                        case "full address":
                            this._fullAddress = v;
                            break;
                            
                        case "compression":
                            this._compression = int.Parse(v);
                            break;

                        case "keyboardhook":
                            this._keyboardHook = (KeyboardHooks)int.Parse(v);
                            break;

                        case "audiomode":
                            this._audiomode = (AudioModes)int.Parse(v);
                            break;

                        case "redirectdrives":
                            this._redirectDrives = int.Parse(v);
                            break;

                        case "redirectprinters":
                            this._redirectPrinters = int.Parse(v);
                            break;

                        case "redirectcomports":
                            this._redirectComPorts = int.Parse(v);
                            break;

                        case "redirectsmartcards":
                            this._redirectSmartCards = int.Parse(v);
                            break;

                        case "displayconnectionbar":
                            this._displayConnectionBar = int.Parse(v);
                            break;

                        case "autoreconnection enabled":
                            this._autoReconnectionEnabled = int.Parse(v);
                            break;

                        case "username":
                            this._username = v;
                            break;

                        case "domain":
                            this._domain = v;
                            break;

                        case "alternate shell":
                            this._alternateShell = v;
                            break;

                        case "shell working directory":
                            this._shellWorkingDirectory = v;
                            break;

                        case "password 51":
                            this._password = v;
                            break;

                        case "disable wallpaper":
                            this._disableWallpaper = int.Parse(v);
                            break;

                        case "disable full window drag":
                            this._disableFullWindowDrag = int.Parse(v);
                            break;

                        case "disable menu anims":
                            this._disableMenuAnims = int.Parse(v);
                            break;

                        case "disable themes":
                            this._disableThemes = int.Parse(v);
                            break;

                        case "disable cursor setting":
                            this._disableCursorSettings = int.Parse(v);
                            break;

                        case "bitmapcachepersistenable":
                            this._bitmapCachePersistEnable = int.Parse(v);
                            break;
                    }
                }
            }
        }

        public void Update()
        {
            Save(this._filename);
        }

        public void Save(string filepath)
        {
            this._filename = filepath;

            string template = string.Empty;

            foreach (string temp in this._rdpTemplate)
            {
                template += temp + "\r\n";
            }

            string data = string.Format(template,
                this._screenMode,
                this._desktopWidth,
                this._desktopHeight,
                (int)this._sessionBPP,
                (int)this._winPosStr.WinState, this._winPosStr.Rect.Top, this._winPosStr.Rect.Left, this._winPosStr.Rect.Width, this._winPosStr.Rect.Height,
                this._fullAddress,
                this._compression,
                (int)this._keyboardHook,
                (int)this._audiomode,
                this._redirectDrives,
                this._redirectPrinters,
                this._redirectComPorts,
                this._redirectSmartCards,
                this._displayConnectionBar,
                this._autoReconnectionEnabled,
                this._username,
                this._domain,
                this._alternateShell,
                this._shellWorkingDirectory,
                this._password,
                this._disableWallpaper,
                this._disableFullWindowDrag,
                this._disableMenuAnims,
                this._disableThemes,
                this._disableCursorSettings,
                this._bitmapCachePersistEnable
            );

            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.Write(data);
            }
        }

        #endregion
    }
}
