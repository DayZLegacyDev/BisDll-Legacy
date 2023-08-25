using System;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000016 RID: 22
	public class BuoyantPoint : IDeserializable
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000043EC File Offset: 0x000025EC
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000043F4 File Offset: 0x000025F4
		public Vector3P Coords { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004400 File Offset: 0x00002600
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004408 File Offset: 0x00002608
		public float SphereRadius { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004414 File Offset: 0x00002614
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000441C File Offset: 0x0000261C
		public float TypicalSurface { get; private set; }

		// Token: 0x06000085 RID: 133 RVA: 0x00004428 File Offset: 0x00002628
		public void ReadObject(BinaryReaderEx input)
		{
			this.Coords = new Vector3P(input);
			this.SphereRadius = input.ReadSingle();
			this.TypicalSurface = input.ReadSingle();
		}
	}
}
