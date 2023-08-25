using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200002B RID: 43
	public class STPairUncompressed : STPair, IDeserializable
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public void ReadObject(BinaryReaderEx input)
		{
			base.S.ReadObject(input);
			base.T.ReadObject(input);
		}
	}
}
