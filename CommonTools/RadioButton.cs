using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonTools
{
	public class MyRadioButton : RadioButton
	{
		public MyRadioButton()
		{
			
		}
		object m_checkedValue = null;
		public object CheckedValue
		{
			get { return m_checkedValue; }
			set {}
		}
		public void AddDatabinding(MyBindingSource datasource, string datamember, object controlValue)
		{
			m_checkedValue = controlValue;
			DataBindings.Add(new RadioButtonBinder("CheckedValue", datasource, datamember));
		}
		protected override void OnCheckedChanged(EventArgs e)
		{
			base.OnCheckedChanged(e);
			if (Checked && DataBindings != null && DataBindings.Count > 0)
			{
				BindingWithNotify binding = DataBindings[0] as BindingWithNotify;
				if (binding != null)
					binding.WriteNotify();
			}
		}
	}
}
