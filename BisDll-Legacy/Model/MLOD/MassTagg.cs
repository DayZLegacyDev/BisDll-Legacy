using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000035 RID: 53
	public class MassTagg : Tagg
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00007658 File Offset: 0x00005858
		public void Read(BinaryReaderEx input)
		{
			uint num = base.DataSize / 4U;
			this.mass = new float[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.mass[num2] = input.ReadSingle();
				num2++;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000076A0 File Offset: 0x000058A0
		public void Write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			uint num = base.DataSize / 4U;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				output.Write(this.mass[num2]);
				num2++;
			}
		}

		// Token: 0x040001A8 RID: 424
		public float[] mass;
	}
}
