using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x0200003E RID: 62
	public class UVSetTagg : Tagg
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x000085A4 File Offset: 0x000067A4
		public void Read(BinaryReaderEx input, Face[] faces)
		{
			this.uvSetNr = input.ReadUInt32();
			this.faceUVs = new float[faces.Length][,];
			for (int i = 0; i < faces.Length; i++)
			{
				this.faceUVs[i] = new float[faces[i].NumberOfVertices, 2];
				for (int j = 0; j < faces[i].NumberOfVertices; j++)
				{
					this.faceUVs[i][j, 0] = input.ReadSingle();
					this.faceUVs[i][j, 1] = input.ReadSingle();
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000864C File Offset: 0x0000684C
		public void Write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			output.Write(this.uvSetNr);
			for (int i = 0; i < this.faceUVs.Length; i++)
			{
				for (int j = 0; j < this.faceUVs[i].Length / 2; j++)
				{
					output.Write(this.faceUVs[i][j, 0]);
					output.Write(this.faceUVs[i][j, 1]);
				}
			}
		}

		// Token: 0x040001B9 RID: 441
		public uint uvSetNr;

		// Token: 0x040001BA RID: 442
		public float[][,] faceUVs;
	}
}
