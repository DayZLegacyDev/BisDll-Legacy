using System;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200001A RID: 26
	public class Keyframe : IDeserializable
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public void ReadObject(BinaryReaderEx input)
		{
			this.time = input.ReadSingle();
			uint num = input.ReadUInt32();
			this.points = new Vector3P[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.points[num2] = new Vector3P(input);
				num2++;
			}
		}

		// Token: 0x040000C0 RID: 192
		public float time;

		// Token: 0x040000C1 RID: 193
		public Vector3P[] points;
	}
}
