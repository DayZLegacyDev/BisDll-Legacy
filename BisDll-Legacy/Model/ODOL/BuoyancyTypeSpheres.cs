using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000015 RID: 21
	public class BuoyancyTypeSpheres : BuoyancyType, IDeserializable
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000424C File Offset: 0x0000244C
		public int ArraySizeX { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004258 File Offset: 0x00002458
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00004260 File Offset: 0x00002460
		public int ArraySizeY { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000426C File Offset: 0x0000246C
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00004274 File Offset: 0x00002474
		public int ArraySizeZ { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004280 File Offset: 0x00002480
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00004288 File Offset: 0x00002488
		public float StepX { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004294 File Offset: 0x00002494
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000429C File Offset: 0x0000249C
		public float StepY { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000042A8 File Offset: 0x000024A8
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000042B0 File Offset: 0x000024B0
		public float StepZ { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000042BC File Offset: 0x000024BC
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000042C4 File Offset: 0x000024C4
		public float FullSphereRadius { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000042D0 File Offset: 0x000024D0
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000042D8 File Offset: 0x000024D8
		public int MinSpheres { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000042E4 File Offset: 0x000024E4
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000042EC File Offset: 0x000024EC
		public int MaxSpheres { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000042F8 File Offset: 0x000024F8
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00004300 File Offset: 0x00002500
		public BuoyantPoint[] BuoyancyPoints { get; private set; }

		// Token: 0x0600007D RID: 125 RVA: 0x0000430C File Offset: 0x0000250C
		public new void ReadObject(BinaryReaderEx input)
		{
			this.ArraySizeX = input.ReadInt32();
			this.ArraySizeY = input.ReadInt32();
			this.ArraySizeZ = input.ReadInt32();
			this.StepX = input.ReadSingle();
			this.StepY = input.ReadSingle();
			this.StepZ = input.ReadSingle();
			this.FullSphereRadius = input.ReadSingle();
			this.MinSpheres = input.ReadInt32();
			this.MaxSpheres = input.ReadInt32();
			int num = this.ArraySizeX * this.ArraySizeY * this.ArraySizeZ;
			this.BuoyancyPoints = new BuoyantPoint[num];
			for (int i = 0; i < num; i++)
			{
				this.BuoyancyPoints[i] = new BuoyantPoint();
				this.BuoyancyPoints[i].ReadObject(input);
			}
			base.ReadObject(input);
		}
	}
}
