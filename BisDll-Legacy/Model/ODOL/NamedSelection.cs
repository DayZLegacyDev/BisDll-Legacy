using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200001E RID: 30
	public class NamedSelection : IDeserializable
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005480 File Offset: 0x00003680
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00005488 File Offset: 0x00003688
		public string Name { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005494 File Offset: 0x00003694
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000549C File Offset: 0x0000369C
		public bool IsSectional { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000054A8 File Offset: 0x000036A8
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000054B0 File Offset: 0x000036B0
		public VertexIndex[] SelectedFaces { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000054BC File Offset: 0x000036BC
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000054C4 File Offset: 0x000036C4
		public int[] Sections { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000054D0 File Offset: 0x000036D0
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000054D8 File Offset: 0x000036D8
		public byte[] SelectedVerticesWeights { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000054E4 File Offset: 0x000036E4
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000054EC File Offset: 0x000036EC
		public VertexIndex[] SelectedVertices { get; private set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x000054F8 File Offset: 0x000036F8
		public void ReadObject(BinaryReaderEx input)
		{
			this.Name = input.ReadAsciiz();
			this.SelectedFaces = input.ReadCompressedVertexIndexArray();
			input.ReadInt32();
			this.IsSectional = input.ReadBoolean();
			this.Sections = input.ReadCompressedIntArray();
			this.SelectedVertices = input.ReadCompressedVertexIndexArray();
			int expectedSize = input.ReadInt32();
			this.SelectedVerticesWeights = input.ReadCompressed((uint)expectedSize);
		}
	}
}
