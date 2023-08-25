using System;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200002E RID: 46
	public struct VertexIndex
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000710C File Offset: 0x0000530C
		public static implicit operator int(VertexIndex vi)
		{
			return vi.value;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007114 File Offset: 0x00005314
		public static implicit operator VertexIndex(ushort vi)
		{
			return new VertexIndex
			{
				value = ((vi == ushort.MaxValue) ? -1 : ((int)vi))
			};
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007148 File Offset: 0x00005348
		public static implicit operator VertexIndex(int vi)
		{
			return new VertexIndex
			{
				value = vi
			};
		}

		// Token: 0x04000198 RID: 408
		public int value;
	}
}
