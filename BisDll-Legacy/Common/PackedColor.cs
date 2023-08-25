using System;

namespace BisDll.Common
{
	// Token: 0x02000044 RID: 68
	public struct PackedColor
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000097D8 File Offset: 0x000079D8
		public byte A8
		{
			get
			{
				return (byte)(this.value >> 24 & 255U);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000097EC File Offset: 0x000079EC
		public byte R8
		{
			get
			{
				return (byte)(this.value >> 16 & 255U);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00009800 File Offset: 0x00007A00
		public byte G8
		{
			get
			{
				return (byte)(this.value >> 8 & 255U);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009814 File Offset: 0x00007A14
		public byte B8
		{
			get
			{
				return (byte)(this.value & 255U);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009824 File Offset: 0x00007A24
		public PackedColor(uint value)
		{
			this.value = value;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009830 File Offset: 0x00007A30
		public PackedColor(byte r, byte g, byte b, byte a = 255)
		{
			this.value = PackedColor.PackColor(r, g, b, a);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009844 File Offset: 0x00007A44
		public PackedColor(float r, float g, float b, float a)
		{
			byte r2 = (byte)(r * 255f);
			byte g2 = (byte)(g * 255f);
			byte b2 = (byte)(b * 255f);
			byte a2 = (byte)(a * 255f);
			this.value = PackedColor.PackColor(r2, g2, b2, a2);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000988C File Offset: 0x00007A8C
		internal static uint PackColor(byte r, byte g, byte b, byte a)
		{
			return (uint)((int)a << 24 | (int)r << 16 | (int)g << 8 | (int)b);
		}

		// Token: 0x040001C5 RID: 453
		private uint value;
	}
}
