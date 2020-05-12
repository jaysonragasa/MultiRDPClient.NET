using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTools
{
	public class NameObject<T>
	{
		public string m_name = string.Empty;
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}
		public T m_object;
		public T Object
		{
			get { return m_object; }
			set { m_object = value; }
		}
		public NameObject(string name, T obj)
		{
			Name = name;
			Object = obj;
		}
		public override string ToString()
		{
			return Name;
		}
	}
	public class NameObjectCollection<T> : List<NameObject<T> >
	{
		public void Add(string name, T value)
		{
			Add(new NameObject<T>(name, value));
		}
		public NameObject<T> FindValue(T value)
		{
			foreach(NameObject<T> item in this)
			{
				if (item.Object.Equals(value))
					return item;
			}
			return null;
		}
	}
}
