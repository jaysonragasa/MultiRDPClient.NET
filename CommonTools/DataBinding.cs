using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace CommonTools
{
	public class MyBindingSource : System.Windows.Forms.BindingSource
	{
		public event EventHandler ValueChanged;
		public void RaiseValueChanged(object sender)
		{
			if (ValueChanged != null)
				ValueChanged(sender, null);
		}
	}
	public class BindingWithNotify : System.Windows.Forms.Binding
	{
		public BindingWithNotify(string propertyName, object dataSource, string dataMember) : base(propertyName, dataSource, dataMember, true)
		{
		}
		protected override void OnBindingComplete(BindingCompleteEventArgs e)
		{
			base.OnBindingComplete(e);
			this.Control.Validating += new CancelEventHandler(Control_Validating);
		}

		void Control_Validating(object sender, CancelEventArgs e)
		{
			WriteNotifyIfChanged();
		}
		public void WriteNotify()
		{
			WriteValue();
			NotifyChanged();
		}
		public void WriteNotifyIfChanged()
		{
			object dataobject = DataSource;
			if (dataobject is ICurrencyManagerProvider)
				dataobject = ((ICurrencyManagerProvider)dataobject).CurrencyManager.Current;

			PropertyInfo info = PropertyUtil.GetNestedProperty(ref dataobject, BindingMemberInfo.BindingMember);
			if (info != null)
			{
				object objBefore = info.GetValue(dataobject, null);
				WriteValue();
				object objAfter = info.GetValue(dataobject, null);
				if (objBefore != null && objAfter != null && objBefore.Equals(objAfter) == false)
					NotifyChanged();
				ReadValue();
			}
		}
		protected virtual void NotifyChanged()
		{
			MyBindingSource ds = DataSource as MyBindingSource;
			if (ds != null)
				ds.RaiseValueChanged(this);
		}
		protected override void OnParse(ConvertEventArgs cevent)
		{
			//base.OnParse(cevent);
			//WriteNotifyIfChanged();
		}
	}
	public class RadioButtonBinder : BindingWithNotify
	{
		public RadioButtonBinder(string propertyName, object dataSource, string dataMember) : base(propertyName, dataSource, dataMember)
		{
		}
		protected override void OnFormat(ConvertEventArgs cevent)
		{
			MyRadioButton b = Control as MyRadioButton;
			b.Checked = b.CheckedValue.Equals(cevent.Value);
		}
		protected override void OnParse(ConvertEventArgs cevent)
		{
			MyRadioButton b = Control as MyRadioButton;
			if (b.Checked)
				cevent.Value = b.CheckedValue;
		}
	}

	public class NameObjectBinder : BindingWithNotify
	{
		public NameObjectBinder(string propertyName, object dataSource, string dataMember) : base(propertyName, dataSource, dataMember)
		{
		}
		protected override void OnFormat(ConvertEventArgs cevent)
		{
			base.OnFormat(cevent);
		}
		protected override void OnParse(ConvertEventArgs cevent)
		{
			Console.WriteLine("OnParse ({0},{1})", cevent.DesiredType, cevent.Value);
			if (cevent.Value == DBNull.Value)
			{
			}
			//base.OnParse(cevent);
			cevent.Value = 1;
		}
	}

}
