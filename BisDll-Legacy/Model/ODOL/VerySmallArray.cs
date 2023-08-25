using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000031 RID: 49
	public abstract class VerySmallArray : IDeserializable
	{
		// Token: 0x06000172 RID: 370 RVA: 0x000072C8 File Offset: 0x000054C8
		public void ReadObject(BinaryReaderEx input)
		{
			this.nSmall = input.ReadInt32();
			this.smallSpace = input.ReadBytes(8);
		}

		// Token: 0x0400019D RID: 413
		protected int nSmall;

		// Token: 0x0400019E RID: 414
		protected byte[] smallSpace;
	}
}
