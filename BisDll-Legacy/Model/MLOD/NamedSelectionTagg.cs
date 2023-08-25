using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000038 RID: 56
	public class NamedSelectionTagg : Tagg
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x000081E0 File Offset: 0x000063E0
		public void Read(BinaryReaderEx input, int nPoints, int nFaces)
		{
			this.points = new byte[nPoints];
			for (int i = 0; i < nPoints; i++)
			{
				this.points[i] = input.ReadByte();
			}
			this.faces = new byte[nFaces];
			for (int j = 0; j < nFaces; j++)
			{
				this.faces[j] = input.ReadByte();
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008244 File Offset: 0x00006444
		public void Write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			for (int i = 0; i < this.points.Length; i++)
			{
				output.Write(this.points[i]);
			}
			for (int j = 0; j < this.faces.Length; j++)
			{
				output.Write(this.faces[j]);
			}
		}

		// Token: 0x040001AF RID: 431
		public byte[] points;

		// Token: 0x040001B0 RID: 432
		public byte[] faces;
	}
}
