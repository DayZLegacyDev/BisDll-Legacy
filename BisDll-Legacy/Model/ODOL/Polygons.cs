using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000022 RID: 34
	public class Polygons
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000068E8 File Offset: 0x00004AE8
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000068F0 File Offset: 0x00004AF0
		public Polygon[] Faces { get; private set; }

		// Token: 0x06000148 RID: 328 RVA: 0x000068FC File Offset: 0x00004AFC
		public Polygons(BinaryReaderEx input)
		{
			uint num = input.ReadUInt32();
			input.ReadUInt32();
			input.ReadUInt16();
			this.Faces = new Polygon[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.Faces[num2] = new Polygon();
				this.Faces[num2].ReadObject(input);
				num2++;
			}
		}
	}
}
