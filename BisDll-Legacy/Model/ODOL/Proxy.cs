using System;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000023 RID: 35
	public class Proxy : IDeserializable
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00006968 File Offset: 0x00004B68
		public void ReadObject(BinaryReaderEx input)
		{
			this.proxyModel = input.ReadAsciiz();
			this.transformation = new Matrix4P(input);
			this.sequenceID = input.ReadInt32();
			this.namedSelectionIndex = input.ReadInt32();
			this.boneIndex = input.ReadInt32();
			if (input.Version >= 40)
			{
				this.sectionIndex = input.ReadInt32();
			}
		}

		// Token: 0x0400016B RID: 363
		public string proxyModel;

		// Token: 0x0400016C RID: 364
		public Matrix4P transformation;

		// Token: 0x0400016D RID: 365
		public int sequenceID;

		// Token: 0x0400016E RID: 366
		public int namedSelectionIndex;

		// Token: 0x0400016F RID: 367
		public int boneIndex;

		// Token: 0x04000170 RID: 368
		public int sectionIndex;
	}
}
