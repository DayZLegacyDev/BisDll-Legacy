using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x0200003B RID: 59
	public class SelectedTagg : Tagg
	{
		// Token: 0x060001AB RID: 427 RVA: 0x000083AC File Offset: 0x000065AC
		public void Read(BinaryReaderEx input, int nPoints, int nFaces)
		{
			this.weightedPoints = new byte[nPoints];
			for (int i = 0; i < nPoints; i++)
			{
				this.weightedPoints[i] = input.ReadByte();
			}
			this.faces = new byte[nFaces];
			for (int j = 0; j < nFaces; j++)
			{
				this.faces[j] = input.ReadByte();
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008410 File Offset: 0x00006610
		public void Write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			for (int i = 0; i < this.weightedPoints.Length; i++)
			{
				output.Write(this.weightedPoints[i]);
			}
			for (int j = 0; j < this.faces.Length; j++)
			{
				output.Write(this.faces[j]);
			}
		}

		// Token: 0x040001B4 RID: 436
		public byte[] weightedPoints;

		// Token: 0x040001B5 RID: 437
		public byte[] faces;
	}
}
