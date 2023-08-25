using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000030 RID: 48
	public class VertexNeighborInfo : IDeserializable
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007208 File Offset: 0x00005408
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00007210 File Offset: 0x00005410
		public ushort PosA { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000721C File Offset: 0x0000541C
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007224 File Offset: 0x00005424
		public AnimationRTWeight RtwA { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007230 File Offset: 0x00005430
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007238 File Offset: 0x00005438
		public ushort PosB { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007244 File Offset: 0x00005444
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000724C File Offset: 0x0000544C
		public AnimationRTWeight RtwB { get; private set; }

		// Token: 0x06000170 RID: 368 RVA: 0x00007258 File Offset: 0x00005458
		public void ReadObject(BinaryReaderEx input)
		{
			this.PosA = input.ReadUInt16();
			input.ReadBytes(2);
			this.RtwA = new AnimationRTWeight();
			this.RtwA.ReadObject(input);
			this.PosB = input.ReadUInt16();
			input.ReadBytes(2);
			this.RtwB = new AnimationRTWeight();
			this.RtwB.ReadObject(input);
		}
	}
}
