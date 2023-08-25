using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x0200003F RID: 63
	public class Vertex
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000086FC File Offset: 0x000068FC
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00008704 File Offset: 0x00006904
		public int PointIndex { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008710 File Offset: 0x00006910
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00008718 File Offset: 0x00006918
		public int NormalIndex { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008724 File Offset: 0x00006924
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000872C File Offset: 0x0000692C
		public float U { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00008738 File Offset: 0x00006938
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00008740 File Offset: 0x00006940
		public float V { get; private set; }

		// Token: 0x060001C1 RID: 449 RVA: 0x0000874C File Offset: 0x0000694C
		public Vertex(BinaryReaderEx input)
		{
			this.Read(input);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000875C File Offset: 0x0000695C
		public Vertex(int point, int normal, float u, float v)
		{
			this.PointIndex = point;
			this.NormalIndex = normal;
			this.U = u;
			this.V = v;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008790 File Offset: 0x00006990
		public void Read(BinaryReaderEx input)
		{
			this.PointIndex = input.ReadInt32();
			this.NormalIndex = input.ReadInt32();
			this.U = input.ReadSingle();
			this.V = input.ReadSingle();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000087D4 File Offset: 0x000069D4
		public void Write(BinaryWriterEx output)
		{
			output.Write(this.PointIndex);
			output.Write(this.NormalIndex);
			output.Write(this.U);
			output.Write(this.V);
		}
	}
}
