using System;
using System.Collections.Generic;
using System.Linq;

namespace BisDll
{
	// Token: 0x02000005 RID: 5
	public static class Methods
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000028A0 File Offset: 0x00000AA0
		public static void Swap<T>(ref T v1, ref T v2)
		{
			T t = v1;
			v1 = v2;
			v2 = t;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000028CC File Offset: 0x00000ACC
		public static bool EqualsFloat(float f1, float f2, float tolerance = 0.0001f)
		{
			return Math.Abs(f1 - f2) <= tolerance;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028E0 File Offset: 0x00000AE0
		public static IEnumerable<T> Yield<T>(this T src)
		{
			yield return src;
			yield break;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028F0 File Offset: 0x00000AF0
		public static IEnumerable<T> Yield<T>(params T[] elems)
		{
			return elems;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000028F4 File Offset: 0x00000AF4
		public static string CharsToString(this IEnumerable<char> chars)
		{
			return new string(chars.ToArray<char>());
		}
	}
}
