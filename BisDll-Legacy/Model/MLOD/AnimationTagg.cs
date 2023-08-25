using System;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000032 RID: 50
	public class AnimationTagg : Tagg
	{
		// Token: 0x06000174 RID: 372 RVA: 0x000072EC File Offset: 0x000054EC
		public void read(BinaryReaderEx input)
		{
			uint num = (base.DataSize - 4U) / 12U;
			this.frameTime = input.ReadSingle();
			this.framePoints = new Vector3P[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.framePoints[num2] = new Vector3P(input);
				num2++;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007344 File Offset: 0x00005544
		public void write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			output.Write(this.frameTime);
			for (int i = 0; i < this.framePoints.Length; i++)
			{
				this.framePoints[i].Write(output);
			}
		}

		// Token: 0x0400019F RID: 415
		public float frameTime;

		// Token: 0x040001A0 RID: 416
		public Vector3P[] framePoints;
	}
}
