using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200001B RID: 27
	public class LoadableLodInfo : IDeserializable
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004CFC File Offset: 0x00002EFC
		public void ReadObject(BinaryReaderEx input)
		{
			this.nFaces = input.ReadInt32();
			this.color = input.ReadUInt32();
			this.special = input.ReadInt32();
			this.orHints = input.ReadUInt32();
			int version = input.Version;
			if (version >= 39)
			{
				this.hasSkeleton = input.ReadBoolean();
			}
			if (version >= 51)
			{
				this.nVertices = input.ReadInt32();
				this.faceArea = input.ReadSingle();
			}
		}

		// Token: 0x040000C2 RID: 194
		private int nFaces;

		// Token: 0x040000C3 RID: 195
		private uint color;

		// Token: 0x040000C4 RID: 196
		private int special;

		// Token: 0x040000C5 RID: 197
		private uint orHints;

		// Token: 0x040000C6 RID: 198
		private bool hasSkeleton;

		// Token: 0x040000C7 RID: 199
		private int nVertices;

		// Token: 0x040000C8 RID: 200
		private float faceArea;
	}
}
