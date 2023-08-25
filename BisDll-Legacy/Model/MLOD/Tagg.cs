using System;

namespace BisDll.Model.MLOD
{
	// Token: 0x0200003D RID: 61
	public abstract class Tagg
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008574 File Offset: 0x00006774
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000857C File Offset: 0x0000677C
		public uint DataSize { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008588 File Offset: 0x00006788
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00008590 File Offset: 0x00006790
		public string Name { get; set; }
	}
}
