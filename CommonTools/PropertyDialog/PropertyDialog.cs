using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonTools
{
	public partial class PropertyDialog : Form
	{
		public PropertyDialog()
		{
			this.DoubleBuffered = true;
			InitializeComponent();
		}
		
		Size m_viewSize = Size.Empty;
		Dictionary<object, object> m_dataObjects = new Dictionary<object, object>();
		
		public void BeforeLoadPages()
		{
			m_viewSize = m_viewPanel.Size;
			m_treeView.BeginUpdate();
		}
		public TreeNode AddPage(object key, Control page, object dataobject)
		{
			return AddPage(key, page, null, dataobject);
		}
		public TreeNode AddPage(object key, Control page, TreeNode parentnode, object dataobject)
		{
			if (page.Width > m_viewSize.Width)
				m_viewSize.Width = page.Width;
			if (page.Height > m_viewSize.Height)
				m_viewSize.Height = page.Height;

			m_viewPanel.AddView(key, page);
			m_dataObjects[key] = dataobject;

			TreeNode newnode = new TreeNode(page.Text);
			newnode.Tag = key;
			if (parentnode != null)
				parentnode.Nodes.Add(newnode);
			else
				m_treeView.Nodes.Add(newnode);
			return newnode;
		}
		public void AfterLoadPages()
		{
			if (m_treeView.Nodes.Count > 0)
				m_treeView.SelectedNode = m_treeView.Nodes[0];
			m_treeView.EndUpdate();

			int diffx = m_viewSize.Width - m_viewPanel.Width;
			int diffy = m_viewSize.Height - m_viewPanel.Height;
			if (diffx < 0)
				diffx = 0;
			if (diffy < 0)
				diffy = 0;
			this.Width += diffx;
			this.Height += diffy;
			this.MinimumSize = this.Size;
		}
		public void SelectPage(object key)
		{
			TreeIteratorMatchTag it = new TreeIteratorMatchTag(m_treeView, key);
			TreeNode node = it.Execute();
			if (node != null)
				m_treeView.SelectedNode = node;
		}
		private void OnAfterTreeSelect(object sender, TreeViewEventArgs e)
		{
			IPropertyDialogPage curpage = m_viewPanel.GetView(m_viewPanel.CurKey) as IPropertyDialogPage;
			if (curpage != null)
				curpage.BeforeDeactivated(m_dataObjects[m_viewPanel.CurKey]);

			curpage = m_viewPanel.GetView(m_treeView.SelectedNode.Tag) as IPropertyDialogPage;
			if (curpage != null)
				curpage.BeforeActivated(m_dataObjects[m_treeView.SelectedNode.Tag]);

			m_viewPanel.SelectView(m_treeView.SelectedNode.Tag);
			m_label.Text = m_treeView.SelectedNode.Text;
		}
	}
}