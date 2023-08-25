using System;

namespace BisDll.Model
{
	// Token: 0x02000010 RID: 16
	public static class Resolution
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003C68 File Offset: 0x00001E68
		public static bool KeepsNamedSelections(float r)
		{
			return r == 1E+15f || r == 7E+15f || r == 1E+13f || r == 6E+15f || r == 1.3E+16f || r == 1.5E+16f || r == 8E+15f || r == 4E+15f || r == 5E+15f || r == 4E+13f || r == 2E+13f;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static LodName getLODType(this float res)
		{
			if (res == 1E+15f)
			{
				return LodName.Memory;
			}
			if (res == 2E+15f)
			{
				return LodName.LandContact;
			}
			if (res == 3E+15f)
			{
				return LodName.Roadway;
			}
			if (res == 4E+15f)
			{
				return LodName.Paths;
			}
			if (res == 5E+15f)
			{
				return LodName.HitPoints;
			}
			if (res == 6E+15f)
			{
				return LodName.ViewGeometry;
			}
			if (res == 7E+15f)
			{
				return LodName.FireGeometry;
			}
			if (res == 8E+15f)
			{
				return LodName.ViewCargoGeometry;
			}
			if (res == 9E+15f)
			{
				return LodName.ViewCargoFireGeometry;
			}
			if (res == 1E+16f)
			{
				return LodName.ViewCommander;
			}
			if (res == 1.1E+16f)
			{
				return LodName.ViewCommanderGeometry;
			}
			if (res == 1.2E+16f)
			{
				return LodName.ViewCommanderFireGeometry;
			}
			if (res == 1.3E+16f)
			{
				return LodName.ViewPilotGeometry;
			}
			if (res == 1.4E+16f)
			{
				return LodName.ViewPilotFireGeometry;
			}
			if (res == 1.4999999E+16f)
			{
				return LodName.ViewGunnerGeometry;
			}
			if (res == 1.6E+16f)
			{
				return LodName.ViewGunnerFireGeometry;
			}
			if (res == 1.7E+16f)
			{
				return LodName.SubParts;
			}
			if (res == 1.8E+16f)
			{
				return LodName.ShadowVolumeViewCargo;
			}
			if (res == 1.9E+16f)
			{
				return LodName.ShadowVolumeViewPilot;
			}
			if (res == 2E+16f)
			{
				return LodName.ShadowVolumeViewGunner;
			}
			if (res == 2.1E+16f)
			{
				return LodName.Wreck;
			}
			if (res == 1000f)
			{
				return LodName.ViewGunner;
			}
			if (res == 1100f)
			{
				return LodName.ViewPilot;
			}
			if (res == 1200f)
			{
				return LodName.ViewCargo;
			}
			if (res == 1E+13f)
			{
				return LodName.Geometry;
			}
			if (res == 4E+13f)
			{
				return LodName.PhysX;
			}
			if ((double)res >= 10000.0 && (double)res <= 20000.0)
			{
				return LodName.ShadowVolume;
			}
			return LodName.Resolution;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003E90 File Offset: 0x00002090
		public static string getLODName(this float res)
		{
			LodName lodtype = res.getLODType();
			string result;
			if (lodtype != LodName.ShadowVolume)
			{
				if (lodtype == LodName.Resolution)
				{
					result = res.ToString("#.000");
				}
				else
				{
					result = Enum.GetName(typeof(LodName), lodtype);
				}
			}
			else
			{
				result = "ShadowVolume" + (res - 10000f).ToString("#.000");
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003F08 File Offset: 0x00002108
		public static bool IsResolution(float r)
		{
			return r < 10000f;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003F14 File Offset: 0x00002114
		public static bool IsShadow(float r)
		{
			return (r >= 10000f && r < 20000f) || r == 2E+16f || r == 1.9E+16f || r == 1.8E+16f;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003F4C File Offset: 0x0000214C
		public static bool IsVisual(float r)
		{
			return Resolution.IsResolution(r) || r == 1200f || r == 1000f || r == 1100f || r == 1E+16f;
		}

		// Token: 0x04000052 RID: 82
		private const float specialLod = 1E+15f;

		// Token: 0x04000053 RID: 83
		public const float GEOMETRY = 1E+13f;

		// Token: 0x04000054 RID: 84
		public const float BUOYANCY = 2E+13f;

		// Token: 0x04000055 RID: 85
		public const float PHYSXOLD = 3E+13f;

		// Token: 0x04000056 RID: 86
		public const float PHYSX = 4E+13f;

		// Token: 0x04000057 RID: 87
		public const float MEMORY = 1E+15f;

		// Token: 0x04000058 RID: 88
		public const float LANDCONTACT = 2E+15f;

		// Token: 0x04000059 RID: 89
		public const float ROADWAY = 3E+15f;

		// Token: 0x0400005A RID: 90
		public const float PATHS = 4E+15f;

		// Token: 0x0400005B RID: 91
		public const float HITPOINTS = 5E+15f;

		// Token: 0x0400005C RID: 92
		public const float VIEW_GEOMETRY = 6E+15f;

		// Token: 0x0400005D RID: 93
		public const float FIRE_GEOMETRY = 7E+15f;

		// Token: 0x0400005E RID: 94
		public const float VIEW_GEOMETRY_CARGO = 8E+15f;

		// Token: 0x0400005F RID: 95
		public const float VIEW_GEOMETRY_PILOT = 1.3E+16f;

		// Token: 0x04000060 RID: 96
		public const float VIEW_GEOMETRY_GUNNER = 1.5E+16f;

		// Token: 0x04000061 RID: 97
		public const float FIRE_GEOMETRY_GUNNER = 1.6E+16f;

		// Token: 0x04000062 RID: 98
		public const float SUBPARTS = 1.7E+16f;

		// Token: 0x04000063 RID: 99
		public const float SHADOWVOLUME_CARGO = 1.8E+16f;

		// Token: 0x04000064 RID: 100
		public const float SHADOWVOLUME_PILOT = 1.9E+16f;

		// Token: 0x04000065 RID: 101
		public const float SHADOWVOLUME_GUNNER = 2E+16f;

		// Token: 0x04000066 RID: 102
		public const float WRECK = 2.1E+16f;

		// Token: 0x04000067 RID: 103
		public const float VIEW_COMMANDER = 1E+16f;

		// Token: 0x04000068 RID: 104
		public const float VIEW_GUNNER = 1000f;

		// Token: 0x04000069 RID: 105
		public const float VIEW_PILOT = 1100f;

		// Token: 0x0400006A RID: 106
		public const float VIEW_CARGO = 1200f;

		// Token: 0x0400006B RID: 107
		public const float SHADOWVOLUME = 10000f;

		// Token: 0x0400006C RID: 108
		public const float SHADOWBUFFER = 11000f;

		// Token: 0x0400006D RID: 109
		public const float SHADOW_MIN = 10000f;

		// Token: 0x0400006E RID: 110
		public const float SHADOW_MAX = 20000f;
	}
}
