using System;
using BisDll.Common;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200002D RID: 45
	public class UVSet
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006E20 File Offset: 0x00005020
		public float[] UVData
		{
			get
			{
				float[] array = new float[this.nVertices * 2U];
				float num = 0f;
				float num2 = 0f;
				double scale = 1.0;
				double scale2 = 1.0;
				if (this.isDiscretized)
				{
					scale = (double)(this.maxU - this.minU);
					scale2 = (double)(this.maxV - this.minV);
				}
				if (this.defaultFill)
				{
					if (this.isDiscretized)
					{
						num = this.Scale(BitConverter.ToInt16(this.defaultValue, 0), scale, this.minU);
						num2 = this.Scale(BitConverter.ToInt16(this.defaultValue, 2), scale2, this.minV);
					}
					else
					{
						num = BitConverter.ToSingle(this.defaultValue, 0);
						num2 = BitConverter.ToSingle(this.defaultValue, 4);
					}
				}
				int num3 = 0;
				while ((long)num3 < (long)((ulong)this.nVertices))
				{
					if (this.isDiscretized)
					{
						array[num3 * 2] = (this.defaultFill ? num : this.Scale(BitConverter.ToInt16(this.uvData, num3 * 4), scale, this.minU));
						array[num3 * 2 + 1] = (this.defaultFill ? num2 : this.Scale(BitConverter.ToInt16(this.uvData, num3 * 4 + 2), scale2, this.minV));
					}
					else
					{
						array[num3 * 2] = (this.defaultFill ? num : BitConverter.ToSingle(this.uvData, num3 * 8));
						array[num3 * 2 + 1] = (this.defaultFill ? num2 : BitConverter.ToSingle(this.uvData, num3 * 8 + 4));
					}
					num3++;
				}
				return array;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006FE0 File Offset: 0x000051E0
		private float Scale(short value, double scale, float min)
		{
			return (float)(1.52587890625E-05 * (double)(value + short.MaxValue) * scale) + min;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006FFC File Offset: 0x000051FC
		public void Read(BinaryReaderEx input, uint odolVersion)
		{
			this.isDiscretized = false;
			if (odolVersion >= 45U)
			{
				this.isDiscretized = true;
				this.minU = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.minU, "minU");
				this.minV = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.minV, "minV");
				this.maxU = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.maxU, "maxU");
				this.maxV = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.maxV, "maxV");
			}
			this.nVertices = input.ReadUInt32();
			Logging_Functions.Echo(input, this.nVertices, "nVertices", true);
			this.defaultFill = input.ReadBoolean();
			int num = (odolVersion >= 45U) ? 4 : 8;
			if (this.defaultFill)
			{
				this.defaultValue = input.ReadBytes(num);
				return;
			}
			this.uvData = input.ReadCompressed((uint)((ulong)this.nVertices * (ulong)((long)num)));
		}

		// Token: 0x0400018F RID: 399
		private bool isDiscretized;

		// Token: 0x04000190 RID: 400
		private float minU;

		// Token: 0x04000191 RID: 401
		private float minV;

		// Token: 0x04000192 RID: 402
		private float maxU;

		// Token: 0x04000193 RID: 403
		private float maxV;

		// Token: 0x04000194 RID: 404
		private uint nVertices;

		// Token: 0x04000195 RID: 405
		private bool defaultFill;

		// Token: 0x04000196 RID: 406
		private byte[] defaultValue;

		// Token: 0x04000197 RID: 407
		private byte[] uvData;
	}
}
