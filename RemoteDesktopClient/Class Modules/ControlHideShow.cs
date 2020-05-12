using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MultiRemoteDesktopClient
{
    public class ControlHideShow
    {
        object[] _controls = null;
        object[] _controlToHide = null;

        public bool Enable = false;

        public ControlHideShow()
        {
        }

        public void AddControl(object[] ctl)
        {
            this._controls = ctl;

            foreach (object o in ctl)
            {
                Type t = o.GetType();

                EventInfo eiMouseHover = t.GetEvent("MouseHover");
                if (eiMouseHover != null)
                {
                    eiMouseHover.AddEventHandler(o, new EventHandler(c_MouseHover));
                }

                EventInfo eiMouseLeave = t.GetEvent("MouseLeave");
                if (eiMouseLeave != null)
                {
                    eiMouseLeave.AddEventHandler(o, new EventHandler(c_MouseLeave));
                }
            }
        }

        public void AddControlToHide(object[] ctl)
        {
            this._controlToHide = ctl;
        }

        private void c_MouseLeave(object sender, EventArgs e)
        {
            if (Enable)
            {
                foreach (object o in this._controlToHide)
                {
                    Type t = o.GetType();
                    PropertyInfo pi = t.GetProperty("Visible");
                    if (pi != null)
                    {
                        pi.SetValue(o, false, null);
                    }
                }
            }
        }

        private void c_MouseHover(object sender, EventArgs e)
        {
            if (Enable)
            {
                foreach (object o in this._controlToHide)
                {
                    Type t = o.GetType();
                    PropertyInfo pi = t.GetProperty("Visible");
                    if (pi != null)
                    {
                        pi.SetValue(o, true, null);
                    }
                }
            }
        }
    }
}
