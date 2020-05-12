using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Drawing;

namespace CommonTools
{
	public class PropertyUtil
	{
		private static PropertyInfo GetNestedProperty(object dataobject, string[] nestedfields, int curindex, ref object nestedDataObject)
		{
			nestedDataObject = dataobject;
			string fieldname = nestedfields[curindex];
			PropertyInfo property = GetProperty(dataobject, fieldname);
			if (property == null)
				return null;
			dataobject = property.GetValue(dataobject, null);
			curindex++;
			if (curindex == nestedfields.Length)
				return property;
			return GetNestedProperty(dataobject, nestedfields, curindex, ref nestedDataObject);
		}
		public static PropertyInfo GetNestedProperty(object dataobject, ref object nestedDataObject, string fullFieldname)
		{
			string[] names = fullFieldname.Split('.');
			return GetNestedProperty(dataobject, names, 0, ref nestedDataObject);
		}
		public static PropertyInfo GetNestedProperty(ref object dataobject, string fullFieldname)
		{
			return GetNestedProperty(dataobject, ref dataobject, fullFieldname);
		}

		public static PropertyInfo GetProperty(object dataobject, string propertyname)
		{
			if (dataobject == null)
				return null;
			return GetProperty(dataobject.GetType(), propertyname);
		}
		public static PropertyInfo GetProperty(Type dataobjecttype, string propertyname)
		{
			MemberInfo[] members = dataobjecttype.GetMember(propertyname);
			foreach (MemberInfo member in members)
			{
				PropertyInfo property = (member as PropertyInfo);
				if (property != null)
					return property;
			}
			return null;
		}
		public static object GetPropertyValue(object dataobject, string propertyname)
		{
			PropertyInfo info = GetProperty(dataobject, propertyname);
			if (info == null)
				return null;
			return info.GetValue(dataobject, null);
		}

		private static bool IsPrimitive(object obj)
		{
			if (obj == null)
				return false;
			Type type = obj.GetType();
			if (type.IsPrimitive || type.IsEnum || type == typeof(string))
				return true;
			return false;
		}
		public static object ChangeType(object value, Type type)
		{
			if (value.GetType() == type)
				return value;
			/*
			if (IsClass(value))
			{
				if (value.GetType().IsSubclassOf(type))
					return value;
				return value;
			}
			 * */
			if (value.GetType() == typeof(string) && type.IsEnum)
			{
				try
				{
					return Enum.Parse(type, value.ToString());
				}
				catch { }
			}
			if (type.IsPrimitive == false)
			{
				object obj = Parse(value.ToString(), type);
				if (obj != null)
					return obj;
			}
			try
			{
				return Convert.ChangeType(value, type);
			}
			catch { }
			return null;
		}
		static public object Parse(string value, Type type)
		{
			if (type == typeof(System.Drawing.SizeF))
				return Parse(new System.Drawing.SizeF(0, 0), value);
			if (type == typeof(System.Drawing.PointF))
				return Parse(new System.Drawing.PointF(0, 0), value);
			if (type == typeof(System.Drawing.Color))
			{
				value = value.Replace("Color ", "");
				value = value.Trim(new char[] { '[', ']' });
				System.Drawing.Color c = System.Drawing.Color.FromName(value);
				if (c.IsKnownColor)
					return c;
				ColorWrapper color = new ColorWrapper();
				Parse(color, value);
				return color.Color;
			}
			return null;
		}
		public static void ParseProperty(string fieldname, string svalue, object dataobject)
		{
			if (fieldname.Length == 0 || svalue.Length == 0)
				return;

			PropertyInfo info = GetProperty(dataobject, fieldname);
			if (info == null || info.CanWrite == false)
				return;
			try
			{
				object value = ChangeType(svalue, info.PropertyType);
				if (value != null)
					info.SetValue(dataobject, value, null);
			}
			catch { };
		}
		public static object Parse(object obj, string valuestring)
		{
			// string will be in format '{property=value, property=value}'
			valuestring = valuestring.Trim(new char[] { '{', '}' });
			string[] properties = valuestring.Split(',');
			foreach (string propertyvalue in properties)
			{
				string[] propvaluepair = propertyvalue.Split('=');
				if (propvaluepair.Length != 2)
					continue;
				string fieldname = propvaluepair[0].Trim();
				string value = propvaluepair[1].Trim();
				ParseProperty(fieldname, value, obj);
			}
			return obj;
		}
		internal class ColorWrapper
		{
			int a = 0, r = 0, g = 0, b = 0;
			public int A
			{
				get { return a; }
				set { a = value; }
			}
			public int R
			{
				get { return r; }
				set { r = value; }
			}
			public int G
			{
				get { return g; }
				set { g = value; }
			}
			public int B
			{
				get { return b; }
				set { b = value; }
			}
			public System.Drawing.Color Color
			{
				get { return System.Drawing.Color.FromArgb(a, r, g, b); }
			}
		}
	}
}
