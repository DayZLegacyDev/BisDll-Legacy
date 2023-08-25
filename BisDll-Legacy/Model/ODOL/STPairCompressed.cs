using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200002A RID: 42
	public class STPairCompressed : STPair, IDeserializable
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public void ReadObject(BinaryReaderEx input)
		{
			base.S.ReadCompressed(input);
			base.T.ReadCompressed(input);
		}
	}
}
