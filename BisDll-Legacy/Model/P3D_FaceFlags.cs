using System;

namespace BisDll.Model
{
	// Token: 0x0200000D RID: 13
	public static class P3D_FaceFlags
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00003C24 File Offset: 0x00001E24
		public static byte GetUserValue(this FaceFlags flags)
		{
			return (byte)((long)((ulong)flags & 0xFE000000uL) >> 24);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003C34 File Offset: 0x00001E34
		public static void SetUserValue(this FaceFlags flags, byte value)
		{
			flags &= (FaceFlags)33554431;
			flags += (int)value << 24;
		}
	}
}
