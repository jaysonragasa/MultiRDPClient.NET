using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonTools
{
	public partial class ColorPickerDialog : Form
	{
		CommonTools.ColorPickerCtrl m_colorPicker;
		public ColorPickerDialog()
		{
			InitializeComponent();

			m_colorPicker = new CommonTools.ColorPickerCtrl();
			m_colorPicker.Dock = DockStyle.Fill;
			m_colorWheelTabPage.Controls.Add(m_colorPicker);
			m_colorPicker.SelectionChanged += new EventHandler(OnItemChanged);
		}

		bool m_acceptItemChangedEvent = true;
		public Color SelectedColor
		{
			get { return m_curColor; }
			set
			{
				m_curColor = value;
				TabControlEventArgs e = new TabControlEventArgs(m_tabControl.SelectedTab, 0, TabControlAction.Selected);
				OnSelected(null, e);
			}
		}
		private void OnSelected(object sender, TabControlEventArgs e)
		{
			m_acceptItemChangedEvent = false;
			if (e.TabPage == m_colorWheelTabPage)
				m_colorPicker.SelectedColor = m_curColor;
			if (e.TabPage == m_knownColorsTabPage)
				m_colorList.SelectedColor = m_curColor;
			if (e.TabPage == m_systemColorsTabPage)
				m_systemColorList.SelectedColor = m_curColor;
			m_acceptItemChangedEvent = true;
		}

		Color m_curColor = Color.Wheat;
		private void OnItemChanged(object sender, EventArgs e)
		{
			if (m_acceptItemChangedEvent == false)
				return;
			if (sender == m_colorPicker)
				m_curColor = m_colorPicker.SelectedColor;
			if (sender == m_colorList)
				m_curColor = m_colorList.SelectedColor;
			if (sender == m_systemColorList)
				m_curColor = m_systemColorList.SelectedColor;
		}
	}
}