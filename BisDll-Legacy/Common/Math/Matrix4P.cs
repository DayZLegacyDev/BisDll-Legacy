using System;
using BisDll.Stream;

namespace BisDll.Common.Math
{
	// Token: 0x02000046 RID: 70
	public class Matrix4P
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009C34 File Offset: 0x00007E34
		public Matrix3P Orientation
		{
			get
			{
				return this.orientation;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009C3C File Offset: 0x00007E3C
		public Vector3P Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000AF RID: 175
		public float this[int row, int col]
		{
			get
			{
				if (col != 3)
				{
					return this.orientation[col][row];
				}
				return this.position[row];
			}
			set
			{
				if (col == 3)
				{
					this.position[row] = value;
					return;
				}
				this.orientation[col][row] = value;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009C98 File Offset: 0x00007E98
		public Matrix4P() : this(0f)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public Matrix4P(float val) : this(new Matrix3P(val), new Vector3P(val))
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009CBC File Offset: 0x00007EBC
		public Matrix4P(BinaryReaderEx input) : this(new Matrix3P(input), new Vector3P(input))
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009CD0 File Offset: 0x00007ED0
		private Matrix4P(Matrix3P orientation, Vector3P position)
		{
			this.orientation = orientation;
			this.position = position;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public static Matrix4P operator *(Matrix4P a, Matrix4P b)
		{
			Matrix4P matrix4P = new Matrix4P();
			float num = b[0, 0];
			float num2 = b[1, 0];
			float num3 = b[2, 0];
			matrix4P[0, 0] = a[0, 0] * num + a[0, 1] * num2 + a[0, 2] * num3;
			matrix4P[1, 0] = a[1, 0] * num + a[1, 1] * num2 + a[1, 2] * num3;
			matrix4P[2, 0] = a[2, 0] * num + a[2, 1] * num2 + a[2, 2] * num3;
			num = b[0, 1];
			num2 = b[1, 1];
			num3 = b[2, 1];
			matrix4P[0, 1] = a[0, 0] * num + a[0, 1] * num2 + a[0, 2] * num3;
			matrix4P[1, 1] = a[1, 0] * num + a[1, 1] * num2 + a[1, 2] * num3;
			matrix4P[2, 1] = a[2, 0] * num + a[2, 1] * num2 + a[2, 2] * num3;
			num = b[0, 2];
			num2 = b[1, 2];
			num3 = b[2, 2];
			matrix4P[0, 2] = a[0, 0] * num + a[0, 1] * num2 + a[0, 2] * num3;
			matrix4P[1, 2] = a[1, 0] * num + a[1, 1] * num2 + a[1, 2] * num3;
			matrix4P[2, 2] = a[2, 0] * num + a[2, 1] * num2 + a[2, 2] * num3;
			num = b.Position.X;
			num2 = b.Position.Y;
			num3 = b.Position.Z;
			matrix4P.Position.X = a[0, 0] * num + a[0, 1] * num2 + a[0, 2] * num3 + a.Position.X;
			matrix4P.Position.Y = a[1, 0] * num + a[1, 1] * num2 + a[1, 2] * num3 + a.Position.Y;
			matrix4P.Position.Z = a[2, 0] * num + a[2, 1] * num2 + a[2, 2] * num3 + a.Position.Z;
			return matrix4P;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009F80 File Offset: 0x00008180
		public void write(BinaryWriterEx output)
		{
			this.orientation.write(output);
			this.position.Write(output);
		}

		// Token: 0x040001C7 RID: 455
		private Matrix3P orientation;

		// Token: 0x040001C8 RID: 456
		private Vector3P position;
	}
}
