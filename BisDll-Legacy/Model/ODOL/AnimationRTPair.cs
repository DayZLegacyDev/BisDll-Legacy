using System;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000011 RID: 17
	public class AnimationRTPair
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003F84 File Offset: 0x00002184
		public byte SelectionIndex { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003F8C File Offset: 0x0000218C
		public byte Weight { get; }

		// Token: 0x06000060 RID: 96 RVA: 0x00003F94 File Offset: 0x00002194
		public AnimationRTPair(byte sel, byte weight)
		{
			this.SelectionIndex = sel;
			this.Weight = weight;
		}
	}
}
