using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000034 RID: 52
	public class LockTagg : Tagg
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00007574 File Offset: 0x00005774
		public void Read(BinaryReaderEx input, int nPoints, int nFaces)
		{
			this.lockedPoints = new bool[nPoints];
			for (int i = 0; i < nPoints; i++)
			{
				this.lockedPoints[i] = input.ReadBoolean();
			}
			this.lockedFaces = new bool[nFaces];
			for (int j = 0; j < nFaces; j++)
			{
				this.lockedFaces[j] = input.ReadBoolean();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000075D8 File Offset: 0x000057D8
		public void Write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			for (int i = 0; i < this.lockedPoints.Length; i++)
			{
				output.Write(this.lockedPoints[i]);
			}
			for (int j = 0; j < this.lockedFaces.Length; j++)
			{
				output.Write(this.lockedFaces[j]);
			}
		}

		// Token: 0x040001A6 RID: 422
		public bool[] lockedPoints;

		// Token: 0x040001A7 RID: 423
		public bool[] lockedFaces;
	}
}
