using System;
using BisDll.Stream;

namespace BisDll.Common.Math
{
	// Token: 0x02000049 RID: 73
	public class Vector3PCompressed : IDeserializable
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A960 File Offset: 0x00008B60
		public float X
		{
			get
			{
				int num = this.value & 1023;
				if (num > 511)
				{
					num -= 1024;
				}
				return (float)num * -0.0019569471f;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A99C File Offset: 0x00008B9C
		public float Y
		{
			get
			{
				int num = this.value >> 10 & 1023;
				if (num > 511)
				{
					num -= 1024;
				}
				return (float)num * -0.0019569471f;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public float Z
		{
			get
			{
				int num = this.value >> 20 & 1023;
				if (num > 511)
				{
					num -= 1024;
				}
				return (float)num * -0.0019569471f;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000AA14 File Offset: 0x00008C14
		public static implicit operator Vector3P(Vector3PCompressed src)
		{
			int num = src.value & 1023;
			int num2 = src.value >> 10 & 1023;
			int num3 = src.value >> 20 & 1023;
			if (num > 511)
			{
				num -= 1024;
			}
			if (num2 > 511)
			{
				num2 -= 1024;
			}
			if (num3 > 511)
			{
				num3 -= 1024;
			}
			return new Vector3P((float)num * -0.0019569471f, (float)num2 * -0.0019569471f, (float)num3 * -0.0019569471f);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public static implicit operator int(Vector3PCompressed src)
		{
			return src.value;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public static implicit operator Vector3PCompressed(int src)
		{
			return new Vector3PCompressed
			{
				value = src
			};
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public void ReadObject(BinaryReaderEx input)
		{
			this.value = input.ReadInt32();
		}

		// Token: 0x040001CE RID: 462
		private int value;

		// Token: 0x040001CF RID: 463
		private const float scaleFactor = -0.0019569471f;
	}
}
