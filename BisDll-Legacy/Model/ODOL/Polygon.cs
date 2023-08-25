using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000021 RID: 33
	public class Polygon : IDeserializable
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006880 File Offset: 0x00004A80
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006888 File Offset: 0x00004A88
		public VertexIndex[] VertexIndices { get; private set; }

		// Token: 0x06000144 RID: 324 RVA: 0x00006894 File Offset: 0x00004A94
		public void ReadObject(BinaryReaderEx input)
		{
			int version = input.Version;
			byte b = input.ReadByte();
			this.VertexIndices = new VertexIndex[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.VertexIndices[i] = input.ReadVertexIndex();
			}
		}
	}
}
