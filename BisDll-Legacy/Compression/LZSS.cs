using System;
using System.IO;

namespace BisDll.Compression
{
	// Token: 0x02000041 RID: 65
	public static class LZSS
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00009290 File Offset: 0x00007490
		public static uint readLZSS( System.IO.Stream input, out byte[] dst, uint expectedSize, bool useSignedChecksum)
		{
			char[] array = new char[4113];
			dst = new byte[expectedSize];
			if (expectedSize == 0U)
			{
				return 0U;
			}
			long position = input.Position;
			uint num = expectedSize;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 4078; i++)
			{
				array[i] = ' ';
			}
			int num4 = 4078;
			int num5 = 0;
			while (num != 0U)
			{
				if (((num5 >>= 1) & 256) == 0)
				{
					num5 = (input.ReadByte() | 65280);
				}
				if ((num5 & 1) != 0)
				{
					int num6 = input.ReadByte();
					num3 = ((!useSignedChecksum) ? (num3 + (int)((byte)num6)) : (num3 + (int)((sbyte)num6)));
					dst[num2++] = (byte)num6;
					num -= 1U;
					array[num4] = (char)num6;
					num4++;
					num4 &= 4095;
				}
				else
				{
					int num7 = input.ReadByte();
					int num8 = input.ReadByte();
					num7 |= (num8 & 240) << 4;
					num8 &= 15;
					num8 += 2;
					int j = num4 - num7;
					int num9 = num8 + j;
					if ((long)(num8 + 1) > (long)((ulong)num))
					{
						throw new ArgumentException("LZSS overflow");
					}
					while (j <= num9)
					{
						int num10 = (int)((byte)array[j & 4095]);
						num3 = ((!useSignedChecksum) ? (num3 + (int)((byte)num10)) : (num3 + (int)((sbyte)num10)));
						dst[num2++] = (byte)num10;
						num -= 1U;
						array[num4] = (char)num10;
						num4++;
						num4 &= 4095;
						j++;
					}
				}
			}
			byte[] array2 = new byte[4];
			input.Read(array2, 0, 4);
			if (BitConverter.ToInt32(array2, 0) != num3)
			{
				throw new ArgumentException("Checksum mismatch");
			}
			return (uint)(input.Position - position);
		}
	}
}
