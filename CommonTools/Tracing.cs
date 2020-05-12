using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommonTools
{
	public class Tracing
	{
		static Tracing m_instance = new Tracing();
		static public void StartTrack(int id)
		{
			if (m_instance.CanTrace(id) == false)
				return;
			m_instance.PushTick();
		}
		static public void EndTrack(int id, string text)
		{
			if (m_instance.CanTrace(id) == false)
				return;
			m_instance.PopTick(text, null);
		}
		static public void EndTrack(int id, string text, params object[] args)
		{
			if (m_instance.CanTrace(id) == false)
				return;
			m_instance.PopTick(text, args);
		}
		static public void AddId(int id)
		{
			m_instance.m_ids[id] = true;
		}
		static public void WriteLine(int id, string text, params object[] args)
		{
			if (m_instance.CanTrace(id) == false)
				return;
			m_instance.WriteLine(text, args);
		}
		static public void EnableTrace()
		{
			m_instance.m_thread = new Thread(new ThreadStart(m_instance.DoTrace));
			m_instance.m_thread.Name = "Tracing";
			m_instance.m_thread.Priority = ThreadPriority.Normal;
			m_instance.m_thread.Start();
		}
		static public void Terminate()
		{
			if (m_instance.m_thread != null)
				m_instance.m_thread.Abort();
			m_instance.m_thread = null;
		}
		Stack<int> m_ticks = new Stack<int>();
		Queue<StringBuilder> m_strings = new Queue<StringBuilder>();
		Dictionary<int, bool> m_ids = new Dictionary<int,bool>();
		Thread m_thread;
		ManualResetEvent m_wait = new ManualResetEvent(false);
		Tracing()
		{
		}
		bool CanTrace(int id)
		{
			return m_thread != null && m_ids.ContainsKey(id);
		}
		void PushTick()
		{
			m_ticks.Push(Environment.TickCount);
		}
		void PopTick(string text, params object[] args)
		{
			StringBuilder sb = new StringBuilder();
			int elapsedtime = Environment.TickCount - m_ticks.Pop();
			if (args == null)
				sb.AppendFormat("{0}: {1}, Ticks({2})", DateTime.Now.ToLongTimeString(), text, elapsedtime.ToString());
			else
			{
				sb.AppendFormat("{0}: ", DateTime.Now.ToLongTimeString());
				sb.AppendFormat(text, args);
				sb.AppendFormat(", Ticks({0})", elapsedtime.ToString());
			}
			sb.AppendLine();
			lock (m_strings)
			{
				m_strings.Enqueue(sb);
				m_wait.Set();
			}
		}
		void WriteLine(string text, params object[] args)
		{
			StringBuilder sb = new StringBuilder();
			if (args == null)
				sb.AppendFormat("{0}: {1}", DateTime.Now.ToLongTimeString(), text);
			else
			{
				sb.AppendFormat("{0}: ", DateTime.Now.ToLongTimeString());
				sb.AppendFormat(text, args);
			}
			sb.AppendLine();
			lock (m_strings)
			{
				m_strings.Enqueue(sb);
				m_wait.Set();
			}
		}
		void DoTrace()
		{
			while (true)
			{
				m_wait.WaitOne();
				while (m_strings.Count > 0)
				{
					StringBuilder sb = null;
					lock (m_strings)
					{
						sb = m_strings.Dequeue();
					}
					Console.Write(sb.ToString());
				}
				m_wait.Reset();
			}
		}
	}
}
