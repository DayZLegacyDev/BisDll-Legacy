using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000033 RID: 51
	public class Face
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000073B0 File Offset: 0x000055B0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000073B8 File Offset: 0x000055B8
		public int NumberOfVertices { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000073C4 File Offset: 0x000055C4
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000073CC File Offset: 0x000055CC
		public Vertex[] Vertices { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000073D8 File Offset: 0x000055D8
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000073E0 File Offset: 0x000055E0
		public FaceFlags Flags { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000073EC File Offset: 0x000055EC
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000073F4 File Offset: 0x000055F4
		public string Texture { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00007400 File Offset: 0x00005600
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00007408 File Offset: 0x00005608
		public string Material { get; private set; }

		// Token: 0x06000181 RID: 385 RVA: 0x00007414 File Offset: 0x00005614
		public Face(int nVerts, Vertex[] verts, FaceFlags flags, string texture, string material)
		{
			this.NumberOfVertices = nVerts;
			this.Vertices = verts;
			this.Flags = flags;
			this.Texture = texture;
			this.Material = material;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007450 File Offset: 0x00005650
		public Face(BinaryReaderEx input)
		{
			this.Read(input);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007460 File Offset: 0x00005660
		public void Read(BinaryReaderEx input)
		{
			this.NumberOfVertices = input.ReadInt32();
			this.Vertices = new Vertex[4];
			for (int i = 0; i < 4; i++)
			{
				this.Vertices[i] = new Vertex(input);
			}
			this.Flags = (FaceFlags)input.ReadInt32();
			this.Texture = input.ReadAsciiz();
			this.Material = input.ReadAsciiz();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000074D0 File Offset: 0x000056D0
		public void Write(BinaryWriterEx output)
		{
			output.Write(this.NumberOfVertices);
			for (int i = 0; i < 4; i++)
			{
				if (i < this.Vertices.Length && this.Vertices[i] != null)
				{
					this.Vertices[i].Write(output);
				}
				else
				{
					output.Write(0);
					output.Write(0);
					output.Write(0);
					output.Write(0);
				}
			}
			output.Write((int)this.Flags);
			output.writeAsciiz(this.Texture);
			output.writeAsciiz(this.Material);
		}
	}
}
