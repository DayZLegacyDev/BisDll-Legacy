using System;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000028 RID: 40
	public class StageTransform
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00006D50 File Offset: 0x00004F50
		public StageTransform(BinaryReaderEx input)
		{
			this.uvSource = (StageTransform.UVSource)input.ReadUInt32();
			this.transformation = new Matrix4P(input);
		}

		// Token: 0x0400018A RID: 394
		public StageTransform.UVSource uvSource;

		// Token: 0x0400018B RID: 395
		public Matrix4P transformation;

		// Token: 0x02000064 RID: 100
		public enum UVSource
		{
			// Token: 0x040002EE RID: 750
			UVNone,
			// Token: 0x040002EF RID: 751
			UVTex,
			// Token: 0x040002F0 RID: 752
			UVTexWaterAnim,
			// Token: 0x040002F1 RID: 753
			UVPos,
			// Token: 0x040002F2 RID: 754
			UVNorm,
			// Token: 0x040002F3 RID: 755
			UVTex1,
			// Token: 0x040002F4 RID: 756
			UVWorldPos,
			// Token: 0x040002F5 RID: 757
			UVWorldNorm,
			// Token: 0x040002F6 RID: 758
			UVTexShoreAnim,
			// Token: 0x040002F7 RID: 759
			NUVSource
		}
	}
}
