using System;
using BisDll.Stream;

namespace BisDll.Common.Math
{
	// Token: 0x02000045 RID: 69
	public class Matrix3P
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000098A0 File Offset: 0x00007AA0
		public Vector3P Aside
		{
			get
			{
				return this.columns[0];
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000098B0 File Offset: 0x00007AB0
		public Vector3P Up
		{
			get
			{
				return this.columns[1];
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000098C0 File Offset: 0x00007AC0
		public Vector3P Dir
		{
			get
			{
				return this.columns[2];
			}
		}

		// Token: 0x170000AB RID: 171
		public Vector3P this[int col]
		{
			get
			{
				return this.columns[col];
			}
		}

		// Token: 0x170000AC RID: 172
		public float this[int row, int col]
		{
			get
			{
				return this[col][row];
			}
			set
			{
				this[col][row] = value;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009900 File Offset: 0x00007B00
		public Matrix3P() : this(0f)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009910 File Offset: 0x00007B10
		public Matrix3P(float val) : this(new Vector3P(val), new Vector3P(val), new Vector3P(val))
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000992C File Offset: 0x00007B2C
		public Matrix3P(BinaryReaderEx input) : this(new Vector3P(input), new Vector3P(input), new Vector3P(input))
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009948 File Offset: 0x00007B48
		private Matrix3P(Vector3P aside, Vector3P up, Vector3P dir)
		{
			this.columns = new Vector3P[]
			{
				aside,
				up,
				dir
			};
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009968 File Offset: 0x00007B68
		public static Matrix3P operator -(Matrix3P a)
		{
			return new Matrix3P(-a.Aside, -a.Up, -a.Dir);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000099A0 File Offset: 0x00007BA0
		public static Matrix3P operator *(Matrix3P a, Matrix3P b)
		{
			Matrix3P matrix3P = new Matrix3P();
			float num = b[0, 0];
			float num2 = b[1, 0];
			float num3 = b[2, 0];
			matrix3P[0, 0] = a[0, 0] * num + a[0, 1] * num2 + a[0, 2] * num3;
			matrix3P[1, 0] = a[1, 0] * num + a[1, 1] * num2 + a[1, 2] * num3;
			matrix3P[2, 0] = a[2, 0] * num + a[2, 1] * num2 + a[2, 2] * num3;
			num = b[0, 1];
			num2 = b[1, 1];
			num3 = b[2, 1];
			matrix3P[0, 1] = a[0, 0] * num + a[0, 1] * num2 + a[0, 2] * num3;
			matrix3P[1, 1] = a[1, 0] * num + a[1, 1] * num2 + a[1, 2] * num3;
			matrix3P[2, 1] = a[2, 0] * num + a[2, 1] * num2 + a[2, 2] * num3;
			num = b[0, 2];
			num2 = b[1, 2];
			num3 = b[2, 2];
			matrix3P[0, 2] = a[0, 0] * num + a[0, 1] * num2 + a[0, 2] * num3;
			matrix3P[1, 2] = a[1, 0] * num + a[1, 1] * num2 + a[1, 2] * num3;
			matrix3P[2, 2] = a[2, 0] * num + a[2, 1] * num2 + a[2, 2] * num3;
			return matrix3P;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009B70 File Offset: 0x00007D70
		public void setTilda(Vector3P a)
		{
			this.Aside.Y = 0f - a.Z;
			this.Aside.Z = a.Y;
			this.Up.X = a.Z;
			this.Up.Z = 0f - a.X;
			this.Dir.X = 0f - a.Y;
			this.Dir.Y = a.X;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009BFC File Offset: 0x00007DFC
		public void write(BinaryWriterEx output)
		{
			this.Aside.Write(output);
			this.Up.Write(output);
			this.Dir.Write(output);
		}

		// Token: 0x040001C6 RID: 454
		private Vector3P[] columns;
	}
}
