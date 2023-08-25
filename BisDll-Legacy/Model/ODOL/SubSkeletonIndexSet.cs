using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200002C RID: 44
	public class SubSkeletonIndexSet : IDeserializable
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00006E08 File Offset: 0x00005008
		public void ReadObject(BinaryReaderEx input)
		{
			this.subSkeletons = input.ReadIntArray();
		}

		// Token: 0x0400018E RID: 398
		private int[] subSkeletons;
	}
}
