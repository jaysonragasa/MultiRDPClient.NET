using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CommonTools
{
	public class ViewMap : Panel
	{
		object m_curKey = null;
		Dictionary<object, Control>	m_views = new Dictionary<object,Control>();
		public ViewMap()
		{
		}
		public void AddView(object key, Control view)
		{
			view.Dock = DockStyle.Fill;
			view.Visible = false;
			Form form = view as Form;
			if (form != null)
			{
				form.TopLevel = false;
				form.FormBorderStyle = FormBorderStyle.None;
			}
			m_views[key] = view;
			if (Controls.Contains(view) == false)
				Controls.Add(view);
		}
		public Control GetView(object key)
		{
			if (key == null)
				return null;
			if (m_views.ContainsKey(key))
				return m_views[key];
			return null;
		}
		public object CurKey
		{
			get { return m_curKey; }
			set { SelectView(value); }
		}
		public void SelectView(object key)
		{
			Control selectedview = GetView(key);
			foreach (Control view in m_views.Values)
			{
				if (object.ReferenceEquals(selectedview, view) == false)
					view.Hide();
			}
			if (selectedview != null)
				selectedview.Show();
			m_curKey = key;
		}
	}
}
