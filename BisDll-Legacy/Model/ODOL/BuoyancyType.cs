using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000014 RID: 20
	public class BuoyancyType : IDeserializable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004218 File Offset: 0x00002418
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00004220 File Offset: 0x00002420
		public float Volume { get; private set; }

		// Token: 0x06000067 RID: 103 RVA: 0x0000422C File Offset: 0x0000242C
		public void ReadObject(BinaryReaderEx input)
		{
			this.Volume = input.ReadSingle();
		}
	}
}
