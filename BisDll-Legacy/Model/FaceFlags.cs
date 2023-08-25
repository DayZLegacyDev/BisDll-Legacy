using System;

namespace BisDll.Model
{
	// Token: 0x0200000A RID: 10
	[Flags]
	public enum FaceFlags
	{
		// Token: 0x04000011 RID: 17
		DEFAULT = 0,
		// Token: 0x04000012 RID: 18
		SHADOW_OFF = 16,
		// Token: 0x04000013 RID: 19
		MERGING_OFF = 16777216,
		// Token: 0x04000014 RID: 20
		ZBIAS_LOW = 256,
		// Token: 0x04000015 RID: 21
		ZBIAS_MID = 512,
		// Token: 0x04000016 RID: 22
		ZBIAS_HIGH = 768,
		// Token: 0x04000017 RID: 23
		LIGHTNING_BOTH = 32,
		// Token: 0x04000018 RID: 24
		LIGHTNING_POSITION = 128,
		// Token: 0x04000019 RID: 25
		LIGHTNING_FLAT = 2097152,
		// Token: 0x0400001A RID: 26
		LIGHTNING_REVERSED = 1048576
	}
}
