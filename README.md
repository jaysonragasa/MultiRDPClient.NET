# Multi RDP Client .NET
Multi RDP Client .NET (formerly known as Multi Remote Desktop Client .NET) comes on handy when managing your RDP connections. A simple and friendly user interface for you to work and navigate easily through tabbed RDP remote window. Allows you to Import/Export .RDP files, Disconnect all connection at one click and Connect all your servers at one click as well. You can also change the resolution while working, set the window into fullscreen mode, and as well as enable window stretching and a lot more features! 
  
# Background
Thanks to AxInterop.MSTSCLib (mstscax.dll) an ActiveX COM Component which you can use to connect on  Remote Desktop. So, I made a GUI for it for you to connect on your servers via terminal service on multiple window view in one application. How to use MSTSCAX.DLL? 
  
1. You must have the ActiveX file in your system called "mstscax.dll".
2. if not, then you can Google for the file and download it, then make sure you registered it using "RegSvr32 ". I think that's one of the IIS package if you installed the Remote Desktop Web Connection.
3. On the UI Design mode in VS2005 or 8.
go to your toolbox and Open Choose Toolbox Items and look for Microsoft RDP Client Control in COM Components tab.
  
I'm guessing you successfully added that control in your toolbar. So I don't think I have to explain how to get that in your Form. 
  
# Screenshots
## Main Window
![https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/MainWindow.jpg](https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/MainWindow.jpg)
  
## Server Lists
![https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/ServerLists_Icon_Views.jpg](https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/ServerLists_Icon_Views.jpg)
  
## Server Group 
![https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/GroupManagerWindow.jpg](https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/GroupManagerWindow.jpg)
  
## Import 
![https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/ImportWindow.jpg](https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/ImportWindow.jpg)

## About Window
![https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/AboutWindow.jpg](https://raw.githubusercontent.com/jaysonragasa/MultiRDPClient.NET/master/Multi%20Remote%20Desktop%20Client%20.NET%20(ScreenShots)/AboutWindow.jpg)

# Features
**Command Line Parameters** - *requested by: Simon Capewell*
* /sname "server name"
Can open a new client window by providing a Server Name
* /gname "group name" 
Can open a multiple client window by specifying the Group Name 
  
**Add, Edit, Delete Servers**   
Those words say it all. 
  
**Server Settings Window**  
the Server Settings window has the following:  
* General Tab 
  * **Server Name** You can give your connection name
  * **Server** The host address or IP address
  * **Custom Port** - *requested by: Simon Capewell.* Allowing the RDP to connect to different port.
  * **Username** Your RD server Username
  * **Password** Your RD server Password 
  * **Description** Your Server description
  * **Group** You can choose the group of your Server. You can also open Group Manager from there
  * **Display Tab** Desktop Sizes - some resolutions were requested by: Simon Capewell
    * 640 x 480 
    * 800 x 600
    * 1024 x 768 
    * 1120 x 700
    * 1152 x 864
    * 1280 x 800
    * 1280 x 1024
    * Custom Desktop Height and Width 
    * Fullscreen 
  * Colors 
    * True Color (24 bit)
    * High Color (16 bit)
    * High Color (15 bit)
    * 256 Colors 
  
**Configuration Window**  
  * General Tab 
    * Password
    * Your Startup Password 
  * Display Tab
      * Hide when Minimize  
        - If Enabled, the window hides and it self and you bring it up anytime by double clicking on notification icon in system tray.  
        - If Disabled, the window just minimized and accessible in taskbar.
      * Notification Window 
        - If Enabled, Notification window pops up everytime you hover into some controls.
        - if Disabled, You know what that means ..
  
**Import/Export**  
  * Import from RDP file format  
You can import your current RDP files
Note: Currently, the password made by MSTSC cannot be decrypted. I Still have to work on this
  * Export to RDP file format  
You can export the servers in RDP file format 
  
**Group Manager** - *requested by: Simon Capewell*  
You can create a group for your RDP connections.
  
**Database**  
The app is using SQLite3 - ADO.NET instead of working with XML which can be very time-consuming. 
  
**Notification Icon**  
This icon sits on your system tray area and you can right click on it to show the context menu or double click to bring up the window when minimized or hidden.
  
**The context menu items contains:**
  * Servers - And under it contains the Groups and the Servers
  * Disconnect All 
  * Configuration 
  * Lock 
  * Exit 
  * Lock Application 
  
For safety purposes, a Lock feature is added to lock the current application and the Password Window will show up after locking the application.

**Startup Password** - *requested by: shmulyeng*  
For safety purposes, a Password feature is added before opening the application.
  
After entering 3 invalid passwords, CAPTCHA verification will show up in Password Window
  
**Server Lists Panel**  
You can change the views in Server Lists panel in Detailed, Tiled, and Tree
  
  * Context Menu in Server Lists Panel - *requested by: Simon Capewell and shmulyeng*  
Context Menu will popup after right clicking on the items and you can Add, Edit, Delete, and Group Connect.
  * Collapse/Expand Server Lists Panel - *requested by: Simon Capewell*  
You can collapse or expand the server lists panel.
  * Groups - requested by: Simon Capewell  
Servers are arranged by Groups
  * Different Views - *requested by: Simon Capewell*  
You can set the Server lists view by Detailed, Tiled, and Tree

**MDI Tabs** - *requested by: shmulyeng*  
Of course, Tabs can be very helpfull when selecting client windows
  
**Disconnect All**  
Disconnects all connected RD clients.
  
**Client Window**  
  * Connect, Disconnect, and Reconnect: This Connects, Disconnects, or Reconnects your RD connection.
  * Fullscreen: Sets the RD connection to Fullscreen. It can also ask you to resize the Resolution based on your Desktop Resolution 
  * Fit To Window: The RD resolution can change based on the RD client window
    * Fit to Window 
    * Stretch 
  * Settings: Opens the server settings 
  
**Info Popup Window**  
This window automatically popups when hovered on the control showing a description of what the control can do.  
  
**Credits**  
I do give a lot of credits to the people who shared their Ideas, Custom Control, and Codes.  
  
Credits can be found in my About Window
  
# Code Snippets
A simple example on RDP connection to connect to a remote desktop.  
```csharp
// for example, I have my AxMsRdpClient control named rdpClient.
rdpClient.Server = "sever name here"; 
rdpClient.UserName = "your username on remote pc"; 
rdpClient.AdvancedSettings2.ClearTextPassword = "you password on remote pc";
// optional 
rdpClient.ColorDepth = 16; // int value can be 8, 15, 16, or 24
rdpClient.DesktopWidth = 1024; // int value 
rdpClient.DesktopHeight = 768; // int value 
rdpClient.FullScreen = true|false; // boolean value that can be True or False
// and connect 
rdpClient.Connect();  
```
  
Setting full screen in runtime is fairly simple  
```csharp
// just set the Fullsceen property to TRUE
rdpClient.Fullscreen = true;
// strecth the screen
rdpClient.AdvanceSettings3.SmartSizing = true; 
```
  
# Points of interest about connection and disconnection
I implemented a Reconnect feature. But its not easy as calling `Disconnect()` and `Connect()`.
  
You should wait until it properly disconnected and call `Connect()`. 
  
In AxMsRdpClient, there is a Connect property which acts as a Connection Status: 
  
  * 1 = Connected 
  * 0 = Disconnected 
  
Now reconnecting can be done by doing this:
```csharp
// call Disconnect() method on AxMsRdpClient
rdpClient.Disconnect();

// wait for the server to properly disconnect
while (rdpClient.Connected != 0)
{
    System.Threading.Thread.Sleep(1000);
    Application.DoEvents(); 
} 
// call Connect() method on AxMsRdpClient  
```
  
You can always implement your own way of reconnecting. The code above is just the very basic, I do not recommend it.
  
**Here's my RDPFile Reader class**
```csharp
/*
    Author: Jayson Ragasa
    Application Developer
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
      
             RegexOptions options = 
               ((RegexOptions.IgnorePatternWhitespace | 
                 RegexOptions.Multiline) | RegexOptions.IgnoreCase);
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
             (int)this._winPosStr.WinState, this._winPosStr.Rect.Top, 
                this._winPosStr.Rect.Left, this._winPosStr.Rect.Width, this._winPosStr.Rect.Height,
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
```
  
We also have DataProtection class which was provided by Microsoft and a little bit of modification and some methods to implement such as converting Byte[] < to > Hex blob. We could create a MSTSC valid password. 
  
**Reference:** 
Building Secure ASP.NET Applications: Authentication, Authorization, and Secure Communication. 
[http://msdn.microsoft.com/en-us/library/aa302402.aspx#secnetht07_topic4](http://msdn.microsoft.com/en-us/library/aa302402.aspx#secnetht07_topic4)
  
Implementing CryptProtectData and CryptUnprotectData from Crypt32.DLL 
  
Remko Weijnen - "psw" descriptor on CryptProtectData  
[http://www.remkoweijnen.nl/blog/2007/10/18/how-rdp-passwords-are-encrypted/](http://www.remkoweijnen.nl/blog/2007/10/18/how-rdp-passwords-are-encrypted/) 
  
Now I have created a wrapper called `DataProtectionForRDPWrapper` to easily Encrypt and Decrypt RDP password CREATED in DataProtection class. Why did I emphasize the "created" word. Some limitation on DataProtection class is, we can't decrypt the password created by MSTSC and still under research. 
  

## DataProtectionForRDPWrapper
```csharp
/*
 * Author: Jayson Ragasa
 * Application Developer
 * 
 * --
 * Made a wrapper for DataProtector so I could
 * Encrypt/Decrypt valid password for RDP
 * 
 * TAKE NOTE:
 * This can't Decrypt MSTSC Password!
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace DataProtection
{
    public class DataProtectionForRDPWrapper
    {
     static DataProtection.DataProtector dp = new DataProtector(DataProtector.Store.USE_USER_STORE);

     public static string Encrypt(string text_password)
     {
         byte[] e = dp.Encrypt(GetBytes(text_password), null, "psw");
         return GetHex(e);
     }

     public static string Decrypt(string enc_password)
     {
         byte[] b = ToByteArray(enc_password);
         byte[] d = dp.Decrypt(b, null, "psw");
         return GetString(d);
     }

     static byte[] GetBytes(string text)
     {
         return UnicodeEncoding.Unicode.GetBytes(text);
     }

     static string GetString(byte[] byt)
     {
         System.Text.Encoding enc = System.Text.Encoding.Unicode;
         return enc.GetString(byt);
     }

     static string GetHex(byte[] byt_text)
     {
         string ret = string.Empty;

         for (int i = 0; i < byt_text.Length; i++)
         {
             ret += Convert.ToString(byt_text[i], 16).PadLeft(2, '0').ToUpper();
         }

         return ret;
     }

     static byte[] ToByteArray(String HexString)
     {
         try
         {
             int NumberChars = HexString.Length;
             byte[] bytes = new byte[NumberChars / 2];
             for (int i = 0; i < NumberChars; i += 2)
             {
                 bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
             }
             return bytes;
         }
         catch (Exception ex)
         {
             // this occures everytime we decrypt MSTSC generated password.
             // so let's just throw an exception for now
             throw new Exception("Problem converting Hex to Bytes", ex);
         }
     }
    }
}
```
  
## Using RDP Reader
```csharp
// RDP Key descriptions found at
// http://dev.remotenetworktechnology.com/ts/rdpfile.htm

RDPFile rdp = new RDPFile();

// we wan't the window to be on a Maximize state
// 1 - windowed
// 2 - fullscreen
rdp.ScreenMode = 1;

// remote desktop resolution
rdp.DesktopWidth = 1024;
rdp.DesktopHeight = 768;

/* remote desktop color depth
public enum SessionBPPs
{
    BPP_8 = 8,
    BPP_15 = 15,
    BPP_16 = 16,
    BPP_24 = 24
}
*/
rdp.SessionBPP = SessionBPPs.BPP_16

/* how the window will look?
Terminal Services Team Blog explained the "winposstr" key!
The location on the virtual desktop where the TS Client initially 
positions itself can be controlled via the winposstr setting in the RDP file 
winposstr:s:0,ShowCmd,Left,Top,Right,Bottom
*/
RDPFile.WindowsPosition winpos = new RDPFile.WindowsPosition();
{
    RDPFile.RECT r = new RDPFile.RECT()
    {
        Top = 0;
        Left = 0;
        Width = ss.DesktopWidth;
        Height = ss.DesktopHeight;
    }
    
    winpos.Rect = r;
}

/* this is equal to ShowCmd from Terminal Services Team Blog 
public enum WindowState : int
{
    NORMAL = 1,
    MAXMIZE = 3
}
*/
winpos.WinState = RDPFile.WindowState.MAXMIZE;

rdp.WinPosStr = winpos; // set all our winposstr from the obove configuration
rdp.FullAddress = "192.168.1.1" // your server name or ip address;
rdp.Compression = 1;
// RemoteNetworkTechnology didn't fully explanied this
// but looks like this is needed for faster data transfer

/* For applying standard key combinations
public enum KeyboardHooks
{
    ON_THE_LOCAL_COMPUTER = 0,
    ON_THE_REMOTE_COMPUTER = 1,
    IN_FULL_SCREEN_MODE_ONLY = 2
};
*/
rdp.KeyboardHook = RDPFile.KeyboardHooks.ON_THE_REMOTE_COMPUTER;

/* How will the audio from the remote pc be played
public enum AudioModes
        {
            BRING_TO_THIS_COMPUTER = 0,
            DO_NOT_PLAY = 1,
            LeAVE_AT_REMOTE_COMOPUTER = 2
        };
*/
rdp.AudioMode = RDPFile.AudioModes.BRING_TO_THIS_COMPUTER;

rdp.RedirectDrives = 0; // should we share our local drives in the remote pc?
rdp.RedirectPrinters = 0; // should we share our printers in the remote pc?
rdp.RedirectComPorts = 0; // should we share our com ports in the remoe pc?
rdp.RedirectSmartCards = 0; // should we share our smart cards in the remote pc?
rdp.DisplayConnectionBar = 1; // will the Connection bar visible when in Fullscreen mode?
rdp.AutoReconnectionEnabled = 1; // do we need to automatically connect?
rdp.Username = "Admin"; // remote pc Username
rdp.Domain = "DomainName"; // remote pc Domain
rdp.AlternateShell = string.Empty; // are we going to use different shell other than C:\Windows\Explorer.exe?
rdp.ShellWorkingDirectory = string.Empty; // Working directory if an alternate shell was specified.

// here's the password implementing our DataProtection and the wrapper
rdp.Password = (ss.Password == string.Empty ? string.Empty : DataProtectionForRDPWrapper.Encrypt(ss.Password));

rdp.DisableWallpaper = 1; // should we disable wallpaper in the remote pc?
rdp.DisableFullWindowDrag = 1; // should we disable the full window drag in the remote pc and just show the box while dragging?
rdp.DisableMenuAnims = 1; // should we disable animations?
rdp.DisableThemes = 1; // should we disable Windows Visual Themes?
rdp.DisableCursorSettings = 1; // should we disable mouse cursor effects?
rdp.BitmapCachePersistEnable = 1; // This setting determines whether bitmaps are cached on the local computer

#region try exporting the file
{
    try
    {
        rdp.Save(@"D:\My Documents\MyRDPConnection.RDP");
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occured while saving the configuration for '" + 
          rdp.FullAddress + "'.\r\n\r\nError Message: " + ex.Message, 
          this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        System.Diagnostics.Debug.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
    }
}
#endregion
```
  
## Reading .RDP files
```csharp
string thisFile = @"D:\My Documents\MyRDPConnection.RDP";

#region Read RDP File
RDPFile rdpfile;
{
    try
    {
        rdpfile = new RDPFile();
        rdpfile.Read(thisFile);
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occured while reading '" + 
          Path.GetFileName(thisFile) + "' and it will be skipped.\r\n\r\nError Message: " + 
          ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        System.Diagnostics.Debug.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
    }
}
#endregion

Console.Writeline("RDP Username: " + rdpfile.Username);

#region Try decrypting the password from RDP file
{
    try
    {
        System.Diagnostics.Debug.WriteLine("reading password " + thisFile);
        Console.Writeline("RDP Password: " + DataProtectionForRDPWrapper.Decrypt(rdpfile.Password));
        System.Diagnostics.Debug.WriteLine("reading password done");
    }
    catch (Exception Ex)
    {
        ss.Password = string.Empty;

        if (Ex.Message == "Problem converting Hex to Bytes")
        {
            MessageBox.Show("This RDP File '" + 
                Path.GetFileNameWithoutExtension(thisFile) + 
                "' contains a secured password which is currently unsported by this " + 
                "application.\r\nThe importing can still continue but without the password.\r\nYou " + 
                "can edit the password later by selecting a server in 'All Listed Servers' " + 
                "and click 'Edit Settings' button on the toolbar", 
                this.Text, 
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else if (Ex.Message.Contains("Exception decrypting"))
        {
            MessageBox.Show("Failed to decrypt the password from '" + 
               Path.GetFileNameWithoutExtension(thisFile) + "'", 
               this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            MessageBox.Show("An unknown error occured while decrypting the password from '" + 
              Path.GetFileNameWithoutExtension(thisFile) + "'", 
              this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
#endregion

Console.Writeline("RDP Desktop Width: " + rdpfile.DesktopWidth);
Console.Writeline("RDP Desktop Height: " + rdpfile.DesktopHeight); 
```
  
## Updating an .RDP file
```csharp
//RDP File Reader test
RDPFileReader.RDPFile rdp = new RDPFileReader.RDPFile();
rdp.Read(@"D:\My Documents\RDP\Application Server (1120x700-16bitc).rdp");

Console.WriteLine(rdp.WinPosStr.WinState);

// set new Window State
// make window mode maxmize
RDPFileReader.RDP.WindowsPosition wpos = new RDPFileReader.RDP.WindowsPosition()
{
    Rect = rdp.WinPosStr.Rect;
    WinState = RDPFileReader.RDP.WindowState.MAXMIZE; // change the window state when in window mode.
};

rdp.WinPosStr = wpos;
rdp.SessionBPP = RDPFileReader.RDP.SessionBPPs.BPP_8; // change the color depth

// and call Update
rdp.Update();  
```
  
# Release History
```text
June 24, 2015
UPDATE:
- Uploaded the code in GitHub
---- Solution updated to Visual Studio 2013
---- Using latest version of SQLite (NuGet)

Full Release - April 11, 2009
ADD:
- Added Hide/Show settings for Information popup window
UPDATE:
- Fixed Server Lists collapse bug when no MDI Child
- Fixed LiveInfoWindow from stealing the focus.
- Fixed database exception in Import Window.
- Group Manager window, redesigned to fit on MRDC.NET layout.
- Updated database schema.
---- Groups table uid is set to auto-increment.

Full Release - April 4, 2009 UPDATE:
- Fixed error when no password is set
- Fixed database scheme
- MSTSC Generated password can now be decrypted! 
- Fixed domain name, not saving 

Full Release / April 3 
UPDATE:
- Fixed stupid RDP Control Focus issue

Full Release / April 2 
NEW: - From the requests of Simon Capewell and shmulyeng - Simon Capewell Requests: 
--- DONE(03222009_1254)1. Allow connection to servers on a specific port using AdvancedSettings2.RDPPort 
--- DONE(03192009_0916)2. More screen resolutions on the slider - most obviously 1152x864 and 1280x1024. 
--- DONE(03202009_1341)3. Context menu on the list of servers. 
--- DONE(03222009_1254)4. Options for different icon sizes for the list of servers. 
--- DONE(03227009_0138)5. Groupings for servers - handy if you have lots of clients 
    each of whom have multiple servers. You could launch sessions for all the items in the group. 
--- DONE(03212009_1321)6. Auto hide or view menu option to show/hide the sidebar (similar to Visual Studio). 
--- DONE(03192009_0200)7. Clicking Delete Server or Edit Settings when there are no servers throws an exception. 

--- Here's another one that's just occurred. I use Launchy to index a directory with a load of .rdp files in it, 
    which means I can just type Ctrl+space then a portion of the server name and launch it. To do that with this tool it'd need to: 
--- DONE(03272009_1129)1. Be able to launch a connection from the built in server list from the command line. 
--- DONE(03272009_1129)2. Have a command line switch to open the connection in an existing instance of the app. 
--- DONE(03272009_1129)3. Perhaps even be able to launch connections from the command line 
    using rdp files rather than the built in server list. 
- shmulyeng requests: 
--- DONE(03272009_0252)Ability to password protect the server list. 
--- DONE(03202009_1341)Ability to right click on a connection item in the list to edit connection details. 
--- (WILL TRY) Ability to log out of a connection rather than disconnect. 
--- DONE(03212009_1321)One thing I like about Palantir is the tabs of open connections. 
- and I added some 
- CAPTCHA verfication on startup password. 
--- When an incorrrect password is entered 3x, CAPTCH verification panel will show up.
- SQLite3
--- scrap XML!! :P 
ADD: 
UPDATE: 
- The last release was pretty much stable so there's a small updates on the code and some bug fix. 

Alpha Release - 6, & 7 / March 19, 2009 
NEW: 
- Application can now run in Windows Vista. 
- Fixed AX MSRDPClient reference 
---- Thanks to Simon Capewell (http://www.codeproject.com/script/Membership/Profiles.aspx?mid=215) 
-------- RemoteDesktopClient.aspx?fid=1537072&df=90&mpp=25&noise=3&sort=Position&view=Quick&select=2969383#xx2969383xx 
MARCH 21 
- LiveInformationBox 1.0 
--- Is a class library that pops up whenever you hover on a specified 
    control and shows some details about what the controls can do. 
--- The information shown on information box is an xml base. 
--- this is better than showing some information on a status strip :P 
- ListViewEx 
--- hooks some control (not just common control) to disable/enable if 
    listview has no item or when a selectio is made on listview items. 
--- good for automating controls instead of doing tons of lines just to disable/enable different controls. 
MARCH 25 
- Am using SQLite3 now instead of XML! :P 
---- it's taking too much time to maintain :P 
ADD: 
UPDATE: 
MARCH 20 
- Finally it's working on both OS, thanks to that AxImp and thanks to @unruledboy 
MARCH 23 
- LiveInformationBox 
-- now supports toolbar buttons 
- Server Lists 
-- now collapsable 
-- can change icon views 
-- context menu 
- For RDP client window 
-- on main form, now contains tab 
- Settings window 
-- added more resolution 
- ListViewEx for Server Lists 
-- control hooks so it can be easily Enable/Disable the controls when something's changed on the listview 
- crashes when clicking Edit Settings and Delete when no items on the Server Lists 
-- fixes the locking issue on rdp control when lossing its focus - Settings window 
-- Connection Port 
MARCH 25 
- LiveInformationBox 
---- fixed the UI 
---- still having some problem on window focus. 

Alpha Release - 5 / March 17, 2009 
NEW: 
- Import / Export RDP file format 
- Can delete selected server in "All Server List" panel. 
- Added new About Window 
ADD: 
UPDATE: 
- Fixed and Improved Fit To Window feature 
- Fixed Saving, Importing, and Exporting password handling. 

Alpha Release - 2, 3, & 4 / March 7, 2009 
NEW: 
- ServerList reader moved to a separate Class Library 
---- Custom Exception Handler 
- TextboxRequiredWrapper moved to a separate Class Library 
MARCH 8 
- Fixed updating settings bug! MARCH 8 - ServerSettingsWindow 
---- added new button("Get client window size"): Set the Desktop Width 
     and Height size base on rdp client window size. 
MARCH 9 
- Fixed Icon status on Server List if multiple windows are openned. 
---- not sure why I didn't noticed that. 
MARCH 12 
- RDP File Reader 
- Used to Read/Create/Update RDP Files 
- DataProtection 
- Used to Encrypt/Decrypt RDP Passwords and create valid RDP Passwords. 
ADD: 
MARCH 9 
- Added new button on main toolbar called "Disconnect All" 
---- which disconnects all connected rdp client. 
UPDATED: 
- saving a value on "dummy" attribute is now MD5 string 
- duplicate check while creating a New or Updating a Connection Settings. 
MARCH 8 
- Updated xml configuration template 
---- added UID element 
---- dummy and uid values are now DateTime(MMddyyhhmmss) format 
- Password is now encrypted using Rijndael Encryption/Decryption 
MARCH 8 - Current configuration on RDP Client Window now updates
- Server list now update after settings are changed on RDP Client Window MARCH 8 
- Settings can be applied on Server Lists or in Rdp Client Window. 

Alpha Release - 1 / March 6, 2009 
First release in CodePlex.coma
```

# Finale
I hope you were able to learn and I hope I was able to help you with your RDP programming and implementation. 

# License
Under GNU GPLv3 [https://choosealicense.com/licenses/gpl-3.0/](https://choosealicense.com/licenses/gpl-3.0/)

# Article
The original article is posted in my CodeProject page here [https://www.codeproject.com/Articles/33979/Multi-RDP-Client-NET](https://www.codeproject.com/Articles/33979/Multi-RDP-Client-NET)