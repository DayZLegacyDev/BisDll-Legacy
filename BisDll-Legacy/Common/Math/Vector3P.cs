using System;
using System.Globalization;
using BisDll.Stream;

namespace BisDll.Common.Math
{
	// Token: 0x02000048 RID: 72
	public class Vector3P : IDeserializable
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A440 File Offset: 0x00008640
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A44C File Offset: 0x0000864C
		public float X
		{
			get
			{
				return this.xyz[0];
			}
			set
			{
				this.xyz[0] = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A458 File Offset: 0x00008658
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000A464 File Offset: 0x00008664
		public float Y
		{
			get
			{
				return this.xyz[1];
			}
			set
			{
				this.xyz[1] = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A470 File Offset: 0x00008670
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000A47C File Offset: 0x0000867C
		public float Z
		{
			get
			{
				return this.xyz[2];
			}
			set
			{
				this.xyz[2] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A488 File Offset: 0x00008688
		public double Length
		{
			get
			{
				return System.Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z));
			}
		}

		// Token: 0x170000BA RID: 186
		public float this[int i]
		{
			get
			{
				return this.xyz[i];
			}
			set
			{
				this.xyz[i] = value;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A4E0 File Offset: 0x000086E0
		public Vector3P() : this(0f)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public Vector3P(float val) : this(val, val, val)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A4FC File Offset: 0x000086FC
		public Vector3P(BinaryReaderEx input) : this(input.ReadSingle(), input.ReadSingle(), input.ReadSingle())
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A528 File Offset: 0x00008728
		public Vector3P(float x, float y, float z)
		{
			this.xyz = new float[]
			{
				x,
				y,
				z
			};
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A548 File Offset: 0x00008748
		public static Vector3P operator -(Vector3P a)
		{
			return new Vector3P(0f - a.X, 0f - a.Y, 0f - a.Z);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A584 File Offset: 0x00008784
		public void ReadCompressed(BinaryReaderEx input)
		{
			int num = input.ReadInt32();
			int num2 = num & 1023;
			int num3 = num >> 10 & 1023;
			int num4 = num >> 20 & 1023;
			if (num2 > 511)
			{
				num2 -= 1024;
			}
			if (num3 > 511)
			{
				num3 -= 1024;
			}
			if (num4 > 511)
			{
				num4 -= 1024;
			}
			this.X = (float)((double)num2 * -0.0019569471624266144);
			this.Y = (float)((double)num3 * -0.0019569471624266144);
			this.Z = (float)((double)num4 * -0.0019569471624266144);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A62C File Offset: 0x0000882C
		public void Write(BinaryWriterEx output)
		{
			output.Write(this.X);
			output.Write(this.Y);
			output.Write(this.Z);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A664 File Offset: 0x00008864
		public static Vector3P operator *(Vector3P a, float b)
		{
			return new Vector3P(a.X * b, a.Y * b, a.Z * b);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A694 File Offset: 0x00008894
		public static float operator *(Vector3P a, Vector3P b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public static Vector3P operator +(Vector3P a, Vector3P b)
		{
			return new Vector3P(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A710 File Offset: 0x00008910
		public static Vector3P operator -(Vector3P a, Vector3P b)
		{
			return new Vector3P(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A750 File Offset: 0x00008950
		public override bool Equals(object obj)
		{
			Vector3P vector3P = obj as Vector3P;
			return vector3P != null && base.Equals(obj) && this.Equals(vector3P);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A788 File Offset: 0x00008988
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A790 File Offset: 0x00008990
		public bool Equals(Vector3P other)
		{
			Func<float, float, bool> func = (float f1, float f2) => (double)System.Math.Abs(f1 - f2) < 0.05;
			return func(this.X, other.X) && func(this.Y, other.Y) && func(this.Z, other.Z);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A808 File Offset: 0x00008A08
		public override string ToString()
		{
			CultureInfo cultureInfo = new CultureInfo("en-GB");
			return string.Concat(new string[]
			{
				"{",
				this.X.ToString(cultureInfo.NumberFormat),
				",",
				this.Y.ToString(cultureInfo.NumberFormat),
				",",
				this.Z.ToString(cultureInfo.NumberFormat),
				"}"
			});
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A894 File Offset: 0x00008A94
		public void ReadObject(BinaryReaderEx input)
		{
			this.xyz[0] = input.ReadSingle();
			this.xyz[1] = input.ReadSingle();
			this.xyz[2] = input.ReadSingle();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public float Distance(Vector3P v)
		{
			Vector3P vector3P = this - v;
			return (float)System.Math.Sqrt((double)(vector3P.X * vector3P.X + vector3P.Y * vector3P.Y + vector3P.Z * vector3P.Z));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A91C File Offset: 0x00008B1C
		public void Normalize()
		{
			float num = (float)this.Length;
			this.X /= num;
			this.Y /= num;
			this.Z /= num;
		}

		// Token: 0x040001CD RID: 461
		private float[] xyz;
	}
}
