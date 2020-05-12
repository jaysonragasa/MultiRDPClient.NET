/*
Author: Jayzon Ragasa | aka: Nullstring
Application Developer - Anomalist Designs LLC

 * --
 * TextboxRequiredWrapper 1.0
 * --
 
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace TextboxRequiredWrappers
{
    using System.Windows.Forms;

    public class TextboxRequiredWrapper
    {
        Control[] _textbox;
        Control[] _assocCtl;

        string reqFieldMessage = "This field is required";

        public TextboxRequiredWrapper()
        {
        }

        /// <summary>
        /// Add common controls for validation.
        /// </summary>
        /// <param name="textbox"></param>
        public void AddRange(Control[] textbox)
        {
            this._textbox = textbox;

            foreach (Control ctrl in this._textbox)
            {
                if (ctrl.Text == string.Empty)
                {
                    ctrl.Text = reqFieldMessage;
                    ctrl.Font = new System.Drawing.Font(ctrl.Font.FontFamily, ctrl.Font.Size, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

                ctrl.LostFocus += new EventHandler(ctrl_LostFocus);
                ctrl.GotFocus += new EventHandler(ctrl_GotFocus);
                ctrl.TextChanged += new EventHandler(ctrl_TextChanged);
            }
        }

        public void AddAssociateControl(Control[] ctl)
        {
            this._assocCtl = ctl;
        }

        void ctrl_TextChanged(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            System.Diagnostics.Debug.WriteLine(">'" + ctrl.Text + "'");

            if (ctrl.Text != reqFieldMessage)
            {
                ctrl.Font = new System.Drawing.Font(ctrl.Font.FontFamily, ctrl.Font.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                if (this._assocCtl != null)
                {
                    foreach (Control c in this._assocCtl)
                    {
                        c.Enabled = true;
                    }
                }
            }
            else
            {
                if (this._assocCtl != null)
                {
                    foreach (Control c in this._assocCtl)
                    {
                        c.Enabled = false;
                    }
                }
            }
        }

        void ctrl_LostFocus(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if (ctrl.Text == string.Empty)
            {
                ctrl.Font = new System.Drawing.Font(ctrl.Font.FontFamily, ctrl.Font.Size, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ctrl.Text = reqFieldMessage;
            }
        }

        void ctrl_GotFocus(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if (ctrl.Text == reqFieldMessage)
            {
                ctrl.Text = string.Empty;
            }
        }

        public bool isAllFieldSet()
        {
            bool ret = true;

            if (this._textbox == null)
            {
                return true;
            }

            foreach (Control ctrl in this._textbox)
            {
                if (ctrl.Text == reqFieldMessage)
                {
                    ret = false;
                    break;
                }
            }

            return ret;
        }
    }
}
