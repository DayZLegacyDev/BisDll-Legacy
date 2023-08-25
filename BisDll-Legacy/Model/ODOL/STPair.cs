using System;
using BisDll.Common.Math;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000029 RID: 41
	public abstract class STPair
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006D70 File Offset: 0x00004F70
		public Vector3P S { get; } = new Vector3P();

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006D78 File Offset: 0x00004F78
		public Vector3P T { get; } = new Vector3P();
	}
}
