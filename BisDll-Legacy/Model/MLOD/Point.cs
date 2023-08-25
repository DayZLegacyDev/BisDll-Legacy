using System;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000039 RID: 57
	public class Point : Vector3P
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000082C4 File Offset: 0x000064C4
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000082CC File Offset: 0x000064CC
		public PointFlags PointFlags { get; private set; }

		// Token: 0x060001A5 RID: 421 RVA: 0x000082D8 File Offset: 0x000064D8
		public Point(Vector3P pos, PointFlags flags) : base(pos.X, pos.Y, pos.Z)
		{
			this.PointFlags = flags;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008308 File Offset: 0x00006508
		public Point(BinaryReaderEx input) : base(input)
		{
			this.PointFlags = (PointFlags)input.ReadUInt32();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008320 File Offset: 0x00006520
		public new void Write(BinaryWriterEx output)
		{
			base.Write(output);
			output.Write((uint)this.PointFlags);
		}
	}
}
