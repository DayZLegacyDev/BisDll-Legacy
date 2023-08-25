using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200002F RID: 47
	public static class VertexIndexExtensions
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000716C File Offset: 0x0000536C
		public static VertexIndex ReadVertexIndex(this BinaryReaderEx input)
		{
			if (input.Version >= 69)
			{
				return input.ReadInt32();
			}
			return input.ReadUInt16();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007194 File Offset: 0x00005394
		public static VertexIndex[] ReadCompressedVertexIndexArray(this BinaryReaderEx input)
		{
			if (input.Version >= 69)
			{
				return input.ReadCompressedArray<VertexIndex>((BinaryReaderEx i) => i.ReadInt32(), 4);
			}
			return input.ReadCompressedArray<VertexIndex>((BinaryReaderEx i) => i.ReadUInt16(), 2);
		}
	}
}
