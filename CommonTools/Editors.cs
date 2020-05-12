using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonTools
{
	public abstract class BaseEditor<T> : TextBox
	{
		protected string m_filterString = string.Empty;
		protected T m_lastValue;
		protected virtual bool IsValidChar(char ch)
		{
			if (m_filterString.Length == 0 || m_filterString.IndexOf(ch) >= 0)
				return true;
			return false;
		}
		protected override void OnLeave(EventArgs e)
		{
			SetValue(GetValue());
			base.OnLeave(e);
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (IsValidChar(e.KeyChar) == false)
			{
				e.Handled = true;
				return;
			}
			base.OnKeyPress(e);
		}
		public T Value
		{
			get { return GetValue(); }
			set { SetValue(value); }
		}

		protected virtual T GetValue()
		{
			string s = GetValidatedText();
			try
			{
				return (T)Convert.ChangeType(s, typeof(T));
			}
			catch {};
			return m_lastValue;
		}
		protected virtual void SetValue(T value)
		{
			Text = value.ToString();
			m_lastValue = value;
		}
		protected virtual string GetValidatedText()
		{
			return Text;
		}
	}

	public class IntEditor : BaseEditor<Int32>
	{
		public bool AllowNegativeNumber
		{
			set
			{
				if (value)
					m_filterString = "-0123456789";
				else
					m_filterString = "0123456789";
			}
		}
		public IntEditor()
		{
			AllowNegativeNumber = true;
			TextAlign = HorizontalAlignment.Right;
		}
	}
	public class FloatEditor : BaseEditor<float>
	{
		public bool AllowNegativeNumber
		{
			set
			{
				if (value)
					m_filterString = "-.0123456789";
				else
					m_filterString = ".0123456789";
			}
		}
		public FloatEditor()
		{
			AllowNegativeNumber = false;
			TextAlign = HorizontalAlignment.Right;
		}
	}
}
