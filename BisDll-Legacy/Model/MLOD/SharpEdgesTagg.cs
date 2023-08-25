using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x0200003C RID: 60
	public class SharpEdgesTagg : Tagg
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00008490 File Offset: 0x00006690
		public void Read(BinaryReaderEx input)
		{
			uint num = base.DataSize / 8U;
			this.pointIndices = new uint[(int)((IntPtr)((long)((ulong)num))), 2];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.pointIndices[num2, 0] = input.ReadUInt32();
				this.pointIndices[num2, 1] = input.ReadUInt32();
				num2++;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000084FC File Offset: 0x000066FC
		public void Write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			uint num = base.DataSize / 8U;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				output.Write(this.pointIndices[num2, 0]);
				output.Write(this.pointIndices[num2, 1]);
				num2++;
			}
		}

		// Token: 0x040001B6 RID: 438
		public uint[,] pointIndices;
	}
}
