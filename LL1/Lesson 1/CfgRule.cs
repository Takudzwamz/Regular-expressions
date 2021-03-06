using System;
using System.Collections.Generic;
using System.Text;

namespace LL1
{
	/// <summary>
	/// Represents a rule in a CFG
	/// A rule takes the form of Left -> Right1 Right2 ... RightN
	/// </summary>
	/// <remarks>This class implements value semantics</remarks>
	class CfgRule : IEquatable<CfgRule>
	{
		public string Left { get; set; } = null;
		public IList<string> Right { get; } = new List<string>();
		public CfgRule(string left,IEnumerable<string> right) { Left = left; if(null!=Right) Right = new List<string>(right); }
		public CfgRule(string left, params string[] right) : this(left,(IEnumerable<string>)right) { }

		public CfgRule() { }
		
		public bool IsNil { get { return 0==Right.Count; } }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append(Left ?? "");
			sb.Append(" ->");
			var ic = Right.Count;
			for(var i = 0;i < ic;++i)
			{
				sb.Append(" ");
				sb.Append(Right[i]);
			}
			return sb.ToString();
		}
		public bool Equals(CfgRule rhs)
		{
			if (ReferenceEquals(this, rhs)) return true;
			if (ReferenceEquals(null,rhs)) return false;
			if(Equals(Left,rhs.Left))
			{
				if(Right.Count == rhs.Right.Count)
				{
					for(int ic = Right.Count,i=0;i<ic;++i)
						if (!Equals(Right[i], rhs.Right[i]))
							return false;
					return true;
				}
			}
			return false;
		}
		public override bool Equals(object obj)
			=> Equals(obj as CfgRule);
		public override int GetHashCode()
		{
			var result = 0;
			if (null != Left)
				result ^= Left.GetHashCode();
			for (int ic = Right.Count, i = 0; i < ic; ++i) {
				var r = Right[i];
				if (null != r) result ^= r.GetHashCode();
			}
			return result;
		}
		public static bool operator==(CfgRule lhs,CfgRule rhs)
		{
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}
		public static bool operator !=(CfgRule lhs, CfgRule rhs)
		{
			if (ReferenceEquals(lhs, rhs))
				return false;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return true;
			return !lhs.Equals(rhs);
		}
	}
}
