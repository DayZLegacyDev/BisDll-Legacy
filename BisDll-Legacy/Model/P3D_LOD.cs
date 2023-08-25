using System;
using BisDll.Common.Math;

namespace BisDll.Model
{
	// Token: 0x0200000E RID: 14
	public abstract class P3D_LOD
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003C48 File Offset: 0x00001E48
		public string Name
		{
			get
			{
				return this.resolution.getLODName();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003C58 File Offset: 0x00001E58
		public float Resolution
		{
			get
			{
				return this.resolution;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83
		public abstract Vector3P[] Points { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000054 RID: 84
		public abstract Vector3P[] Normals { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85
		public abstract string[] Textures { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000056 RID: 86
		public abstract string[] MaterialNames { get; }

		// Token: 0x0400003A RID: 58
		protected float resolution;
	}
}
