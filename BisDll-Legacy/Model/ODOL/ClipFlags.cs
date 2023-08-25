using System;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum ClipFlags
	{
		// Token: 0x04000085 RID: 133
		ClipNone,
		// Token: 0x04000086 RID: 134
		ClipFront,
		// Token: 0x04000087 RID: 135
		ClipBack,
		// Token: 0x04000088 RID: 136
		ClipLeft = 4,
		// Token: 0x04000089 RID: 137
		ClipRight = 8,
		// Token: 0x0400008A RID: 138
		ClipBottom = 16,
		// Token: 0x0400008B RID: 139
		ClipTop = 32,
		// Token: 0x0400008C RID: 140
		ClipUser0 = 64,
		// Token: 0x0400008D RID: 141
		ClipAll = 63,
		// Token: 0x0400008E RID: 142
		ClipLandMask = 3840,
		// Token: 0x0400008F RID: 143
		ClipLandStep = 256,
		// Token: 0x04000090 RID: 144
		ClipLandNone = 0,
		// Token: 0x04000091 RID: 145
		ClipLandOn = 256,
		// Token: 0x04000092 RID: 146
		ClipLandUnder = 512,
		// Token: 0x04000093 RID: 147
		ClipLandAbove = 1024,
		// Token: 0x04000094 RID: 148
		ClipLandKeep = 2048,
		// Token: 0x04000095 RID: 149
		ClipDecalMask = 12288,
		// Token: 0x04000096 RID: 150
		ClipDecalStep = 4096,
		// Token: 0x04000097 RID: 151
		ClipDecalNone = 0,
		// Token: 0x04000098 RID: 152
		ClipDecalNormal = 4096,
		// Token: 0x04000099 RID: 153
		ClipDecalVertical = 8192,
		// Token: 0x0400009A RID: 154
		ClipFogMask = 49152,
		// Token: 0x0400009B RID: 155
		ClipFogStep = 16384,
		// Token: 0x0400009C RID: 156
		ClipFogNormal = 0,
		// Token: 0x0400009D RID: 157
		ClipFogDisable = 16384,
		// Token: 0x0400009E RID: 158
		ClipFogSky = 32768,
		// Token: 0x0400009F RID: 159
		ClipLightMask = 983040,
		// Token: 0x040000A0 RID: 160
		ClipLightStep = 65536,
		// Token: 0x040000A1 RID: 161
		ClipLightNormal = 0,
		// Token: 0x040000A2 RID: 162
		ClipLightLine = 524288,
		// Token: 0x040000A3 RID: 163
		ClipUserMask = 267386880,
		// Token: 0x040000A4 RID: 164
		ClipUserStep = 1048576,
		// Token: 0x040000A5 RID: 165
		MaxUserValue = 255,
		// Token: 0x040000A6 RID: 166
		ClipHints = 268435200
	}
}
