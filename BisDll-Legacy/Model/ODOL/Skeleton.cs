using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000026 RID: 38
	public class Skeleton
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006C18 File Offset: 0x00004E18
		public string Name { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006C20 File Offset: 0x00004E20
		public bool isDiscrete { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006C28 File Offset: 0x00004E28
		public string[] bones { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006C30 File Offset: 0x00004E30
		public string pivotsNameObsolete { get; }

		// Token: 0x06000152 RID: 338 RVA: 0x00006C38 File Offset: 0x00004E38
		public Skeleton(BinaryReaderEx input)
		{
			int version = input.Version;
			this.Name = input.ReadAsciiz();
			if (!(this.Name == ""))
			{
				if (version >= 23)
				{
					this.isDiscrete = input.ReadBoolean();
				}
				int num = input.ReadInt32();
				this.bones = new string[num * 2];
				for (int i = 0; i < num; i++)
				{
					this.bones[i * 2] = input.ReadAsciiz();
					this.bones[i * 2 + 1] = input.ReadAsciiz();
				}
				if (version > 40)
				{
					this.pivotsNameObsolete = input.ReadAsciiz();
				}
			}
		}
	}
}
