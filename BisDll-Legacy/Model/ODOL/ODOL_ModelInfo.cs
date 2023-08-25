using System;
using BisDll.Common;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000020 RID: 32
	public class ODOL_ModelInfo
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005AE4 File Offset: 0x00003CE4
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005AEC File Offset: 0x00003CEC
		public int special { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005AF8 File Offset: 0x00003CF8
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00005B00 File Offset: 0x00003D00
		public float BoundingSphere { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005B0C File Offset: 0x00003D0C
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00005B14 File Offset: 0x00003D14
		public float GeometrySphere { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005B20 File Offset: 0x00003D20
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00005B28 File Offset: 0x00003D28
		public int remarks { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005B34 File Offset: 0x00003D34
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00005B3C File Offset: 0x00003D3C
		public int andHints { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005B48 File Offset: 0x00003D48
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00005B50 File Offset: 0x00003D50
		public int orHints { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005B5C File Offset: 0x00003D5C
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00005B64 File Offset: 0x00003D64
		public Vector3P AimingCenter { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005B70 File Offset: 0x00003D70
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00005B78 File Offset: 0x00003D78
		public PackedColor color { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005B84 File Offset: 0x00003D84
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00005B8C File Offset: 0x00003D8C
		public PackedColor colorType { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005B98 File Offset: 0x00003D98
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public float viewDensity { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005BAC File Offset: 0x00003DAC
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public Vector3P bboxMin { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005BC0 File Offset: 0x00003DC0
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public Vector3P bboxMax { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005BD4 File Offset: 0x00003DD4
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005BDC File Offset: 0x00003DDC
		public float propertyLodDensityCoef { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005BE8 File Offset: 0x00003DE8
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005BF0 File Offset: 0x00003DF0
		public float propertyDrawImportance { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005BFC File Offset: 0x00003DFC
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005C04 File Offset: 0x00003E04
		public Vector3P bboxMinVisual { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005C10 File Offset: 0x00003E10
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005C18 File Offset: 0x00003E18
		public Vector3P bboxMaxVisual { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005C24 File Offset: 0x00003E24
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005C2C File Offset: 0x00003E2C
		public Vector3P boundingCenter { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005C38 File Offset: 0x00003E38
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00005C40 File Offset: 0x00003E40
		public Vector3P geometryCenter { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005C4C File Offset: 0x00003E4C
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005C54 File Offset: 0x00003E54
		public Vector3P centerOfMass { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005C60 File Offset: 0x00003E60
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005C68 File Offset: 0x00003E68
		public Matrix3P invInertia { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005C74 File Offset: 0x00003E74
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005C7C File Offset: 0x00003E7C
		public bool autoCenter { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005C88 File Offset: 0x00003E88
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005C90 File Offset: 0x00003E90
		public bool lockAutoCenter { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005C9C File Offset: 0x00003E9C
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public bool canOcclude { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005CB0 File Offset: 0x00003EB0
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public bool canBeOccluded { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005CC4 File Offset: 0x00003EC4
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00005CCC File Offset: 0x00003ECC
		public bool AICovers { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005CD8 File Offset: 0x00003ED8
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public float htMin { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005CEC File Offset: 0x00003EEC
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public float htMax { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005D00 File Offset: 0x00003F00
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005D08 File Offset: 0x00003F08
		public float afMax { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00005D1C File Offset: 0x00003F1C
		public float mfMax { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005D28 File Offset: 0x00003F28
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005D30 File Offset: 0x00003F30
		public float mFact { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005D3C File Offset: 0x00003F3C
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005D44 File Offset: 0x00003F44
		public float tBody { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005D50 File Offset: 0x00003F50
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005D58 File Offset: 0x00003F58
		public bool forceNotAlphaModel { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005D64 File Offset: 0x00003F64
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00005D6C File Offset: 0x00003F6C
		public SBSource sbSource { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005D78 File Offset: 0x00003F78
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00005D80 File Offset: 0x00003F80
		public bool prefershadowvolume { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005D8C File Offset: 0x00003F8C
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00005D94 File Offset: 0x00003F94
		public float shadowOffset { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005DA0 File Offset: 0x00003FA0
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public bool animated { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005DB4 File Offset: 0x00003FB4
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005DBC File Offset: 0x00003FBC
		public Skeleton skeleton { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005DC8 File Offset: 0x00003FC8
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public MapType mapType { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005DDC File Offset: 0x00003FDC
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public float[] massArray { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005DF0 File Offset: 0x00003FF0
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public float mass { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005E04 File Offset: 0x00004004
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00005E0C File Offset: 0x0000400C
		public float invMass { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005E18 File Offset: 0x00004018
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00005E20 File Offset: 0x00004020
		public float armor { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005E2C File Offset: 0x0000402C
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00005E34 File Offset: 0x00004034
		public float invArmor { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005E40 File Offset: 0x00004040
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005E48 File Offset: 0x00004048
		public float propertyExplosionShielding { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005E54 File Offset: 0x00004054
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005E5C File Offset: 0x0000405C
		public byte memory { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005E68 File Offset: 0x00004068
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005E70 File Offset: 0x00004070
		public byte geometry { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005E7C File Offset: 0x0000407C
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005E84 File Offset: 0x00004084
		public byte geometrySimple { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005E90 File Offset: 0x00004090
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00005E98 File Offset: 0x00004098
		public byte geometryPhys { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005EA4 File Offset: 0x000040A4
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005EAC File Offset: 0x000040AC
		public byte geometryFire { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005EB8 File Offset: 0x000040B8
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005EC0 File Offset: 0x000040C0
		public byte geometryView { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005ECC File Offset: 0x000040CC
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00005ED4 File Offset: 0x000040D4
		public byte geometryViewPilot { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005EE0 File Offset: 0x000040E0
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00005EE8 File Offset: 0x000040E8
		public byte geometryViewGunner { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005EF4 File Offset: 0x000040F4
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00005EFC File Offset: 0x000040FC
		public byte geometryViewCargo { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005F08 File Offset: 0x00004108
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00005F10 File Offset: 0x00004110
		public byte landContact { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005F1C File Offset: 0x0000411C
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00005F24 File Offset: 0x00004124
		public byte roadway { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005F30 File Offset: 0x00004130
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00005F38 File Offset: 0x00004138
		public byte paths { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005F44 File Offset: 0x00004144
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00005F4C File Offset: 0x0000414C
		public byte hitpoints { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005F58 File Offset: 0x00004158
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00005F60 File Offset: 0x00004160
		public byte minShadow { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005F6C File Offset: 0x0000416C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005F74 File Offset: 0x00004174
		public bool canBlend { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005F80 File Offset: 0x00004180
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005F88 File Offset: 0x00004188
		public string propertyClass { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005F94 File Offset: 0x00004194
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005F9C File Offset: 0x0000419C
		public string propertyDamage { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005FA8 File Offset: 0x000041A8
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005FB0 File Offset: 0x000041B0
		public bool propertyFrequent { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005FBC File Offset: 0x000041BC
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00005FC4 File Offset: 0x000041C4
		public int[] preferredShadowVolumeLod { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005FD0 File Offset: 0x000041D0
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005FD8 File Offset: 0x000041D8
		public int[] preferredShadowBufferLod { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005FE4 File Offset: 0x000041E4
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00005FEC File Offset: 0x000041EC
		public int[] preferredShadowBufferLodVis { get; private set; }

		// Token: 0x06000140 RID: 320 RVA: 0x00005FF8 File Offset: 0x000041F8
		internal ODOL_ModelInfo(BinaryReaderEx input, int nLods)
		{
			this.Read(input, nLods);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006008 File Offset: 0x00004208
		public void Read(BinaryReaderEx input, int nLods)
		{
			int version = input.Version;
			this.special = input.ReadInt32();
			Logging_Functions.Echo(input, this.special, "special", true);
			this.BoundingSphere = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.BoundingSphere, "BoundingSphere");
			this.GeometrySphere = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.GeometrySphere, "GeometrySphere");
			this.remarks = input.ReadInt32();
			Logging_Functions.Echo(input, this.remarks, "remarks", true);
			this.andHints = input.ReadInt32();
			Logging_Functions.Echo(input, this.andHints, "andHints", true);
			this.orHints = input.ReadInt32();
			Logging_Functions.Echo(input, this.orHints, "orHints", true);
			this.AimingCenter = new Vector3P(input);
			Logging_Functions.Echo<Vector3P>(input, this.AimingCenter, "AimingCenter");
			this.color = new PackedColor(input.ReadUInt32());
			Logging_Functions.Echo<PackedColor>(input, this.color, "color");
			this.colorType = new PackedColor(input.ReadUInt32());
			Logging_Functions.Echo<PackedColor>(input, this.colorType, "colorType");
			this.viewDensity = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.viewDensity, "viewDensity");
			this.bboxMin = new Vector3P(input);
			Logging_Functions.Echo<Vector3P>(input, this.bboxMin, "bboxMin");
			this.bboxMax = new Vector3P(input);
			Logging_Functions.Echo<Vector3P>(input, this.bboxMax, "bboxMax");
			if (version >= 70)
			{
				this.propertyLodDensityCoef = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.propertyLodDensityCoef, "propertyLodDensityCoef");
			}
			if (version >= 71)
			{
				this.propertyDrawImportance = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.propertyDrawImportance, "propertyDrawImportance");
			}
			if (version >= 52)
			{
				this.bboxMinVisual = new Vector3P(input);
				Logging_Functions.Echo<Vector3P>(input, this.bboxMinVisual, "bboxMinVisual");
				this.bboxMaxVisual = new Vector3P(input);
				Logging_Functions.Echo<Vector3P>(input, this.bboxMaxVisual, "bboxMaxVisual");
			}
			this.boundingCenter = new Vector3P(input);
			Logging_Functions.Echo<Vector3P>(input, this.boundingCenter, "boundingCenter");
			this.geometryCenter = new Vector3P(input);
			Logging_Functions.Echo<Vector3P>(input, this.geometryCenter, "geometryCenter");
			this.centerOfMass = new Vector3P(input);
			Logging_Functions.Echo<Vector3P>(input, this.centerOfMass, "centerOfMass");
			this.invInertia = new Matrix3P(input);
			Logging_Functions.Echo<Matrix3P>(input, this.invInertia, "invInertia");
			this.autoCenter = input.ReadBoolean();
			Logging_Functions.Echo<bool>(input, this.autoCenter, "autoCenter");
			this.lockAutoCenter = input.ReadBoolean();
			Logging_Functions.Echo<bool>(input, this.lockAutoCenter, "lockAutoCenter");
			this.canOcclude = input.ReadBoolean();
			Logging_Functions.Echo<bool>(input, this.canOcclude, "canOcclude");
			this.canBeOccluded = input.ReadBoolean();
			Logging_Functions.Echo<bool>(input, this.canBeOccluded, "canBeOccluded");
			if (version >= 73)
			{
				this.AICovers = input.ReadBoolean();
				Logging_Functions.Echo<bool>(input, this.AICovers, "AICovers");
			}
			if (version >= 53)
			{
				input.ReadBytes(5);
			}
			if ((version >= 42 && version < 10000) || version >= 10042)
			{
				this.htMin = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.htMin, "htMin");
				this.htMax = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.htMax, "htMax");
				this.afMax = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.afMax, "afMax");
				this.mfMax = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.mfMax, "mfMax");
			}
			if ((version >= 43 && version < 10000) || version >= 10043)
			{
				this.mFact = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.mFact, "mFact");
				this.tBody = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.tBody, "tBody");
			}
			if (version >= 33)
			{
				this.forceNotAlphaModel = input.ReadBoolean();
				Logging_Functions.Echo<bool>(input, this.forceNotAlphaModel, "forceNotAlphaModel");
			}
			if (version >= 37)
			{
				int num = input.ReadInt32();
				Logging_Functions.Echo(input, num, "sbSource", true);
				this.sbSource = (SBSource)num;
				this.prefershadowvolume = input.ReadBoolean();
				Logging_Functions.Echo<bool>(input, this.prefershadowvolume, "prefershadowvolume");
			}
			if (version >= 48)
			{
				this.shadowOffset = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.shadowOffset, "shadowOffset");
			}
			this.animated = input.ReadBoolean();
			Logging_Functions.Echo<bool>(input, this.animated, "animated");
			this.skeleton = new Skeleton(input);
			Logging_Functions.Echo<Skeleton>(input, this.skeleton, "skeleton");
			this.mapType = (MapType)input.ReadByte();
			Logging_Functions.Echo<MapType>(input, this.mapType, "mapType");
			this.massArray = input.ReadCompressedFloatArray();
			Logging_Functions.Echo<float[]>(input, this.massArray, "massArray");
			this.mass = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.mass, "mass");
			this.invMass = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.invMass, "invMass");
			this.armor = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.armor, "armor");
			this.invArmor = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.invArmor, "invArmor");
			if (version >= 72)
			{
				this.propertyExplosionShielding = input.ReadSingle();
				Logging_Functions.Echo<float>(input, this.propertyExplosionShielding, "propertyExplosionShielding");
			}
			if (version > 53)
			{
				this.geometrySimple = input.ReadByte();
				Logging_Functions.Echo<byte>(input, this.geometrySimple, "geometrySimple");
			}
			if (version >= 54)
			{
				this.geometryPhys = input.ReadByte();
				Logging_Functions.Echo<byte>(input, this.geometryPhys, "geometryPhys");
			}
			this.memory = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.memory, "memory");
			this.geometry = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.geometry, "geometry");
			this.geometryFire = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.geometryFire, "geometryFire");
			this.geometryView = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.geometryView, "geometryView");
			this.geometryViewPilot = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.geometryViewPilot, "geometryViewPilot");
			this.geometryViewGunner = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.geometryViewGunner, "geometryViewGunner");
			input.ReadSByte();
			this.geometryViewCargo = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.geometryViewCargo, "geometryViewCargo");
			this.landContact = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.landContact, "landContact");
			this.roadway = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.roadway, "roadway");
			this.paths = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.paths, "paths");
			this.hitpoints = input.ReadByte();
			Logging_Functions.Echo<byte>(input, this.hitpoints, "hitpoints");
			this.minShadow = (byte)input.ReadUInt32();
			Logging_Functions.Echo<byte>(input, this.minShadow, "minShadow");
			if (version >= 38)
			{
				this.canBlend = input.ReadBoolean();
				Logging_Functions.Echo<bool>(input, this.canBlend, "canBlend");
			}
			this.propertyClass = input.ReadAsciiz();
			Logging_Functions.Echo<string>(input, this.propertyClass, "propertyClass");
			this.propertyDamage = input.ReadAsciiz();
			Logging_Functions.Echo<string>(input, this.propertyDamage, "propertyDamage");
			this.propertyFrequent = input.ReadBoolean();
			Logging_Functions.Echo<bool>(input, this.propertyFrequent, "propertyFrequent");
			if (version >= 31)
			{
				input.ReadUInt32();
				Logging_Functions.Echo("Skipping 4 bytes.");
			}
			if (version >= 57)
			{
				this.preferredShadowVolumeLod = new int[nLods];
				this.preferredShadowBufferLod = new int[nLods];
				this.preferredShadowBufferLodVis = new int[nLods];
				for (int i = 0; i < nLods; i++)
				{
					this.preferredShadowVolumeLod[i] = input.ReadInt32();
				}
				for (int j = 0; j < nLods; j++)
				{
					this.preferredShadowBufferLod[j] = input.ReadInt32();
				}
				for (int k = 0; k < nLods; k++)
				{
					this.preferredShadowBufferLodVis[k] = input.ReadInt32();
				}
			}
		}
	}
}
